using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ViewModel.Reports.Liquiloan
{
     public class psp_dsp_liquiloan_investor_dashboard
    {
        [DataMember]
        public string main_client_name { get; set; }
        [DataMember]
        public string family_name { get; set; }
        [DataMember]
        public string pan_number { get; set; }
        [DataMember]
        public string investor_id { get; set; }

        [DataMember]
        public float Net_Principal_Investment { get; set; }
        [DataMember]
        public float Portfolio_Value { get; set; }
           
    }
}
