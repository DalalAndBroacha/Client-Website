using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Login
{
    [DataContract]
    public class psp_dsp_survey_status
    {
        [DataMember]
        public string family_id { get; set; }
        [DataMember]
        public string survey_id { get; set; }
        [DataMember]
        public string dsp_flag { get; set; }
        [DataMember]
        public string sql_message { get; set; }
        
    }
}
