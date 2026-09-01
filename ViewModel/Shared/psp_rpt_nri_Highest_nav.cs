using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Shared
{
    [DataContract]
    public class psp_rpt_nri_Highest_nav
    {
        [DataMember]
        public string main_client_id { get; set; }
        [DataMember]
        public string client_category { get; set; }
        [DataMember]
        public string cal_year { get; set; }
        [DataMember]
        public string Particulars { get; set; }
        [DataMember]
        public string boid { get; set; }
        [DataMember]
        public float amount { get; set; }
        [DataMember]
        public float USD_amount { get; set; }
        [DataMember]
        public DateTime trans_date { get; set; }
    }
}
