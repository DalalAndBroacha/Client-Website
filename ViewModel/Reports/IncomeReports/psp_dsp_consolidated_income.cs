using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Reports
{
    [DataContract]
    public class psp_dsp_consolidated_income: dtoBase
    {

        [DataMember]
        public string fin_year { get; set; }

        [DataMember]
        public string fam_id { get; set; }
        [DataMember]
        public string main_client_id { get; set; }
        [DataMember]
        public string client_name { get; set; }
        [DataMember]
        public string pan_number { get; set; }
        [DataMember]
        public string category { get; set; }
        [DataMember]
        public string sub_category { get; set; }

        [DataMember]
        public string sub_cat_code { get; set; }

        [DataMember]
        public float int_real_profit { get; set; }
        [DataMember]
        public float srt_real_profit { get; set; }
        [DataMember]
        public float lng_real_profit { get; set; }
        [DataMember]
        public float lng_real_profit_gf { get; set; }

        [DataMember]
        public float dividend { get; set; }
        [DataMember]
        public int order_id { get; set; }

    }
}
