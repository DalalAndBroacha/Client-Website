using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Login
{
    [DataContract]
    public class psp_dsp_survey_list
    {
        [DataMember]
        public string survey_id { get; set; }

        [DataMember]
        public string survey_name { get; set; }
    }
}
