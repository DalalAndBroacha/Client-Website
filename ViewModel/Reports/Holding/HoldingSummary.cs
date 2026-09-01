using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Reports.Holding
{
    [DataContract]
    public class HoldingSummary
    {

        [DataMember]
        public string main_client_id { get; set; }
        [DataMember]
        public string FINYR { get; set; }
        [DataMember]
        public string ToDate { get; set; }
        [DataMember]
        public string summaryFlag { get; set; }
        [DataMember]
        public string acc_sub_category { get; set; }
        [DataMember]
        public string sub_category { get; set; }
        [DataMember]
        public string scrip_name { get; set; }
        [DataMember]
        public string hold_qty { get; set; }
        [DataMember]
        public float holding_rate { get; set; }
        [DataMember]
        public float Total_cost { get; set; }
        [DataMember]
        public float holding_mkt_rate { get; set; }
        [DataMember]
        public DateTime mkt_rate_date { get; set; }
        [DataMember]
        public float mkt_value { get; set; }
        [DataMember]
        public float hold_per { get; set; }
        [DataMember]
        public float profit_loss { get; set; }

        //Holding detail

        [DataMember]
        public float holding_qty { get; set; }
        [DataMember]
        public DateTime buy_trn_date { get; set; }
        [DataMember]
        public float buy_trn_rate { get; set; }
        [DataMember]
        public float value { get; set; }
        [DataMember]
        public float holding_per { get; set; }
        [DataMember]
        public float cum_per { get; set; }
        [DataMember]
        public float market_rate { get; set; }
        [DataMember]
        public float markt_value { get; set; }
        [DataMember]
        public float holding_per1 { get; set; }
        [DataMember]
        public int no_of_days { get; set; }
        [DataMember]
        public string ST_LT { get; set; }

    }
}
