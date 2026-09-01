using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Reports.IncomeReports
{
    [DataContract]
    public class psp_dsp_equity_client_fy_factors: dtoBase
    {
        [DataMember]
        public string account_code { get; set; }
        [DataMember]
        public string fin_year { get; set; }
        [DataMember]
        public string rpt_type { get; set; }
        [DataMember]
        public float Amount { get; set; }
        [DataMember]
        public DateTime Date { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string sort_order { get; set; }
    }
}
