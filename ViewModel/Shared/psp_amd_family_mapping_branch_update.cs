using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Shared
{
    [DataContract]
    public class psp_amd_family_mapping_branch_update
    {
        [DataMember]
        public string login_id { get; set; }
        [DataMember]
        public string family_token { get; set; }
        
        [DataMember]
        public string new_branch_id { get; set; }

        [DataMember]
        public string sql_msg { get; set; }
    }
}
