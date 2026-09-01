using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Shared
{
    [DataContract]
    public class psp_dsp_get_kyc_update_history
    {
        [DataMember]
        public string main_client_id { get; set; }
        
        [DataMember]
        public string account_code { get; set; }
        [DataMember]
        public string new_email { get; set; }
        [DataMember]
        public string new_email_relation { get; set; }
        [DataMember]
        public string new_mobile { get; set; }
        [DataMember]
        public string new_mobile_relation { get; set; }
        [DataMember]
        public string new_income_range { get; set; }
        [DataMember]
        public string new_income_range_dsp { get; set; }
        [DataMember]
        public string new_networth { get; set; }
        [DataMember]
        public string new_networth_date { get; set; }
        [DataMember]
        public string request_date { get; set; }
        [DataMember]
        public string status { get; set; }
        [DataMember]
        public string remarks { get; set; }

    }
}

 									