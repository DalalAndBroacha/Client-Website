using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Login
{
    [DataContract]
    public class psp_dsp_client_portal_dashboard_notification
    {
        [DataMember]
        public string family_token { get; set; }
        [DataMember]
        public string msg { get; set; }
    }
}
