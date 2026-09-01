using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ViewModel.Reports
{
    [DataContract]
    public class EquityClientHolding:dtoBase
    { 
        [DataMember]
        public string account_code { get; set; }
        [DataMember]
        public string holding_type { get; set; }
        [DataMember]
        public string client_code { get; set; }
        [DataMember]
        public string scrip_code { get; set; }
        [DataMember]
        public string scrip_name { get; set; }
        [DataMember]
        public string scrip_industry { get; set; }
        [DataMember]
        public float holding_qty { get; set; }
        [DataMember]
        public float dp_holding_qty { get; set; }
        [DataMember]
        public float holding_rate { get; set; }
        [DataMember]
        public float holding_cost { get; set; }
        [DataMember]
        public float holding_mkt_rate { get; set; }
        [DataMember]
        public float market_value { get; set; }
        [DataMember]
        public float gainloss { get; set; }
        [DataMember]        
        public float return_xirr { get; set; }
        [DataMember]
        public float return_abs { get; set; }
        [DataMember]
        public float latest_buy { get; set; }
        [DataMember]
        public float latest_sell { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public float net_market_value { get; set; }
        
    }
}
