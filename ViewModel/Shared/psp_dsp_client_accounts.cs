using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Shared
{
    [DataContract]
    public class psp_dsp_client_accounts
    {
        [DataMember]
        public string family_id { get; set; }
        [DataMember]
        public string asset_class { get; set; }
        [DataMember]
        public string pan_no { get; set; }
        [DataMember]
        public string level { get; set; }
        
        [DataMember]
        public string main_client_id { get; set; }
        [DataMember]
        public string main_client_name { get; set; }
        [DataMember]
        public string client_name { get; set; }
        [DataMember]
        public string asset_name { get; set; }

        [DataMember]
        public string account_code { get; set; }
		[DataMember]
		public string mint_clientname { get; set; }

		
	}
}
