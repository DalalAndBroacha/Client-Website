using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Reports
{
    [DataContract]
    public class psp_dsp_brach_family_details
    {
        [DataMember]
        public string login_id { get; set; }
        [DataMember]
        public string branch_id { get; set; }
        [DataMember]
        public string family_name { get; set; }
        [DataMember]
        public string branch_head { get; set; }
        [DataMember]
        public string branch_name { get; set; }
        [DataMember]
        public string RM { get; set; }
        [DataMember]
        public string Fam_Email { get; set; }
        [DataMember]
        public string Fam_contact { get; set; }
        [DataMember]
        public string family_code { get; set; }

    }
}
