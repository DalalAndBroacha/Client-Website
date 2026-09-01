using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Reports
{
    public class psp_dsp_Nri_bank_interest_details_drill
    {
        [DataMember]
        public string main_client_id { get; set; }
        [DataMember]
        public string cal_year { get; set; }
        [DataMember]
        public string client_cat { get; set; }
        [DataMember]
        public string flag { get; set; }
        [DataMember]
        public string bank_account_no { get; set; }
        [DataMember]
        public string bank_account_type { get; set; }
        [DataMember]
        public string display_order { get; set; }
        [DataMember]
        public string trans_date { get; set; }
        [DataMember]
        public string narration { get; set; }
        [DataMember]
        public float amount { get; set; }
    }
}


 					
