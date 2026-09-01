using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ViewModel.Reports
{
    [DataContract]
   public class EquityClientFyFactor:dtoBase
    {
       
        [DataMember]
        public string account_code { get; set; }
        [DataMember]
        public string sort_order { get; set; }
        [DataMember]
        public string FY { get; set; }
        [DataMember]
        public string fin_year { get; set; }
        [DataMember]
        public float long_real_profit { get; set; }
        [DataMember]
        public float srt_real_profit { get; set; }
        [DataMember]
        public float int_real_profit { get; set; }
        [DataMember]
        public float dividend { get; set; }
        [DataMember]
        public float XIRR { get; set; }
        [DataMember]
        public float index_xirr { get; set; }
        [DataMember]
        public float abs_ret { get; set; }
        [DataMember]
        public float midcap_xirr { get; set; }

    }
}
