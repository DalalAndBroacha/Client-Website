using System;
using System.Runtime.Serialization;

namespace PieAPI.SMS
{
    [DataContract]
    public class psp_dsp_API_Details
    {
        [DataMember]
        public DateTime Expire_time { get; set; }

        [DataMember]
        public string Token { get; set; }
        [DataMember]
        public string msg { get; set; }
    }

    public class AuthResponse
    {
        public int Status { get; set; }
        public string Token { get; set; }
        public string msg { get; set; }
    }
}
