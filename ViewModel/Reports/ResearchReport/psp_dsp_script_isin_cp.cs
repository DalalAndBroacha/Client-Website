using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Reports.ResearchReport
{
    [DataContract]
    public class psp_dsp_script_isin_cp
    {
        [DataMember]
        public string Isin_Code { get; set; }
        [DataMember]
        public string Script_Name { get; set; }
    }
}
