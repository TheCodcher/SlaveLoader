using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SlaveLoader2
{
    class UserINFOItem
    {
        [JsonConstructor]
        private UserINFOItem() { }
        public UserINFOItem(IPEndPoint IP, string Name = "")
        {
            _name = Name;
            _ip = IP.Address.ToString();
            _port = IP.Port;
        }
        [JsonProperty]
        string _name;
        [JsonIgnore]
        public string Name { get => _name; set => _name = value; }
        [JsonProperty]
        string _ip;
        [JsonProperty]
        int _port = 0;
        [JsonIgnore]
        public IPAddress IP { get => IPAddress.Parse(_ip); }
        [JsonIgnore]
        public int Port { get => _port; }
        [JsonIgnore]
        public IPEndPoint IPEnd { get => new IPEndPoint(IP, Port); }
        public override string ToString() => _name == "" ? _ip : _name;
    }

    static class NetWorkerExtentions
    {
        public static byte[] ToUTF8Byte(this NetWorker.NetWorkerRequest req) => new NetWorker.RequestData(req).ToUTF8Byte();

        public static byte[] ReadToEnd(this NetworkStream sourceStream, int cellSize = 256)
        {
            using (var memory = new MemoryStream()) 
            {
                using (var writer = new BinaryWriter(memory))
                {
                    do
                    {
                        var data = new byte[cellSize];
                        int bytes = sourceStream.Read(data, 0, data.Length);
                        writer.Write(data, 0, bytes);
                    }
                    while (sourceStream.DataAvailable);
                }
                return memory.ToArray();
            }
        }

        public static void WriteToEnd(this Stream outStream, Stream sourceStream, int cellSize = 256, Action<int> progressByteSum = null)
        {
            int progress = 0;
            while (sourceStream.Position != sourceStream.Length)
            {
                var buff = new byte[cellSize];
                int bytes = sourceStream.Read(buff, 0, buff.Length);
                progress += bytes;
                outStream.Write(buff, 0, bytes);
                progressByteSum?.Invoke(progress);
            }
        }

        public static void WriteToEnd(this Stream outStream, NetworkStream sourceStream, int CellSize = 256, Action<int> PrograssByteSum = null, Func<bool> ExpectationFlag = null)
        {
            int progress = 0;
            var Check = ExpectationFlag == null ? () => false : ExpectationFlag;
            do
            {
                var buff = new byte[CellSize];
                int bytes = sourceStream.Read(buff, 0, buff.Length);
                progress += bytes;
                outStream.Write(buff, 0, bytes);
                PrograssByteSum?.Invoke(progress);
            }
            while (sourceStream.DataAvailable || Check());
        }
    }
}
