using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Reports.PMS
{
    [DataContract]
    public class psp_dsp_AIF_scheme
    {
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public string scheme_name { get; set; }
    }
}
