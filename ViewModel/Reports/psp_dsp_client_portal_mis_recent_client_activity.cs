using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ViewModel.Reports
{
    [DataContract]
    public class psp_dsp_client_portal_mis_recent_client_activity
    {
        [DataMember]
        public string login_id { get; set; }
        [DataMember]
        public string main_client_id { get; set; }

        [DataMember]
        public string client_name { get; set; }
        [DataMember]
        public string eqt_last_trade_date { get; set; }
        [DataMember]
        public string mf_last_trade_date { get; set; }
        [DataMember]
        public string last_review_date { get; set; }
        [DataMember]
        public string last_reviewed_by { get; set; }
        [DataMember]
        public string remarks { get; set; }
        [DataMember]
        public string history_flag { get; set; }
    }
}
