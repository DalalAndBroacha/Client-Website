using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ViewModel.Login
{
    [DataContract]
    public class psp_dsp_user_access_token
    {
        [DataMember]
        public int? login_id { get; set; }
        [DataMember]
        public string web_session_id { get; set; }
    }
}
