using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
namespace ViewModel.Reports
{
    [DataContract]
   public class psp_rpt_performance_holding_equity_mf_report: dtoBase
    {
        [DataMember]
        public int LoginId { get; set; }
        [DataMember]
        public string Family { get; set; }

        [DataMember]
        public string Client { get; set; }
        [DataMember]
        public int FINYR { get; set; }
        [DataMember]
        public string Subcategory { get; set; }
        [DataMember]
        public string family_name { get; set; }
        [DataMember]
        public string main_client_id { get; set; }
        [DataMember]
        public string main_client_name { get; set; }
        [DataMember]
        public int reord_order { get; set; }
        [DataMember]
        public string fund_style { get; set; }
        [DataMember]
        public string date_of_purchase { get; set; }
        [DataMember]
        public string no_of_days { get; set; }
        [DataMember]
        public string account_code { get; set; }
        [DataMember]
        public string asset_name { get; set; }
        [DataMember]
        public string scrip_code { get; set; }
        [DataMember]
        public string scrip_name { get; set; }
        [DataMember]
        public string folio_no { get; set; }
       
        [DataMember]
        public float Quantity { get; set; }
        [DataMember]
        public float hld_cost { get; set; }
        [DataMember]
        public float avg_cost { get; set; }
        [DataMember]
        public float current_nav { get; set; }
        [DataMember]
        public float Market_Value { get; set; }
        [DataMember]
        public float holding_per { get; set; }
        [DataMember]
        public float dividend_inc { get; set; }
        [DataMember]
        public float srt_unreal_profit   { get; set; }
        [DataMember]
        public float long_unreal_profit { get; set; }
        [DataMember]
        public float srt_real_profit { get; set; }
        [DataMember]
        public float long_real_profit { get; set; }
        [DataMember]
        public float srt_ttl_gain { get; set; }
        [DataMember]
        public float lng_ttl_gain { get; set; }
        [DataMember]
        public float return_abs { get; set; }
        [DataMember]
        public float return_xirr { get; set; }

        [DataMember]
        public float return_cagr { get; set; }
        [DataMember]
        public float bench_abs { get; set; }
        [DataMember]
        public float bench_xirr { get; set; }
        [DataMember]
        public float bench_cagr { get; set; }
       
        [DataMember]
        public float net_return_abs { get; set; }
        [DataMember]
        public float net_return_xirr { get; set; }
    }
}
