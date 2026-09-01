using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
namespace ViewModel.Reports
{
    [DataContract]
   public class psp_rpt_client_performance_check:dtoBase
    {
        [DataMember]
        public string FINYR { get; set; } 
        [DataMember]
        public string Client { get; set; }
        [DataMember]
        public string Scrip_Code { get; set; }
        [DataMember]
        public string subcategory { get; set; }
        [DataMember]
        public string account_code { get; set; }
        [DataMember]
        public string asset_code { get; set; }
        [DataMember]
        public string client_code { get; set; }
        [DataMember]
        public string client_name { get; set; }
        [DataMember]
        public string family_name { get; set; }
        [DataMember]
        public string script_name { get; set; }
        [DataMember]
        public string buy_sell_trn_date { get; set; }
        [DataMember]
        public float buy_sell_trn_qty { get; set; }
        [DataMember]
        public float buy_sell_trn_rate { get; set; }
        [DataMember]
        public float dividend { get; set; }
        [DataMember]
        public float cmp { get; set; }
        [DataMember]
        public float ust_pnl { get; set; }
        [DataMember]
        public float ult_pnl { get; set; }
        [DataMember]
        public float rst_pnl { get; set; }
        [DataMember]
        public float rlt_pnl { get; set; }
        [DataMember]
        public float ttl_srt_gain { get; set; }
        [DataMember]
        public float ttl_long_gain { get; set; }
        [DataMember]
        public float absolute { get; set; }
        [DataMember]
        public float xirr { get; set; }
        [DataMember]
        public float cagr { get; set; } 
        [DataMember]
        public string flag { get; set; }
      
    }
}
