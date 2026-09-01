using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Shared
{
    public class psp_rpt_nri_client_taxwithheld_drill
    {
        [DataMember]
        public string main_client_id { get; set; }
        [DataMember]
        public string cal_year { get; set; }
        [DataMember]
        public string client_cat { get; set; }
        [DataMember]
        public string trans_date { get; set; }
        [DataMember]
        public string narration { get; set; }
        [DataMember]
        public string category_name { get; set; }
        [DataMember]
        public float LTCG_amount { get; set; }
        [DataMember]
        public float STCG_amount { get; set; }
        [DataMember]
        public float Interest { get; set; }
        [DataMember]
        public float Dividend { get; set; }
        [DataMember]
        public float display_order { get; set; }
    }
}


 					
