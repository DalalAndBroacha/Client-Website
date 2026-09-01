using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Shared
{
    public class psp_rpt_nri_client_dividend
    {
        [DataMember]
        public string main_client_id { get; set; }
        [DataMember]
        public string cal_year { get; set; }
        [DataMember]
        public string client_category { get; set; }
        [DataMember]
        public string sub_category { get; set; }
        [DataMember]
        public string scrip_name { get; set; }
        [DataMember]
        public string dividend_date { get; set; }
        [DataMember]
        public float? value { get; set; }
    }
}
 	
