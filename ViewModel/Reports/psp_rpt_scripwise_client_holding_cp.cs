using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ViewModel.Reports
{
    [DataContract]
    public class psp_rpt_scripwise_client_holding_cp
    {
        [DataMember]
        public string LoginId { get; set; }
        [DataMember]
        public string Asset { get; set; }
        [DataMember]
        public string Script { get; set; }
        [DataMember]
        public string family_id { get; set; }
        [DataMember]
        public string main_client_name { get; set; }
        [DataMember]
        public string rm_name { get; set; }
        [DataMember]
        public string main_client_id { get; set; }
        [DataMember]
        public string client_id { get; set; }
        [DataMember]
        public string asset_name { get; set; }
        [DataMember]
        public string asset_class { get; set; }
        [DataMember]
        public string account_code { get; set; }
        [DataMember]
        public string isin { get; set; }
        [DataMember]
        public string scrip_code { get; set; }
        [DataMember]
        public string scrip_name { get; set; }
        [DataMember]
        public float holding_qty { get; set; }
        [DataMember]
        public float st_qty { get; set; }
        [DataMember]
        public float lt_qty { get; set; }
        [DataMember]
        public float holding_rate { get; set; }
        [DataMember]
        public float hld_cost { get; set; }
        [DataMember]
        public float holding_mkt_rate { get; set; }
        [DataMember]
        public float current_value { get; set; }
        [DataMember]
        public float dividend { get; set; }
        [DataMember]
        public float srt_unreal_profit { get; set; }
        [DataMember]
        public float long_unreal_profit { get; set; }
        [DataMember]
        public float return_xirr { get; set; }
        [DataMember]
        public float return_cagr { get; set; }
        [DataMember]
        public float dp_holding_qty { get; set; }
        [DataMember]
        public float latest_buy { get; set; }
        [DataMember]
        public float latest_sell { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string branch_name { get; set; }

        
    }
}
