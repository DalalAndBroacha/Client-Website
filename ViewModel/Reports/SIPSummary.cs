using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
namespace ViewModel.Reports
{
    [DataContract]
   public class SIPSummary:dtoBase
    {
        [DataMember]
        public int loginid { get; set; }
        [DataMember]
        public string Family_name { get; set; }
        [DataMember]
        public int Folio_no { get; set; }
        [DataMember]
        public double? Amount { get; set; }
        [DataMember]
        public int family_id { get; set; }

    }
}
