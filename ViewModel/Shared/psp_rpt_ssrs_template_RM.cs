using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ViewModel.Shared
{
    [DataContract]
    public class psp_rpt_ssrs_template_RM:dtoBase
    {
        [DataMember]
        public int rm_id { get; set; }
        [DataMember]
        public string rm_login_name { get; set; }
        [DataMember]
        public string Branch { get; set; }
    }
}
