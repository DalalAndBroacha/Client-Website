using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Login
{
    [DataContract]
    public class psp_dsp_survey
    {
        [DataMember]
        public string survey_id { get; set; }

        [DataMember]
        public string question_sequnce { get; set; }
        [DataMember]
        public string question { get; set; }
        [DataMember]
        public string sub_question { get; set; }
        [DataMember]
        public string sub_question_sequence { get; set; }
        [DataMember]
        public string display_value { get; set; }
        [DataMember]
        public string control_name { get; set; }

    }
}
				
   