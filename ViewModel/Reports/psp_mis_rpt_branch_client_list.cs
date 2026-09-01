using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Reports
{
    [DataContract]
    public class psp_mis_rpt_branch_client_list:dtoBase
    {

        [DataMember]
        public string Branch { get; set; }
        [DataMember]
        public string branch_id { get; set; }
        [DataMember]
        public string branch_name { get; set; }

        [DataMember]
        public string brhead_name { get; set; }
        [DataMember]
        public string rm_id { get; set; }
        [DataMember]
        public string rm_name { get; set; }
        [DataMember]
        public string family_id { get; set; }
        [DataMember]
        public string family_login_id { get; set; }
        [DataMember]
        public string family_name { get; set; }
        [DataMember]
        public string email_id { get; set; }
        [DataMember]
        public string contact_no { get; set; }
         
    }
}
