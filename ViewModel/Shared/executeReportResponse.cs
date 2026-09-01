using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Shared
{
    [DataContract]
    public  class executeReportResponse
    {
        [DataMember]
        public string status { get; set; }
        [DataMember]
        public string msg { get; set; }
        [DataMember]
        public string data { get; set; }
    }
}
