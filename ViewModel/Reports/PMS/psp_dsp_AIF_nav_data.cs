using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Reports.PMS
{
    [DataContract]
    public class psp_dsp_AIF_nav_data
    {
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public string nav_date { get; set; }
        [DataMember]
        public string scheme_code { get; set; }
        [DataMember]
        public string scheme_name { get; set; }
        [DataMember]
        public float nav { get; set; }
    }
}
