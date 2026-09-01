using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
namespace ViewModel.Reports
{
    [DataContract]
   public class psp_rpt_performance_holding_direct_equity_report:dtoBase
    {
        [DataMember]
        public string source { get; set; }
        [DataMember]
        public string main_category { get; set; }
        [DataMember]
        public string FamilyID { get; set; }  

        [DataMember]
        public string ClientID { get; set; }
        [DataMember]
        public string FINYRData { get; set; }
        [DataMember]
        public string Subcategory { get; set; }
        [DataMember]
        public string family_name { get; set; }
        [DataMember]
        public string main_client_name { get; set; }
        [DataMember]
        public string main_client_id { get; set; }
        [DataMember]
        public string account_code { get; set; }
        [DataMember]
        public string asset_name { get; set; }
        [DataMember]
        public string scrip_code { get; set; }
        [DataMember]
        public int reord_order { get; set; }
        [DataMember]
        public string scrip_name { get; set; }
        [DataMember]
        public string isin { get; set; }
        [DataMember]
        public string scrip_industry { get; set; }
        [DataMember]
        public float? Quantity { get; set; }
        [DataMember]
        public float? hld_cost { get; set; }
        [DataMember]
        public float? avg_cost { get; set; }
        [DataMember]
        public float? cmp { get; set; }
        [DataMember]
        public float? Market_Value { get; set; }
        [DataMember]
        public float? holding_per { get; set; }
        [DataMember]
        public float? dividend { get; set; }
        [DataMember]
        public float? srt_unreal_profit { get; set; }
        [DataMember]
        public float? long_unreal_profit { get; set; }
        [DataMember]
        public float? srt_real_profit { get; set; }
        [DataMember]
        public float? long_real_profit { get; set; }
        [DataMember]
        public float? srt_ttl_gain { get; set; }
        [DataMember]
        public float? lng_ttl_gain { get; set; }
        [DataMember]
        public float? return_abs { get; set; }
        [DataMember]
        public float? return_xirr { get; set; }

        [DataMember]
        public float? return_cagr { get; set; }
        [DataMember]
        public float? bench_abs { get; set; }
        [DataMember]
        public float? bench_xirr { get; set; }
        [DataMember]
        public float? bench_cagr { get; set; }
        [DataMember]
        public float? net_return_abs { get; set; }
        [DataMember]
        public float? net_return_xirr { get; set; }

    }
}
