using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Reports
{
    [DataContract]
    public class Brokerage_MIS //Used for both psp_dsp_exchangewise_brokerage & psp_dsp_branchwise_brokerage
    {
        [DataMember]
        public string as_on_date { get; set; }
        [DataMember]
        public string Branch_Name { get; set; }
        [DataMember]
        public string exchange_name { get; set; }

        [DataMember]
        public float ftd_volume { get; set; }
        [DataMember]
        public float ftd_brokerage { get; set; }
        [DataMember]
        public float mtd_volume { get; set; }
        [DataMember]
        public float mtd_brokerage { get; set; }
        [DataMember]
        public float ytd_volume { get; set; }
        [DataMember]
        public float ytd_brokerage { get; set; }

    }
}
 					