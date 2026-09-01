using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ViewModel.Shared
{
    [DataContract]
    public class kyc_initiate_auth_resend_request
    {
        [DataMember]
        public string token_no { get; set; }
        [DataMember]
        public string otp { get; set; }
        [DataMember]
        public string account_code { get; set; }
        [DataMember]
        public string login_id { get; set; }
        [DataMember]
        public string stage { get; set; }
        [DataMember]
        public string sql_message { get; set; }
        [DataMember]
        public string sql_status { get; set; }
        [DataMember]
        public string request_stage { get; set; }
        

    }
}
