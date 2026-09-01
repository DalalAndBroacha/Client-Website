using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Shared
{
    [DataContract]
    public class psp_dsp_family_mapping_branch_details
    {
        [DataMember]
        public string family_token { get; set; }
        [DataMember]
        public string branch_id { get; set; }
        [DataMember]
        public string family_name { get; set; }
        [DataMember]
        public string branch_name { get; set; }
    }
}
