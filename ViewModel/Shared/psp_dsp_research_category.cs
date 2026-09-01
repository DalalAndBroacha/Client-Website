using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Shared
{
    [DataContract]
    public class psp_dsp_research_category
    {
        [DataMember]
        public string category_id { get; set; }
        [DataMember]
        public string category_name { get; set; }
        [DataMember]
        public string stock_related_flag { get; set; }
    }
}
