using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ViewModel.Shared
{
    [DataContract]
    public class psp_dsp_master_list_cp
    {
        [DataMember]
        public string flag { get; set; }
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public string dsp_name { get; set; }
    }
}
