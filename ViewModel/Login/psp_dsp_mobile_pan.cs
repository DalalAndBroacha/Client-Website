using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Login
{
    [DataContract]
    public class psp_dsp_mobile_pan
    {
        [DataMember]
        public string pan_number { get; set; }
        [DataMember]
        public string Mob_No { get; set; }
        [DataMember]
        public string Flag { get; set; }
        [DataMember]
        public string UCC_Value { get; set; }
        [DataMember]
        public string JSON_Asset_Class { get; set; }

        [DataMember]
        public int Asset_Class { get; set; }
        [DataMember]
        public string sql_message { get; set; }
        [DataMember]
        public string otp_message { get; set; }
        [DataMember]
        public string sql_status { get; set; }
        [DataMember]
        public string mobile { get; set; }
        [DataMember]
        public string masked_mobile { get; set; }
        [DataMember]
        public string LoginId { get; set; }
        [DataMember]
        public string token_no { get; set; }
        [DataMember]
        public string expires_in { get; set; }
        [DataMember]
        public string response { get; set; }
        [DataMember]
        public string username { get; set; }
    }
}
