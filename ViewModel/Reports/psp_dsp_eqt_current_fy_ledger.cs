using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Reports
{
    [DataContract]
    public class psp_dsp_eqt_current_fy_ledger
    {
        [DataMember]
        public string LoginId { get; set; }
        [DataMember]
        public string Client { get; set; }
        [DataMember]
        public string account_code { get; set; }
        [DataMember]
        public string client_name { get; set; }
        [DataMember]
        public DateTime tr_date { get; set; }
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
