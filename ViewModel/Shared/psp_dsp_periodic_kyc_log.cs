using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Shared
{
    [DataContract]
    public class psp_dsp_periodic_kyc_log
    {
        [DataMember]
        public string start_date { get; set; }
        [DataMember]
        public string end_date { get; set; }
        [DataMember]
        public string request_id { get; set; }
        [DataMember]
        public string client_name { get; set; }
        [DataMember]
        public string account_code { get; set; }
        [DataMember]
        public string income_update { get; set; }
        [DataMember]
        public string new_income_value { get; set; }
        [DataMember]
        public string email_update { get; set; }
        [DataMember]
        public string new_email_value { get; set; }
        [DataMember]
        public string mobile_update { get; set; }
        [DataMember]
        public string new_mobile_value { get; set; }
        [DataMember]
        public string request_date { get; set; }
        [DataMember]
        public string request_completion_date { get; set; }
        [DataMember]
        public string remarks { get; set; }
    }
}



 										