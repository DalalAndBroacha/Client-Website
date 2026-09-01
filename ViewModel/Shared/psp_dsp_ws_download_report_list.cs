using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Shared
{
    [DataContract]
    public class psp_dsp_ws_download_report_list
    {
        [DataMember]
        public string report_id { get; set; }
        [DataMember]
        public string report_name { get; set; }
    }
}
