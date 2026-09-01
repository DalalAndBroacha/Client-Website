using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Reports
{
    [DataContract]
    public class psp_dsp_client_trade_exceptions:dtoBase
    {
        [DataMember]
        public string client_code { get; set; }
        [DataMember]
        public string script_code { get; set; }
        [DataMember]
        public string rec_count { get; set; }
        [DataMember]
        public string rec_type { get; set; }

        [DataMember]
        public string script_name { get; set; }
    }
}
