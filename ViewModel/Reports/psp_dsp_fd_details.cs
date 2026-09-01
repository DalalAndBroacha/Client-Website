using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Reports
{
	public class psp_dsp_fd_details
	{
		[DataMember]
		public DateTime from_date { get; set; }
		[DataMember]
		public DateTime To_date { get; set; }
		[DataMember]
		public int rec_id { get; set; }
		[DataMember]
		public string FamilyID { get; set; }

		[DataMember]
		public string main_client_id { get; set; }
		[DataMember]
		public string client_name { get; set; }
		[DataMember]
		public string FINYRData { get; set; }
		[DataMember]
		public string pan_no { get; set; }
		[DataMember]
		public string IssuerName { get; set; }

		[DataMember]
		public string SchemeName { get; set; }

		public string AccSchemeName { get { return this.SchemeName.Replace(" ", ""); } }

		[DataMember]
		public DateTime FdApplicationDate { get; set; }
		[DataMember]
		public string FDCouponType { get; set; }
		[DataMember]
		public string FDInterestFrequency { get; set; }
		[DataMember]
		public DateTime Maturity_Date { get; set; }
		[DataMember]
		public DateTime cash_flow_date { get; set; }
		[DataMember]
		public string cash_flow_type { get; set; }
		[DataMember]
		public float? Amount { get; set; }
		[DataMember]
		public float? FDInterestRate_perc { get; set; }
		[DataMember]
		public float? cash_flow_amount { get; set; }
	}
}
