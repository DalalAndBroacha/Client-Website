using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Shared
{
    [DataContract]
    public class PspAmdFamilyMappingRmUpdate
    {
        [DataMember]
        public string login_id { get; set; }
        [DataMember]
        public string family_token { get; set; }

        [DataMember]
        public string rm_id { get; set; }
        [DataMember]
        public string amd_flag { get; set; }

        [DataMember]
        public string sql_msg { get; set; }
    }
}
