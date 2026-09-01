using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Shared
{
    [DataContract]
    public class psp_dsp_nri_client_category_list
    {
        [DataMember]
        public string rec_id { get; set; }
        [DataMember]
        public string category_name { get; set; }
        
    }
}
