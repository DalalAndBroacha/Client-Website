using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ViewModel.Reports
{
    public class psp_dsp_mf_transction_details
    {
        [DataMember]
        public string JSON_main_client_code { get; set; }
        [DataMember]
        public string JSON_sub_category { get; set; }
        [DataMember]
        public string JSON_folio_no { get; set; }
        [DataMember]
        public string JSON_scrip_code { get; set; }
        [DataMember]
        public int rec_id { get; set; }
        [DataMember]
        public int sort_order { get; set; }
        [DataMember]
        public string client_name { get; set; }
        [DataMember]
        public string account_code { get; set; }
        [DataMember]
        public string scheme_name { get; set; }
        [DataMember]
        public DateTime invdate { get; set; }
        [DataMember]
        public string tr_type { get; set; }
        [DataMember]
        public float invvalue { get; set; }
        [DataMember]
        public float units { get; set; }
        [DataMember]
        public float bal_units { get; set; }
        [DataMember]
        public float nav { get; set; }
        [DataMember]
        public int no_of_days { get; set; }
        [DataMember]
        public float cmp { get; set; }
        [DataMember]
        public float mktvalue { get; set; }
        [DataMember]
        public float urpl { get; set; }
        [DataMember]
        public float rpl { get; set; }
        [DataMember]
        public float inc_dividend { get; set; }
        [DataMember]
        public float return_xirr { get; set; }
        [DataMember]
        public float return_abs { get; set; }
        
    }
}
