using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Shared
{
    [DataContract]
    public class psp_dsp_nri_client_data_entry_taxwithheld_details
    {
        [DataMember]
        public string main_client_id { get; set; }
        [DataMember]
        public string bank_account { get; set; }
        [DataMember]
        public string cal_year { get; set; }

        [DataMember]
        public string rec_id { get; set; }
        [DataMember]
        public DateTime? trans_date { get; set; }
        
        [DataMember]
        public string category_order { get; set; }
        [DataMember]
        public string category_name { get; set; }
        
        [DataMember]
        public string subcategory_id { get; set; }
        
        [DataMember]
        public string subcategory_name { get; set; }
        [DataMember]
        public string narration { get; set; }
        [DataMember]
        public float amount { get; set; }
        [DataMember]
        public DateTime? amd_date { get; set; }
    }
}
