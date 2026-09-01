using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ViewModel.Login
{
    [DataContract]
   public class OutputForgotUser:dtoBase
    {
       
        [DataMember]
        public string sql_message { get; set; }
        [DataMember]
        public string sql_status { get; set; }
        [DataMember]
        public string token_no { get; set; }
        [DataMember]
        public int expires_in { get; set; }


    }
}
