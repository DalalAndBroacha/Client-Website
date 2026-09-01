using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Shared
{
    [DataContract]
    public class psp_dsp_global_report_date_pills
    {
        [DataMember]
        public string date { get; set; }
        [DataMember]
        public string Q1_date { get; set; }
        [DataMember]
        public string Q1_Flag { get; set; }
        [DataMember]
        public string Q2_date { get; set; }
        [DataMember]
        public string Q2_Flag { get; set; }
        [DataMember]
        public string Q3_date { get; set; }
        [DataMember]
        public string Q3_Flag { get; set; }
        [DataMember]
        public string curr_fy_start_date { get; set; }
        [DataMember]
        public string curr_fy_end_date { get; set; }
        [DataMember]
        public string prev_fy_start_date { get; set; }
        [DataMember]
        public string prev_fy_end_date { get; set; }
    }
}

 								

