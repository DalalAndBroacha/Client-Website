using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.IPO
{
    public class psp_amd_ipo_requests
    {
        [DataMember]
        public string applicationno { get; set; }
        [DataMember]
        public string pan_number { get; set; }
        [DataMember]
        public string dp_id { get; set; }
        [DataMember]
        public string main_client_id { get; set; }
        [DataMember]
        public string client_name { get; set; }

        [DataMember]
        public string family_token { get; set; }
        [DataMember]
        public string ISIN { get; set; }
        [DataMember]
        public string symbol { get; set; }
        [DataMember]
        public string bid_id { get; set; }
        [DataMember]
        public string orderno { get; set; }
        [DataMember]
        public string bid_qty { get; set; }
        [DataMember]
        public string bid_price { get; set; }
        [DataMember]
        public string bid_cutoff { get; set; }
        [DataMember]
        public string bid_ttl_amount { get; set; }
        [DataMember]
        public string bid_actioncode { get; set; } //N: -NEW,M: -MODIFY,D: -CANCEL For API

        [DataMember]
        public string upi_id { get; set; }
        [DataMember]
        public string otp { get; set; }
        [DataMember]
        public string otp_encrypt { get; set; }
        [DataMember]
        public string login_id { get; set; }

        [DataMember]
        public string sql_msg { get; set; }
        [DataMember]
        public string ibbs_remarks { get; set; }

    }
}
