using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Exports
{
    [DataContract]
    public class psp_amd_send_report_client_portal
    {
        [DataMember]
        public string loginID { get; set; }
        [DataMember]
        public string parameters { get; set; }
        [DataMember]
        public string family_id { get; set; }
        [DataMember]
        public string account_code { get; set; }
        [DataMember]
        public string report_id { get; set; }
        [DataMember]
        public string mail_to { get; set; }
        [DataMember]
        public string export_format { get; set; } = "PDF";
        [DataMember]
        public string export_type { get; set; } = "E";
        
        [DataMember]
        public string FINYR { get; set; }
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public string Script_code { get; set; }
        [DataMember]
        public string Scheme_code { get; set; }
        [DataMember]
        public string parameterOne { get; set; }
        [DataMember]
        public string parameterTwo { get; set; }

        [DataMember]
        public string parameterThree { get; set; }
        [DataMember]
        public string parameterFour { get; set; }
    }
}
