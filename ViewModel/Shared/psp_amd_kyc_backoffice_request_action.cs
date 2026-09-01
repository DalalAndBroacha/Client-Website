using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ViewModel.Shared
{
    public class psp_amd_kyc_backoffice_request_action
    {
        [DataMember]
        public string request_id { get; set; }
        [DataMember]
        public string account_code { get; set; }
        [DataMember]
        public string status { get; set; }
        [DataMember]
        public string remarks { get; set; }
    }
}
