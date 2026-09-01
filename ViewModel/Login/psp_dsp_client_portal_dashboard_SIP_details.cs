using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ViewModel.Login
{
    [DataContract]
    public class psp_dsp_client_portal_dashboard_SIP_details
    {
        [DataMember]
        public string family_token { get; set; }
        [DataMember]
        public string main_client_name { get; set; }
        [DataMember]
        public string Scheme_Name { get; set; }
        [DataMember]
        public string Folio_no { get; set; }
        [DataMember]
        public string Start_Date { get; set; }
        [DataMember]
        public string End_Date { get; set; }
        [DataMember]
        public float Amount { get; set; }
        [DataMember]
        public string Frequency { get; set; }
        [DataMember]
        public string Target_scheme { get; set; }
        [DataMember]
        public string terminating { get; set; }

    }
}
