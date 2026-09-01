using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Shared
{
    [DataContract]
    public class psp_dsp_client_list
    {
        [DataMember]
        public string family_id { get; set; }

        [DataMember]
        public string main_client_id { get; set; }
        [DataMember]
        public string main_client_guid { get; set; }
        [DataMember]
        public string main_client_name { get; set; }
    }
}
