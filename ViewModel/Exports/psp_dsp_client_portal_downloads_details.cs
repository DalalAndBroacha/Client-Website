using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ViewModel.Exports
{
    [DataContract]
    public class psp_dsp_client_portal_downloads_details
    {
        [DataMember]
        public string requester_id { get; set; }
        //[DataMember]
        //public string report_id { get; set; }
        [DataMember]
        public string report_name { get; set; }
        [DataMember]
        public string export_format { get; set; }

        [DataMember]
        public string description { get; set; }
        [DataMember]
        public string status { get; set; }
        [DataMember]
        public string download_file { get; set; }
        [DataMember]
        public string display_msg { get; set; }
        [DataMember]
        public DateTime request_date { get; set; }
    }
}


 							