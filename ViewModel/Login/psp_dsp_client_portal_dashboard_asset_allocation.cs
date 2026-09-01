using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ViewModel.Login
{
    [DataContract]
    public class psp_dsp_client_portal_dashboard_asset_allocation
    {
        [DataMember]
        public string family_token { get; set; }
        [DataMember]
        public string flag { get; set; }
        [DataMember]
        public string category { get; set; }
        [DataMember]
        public string display_order { get; set; }
        [DataMember]
        public string asset_name { get; set; }
        [DataMember]
        public float holding_cost { get; set; }
        [DataMember]
        public float market_cost { get; set; }

        [DataMember]
        public float mkt_per { get; set; }
        [DataMember]
        public string client_name { get; set; }
    }
}
