using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Reports.Global_Report
{
    [DataContract]
    public class psp_dsp_global_report_trades
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
        public string trans_date { get; set; }
        [DataMember]
        public string scrip_name { get; set; }
        [DataMember]
        public float trn_qty { get; set; }
        [DataMember]
        public float trn_rate { get; set; }
        [DataMember]
        public float Amount { get; set; }
        [DataMember]
        public string dr_cr { get; set; }
        [DataMember]
        public string remark { get; set; }
    }
}

 						

