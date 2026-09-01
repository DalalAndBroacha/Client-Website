using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.IPO
{
    
    public class psp_verify_ipo_otp
    {
        [DataMember]
        public string dp_id { get; set; }
        [DataMember]
        public string pan { get; set; }
        [DataMember]
        public string client_name { get; set; }
        [DataMember]
        public string symbol { get; set; }
        [DataMember]
        public string bid_ttl_amount { get; set; }
        [DataMember]
        public string upi_id { get; set; }
        [DataMember]
        public string bid_price { get; set; }
        [DataMember]
        public string bid_qty { get; set; }
        [DataMember]
        public string bid_cutoff { get; set; }
        [DataMember]
        public string flag { get; set; }
        [DataMember]
        public string otp_encrypt { get; set; }
        [DataMember]
        public string otp { get; set; }
        [DataMember]
        public string sql_msg { get; set; }
        [DataMember]
        public string req_status { get; set; }
        [DataMember]
        public string ibbs_remarks { get; set; }
        [DataMember]
        public string bid_actioncode { get; set; }
        [DataMember]
        public string applicationno { get; set; }

    }
}
