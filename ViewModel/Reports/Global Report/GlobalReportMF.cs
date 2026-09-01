using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Reports.Global_Report
{
	[DataContract]
	public class GlobalReportMF
	{
		[DataMember]
		public string main_client_id { get; set; }
		[DataMember]
		public string FINYR { get; set; }
		[DataMember]
		public string to_date { get; set; }
		[DataMember]
		public string from_date { get; set; }
		[DataMember]
		public string showTrades { get; set; }
		[DataMember]
		public string showHolding { get; set; }
        [DataMember]
        public string showPnL { get; set; }
        [DataMember]
		public string showDivi { get; set; }

	}
}
