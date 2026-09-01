using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.IPO
{
    public class psp_dsp_ipo_clients_list
    {
        [DataMember]
        public string family_token { get; set; }
        [DataMember]
        public string dsp_client_name { get; set; }
        [DataMember]
        public string boid { get; set; }
        [DataMember]
        public string main_client_id { get; set; }

        
    }
}
