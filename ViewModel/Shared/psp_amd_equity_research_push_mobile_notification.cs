using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Shared
{
    public class psp_amd_equity_research_push_mobile_notification
    {
        [DataMember]
        public string Login_Id { get; set; }
        [DataMember]
        public string rec_id { get; set; }
        [DataMember]
        public string sql_msg { get; set; }
    }
}
