using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ViewModel.Reports
{
    [DataContract]
    public class psp_dsp_client_contact_details
    {
        [DataMember]
        public string family_id { get; set; }
        [DataMember]
        public string pan_number { get; set; }
        [DataMember]
        public string main_client_id { get; set; }
        [DataMember]
        public string main_client_name { get; set; }
        [DataMember]
        public string segment { get; set; }
        [DataMember]
        public string client_id { get; set; }
        [DataMember]
        public string client_name { get; set; }
        [DataMember]
        public string account_code { get; set; }
        [DataMember]
        public string address { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public string contact_no1 { get; set; }
        [DataMember]
        public string contact_no2 { get; set; }
        [DataMember]
        public string aadhar_no1 { get; set; }
        [DataMember]
        public string aadhar_no2 { get; set; }
        [DataMember]
        public string aadhar_no3 { get; set; }
    }
}
