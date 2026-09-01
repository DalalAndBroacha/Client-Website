using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Reports
{
    [DataContract]
    public class psp_rpt_performance_holding_other_pms_report
    {
        [DataMember]
        public string FamilyID { get; set; }

        [DataMember]
        public string ClientID { get; set; }
        [DataMember]
        public string client_name { get; set; }
        [DataMember]
        public string FINYRData { get; set; }
        [DataMember]
        public string source { get; set; }
        [DataMember]
        public string asset_name { get; set; }

        [DataMember]
        public string Subcategory { get; set; }
        [DataMember]
        public string scrip_name { get; set; }
        [DataMember]
        public float? Quantity { get; set; }
        [DataMember]
        public float? unit_cost { get; set; }
        [DataMember]
        public float? Total_cost { get; set; }
        [DataMember]
        public float? Market_rate { get; set; }
        [DataMember]
        public float? market_Value { get; set; }
        [DataMember]
        public DateTime Market_date { get; set; }

        [DataMember]
        public float hold_per { get; set; }
        [DataMember]
        public float profit_loss { get; set; }
    }
}