using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
namespace ViewModel.Reports
{
    [DataContract]
   public class CommonDocumentReports:dtoBase
    {
        [DataMember]
        public int login_id { get; set; }
        [DataMember]
        public string report_category { get; set; }
        [DataMember]
        public string report_subcategory { get; set; }
        [DataMember]
        public string report_caption { get; set; }
        [DataMember]
        public string report_link { get; set; }
        [DataMember]
        public DateTime? from_date { get; set; }
        [DataMember]
        public int? show_date { get; set; }
    }
}
