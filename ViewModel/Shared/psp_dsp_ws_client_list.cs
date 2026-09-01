using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Shared
{
    [DataContract]
    public class psp_dsp_ws_client_list
    {
        [DataMember]
        public string LoginId { get; set; }
        [DataMember]
        public string main_client_id { get; set; }
        [DataMember]
        public string ws_client_id { get; set; }
        [DataMember]
        public string client_name { get; set; }
        [DataMember]
        public DateTime maturity_date { get; set; }
    }
}
