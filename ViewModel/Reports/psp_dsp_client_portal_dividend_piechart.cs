using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ViewModel.Reports
{
    [DataContract]
    public class psp_dsp_client_portal_dividend_piechart
    {
        [DataMember]
        public string family_id { get; set; }
        [DataMember]
        public string fin_year { get; set; }
        [DataMember]
        public float value { get; set; }
        [DataMember]
        public float percentage { get; set; }
        [DataMember]
        public string sub_category { get; set; }
    }
}

 	
