using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace RBEPortalServer {
    [DataContract]
    public class Response {
        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public string StatusInfo { get; set; }
    }

    [DataContract]
    public class Response<T> {
        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public string StatusInfo { get; set; }

        [DataMember]
        public T Data { get; set; }
    }
}
