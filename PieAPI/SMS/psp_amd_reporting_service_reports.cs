using System.Runtime.Serialization;

namespace PieAPI.SMS
{
    [DataContract]
    public class psp_amd_reporting_service_reports
    {
        [DataMember]
        public int report_id { get; set; }
        [DataMember]
        public string report_to { get; set; }
        [DataMember]
        public string report_cc { get; set; }
        [DataMember]
        public string report_export_format { get; set; }
        [DataMember]
        public string report_parameters { get; set; }
        [DataMember]
        public string report_subject { get; set; }
        [DataMember]
        public string report_body { get; set; }
        [DataMember]
        public string message_type { get; set; }
        [DataMember]
        public int profile_id { get; set; }
        [DataMember]
        public int priority { get; set; }
    }
}
