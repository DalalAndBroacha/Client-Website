using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Shared
{
    [DataContract]
    public class psp_dsp_single_family_details //Even Used for psp_amd_family_details
    {
        [DataMember]
        public string user_id { get; set; }
        
        [DataMember]
        public string LoginId { get; set; }
        [DataMember]
        public string family_id { get; set; }
        [DataMember]
        public string Family_name { get; set; }
        [DataMember]
        public string Email_Id { get; set; }
        [DataMember]
        public string Mobile_No { get; set; }
        [DataMember]
        public string display_id { get; set; }

    }
}
