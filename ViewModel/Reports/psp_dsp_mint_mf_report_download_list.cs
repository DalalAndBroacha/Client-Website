using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Reports
{
	[DataContract]
	public class psp_dsp_mint_mf_report_download_list
	{
		[DataMember]
        public string report_id { get; set; }
		[DataMember]
		public string report_name { get; set; }
		[DataMember]
		public string report_params { get; set; }

	}
}
