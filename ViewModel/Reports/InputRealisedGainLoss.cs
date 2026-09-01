using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Reports
{
    [DataContract]
    public class InputRealisedGainLoss : dtoBase
    {
        [DataMember]
        public string FINYEAR { get; set; }
        [DataMember]
        public string ClientID { get; set; }
        [DataMember]
        public string Subcategory { get; set; }
        [DataMember]
        public string Rtpperiod { get; set; }
        [DataMember]
        public string Rptperiod_value { get; set; }
    }
}
