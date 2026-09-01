using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Login
{
    [DataContract]
    public class psp_amd_survey_response
    {
        [DataMember]
        public string response { get; set; }
        [DataMember]
        public string login_id { get; set; }
        [DataMember]
        public string survey_id { get; set; }
        [DataMember]
        public string family_id { get; set; }
        [DataMember]
        public string amd_user { get; set; }
        [DataMember]
        public string sql_message { get; set; }
        
    }
}
