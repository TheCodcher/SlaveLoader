using Newtonsoft.Json;

namespace SlaveLoader2
{
    partial class NetWorker
    {
        internal class PingData : RequestData
        {
            public PingData(string myName) : base(NetWorkerRequest.PingAnswer)
            {
                Name = myName;
            }
            [JsonProperty]
            public readonly string Name;
        }
         
    }
}
