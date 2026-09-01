using System.Runtime.Serialization;

namespace PieAPI.SMS
{
    [DataContract]
    public class psp_dsp_generate_token
    {
        [DataMember]
        public string api_user { get; set; }

        [DataMember]
        public string api_password { get; set; }
    }
}
