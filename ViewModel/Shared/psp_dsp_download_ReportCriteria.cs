using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Shared
{
    [DataContract]
    public class psp_dsp_download_ReportCriteria
    {

        [DataMember]
        public string LoginId { get; set; }
        [DataMember]
        public string report_id { get; set; }
        [DataMember]
        public string ws_client_id { get; set; }
        [DataMember]
        public string report_name { get; set; }
        [DataMember]
        public string label { get; set; }
        [DataMember]
        public string field { get; set; }
        [DataMember]
        public string type { get; set; }
        [DataMember]
        public string defaultValue { get; set; }
        [DataMember]
        public string code { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string flag { get; set; }
		[DataMember]
		public DateTime EOD_date { get; set; }
		[DataMember]
        public string defaultValue_CP { get; set; }
        
    }
}
