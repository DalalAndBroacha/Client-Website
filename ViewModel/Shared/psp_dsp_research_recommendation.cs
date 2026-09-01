using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Shared
{
    [DataContract]
    public class psp_dsp_research_recommendation
    {
        [DataMember]
        public string reco_id { get; set; }
        [DataMember]
        public string reco_name { get; set; }
    }
}
