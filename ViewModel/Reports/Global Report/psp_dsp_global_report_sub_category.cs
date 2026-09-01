using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Reports.Global_Report
{
    [DataContract]
    public class psp_dsp_global_report_sub_category
    {
        [DataMember]
        public string main_client_id { get; set; }
        [DataMember]
        public string start_date { get; set; }
        [DataMember]
        public string end_date { get; set; }
        [DataMember]
        public string sub_category { get; set; }
        [DataMember]
        public string sub_category_order { get; set; }
        [DataMember]
        public string trade_flag { get; set; }
        [DataMember]
        public string ledger_flag { get; set; }
        [DataMember]
        public string gain_loss_flag { get; set; }
        [DataMember]
        public string dividend_flag { get; set; }
    }
}
