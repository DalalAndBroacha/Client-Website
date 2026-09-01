using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ViewModel.Shared
{
    public class psp_dsp_kyc_backoffice_requests
    {
        [DataMember]
        public string login_id { get; set; }
        [DataMember]
        public string request_id { get; set; }
        [DataMember]
        public string request_date { get; set; }
        [DataMember]
        public string account_code { get; set; }
        
        [DataMember]
        public string old_email_address { get; set; }
        [DataMember]
        public string email_address { get; set; }
        [DataMember]
        public string old_email_relation { get; set; }
        [DataMember]
        public string email_relation { get; set; }
        [DataMember]
        public string old_mobile_number { get; set; }
        [DataMember]
        public string client_name { get; set; }
        
        [DataMember]
        public string mobile_number { get; set; }
        [DataMember]
        public string old_mobile_relation { get; set; }
        [DataMember]
        public string mobile_relation { get; set; }
        [DataMember]
        public string old_income { get; set; }
        [DataMember]
        public string networth { get; set; }
        [DataMember]
        public string networth_date { get; set; }
        [DataMember]
        public string income_range { get; set; }
        [DataMember]
        public string str_old_values { get; set; }
        [DataMember]
        public string str_new_values { get; set; }

    }
}
