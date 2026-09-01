using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Shared
{
    [DataContract]
    public class psp_dsp_BH_RM_list
    {
        [DataMember]
        public string login_id { get; set; }
        [DataMember]
        public string display_name { get; set; }
        [DataMember]
        public string roles { get; set; }
    }
}
