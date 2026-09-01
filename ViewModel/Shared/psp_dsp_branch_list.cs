using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Shared
{
    [DataContract]
    public class psp_dsp_branch_list
    {
        [DataMember]
        public string LoginId { get; set; }
        [DataMember]
        public string cat_type { get; set; }
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public string name { get; set; }
    }
}
