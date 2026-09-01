using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Shared
{
    public class psp_dsp_search_client_cp
    {
        [DataMember]
        public string login_id { get; set; }
        public string search_str { get; set; }
        public string family_name { get; set; }
        public string client_name { get; set; }
        public string PAN { get; set; }
        public string branch { get; set; }
        public string RM_name { get; set; }
        public string mask_flag { get; set; }

    }
}
