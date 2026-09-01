using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Login
{
    [DataContract]
   public class otp :dtoBase
    {
        //[DataMember]
        public string otp1 { get; set; }
        //[DataMember]
        public string otp2 { get; set; }
        //[DataMember]
        public string otp3 { get; set; }
        [DataMember]
        public string otp4 { get; set; }
        [DataMember]
        public string otp5 { get; set; }
        [DataMember]
        public string otp6 { get; set; }
        [DataMember]
        public string finalotp { get; set; }
        [DataMember]
        public string token_no { get; set; }
        [DataMember]
        public string flag { get; set; }
        [DataMember]
        public string sql_status { get; set; }
        [DataMember]
        public string sql_message { get; set; }
    }
}
