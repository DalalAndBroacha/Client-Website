using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Login
{
    [DataContract]
    public class psp_dsp_client_portal_dashboard_RM_details
    {
        [DataMember]
        public string family_token { get; set; }
        [DataMember]
        public string rm_name { get; set; }
        [DataMember]
        public string Email_Id { get; set; }
        [DataMember]
        public string Contact_No { get; set; }
    }
}
