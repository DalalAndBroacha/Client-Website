using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Reports
{
    [DataContract]
    public class psp_amd_nri_client_data_entry
    {
        [DataMember]
        public string loginid { get; set; }
        [DataMember]
        public string rec_id { get; set; }
        [DataMember]
        public string main_client_id { get; set; }
        [DataMember]
        public string bank_account { get; set; }
        [DataMember]
        public string client_cat { get; set; }
        [DataMember]
        public string trans_date { get; set; }
        [DataMember]
        public string segment { get; set; }
        [DataMember]
        public string trans_type { get; set; }
        [DataMember]
        public string narration { get; set; }
        [DataMember]
        public string amount { get; set; }
        [DataMember]
        public string flag{ get; set; }

    }
}
