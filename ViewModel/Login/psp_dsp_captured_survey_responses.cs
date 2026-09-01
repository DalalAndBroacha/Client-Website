using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ViewModel.Login
{
    [DataContract]
    public class psp_dsp_captured_survey_responses
    {
        [DataMember]
        public string family_id { get; set; }
        [DataMember]
        public string survey_id { get; set; }
        [DataMember]
        public string sequence { get; set; }
        [DataMember]
        public string question { get; set; }
        [DataMember]
        public string response { get; set; }
        [DataMember]
        public string filled_by_name { get; set; }
        [DataMember]
        public string rm_name { get; set; }
        [DataMember]
        public string branch_name { get; set; }
        [DataMember]
        public string survey_filled_date { get; set; }


    }
}
