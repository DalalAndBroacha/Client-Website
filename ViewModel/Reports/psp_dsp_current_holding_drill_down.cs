using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ViewModel.Reports
{
    [DataContract]
   public class psp_dsp_current_holding_drill_down:dtoBase 
    {
        [DataMember]
        public string client_Code { get; set; }
        [DataMember]
        public string script_code { get; set; }
        [DataMember]
        public string client_name { get; set; }
        [DataMember]
        public string script_name { get; set; }
        [DataMember]
        public float cmp { get; set; }
        [DataMember]
        public string tr_date { get; set; }
        [DataMember]
        public string remarks { get; set; }
        [DataMember]
        public float cr_qty { get; set; }
        [DataMember]
        public float tr_rate { get; set; }
        [DataMember]
        public float srt_unreal_profit { get; set; }
        [DataMember]
        public float long_unreal_profit { get; set; }
    }
}
