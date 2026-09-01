using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
namespace ViewModel.Reports
{
    [DataContract]
    public class RPPerfirmanceTableAll : dtoBase
    {
        [DataMember]
        public List<PerformanceTable1> PT1 { get; set; }
        [DataMember]
        public List<PerformanceTable2> PT2 { get; set; }
        [DataMember]
        public List<PerformanceTable3> PT3 { get; set; }
        
       

    }
}
