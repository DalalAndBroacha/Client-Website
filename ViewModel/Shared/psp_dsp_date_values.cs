using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Shared
{
    [DataContract]
    public class psp_dsp_date_values
    {
        [DataMember]
        public string date { get; set; }
        [DataMember]
        public string cal_year { get; set; }

        [DataMember]
        public string fy_year { get; set; }
        [DataMember]
        public string fy_start_date { get; set; }
        [DataMember]
        public string fy_end_date { get; set; }
        [DataMember]
        public string cal_start_date { get; set; }
        [DataMember]
        public string cal_end_date { get; set; }
    }
}
