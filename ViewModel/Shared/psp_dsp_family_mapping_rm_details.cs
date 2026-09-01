using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Shared
{
    [DataContract]
    public class psp_dsp_family_mapping_rm_details
    {
        [DataMember]
        public string family_token { get; set; }
        [DataMember]
        public string Rm_Id { get; set; }
        [DataMember]
        public string rm_name { get; set; }
    }
}
