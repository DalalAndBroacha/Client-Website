using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ViewModel.Reports.Global_Report
{
    [DataContract]
    public class initGlobalReport
    {
        [DataMember]
        public string family_token { get; set; }
        [DataMember]
        public string family_name { get; set; }
        [DataMember]
        public string main_client_id { get; set; }
    }
}
