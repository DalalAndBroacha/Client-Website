using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Reports
{
    public class psp_dsp_mapping_client_list
    {
        [DataMember]
        public string login_id { get; set; }
        [DataMember]
        public string rm_id { get; set; }
        [DataMember]
        public string account_code { get; set; }
        [DataMember]
        public string client_name { get; set; }
        [DataMember]
        public string family_name { get; set; }
        [DataMember]
        public string rm_name { get; set; }
        [DataMember]
        public string last_trade_date { get; set; }

    }
}


 		
