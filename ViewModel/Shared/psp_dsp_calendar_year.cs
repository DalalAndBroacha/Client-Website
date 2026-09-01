using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Shared
{
    [DataContract]
    public class psp_dsp_calendar_year
    {
        [DataMember]
        public string calendar_value { get; set; }
        [DataMember]
        public string calendar_dsp { get; set; }
        [DataMember]
        public string min_cal_date { get; set; }
        [DataMember]
        public string max_cal_date { get; set; }
    }
}
