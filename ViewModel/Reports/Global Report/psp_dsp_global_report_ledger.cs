using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ViewModel.Reports.Global_Report
{
    [DataContract]
    public class psp_dsp_global_report_ledger
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
        public string account_code { get; set; }
        [DataMember]
        public string tr_date { get; set; }
        [DataMember]
        public string exchange { get; set; }
        [DataMember]
        public string narration { get; set; }
        [DataMember]
        public string document_no { get; set; }
        [DataMember]
        public float debit_mount { get; set; }
        [DataMember]
        public float credit_mount { get; set; }
        [DataMember]
        public float running_balance { get; set; }
    }
}


 						