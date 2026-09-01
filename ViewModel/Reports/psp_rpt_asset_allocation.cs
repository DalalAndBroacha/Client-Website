using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Reports
{
    [DataContract]
    public class psp_rpt_asset_allocation
    {
        [DataMember]
        public string LoginId { get; set; }
        [DataMember]
        public string acces_token { get; set; }
        [DataMember]
        public string family_id { get; set; }
        [DataMember]
        public string fin_year { get; set; }
        [DataMember]
        public string family_name { get; set; }
        [DataMember]
        public string client_id { get; set; }
        [DataMember]
        public string main_client_id { get; set; }
        [DataMember]
        public string AsOnDatePortReturn { get; set; }
        [DataMember]
        public string pan { get; set; }
        [DataMember]
        public string Mint_client_name { get; set; }
        [DataMember]
        public string account_code { get; set; }
        [DataMember]
        public string client_name { get; set; }
        [DataMember]
        public string display_category { get; set; }
        [DataMember]
        public string display_category_summary { get; set; }
        [DataMember]
        public string display_order { get; set; }

        [DataMember]
        public string category { get; set; }
        [DataMember]
        public string main_category { get; set; }
        [DataMember]
        public decimal Holding { get; set; }

        [DataMember]
        public decimal Market_value { get; set; }
        [DataMember]
        public float PercentageOfPortfolio { get; set; }
        [DataMember]
        public float hld_per_clientwise { get; set; }
        [DataMember]
        public string sql_message { get; set; }
        [DataMember]
        public string sql_status { get; set; }
        [DataMember]
        public string source { get; set; }
    }
}
