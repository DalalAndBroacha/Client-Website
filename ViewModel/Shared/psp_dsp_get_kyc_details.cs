using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ViewModel.Shared
{
    [DataContract]
    public class psp_dsp_get_kyc_details
    {
        [DataMember]
        public string main_client_id { get; set; }
        
        [DataMember]
        public string account_code { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public string email_relation { get; set; }
        [DataMember]
        public string mobile { get; set; }
        [DataMember]
        public string mobile_relation { get; set; }
        [DataMember]
        public string income_range { get; set; }
        [DataMember]
        public string income_range_date { get; set; }
        
        [DataMember]
        public string income_range_flag { get; set; }

        [DataMember]
        public string networth { get; set; }

        [DataMember]
        public string networth_date { get; set; }
        [DataMember]
        public string networth_flag { get; set; }
        [DataMember]
        public string remarks { get; set; }
        [DataMember]
        public string enable_flag { get; set; }
        [DataMember]
        public string account_flag { get; set; }
        [DataMember]
        public string account_remarks { get; set; }
    }
}
