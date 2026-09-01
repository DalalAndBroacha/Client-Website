using System;
using System.Runtime.Serialization;

namespace PieAPI.SMS
{
    [DataContract]
    public class psp_dsp_get_token
    {
        [DataMember]
        public string Token { get; set; }

        [DataMember]
        public DateTime Start_time { get; set; }
        [DataMember]
        public DateTime Expire_time { get; set; }

    }
}
