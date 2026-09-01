using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Shared
{
    [DataContract]
    public class psp_dsp_nri_bank_details
    {
        [DataMember]
        public string main_client_id { get; set; }
        [DataMember]
        public string data_level { get; set; }
        [DataMember]
        public string bank_account_no { get; set; }
        [DataMember]
        public string bank_account_type { get; set; }
        [DataMember]
        public string bank_name { get; set; }
        [DataMember]
        public string client_category { get; set; }
        [DataMember]
        public string client_cat_id { get; set; }
        
    }
}
