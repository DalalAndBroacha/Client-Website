using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Reports
{
    [DataContract]
    public class psp_dsp_client_kyc_attributes
    {
        [DataMember]
        public string account_code { get; set; }
        [DataMember]
        public string client_name { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public string mobile_no { get; set; }
        [DataMember]
        public string income_range { get; set; }
        [DataMember]
        public string address { get; set; }

        [DataMember]
        public string pan_number { get; set; }
        
    }
}
