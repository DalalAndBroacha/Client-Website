using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ViewModel.Reports
{
    [DataContract]
    public class psp_dsp_client_script_trades
    {
        [DataMember]
        public string client_Code { get; set; }
        [DataMember]
        public string script_code { get; set; }
        [DataMember]
        public string tr_date { get; set; }
        [DataMember]
        public string value_date { get; set; }
        [DataMember]
        public string remarks { get; set; }
        [DataMember]
        public float cr_qty { get; set; }
        [DataMember]
        public float dr_qty { get; set; }
        [DataMember]
        public float tr_rate { get; set; }
        [DataMember]
        public float running_balance { get; set; }
    }
}
