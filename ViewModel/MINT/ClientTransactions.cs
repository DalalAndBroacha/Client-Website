using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.MINT
{
	public class ClientTransactions
	{
		public int status { get; set; }
		public string message { get; set; }

		public Transaction[] result { get; set; }
	}
	public class Transaction
	{
		public DateTime navDate { get; set; }
        public string dspNavDate { 
			get {
                return TimeZoneInfo.ConvertTimeFromUtc(this.navDate,
					TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"))
					.ToString("dd-MMM-yy"); }
		}
		public string nav { get; set; }
		public string txnType { get; set; }
		public float amount { get; set; }
		public string folioNo { get; set; }
		public string folioid { get; set; }
		public string schemeName { get; set; }
		public string clientName { get; set; }
		public string title { get; set; }
		public string applicantUid { get; set; }
		public string units { get; set; }
		public string type { get; set; }
		public string txnid { get; set; }
		public string txnNo { get; set; }
		public string seqNo { get; set; }
		public string schid { get; set; }
		public string adjustAmount { get; set; }
		public string TDS { get; set; }
		public string taxStat { get; set; }
		public string transferSchid { get; set; }
		public string userTxnNo { get; set; }
		public string isinNo { get; set; }
		public string arnNo { get; set; }
		public string stampDuty { get; set; }
		public string STT { get; set; }
		public float totalAmount { get; set; }
	}
}