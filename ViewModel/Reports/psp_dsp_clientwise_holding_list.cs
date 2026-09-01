using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Reports
{
    public class psp_dsp_clientwise_holding_list
    {
        [DataMember]
        public string LoginId { get; set; }
        [DataMember]
        public string rm_id { get; set; }
        [DataMember]
        public string scrip_code { get; set; }
        [DataMember]
        public string data { get; set; }
        [DataMember]
        public string Family_Name { get; set; }
        [DataMember]
        public string client_name { get; set; }
        [DataMember]
        public string account_code { get; set; }
        [DataMember]
        public string scrip_name { get; set; }
        [DataMember]
        public string rm_name { get; set; }
        [DataMember]
        public decimal holding_qty { get; set; }
        [DataMember]
        public decimal mkt_rate { get; set; }
        [DataMember]
        public decimal holding_value { get; set; }
        [DataMember]
        public decimal portfolio_value { get; set; }
        [DataMember]
        public decimal holding_percentage { get; set; }
        [DataMember]
        public decimal purchase_rate { get; set; }
        [DataMember]
        public decimal purchase_value { get; set; }
    }
}