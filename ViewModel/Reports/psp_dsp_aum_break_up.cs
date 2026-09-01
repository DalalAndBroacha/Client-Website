using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Reports
{
    [DataContract]
    public class psp_dsp_aum_break_up:dtoBase
    {
        [DataMember]
        public string branch { get; set; }
        [DataMember]
        public string family_name { get; set; }
        [DataMember]
        public string client_anme { get; set; }
        [DataMember]
        public string pan_number { get; set; }
        [DataMember]
        public string rm { get; set; }
        [DataMember]
        public float total { get; set; }
        [DataMember]
        public float Direct_Equity { get; set; }
        [DataMember]
        public float Equity_PMS { get; set; }
        [DataMember]
        public float Equity_MF { get; set; }
        [DataMember]
        public float Debt_MF { get; set; }
        [DataMember]
        public float Bonds { get; set; }

    }
}
