using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Shared
{
    [DataContract]
    public class psp_dsp_family_equity_client_list
    {
        [DataMember]
        public string family_hash { get; set; }
        [DataMember]
        public string flag { get; set; }
        [DataMember]
        public string client_code { get; set; }
        [DataMember]
        public string account_code { get; set; }
        [DataMember]
        public string client_name { get; set; }

    }
}
