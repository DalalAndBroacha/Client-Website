using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ViewModel.Reports.Liquiloan
{
    public class psp_dsp_liquiloan_investor_ledger
    {
        [DataMember]
        public string investor_id { get; set; }

        [DataMember]
        public DateTime Date { get; set; }
        [DataMember]
        public float Amount { get; set; }
        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public string Transaction_Type { get; set; }  

    }
}
