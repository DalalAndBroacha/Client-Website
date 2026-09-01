using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Reports.Global_Report
{
    [DataContract]
    public class psp_dsp_global_report_gain_loss
    {
        [DataMember]
        public string main_client_id { get; set; }
        [DataMember]
        public string from_date { get; set; }
        [DataMember]
        public string to_date { get; set; }
        [DataMember]
        public string div_category { get; set; }
        [DataMember]
        public string sub_category { get; set; }

        [DataMember]
        public string display_order { get; set; }
        [DataMember]
        public string scrip_name { get; set; }
        [DataMember]
        public string isin_number { get; set; }
        [DataMember]
        public string buy_trn_date { get; set; }
        [DataMember]
        public string sell_trn_date { get; set; }
        [DataMember]
        public float buy_trn_qty { get; set; }
        [DataMember]
        public float buy_trn_rate { get; set; }
        [DataMember]
        public float buy_amount { get; set; }
        [DataMember]
        public float sell_trn_rate { get; set; }
        [DataMember]
        public float sell_amount { get; set; }
        [DataMember]
        public float fair_rate { get; set; }
        [DataMember]
        public float acquisition_rate { get; set; }
        [DataMember]
        public float st_pnl { get; set; }
        [DataMember]
        public float lt_pnl { get; set; }
        [DataMember]
        public float lt_pnl_gf { get; set; }
        [DataMember]
        public float int_pnl { get; set; }
        [DataMember]
        public float total_pnl { get; set; }
        [DataMember]
        public float total_pnl_gf { get; set; }
        [DataMember]
        public string remarks_in { get; set; }
        [DataMember]
        public string remarks_out { get; set; }

    }
}

 										
