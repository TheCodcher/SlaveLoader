using System.Text;
using Newtonsoft.Json;

namespace SlaveLoader2
{
    partial class NetWorker
    {
        internal class RequestData
        {
            [JsonConstructor]
            protected RequestData() { }

            public RequestData(NetWorkerRequest Request)
            {
                this.Request = Request;
            }

            [JsonProperty]
            public readonly NetWorkerRequest Request;

            public string Serialize()
            {
                return JsonConvert.SerializeObject(this);
            }

            public byte[] ToUTF8Byte()
            {
                return Encoding.UTF8.GetBytes(Serialize());
            }
        }
         
    }
}
