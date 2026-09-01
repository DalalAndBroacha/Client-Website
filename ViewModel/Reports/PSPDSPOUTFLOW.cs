using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ViewModel.Reports
{
    [DataContract]
    public class PSPDSPOUTFLOW:dtoBase
    {
       
        [DataMember]
        public string account_code { get; set; }
        [DataMember]
        public string trans_date { get; set; }
        [DataMember]
        public string summary { get; set; }
        [DataMember]
        public string flow_type { get; set; }
        [DataMember]
        public string scrip_Code { get; set; }
        [DataMember]
        public string script_name { get; set; }
        [DataMember]
        public string remarks { get; set; }
        [DataMember]
        public float trn_rate { get; set; }
        [DataMember]
        public float amount { get; set; }
    }
}
