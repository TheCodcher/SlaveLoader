using System;
using System.Collections.Generic;
using System.Linq;
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
    class NetWorker : IDisposable
    {
        /// <summary>
        /// Вызывается при уничтожении экземпляра NetWorker
        /// </summary>
        public event Action DisposePoint;
        public bool isDispose { get; private set; } = false;
        public Func<SaveInformation> GetDate;
        public delegate FileInfo DowloadReuestHandler(IPAddress RemoteIP, DownloadRequestDate Date, out Func<bool> GetEnd, out Action<string> StateEvent);
        DowloadReuestHandler Reqest;
        //Func<IProgressBar> GetProgressBar;
        //IProgressBar _progressBar;
        //IProgressBar ProgressBar
        //{
        //    get
        //    {
        //        if (_progressBar == null)
        //        {
        //            _progressBar = GetProgressBar?.Invoke();
        //        }
        //        return _progressBar;
        //    }
        //    set => _progressBar = value;
        //}
        public NetWorker(IPEndPoint Host, Func<SaveInformation> GetDate, DowloadReuestHandler DowloadReqest)
        {
            this.GetDate = GetDate;
            Task.Run(() => Run(Host));
            Reqest = DowloadReqest;
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
            var Listener = new TcpListener(Host);
            Listener.Start();
            while (!isDispose)
            {
                TcpClient accept = Listener.AcceptTcpClient();
                Task.Run(() => ClientСommunication(accept));
            }
            Listener.Stop();
        }
        private void ClientСommunication(TcpClient Client)
        {
            NetworkStream ClientStream = Client.GetStream();
            try
            {
                while (!isDispose)
                {
                    var endS = Encoding.UTF8.GetString(ClientStream.ReadToEnd());
                    dynamic pingAnsv = JsonConvert.DeserializeObject(endS);
                    switch ((NetWorkerReqest)pingAnsv.Request)
                    {
                        case NetWorkerReqest.Ping:
                            var buff = new PingDate(GetDate().MyInfo.Name).ToUTF8Byte();
                            ClientStream.Write(buff, 0, buff.Length);
                            break;
                        case NetWorkerReqest.DownloadReqest:
                            var buf = UploadReqest(Client, JsonConvert.DeserializeObject<DownloadRequestDate>(endS)).ToUTF8Byte();
                            ClientStream.Write(buf, 0, buf.Length);
                            break;
                    }
                }
            }
            catch
            {

            }
            finally
            {
                ClientStream?.Close();
                ClientStream?.Dispose();
                Client?.Close();
                Client?.Dispose();
            }
        }
        public void Upload(UserINFOItem EndAdress, Func<bool> GetEndFlag, Func<FileInfo> GetFile, Action<string> NowStateEvent, int Deley = 1000)
        {
            NowStateEvent($"Попытка подлючения к {EndAdress.ToString()}");
            var task = ConnectAsync(EndAdress.IPEnd, GetEndFlag);
            while (!(task.IsCompleted || GetEndFlag()))
                Thread.Sleep(Deley);
            if (GetEndFlag())
            {
                if (task.IsCompleted) task.Result.Close();
                return;
            }
            var Client = task.Result;
            try
            {
                NowStateEvent($"Подключение к {EndAdress.ToString()} успешно");
                Task.Run(() =>
                {
                    while (!GetEndFlag())
                        Thread.Sleep(Deley);
                    Client?.Close();
                });
                var file = GetFile();
                if (file == null)
                {
                    Client?.Close();
                    return;
                }
                NowStateEvent("Отправка запроса");
                var DownloadReqest = new DownloadRequestDate(file.Name, file.Length, GetDate().MyInfo.Port);
                using (var stream = Client.GetStream())
                {
                    var buffer = DownloadReqest.ToUTF8Byte();
                    stream.Write(buffer, 0, buffer.Length);
                    NowStateEvent($"Ожидание ответа от {EndAdress.ToString()}");
                    var ansver = JsonConvert.DeserializeObject<DateRequest>(Encoding.UTF8.GetString(stream.ReadToEnd()));
                    if (ansver == null || ansver?.Request == NetWorkerReqest.Cancel)
                    {
                        if (ansver == null) NowStateEvent("Некорректный ответ");
                        else NowStateEvent("Пользователь отменил загрузку файла");
                        throw new Exception();
                    }
                    NowStateEvent("Файл выгружается");
                    using (var filestream = file.OpenRead())
                    {
                        stream.WriteToEnd(filestream, 8192, sum => NowStateEvent($"Выполнено {Math.Floor((double)sum * 100 / filestream.Length).ToString()}% {sum}/{filestream.Length}"));
                    }
                    NowStateEvent("Выгрузка завершена, ожидание подтверждения");
                    ansver = JsonConvert.DeserializeObject<DateRequest>(Encoding.UTF8.GetString(stream.ReadToEnd()));
                    if (ansver == null || ansver?.Request == NetWorkerReqest.Cancel)
                    {
                        if (ansver == null) NowStateEvent("Некорректный ответ");
                        else NowStateEvent($"У {EndAdress.ToString()} что-то пошло не так");
                        throw new Exception();
                    }
                    else
                    {
                        NowStateEvent("Передача файла выполнена успешно");
                    }
                }
            }
            catch
            {
                NowStateEvent("Соединение разорвано");
            }
            finally
            {
                Client?.Close();
            }
        }
        private NetWorkerReqest UploadReqest(TcpClient Client, DownloadRequestDate Date)
        {
            if (Client == null || Date == null) throw new Exception();
            var userEndPoint = new IPEndPoint(((IPEndPoint)Client.Client.RemoteEndPoint).Address, Date.ListenerPort).ToString();
            Func<bool> GetEndFlag = null;
            Action<string> NowStateEvent = null;
            FileInfo file = Reqest(((IPEndPoint)Client.Client.RemoteEndPoint).Address, Date, out GetEndFlag, out NowStateEvent);
            if (file == null) return NetWorkerReqest.Cancel;
            Task.Run(() =>
            {
                while (!GetEndFlag())
                    Thread.Sleep(1000);
                Client?.Close();
            });
            var user = GetDate().UserList.Find(u => u.IPEnd.ToString() == userEndPoint);
            if (user == null)
                NowStateEvent($"Загрузка файла от {userEndPoint}");
            else
                NowStateEvent($"Загрузка файла от {user.ToString()}");
            using (var filestream = new FileStream(file.FullName, FileMode.Create))
            {
                var clientstream = Client.GetStream();
                var ansvBuff = NetWorkerReqest.Сonfirmed.ToUTF8Byte();
                clientstream.Write(ansvBuff, 0, ansvBuff.Length);
                filestream.WriteToEnd(clientstream, 8192, sum => NowStateEvent($"Выполнено {Math.Floor((double)sum * 100 / Date.ByteLength).ToString()}% {sum}/{Date.ByteLength}"), () => filestream.Length != Date.ByteLength);
                if (filestream.Length == Date.ByteLength)
                {
                    NowStateEvent("Файл записан успешно");
                    return NetWorkerReqest.Complite;
                }
                else
                {
                    NowStateEvent($"Что-то пошло не так, записано {filestream.Length} из {Date.ByteLength} байт");
                    return NetWorkerReqest.Cancel;
                }
            }
        }
        public void Ping(UserINFOItem EndAdress, Func<bool> GetEndFlag, Action<string> NowStateEvent, int Deley = 1000)
        {
            int trycount = 0;
            bool GetEnd() => trycount == int.MaxValue ? true : GetEndFlag();
            var task = ConnectAsync(EndAdress.IPEnd, GetEnd);
            while (!(GetEnd() || isDispose || task.IsCompleted))
            {
                NowStateEvent($"Попытка подключения к {EndAdress.ToString()}: {(++trycount).ToString()}");
                Thread.Sleep(Deley);
            }
            if (!task.IsCompleted || GetEnd()) return;
            TcpClient myClientSocket = task.Result;
            try
            {  
                using (var stream = myClientSocket.GetStream())
                {
                    while (!(GetEndFlag() || isDispose))
                    {
                        var buff = NetWorkerReqest.Ping.ToUTF8Byte();
                        int Time = DateTime.Now.Millisecond;
                        stream.Write(buff, 0, buff.Length);
                        var endS = Encoding.UTF8.GetString(stream.ReadToEnd());
                        Time = DateTime.Now.Millisecond - Time;
                        var pingAnsv = JsonConvert.DeserializeObject<PingDate>(endS);
                        if (pingAnsv == null) NowStateEvent("Некорректный ответ");
                        else
                        {
                            NowStateEvent($"Время отклика: {Time.ToString()} ms");
                            if (EndAdress.Name != pingAnsv.Name) EndAdress.Name = pingAnsv.Name;
                        }
                        Thread.Sleep(Deley);
                    }
                }
            }
            catch
            {
                NowStateEvent("Соединение разорвано");
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
        internal enum NetWorkerReqest : byte
        {
            Ping,
            PingAnsver,
            DownloadReqest,
            Сonfirmed,
            Cancel,
            Complite
        }
        internal class DateRequest
        {
            [JsonConstructor]
            protected DateRequest() { }
            public DateRequest(NetWorkerReqest Request)
            {
                this.Request = Request;
            }
            [JsonProperty]
            public readonly NetWorkerReqest Request;
            public string Serialize()
            {
                return JsonConvert.SerializeObject(this);
            }
            public byte[] ToUTF8Byte()
            {
                return Encoding.UTF8.GetBytes(Serialize());
            }
        }
        internal class PingDate : DateRequest
        {
            public PingDate(string myName) : base(NetWorkerReqest.PingAnsver)
            {
                Name = myName;
            }
            [JsonProperty]
            public readonly string Name;
        }
        internal class DownloadRequestDate : DateRequest
        {
            public DownloadRequestDate(string fileName, long Length, int listenerPort) : base(NetWorkerReqest.DownloadReqest)
            {
                FileName = fileName;
                ByteLength = Length;
                ListenerPort = listenerPort;
            }
            [JsonProperty]
            public readonly string FileName;
            [JsonProperty]
            public readonly long ByteLength;
            [JsonProperty]
            public readonly int ListenerPort;
            public static (double Count, FileSize Size) GetFileSize(long Count)
            {
                var SizeArr = Enum.GetValues(typeof(FileSize)) as int[];
                if (SizeArr == null) throw new Exception("FileSize not correct");
                SizeArr = SizeArr.OrderBy(i => i).ToArray();
                for (int i = 0; i < SizeArr.Length; i++)
                {
                    double temp = Count / (double)SizeArr[i];
                    if (i + 1 != SizeArr.Length)
                    {
                        if (temp < SizeArr[i + 1] / SizeArr[i]) return (temp, (FileSize)SizeArr[i]);
                    }
                    else return (temp, (FileSize)SizeArr[i]);
                }
                throw new Exception("wtf is going on with FileSize enum? Wtf is going on with .NET, Bill Gades?");
            }
            public (double Count, FileSize Size) GetFileSize() => GetFileSize(ByteLength);
            public enum FileSize : int
            {
                b = 1,
                Kb = 1024,
                Mb = 1048576,
                Gb = 1073741824
            }
        }
         
    }
}
