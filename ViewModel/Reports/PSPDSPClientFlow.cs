using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
namespace ViewModel.Reports
{
    [DataContract]
    public class PSPDSPClientFlow:dtoBase
    {
        [DataMember]
        public int LoginId { get; set; }
        [DataMember]
        public string account_code { get; set; }
        [DataMember]
        public int flow_type { get; set; }
        [DataMember]
        public int summary { get; set; }
        [DataMember]
        public string trans_date { get; set; }
        [DataMember]
        public string remarks { get; set; }
        [DataMember]
        public string amount { get; set; }


    }
}
