using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Shared
{
    [DataContract]
    public class psp_rpt_nri_client_details
    {
        [DataMember]
        public string main_client_id { get; set; }
        [DataMember]
        public string client_category { get; set; }
        [DataMember]
        public string cal_year { get; set; }
        [DataMember]
        public string flag { get; set; }

        [DataMember]
        public string category { get; set; }
        [DataMember]
        public string category_order { get; set; }
        [DataMember]
        public string subcategory { get; set; }
        [DataMember]
        public string subcategory_order { get; set; }

        [DataMember]
        public float amount { get; set; }
        [DataMember]
        public float tax_Amount  { get; set; }
        [DataMember]
        public float net_Income_IRS    { get; set; }
        [DataMember]
        public float USD_amount { get; set; }
        [DataMember]
        public float USD_Tax_Amount { get; set; }
        [DataMember]
        public float net_Income_USD { get; set; }
        [DataMember]
        public float currency { get; set; }
    }
}
