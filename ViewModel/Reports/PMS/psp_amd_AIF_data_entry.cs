using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Reports.PMS
{
    [DataContract]
    public class psp_amd_AIF_data_entry
    {
        [DataMember]
        public string rec_id { get; set; }
        [DataMember]
        public string scheme_id { get; set; }
        [DataMember]
        public string nav_date { get; set; }
        [DataMember]
        public float nav { get; set; }
        [DataMember]
        public string para_nav { get; set; }
        [DataMember]
        public string user { get; set; }

        [DataMember]
        public string action { get; set; }


    }
}
