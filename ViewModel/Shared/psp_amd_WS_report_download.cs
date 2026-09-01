using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Shared
{
    [DataContract]
    public class psp_amd_WS_report_download
    {
        [DataMember]
        public string Login_id { get; set; }
        [DataMember]
        public string Report_id { get; set; }
        [DataMember]
        public string reportCriteria { get; set; }
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public string filename { get; set; }
        [DataMember]
        public string Error_msg { get; set; }
        [DataMember]
        public string status { get; set; }

	}
}
