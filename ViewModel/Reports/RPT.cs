using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
namespace ViewModel.Reports
{
    [DataContract]
  public class RPT:dtoBase
    {
        [DataMember]
        public List<CommonDocumentReports> commdocrpt { get; set; }
        [DataMember]
        public List<SIPDetail> sd { get; set; }
        [DataMember]
        public List<Dividend> dividend { get; set; }
        [DataMember]
        public List<IncomeStatement> inc { get; set; }
        [DataMember]
        public List<RealisedGainLoss> rgl { get; set; }
        [DataMember]
        public List<EquityClientSummary> ecs { get; set; }
        [DataMember]
        public List<EquityClientFlow> ecf { get; set; }
        [DataMember]
        public List<EquityClientFyFactor> ecff { get; set; }
        [DataMember]
        public List<EquityClientHolding> ech { get; set; }
        [DataMember]
        public List<PSPDSPClientFlow> pdcf { get; set; }
        [DataMember]
        public List<PSPDSPOUTFLOW> pdof { get; set; }
        [DataMember]
        public double? totamt { get; set; }
        [DataMember]
        public string yearID { get; set; }
        [DataMember]
        public string familyListID { get; set; }

        [DataMember]
        public Array[] Total_family_name { get; set; }

        [DataMember]
        public string[] TotalRm_name { get; set; }

        [DataMember]
        public string[] Total { get; set; }
        [DataMember]
        public string main_client_id { get; set; }
        
    }
}
