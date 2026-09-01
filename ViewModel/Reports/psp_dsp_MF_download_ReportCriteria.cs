using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Reports
{
	[DataContract]
	public class psp_dsp_mint_mf_download_ReportCriteria
	{
		[DataMember]
		public string report_id { get; set; }
		[DataMember]
		public string param_label { get; set; }
		[DataMember]
		public string param_type { get; set; }
		[DataMember]
		public string param_grp { get; set; }
		[DataMember]
		public string active_flag { get; set; }
		[DataMember]
		public string dsp_flag { get; set; }
		[DataMember]
		public string default_val { get; set; }
		[DataMember]
		public string param_id { get; set; }
	}
}
