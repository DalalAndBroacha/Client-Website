using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Shared
{
    [DataContract]
    public class psp_dsp_kyc_relations
    {
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public string relation_name { get; set; }
    }
}
