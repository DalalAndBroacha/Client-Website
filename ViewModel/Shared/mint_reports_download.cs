using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Shared
{
	public class mint_reports_download
	{
		[DataMember]
		public string ddlClientList { get; set; }
		[DataMember]
		public string ddlReportList { get; set; }
		[DataMember]
		public string ddlFinYearList { get; set; }
		[DataMember]
		public string inpAsOnDatePortSummary { get; set; }
		[DataMember]
		public string inpAsOnDatePortReturn { get; set; }
		[DataMember]
		public string ddlfinYearDetailed { get; set; }

	}
}
