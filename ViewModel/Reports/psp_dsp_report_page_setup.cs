using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
namespace ViewModel.Reports
{
    [DataContract]
    public class psp_dsp_report_page_setup:dtoBase
    {
        [DataMember]
        public string Hdr_name { get; set; }
        [DataMember]
        public string hdr_address { get; set; }
        [DataMember]
        public string hdr_contact { get; set; }
        [DataMember]
        public string hdr_email { get; set; }
        [DataMember]
        public string ftr1 { get; set; }
        [DataMember]
        public string ftr2 { get; set; }
        [DataMember]
        public string ftr3 { get; set; }
        [DataMember]
        public string ftr4 { get; set; }
    }
}
