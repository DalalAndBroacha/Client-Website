using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ViewModel.Reports.Liquiloan
{
    [DataContract]
    public class psp_dsp_liquiloan_investment
    {
        [DataMember]
        public string investor_id { get; set; }

        [DataMember]
        public string category { get; set; }
        [DataMember]
        public string scheme_details { get; set; }
        [DataMember]
        public DateTime Investment_Date { get; set; }
        [DataMember]
        public float Amount { get; set; }
        [DataMember]
        public float XIRR { get; set; }
        [DataMember]
        public float Interest_Repaid{ get; set; }
        [DataMember]
        public float Principal_Repaid { get; set; }
        [DataMember]
        public float Total_Repaid { get; set; }
        [DataMember]
        public float Portfolio_Value { get; set; }
        [DataMember]
        public DateTime End_Date { get; set; }
    }
}
