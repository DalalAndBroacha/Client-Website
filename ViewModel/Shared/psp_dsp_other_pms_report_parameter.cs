using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Shared
{
    [DataContract]
    public class psp_dsp_other_pms_report_parameter
    {
        [DataMember]
        public string APIURL { get; set; }
        [DataMember]
        public string API_Key { get; set; }
        [DataMember]
        public string downloadedFilepath { get; set; }
        [DataMember]
        public string reportCriteria { get; set; }

        [DataMember]
        public string reportID { get; set; }
        [DataMember]
        public string Report_name { get; set; }
        [DataMember]
        public string client_name { get; set; }
        [DataMember]
        public string Login_Id { get; set; }

    }
}
