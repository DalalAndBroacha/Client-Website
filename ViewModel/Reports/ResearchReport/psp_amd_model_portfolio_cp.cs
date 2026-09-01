using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace ViewModel.Reports.ResearchReport
{
    [DataContract]
    public class psp_amd_model_portfolio_cp
    {
        [DataMember]
        public string rec_id { get; set; }
        [DataMember]
        public string reco_date { get; set; }
        [DataMember]
        public string isin { get; set; }
        [DataMember]
        public string portfolio_type { get; set; }
        [DataMember]
        //[Column("52_wk_high")]
        public string wk_high { get; set; }
        [DataMember]
        //[Column("52_wk_low")]
        public string wk_low { get; set; }
        [DataMember]
        public string eps1 { get; set; }
        [DataMember]
        public string eps2 { get; set; }
        [DataMember]
        public string price_target { get; set; }
        [DataMember]
        public string risk_profile { get; set; }
        [DataMember]
        public string inv_rationale { get; set; }
        [DataMember]
        public string active { get; set; }
        [DataMember]
        public string actionable { get; set; }
        [DataMember]
        public string action { get; set; }
        [DataMember]
        public string user { get; set; }
    }
}
