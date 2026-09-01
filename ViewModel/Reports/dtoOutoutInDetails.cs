using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Reports
{
    [DataContract]
   public class dtoOutoutInDetails
    {
        [DataMember]
        public long TotalDividend { get; set; }
        [DataMember]
        public long TotalShort { get; set; }
        [DataMember]
        public long TotalLong { get; set; }
        [DataMember]
        public long TotalRealise { get; set; }
        [DataMember]
        public List<IncomeStatement> incomeStatements { get; set; }
    }
}
