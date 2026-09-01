using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Reports
{
    [DataContract]
    public class psp_rpt_rm_details:dtoBase
    {

        [DataMember]
        public string rm_name { get; set; }
        [DataMember]
        public string family_name { get; set; }
        [DataMember]
        public string client_name { get; set; }
        [DataMember]
        public string pan_number { get; set; }
        [DataMember]
        public string account_code { get; set; }
        [DataMember]
        public string Asset_Name { get; set; }
        [DataMember]
        public string Branch { get; set; }
        [DataMember]
        public string RM { get; set; }
        [DataMember]
        public string Asset { get; set; }


    }
}
