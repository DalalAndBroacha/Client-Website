using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
namespace ViewModel.Reports
{
    [DataContract]
   public class EquityClientSummary:dtoBase
    {
       
        [DataMember]
        public string account_code { get; set; }
        [DataMember]
        public string sort_order { get; set; }
        [DataMember]
        public string scrip_industry { get; set; }
        [DataMember]
        public float amount { get; set; }
        [DataMember]
        public float net_amount { get; set; }
        [DataMember]
        public float sector_weightage { get; set; }
    }
}
