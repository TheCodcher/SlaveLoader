using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json;
using System.IO;
using System.Windows.Forms;

namespace SlaveLoader2
{
    partial class NetWorker : IDisposable
    {
        /// <summary>
        /// Вызывается при уничтожении экземпляра NetWorker
        /// </summary>
        public event Action DisposePoint;
        public bool isDispose { get; private set; } = false;

        public delegate FileInfo DownloadRequestHandler(IPAddress remoteIP, DownloadRequestData Date, out Func<bool> GetEnd, out Action<string> StateEvent);

        private readonly DownloadRequestHandler _requestHandler;
        private readonly Func<SaveInformation> _getContactsData;

        public NetWorker(IPEndPoint host, Func<SaveInformation> getDate, DownloadRequestHandler dowloadReqest)
        {
            this._getContactsData = getDate;
            Task.Run(() => Run(host));
            _requestHandler = dowloadReqest;
        }

        public static int CreatePort(int min = 0, int max = ushort.MaxValue)
        {
            int tempPort = new Random((int)DateTime.Now.Ticks).Next(min, max);
            bool error = true;
            while (error)
            {
                try
                {
                    TcpListener tempClient = new TcpListener(IPAddress.Any, tempPort);
                    tempClient.Server.Close();
                    error = false;
                }
                catch
                {
                    tempPort = tempPort == max ? min : ++tempPort;
                }
            }
            return tempPort;
        }

        public void Dispose()
        {
            isDispose = true;
            DisposePoint?.Invoke();
        }

        private void Run(IPEndPoint Host)
        {
            var listener = new TcpListener(Host);
            listener.Start();

            while (!isDispose)
            {
                TcpClient accept = listener.AcceptTcpClient();
                Task.Run(() => ClientСommunication(accept));
            }

            listener.Stop();
        }

        private void ClientСommunication(TcpClient client)
        {
            NetworkStream clientStream = client.GetStream();

            try
            {
                while (!isDispose)
                {
                    var endS = Encoding.UTF8.GetString(clientStream.ReadToEnd());
                    dynamic pingAnsv = JsonConvert.DeserializeObject(endS);
                    switch ((NetWorkerRequest)pingAnsv.Request)
                    {
                        case NetWorkerRequest.Ping:
                            var buff = new PingData(_getContactsData().MyInfo.Name).ToUTF8Byte();
                            clientStream.Write(buff, 0, buff.Length);
                            break;
                        case NetWorkerRequest.DownloadRequest:
                            var buf = UploadRequest(client, JsonConvert.DeserializeObject<DownloadRequestData>(endS)).ToUTF8Byte();
                            clientStream.Write(buf, 0, buf.Length);
                            break;
                    }
                }
            }
            catch
            {

            }
            finally
            {
                clientStream?.Close();
                clientStream?.Dispose();
                client?.Close();
                client?.Dispose();
            }
        }

        public void Upload(UserINFOItem endAdress, Func<bool> getEndFlag, Func<FileInfo> getFile, Action<string> nowStateEvent)
        {
            nowStateEvent($"Попытка подлючения к {endAdress.ToString()}");

            var task = ConnectAsync(endAdress.IPEnd, getEndFlag);
            while (!(task.IsCompleted || getEndFlag()))
                Thread.Sleep(Settings.MySettings.RequestDelay);

            if (getEndFlag())
            {
                if (task.IsCompleted) task.Result.Close();
                return;
            }

            var client = task.Result;

            try
            {
                nowStateEvent($"Подключение к {endAdress.ToString()} успешно");

                Task.Run(() =>
                {
                    while (!getEndFlag())
                        Thread.Sleep(Settings.MySettings.RequestDelay);
                    client?.Close();
                });

                var file = getFile();
                if (file == null)
                {
                    client?.Close();
                    return;
                }

                nowStateEvent("Отправка запроса");

                var downloadReqest = new DownloadRequestData(file.Name, file.Length, _getContactsData().MyInfo.Port);
                using (var stream = client.GetStream())
                {
                    var buffer = downloadReqest.ToUTF8Byte();
                    stream.Write(buffer, 0, buffer.Length);

                    nowStateEvent($"Ожидание ответа от {endAdress.ToString()}");

                    var response = JsonConvert.DeserializeObject<RequestData>(Encoding.UTF8.GetString(stream.ReadToEnd()));
                    if (response == null || response?.Request == NetWorkerRequest.Cancel)
                    {
                        if (response == null)
                            nowStateEvent("Некорректный ответ");
                        else
                            nowStateEvent("Пользователь отменил загрузку файла");

                        throw new Exception();
                    }

                    nowStateEvent("Файл выгружается");

                    using (var filestream = file.OpenRead())
                    {
                        double progress = 0;
                        void PublishSum(int sum)
                        {
                            var temp = Math.Round((double)sum * 100 / filestream.Length, Settings.MySettings.SignCount);
                            if (temp != progress)
                            {
                                nowStateEvent($"Выполнено {temp.ToString()}% {sum}/{filestream.Length}");
                                progress = temp;
                            }
                        }

                        stream.WriteToEnd(filestream, Settings.MySettings.UploadBuffer, PublishSum);
                    }

                    nowStateEvent("Выгрузка завершена, ожидание подтверждения");

                    response = JsonConvert.DeserializeObject<RequestData>(Encoding.UTF8.GetString(stream.ReadToEnd()));
                    if (response == null || response?.Request == NetWorkerRequest.Cancel)
                    {
                        if (response == null)
                            nowStateEvent("Некорректный ответ");
                        else
                            nowStateEvent($"У {endAdress.ToString()} что-то пошло не так");

                        throw new Exception();
                    }
                    else
                    {
                        nowStateEvent("Передача файла выполнена успешно");
                    }
                }
            }
            catch
            {
                nowStateEvent("Соединение разорвано");
            }
            finally
            {
                client?.Close();
            }
        }

        private NetWorkerRequest UploadRequest(TcpClient client, DownloadRequestData data)
        {
            if (client == null || data == null)
                throw new ArgumentNullException();

            var userEndPoint = new IPEndPoint(((IPEndPoint)client.Client.RemoteEndPoint).Address, data.ListenerPort).ToString();
            Func<bool> getEndFlag = null;
            Action<string> nowStateEvent = null;

            FileInfo file = _requestHandler(((IPEndPoint)client.Client.RemoteEndPoint).Address, data, out getEndFlag, out nowStateEvent);
            if (file == null) return NetWorkerRequest.Cancel;

            Task.Run(() =>
            {
                while (!getEndFlag())
                    Thread.Sleep(Settings.MySettings.RequestDelay);
                client?.Close();
            });

            var user = _getContactsData().UserList.Find(u => u.IPEnd.ToString() == userEndPoint);
            if (user == null)
                nowStateEvent($"Загрузка файла от {userEndPoint}");
            else
                nowStateEvent($"Загрузка файла от {user.ToString()}");

            using (var filestream = new FileStream(file.FullName, FileMode.Create))
            {
                var clientstream = client.GetStream();
                var ansvBuff = NetWorkerRequest.Сonfirmed.ToUTF8Byte();
                clientstream.Write(ansvBuff, 0, ansvBuff.Length);

                double progress = 0;
                void PublishSum(int sum)
                {
                    var temp = Math.Round((double)sum * 100 / data.ByteLength, Settings.MySettings.SignCount);
                    if (temp != progress)
                    {
                        nowStateEvent($"Выполнено {temp.ToString()}% {sum}/{data.ByteLength}");
                        progress = temp;
                    }
                }

                filestream.WriteToEnd(clientstream, Settings.MySettings.DownloadBuffer, PublishSum, () => filestream.Length != data.ByteLength);

                if (filestream.Length == data.ByteLength)
                {
                    nowStateEvent("Файл записан успешно");
                    return NetWorkerRequest.Complite;
                }
                else
                {
                    nowStateEvent($"Что-то пошло не так, записано {filestream.Length} из {data.ByteLength} байт");
                    return NetWorkerRequest.Cancel;
                }
            }
        }

        public void Ping(UserINFOItem endAdress, Func<bool> getEndFlag, Action<string> nowStateEvent)
        {
            int trycount = 0;
            bool GetEnd() => trycount == int.MaxValue ? true : getEndFlag();
            var task = ConnectAsync(endAdress.IPEnd, GetEnd);
            while (!(GetEnd() || isDispose || task.IsCompleted))
            {
                nowStateEvent($"Попытка подключения к {endAdress.ToString()}: {(++trycount).ToString()}");
                Thread.Sleep(Settings.MySettings.RequestDelay);
            }
            if (!task.IsCompleted || GetEnd()) return;
            TcpClient myClientSocket = task.Result;
            try
            {  
                using (var stream = myClientSocket.GetStream())
                {
                    while (!(getEndFlag() || isDispose))
                    {
                        var buff = NetWorkerRequest.Ping.ToUTF8Byte();
                        int Time = DateTime.Now.Millisecond;
                        stream.Write(buff, 0, buff.Length);
                        var endS = Encoding.UTF8.GetString(stream.ReadToEnd());
                        Time = DateTime.Now.Millisecond - Time;
                        var pingAnsv = JsonConvert.DeserializeObject<PingData>(endS);
                        if (pingAnsv == null) nowStateEvent("Некорректный ответ");
                        else
                        {
                            nowStateEvent($"Время отклика: {Time.ToString()} ms");
                            if (endAdress.Name != pingAnsv.Name) endAdress.Name = pingAnsv.Name;
                        }
                        Thread.Sleep(Settings.MySettings.RequestDelay);
                    }
                }
            }
            catch
            {
                nowStateEvent("Соединение разорвано");
            }
            finally
            {
                myClientSocket.Close();
            }
        }
        /// <summary>
        /// Возвращает null, если опирация подключения прервалась до того, как была завершена успешно
        /// </summary>
        /// <param name="RemoteHost"></param>
        /// <param name="GetEndFlag"></param>
        /// <returns></returns>
        private Task<TcpClient> ConnectAsync(IPEndPoint RemoteHost, Func<bool> GetEndFlag)
        {
            TcpClient Connection()
            {
                var Client = new TcpClient();
                while (!(isDispose || GetEndFlag() || Client.Connected))
                {
                    try
                    {
                        Client.Connect(RemoteHost);
                    }
                    catch { }
                }
                if (isDispose || GetEndFlag())
                {
                    Client.Close();
                    return null;
                }
                DisposePoint += () => { Client.Close(); DisposePoint -= Client.Close; };
                return Client;
            }
            return Task.Run(Connection);
        }

        internal enum NetWorkerRequest : byte
        {
            Ping,
            PingAnswer,
            DownloadRequest,
            Сonfirmed,
            Cancel,
            Complite
        }
         
    }
}
