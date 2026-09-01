using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Shared
{
    [DataContract]
    public class psp_dsp_nri_client_list
    {
        [DataMember]
        public string country { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string client_id { get; set; }
    }
     
}
