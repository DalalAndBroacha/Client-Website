using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Reports
{
    [DataContract]
    public class psp_dsp_mf_sip_dashboard:dtoBase
    {
        [DataMember]
        public string loginid { get; set; }
        [DataMember]
        public string login_source { get; set; }
        [DataMember]
        public string sip_total { get; set; }
        [DataMember]
        public string sip_cnt { get; set; }
        [DataMember]
        public string recent_sip_cnt { get; set; }
        [DataMember]
        public string recent_sip_total { get; set; }
        [DataMember]
        public string terminating_sip_cnt { get; set; }
        [DataMember]
        public string terminating_sip_total { get; set; }
    }
}
