using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ViewModel.Login
{
    [DataContract]
    public class psp_dsp_survey_family_list
    {
        [DataMember]
        public string login_id { get; set; }
        [DataMember]
        public string survey_id { get; set; }
        [DataMember]
        public string Family_Id { get; set; }

        [DataMember]
        public string Family_Name { get; set; }
        [DataMember]
        public string survey_flag { get; set; }
    }
}
