using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ViewModel.Shared
{
    [DataContract]
    public class psp_rpt_scripwiseholding_scrip
    {
        [DataMember]
        public string Asset { get; set; }
        [DataMember]
        public string rm { get; set; }
        [DataMember]
        public string SCRIP_NAME { get; set; }
        [DataMember]
        public string scrip_code { get; set; }

    }
}
