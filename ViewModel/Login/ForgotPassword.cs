using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Login
{
    [DataContract]
   public class ForgotPassword : dtoBase
    {
        [DataMember]
        public string Username { get; set; }

        [DataMember]
        public string EmailId { get; set; }

        [DataMember]
        public string sql_message { get; set; }
        [DataMember]
        public string sql_status { get; set; }
        [DataMember]
        public string token_no { get; set; }
        [DataMember]
        public otp otps { get; set; }
    }
}
