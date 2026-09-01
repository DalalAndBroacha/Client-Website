using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Reports
{
    public class psp_dsp_research_view_given_brokerage
    {
        [DataMember]
        public string from_date { get; set; }
        [DataMember]
        public string Script_Name { get; set; }
        [DataMember]
        public string to_date { get; set; }
        [DataMember]
        public string flag { get; set; }
        [DataMember]
        public string scrip_code { get; set; }
        [DataMember]
        public string rm_id { get; set; }
        [DataMember]
        public string branch_name { get; set; }
        [DataMember]
        public decimal Buy_Qty { get; set; }
        [DataMember]
        public decimal buy_client { get; set; }
        [DataMember]
        public decimal Sell_Qty { get; set; }
        [DataMember]
        public decimal sell_client { get; set; }
        [DataMember]
        public decimal buy_volume { get; set; }
        [DataMember]
        public decimal sell_volume { get; set; }
        [DataMember]
        public decimal Brokerage { get; set; }
        [DataMember]
        public decimal branch_client_cnt { get; set; }
        [DataMember]
        public decimal client_scrip_holding { get; set; }
        [DataMember]
        public decimal client_hld_scrip_cnt { get; set; }
    }
}
