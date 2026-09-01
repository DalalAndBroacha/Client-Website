using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Reports
{
    [DataContract]
    public class psp_dsp_equity_client_fy_dividend
    {
        [DataMember]
        public string account_code { get; set; }
        [DataMember]
        public string fin_year { get; set; }
        [DataMember]
        public string trans_date { get; set; }
        [DataMember]
        public string scrip_name { get; set; }
        [DataMember]
        public float? amount { get; set; }

    }
}
