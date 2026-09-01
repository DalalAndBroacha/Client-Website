using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ViewModel.Login
{
    [DataContract]
    public class psp_dsp_client_portal_dashboard_xirr_details
    {
        [DataMember]
        public string family_token { get; set; }
        [DataMember]
        public string client_name { get; set; }
        [DataMember]
        public string account_code { get; set; }
        [DataMember]
        public float stock_val { get; set; }
        [DataMember]
        public float abs_ret { get; set; }
        [DataMember]
        public float xirr_ret { get; set; }
    }
}
