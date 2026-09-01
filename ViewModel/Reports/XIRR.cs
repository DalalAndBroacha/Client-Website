using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;


namespace ViewModel.Reports
{
    [DataContract]
    public class XIRR
    {
        [DataMember]
        public List<EquityClientFyFactor> EquityClientFyFactor { get; set; }

        [DataMember]
        public EquityClientFlow EquityClientFlow { get; set; }
        [DataMember]
        public List<EquityClientSummary> EquityClientSummary { get; set; }
    }
}
