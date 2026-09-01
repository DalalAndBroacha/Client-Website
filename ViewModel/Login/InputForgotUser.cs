using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
namespace ViewModel.Login
{
    [DataContract]
        public class InputForgotUser:dtoBase
    {
        [DataMember]
        public string account_code { get; set; }
        [DataMember]

        public string pan_number { get; set; }
        [DataMember]

        public string mobile_number { get; set; }
        [DataMember]

        public string email_id { get; set; }
        [DataMember]
        public string sql_message { get; set; }
        [DataMember]
        public string sql_status { get; set; }
        [DataMember]
        public string token_no { get; set; }
        [DataMember]
        public int expires_in { get; set; }

        [DataMember]
        public otp otps { get; set; }

        [DataMember]
        public string LoginId { get; set; }

        [DataMember]
        public string response { get; set; }


        [DataMember]
        public ChangeUsername changeUsername { get; set; }

    }
}
