using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ViewModel.Login
{
    [DataContract]
    public class psp_dsp_survey_respnse_dump
    {
        [DataMember]
        public string survey_id { get; set; }
        [DataMember]
        public string login_id { get; set; }
        [DataMember]
        public string flag { get; set; }
        [DataMember]
        public string family_id { get; set; }
        [DataMember]
        public string family_name { get; set; }
        [DataMember]
        public string Branch_name { get; set; }
        [DataMember]
        public string rm_name { get; set; }
        [DataMember]
        public string filled_by { get; set; }
        [DataMember]
        public string filled_on { get; set; }
        [DataMember]
        public string current_week { get; set; }
        [DataMember]
        public string request_status { get; set; }
        [DataMember]
        public string Q1 { get; set; }
        [DataMember]
        public string Q2 { get; set; }
        [DataMember]
        public string Q3 { get; set; }
        [DataMember]
        public string Q4 { get; set; }
        [DataMember]
        public string Q5 { get; set; }
        [DataMember]
        public string Q6 { get; set; }
        [DataMember]
        public string Q7 { get; set; }
        [DataMember]
        public string Q8 { get; set; }
        [DataMember]
        public string Q9 { get; set; }
        [DataMember]
        public string Q10 { get; set; }
        [DataMember]
        public string Q11 { get; set; }
        [DataMember]
        public string Q12 { get; set; }
        [DataMember]
        public string Q13 { get; set; }
        [DataMember]
        public string Q14 { get; set; }
        [DataMember]
        public string Q15 { get; set; }
        [DataMember]
        public string Q16 { get; set; }
        [DataMember]
        public string Q17 { get; set; }
        [DataMember]
        public string Q18 { get; set; }
        [DataMember]
        public string Q19 { get; set; }
        [DataMember]
        public string Q20 { get; set; }
    }
}

