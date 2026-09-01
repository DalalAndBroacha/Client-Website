using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ViewModel.Shared
{
    [DataContract]
    public class psp_dsp_client_portal_assets
    {
        [DataMember]
        public int asset_id { get; set; }
        [DataMember]
        public string asset_name { get; set; }
    }
}
