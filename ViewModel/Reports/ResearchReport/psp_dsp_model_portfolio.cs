using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Reports.ResearchReport
{
    [DataContract]
    public class psp_dsp_model_portfolio
    {
        [DataMember]
        public string rec_id { get; set; }
        [DataMember]
        [DataType(DataType.Date)]
        public DateTime reco_date { get; set; }
        [DataMember]
        public string isin { get; set; }
        [DataMember]
        public string stock_name { get; set; }
        [DataMember]
        public string portfolio_type { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "52_wk_high")]
        public string wk_high { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "52_wk_low")]
        public string wk_low { get; set; }
        [DataMember]
        public float eps1 { get; set; }
        [DataMember]
        public float eps2 { get; set; }
        [DataMember]
        public float range_start { get; set; }
        [DataMember]
        public float range_end { get; set; }
        [DataMember]
        public float reco_price { get; set; }
        [DataMember]
        public float current_price { get; set; }
        [DataMember]
        public float price_target { get; set; }
        [DataMember]
        public string stock_action { get; set; }
        [DataMember]
        public string risk_profile { get; set; }
        [DataMember]
        public string inv_rationale { get; set; }
        [DataMember]
        public string active { get; set; }
        [DataMember]
        public string actionable { get; set; }

    }
}
