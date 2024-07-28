using System;
using System.Linq;
using Newtonsoft.Json;

namespace SlaveLoader2
{
    partial class NetWorker
    {
        internal class DownloadRequestData : RequestData
        {
            public DownloadRequestData(string fileName, long Length, int listenerPort) : base(NetWorkerRequest.DownloadRequest)
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
