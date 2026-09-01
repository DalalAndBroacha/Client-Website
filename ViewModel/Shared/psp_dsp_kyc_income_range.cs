using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ViewModel.Shared
{
    [DataContract]
    public class psp_dsp_kyc_income_range
    {
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public string display_value { get; set; }
    }
}
