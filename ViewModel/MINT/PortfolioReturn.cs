using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.MINT
{
	public class PortfolioReturn
	{
		public int status { get; set; }
		public string message { get; set; }
		public PortfolioReturnSchemeResult result { get; set; }
	}
	public class PortfolioReturnSchemeResult
    {
		public PortfolioReturnScheme[] data { get; set; }
	}
	public class PortfolioReturnScheme
    {
		public float changePercent { get; set; }
		public string pan { get; set; }
		public string txnType { get; set; }
		public DateTime purchaseDate { get; set; }
		public float currentNav { get; set; }
		public string fundid { get; set; }
		public string fundName { get; set; }
		public string folioid { get; set; }
		public string appid { get; set; }
		//public string applicantUid { get; set; }
		public string applicantName { get; set; }
		public string objectiveName { get; set; }
		public string dueDiligence { get; set; }
		public string objectiveid { get; set; }
		public string schemeName { get; set; }
		public int viewPriority { get; set; }
		public string isinNo { get; set; }
		public string category2 { get; set; }
		public string schid { get; set; }
		public string folioNo { get; set; }
		public object comments { get; set; }
		public string arnNo { get; set; }
		public object matDate { get; set; }
		public float nav { get; set; }
		public float purchaseValue { get; set; }
		public float currentValue { get; set; }
		public float balanceUnits { get; set; }
		public float dividend { get; set; }
		public float avgHoldingDays { get; set; }
		public float gain { get; set; }
		public string uccNumber { get; set; }
		public string lastTxnArnid { get; set; }
		public string iinNo { get; set; }
		public string canNumber { get; set; }
		public DateTime currentNavDate { get; set; }
        public string dspCurrentNavDate
        {
            get
            {
                return TimeZoneInfo.ConvertTimeFromUtc(this.currentNavDate,
                    TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"))
                    .ToString("yyyy-MM-dd");
            }
        }
        public string portfolioObjective { get; set; }
		public string dematAccount { get; set; }
		public string uid { get; set; }
		public int levelNo { get; set; }
		public int isOutsideData { get; set; }
		public float avgCost { get; set; }
		public float CAGR { get; set; }
		public float currentAmount { get; set; }
		public float absoluteReturn { get; set; }
		public float oneDayChange { get; set; }
		public float share { get; set; }
	}
	public class ResponsePortfolioReturnSubCategory
	{
		public int status { get; set; }
		public string message { get; set; }
		public SubCategoryResult result { get; set; }

	}
	public class SubCategoryResult
	{
		public SubCategorydata[] data { get; set; }
	}
	public class SubCategorydata
	{
		public float changePercent { get; set; }
		public string pan { get; set; }
		public string txnType { get; set; }
		public DateTime purchaseDate { get; set; }
		public float currentNav { get; set; }
		public string fundid { get; set; }
		public string fundName { get; set; }
		public string folioid { get; set; }
		public string appid { get; set; }
		//public string applicantUid { get; set; }
		public string applicantName { get; set; }
		public string objectiveName { get; set; }
		public string dueDiligence { get; set; }
		public string objectiveid { get; set; }
		public string schemeName { get; set; }
		public int viewPriority { get; set; }
		public string isinNo { get; set; }
		public string category2 { get; set; }
		public string schid { get; set; }
		public string folioNo { get; set; }
		public object comments { get; set; }
		public string arnNo { get; set; }
		public object matDate { get; set; }
		public float nav { get; set; }
		public float purchaseValue { get; set; }
		public float currentValue { get; set; }
		public float balanceUnits { get; set; }
		public float dividend { get; set; }
		public float avgHoldingDays { get; set; }
		public float gain { get; set; }
		public string uccNumber { get; set; }
		public string lastTxnArnid { get; set; }
		public string iinNo { get; set; }
		public string canNumber { get; set; }
		public DateTime currentNavDate { get; set; }
		public string portfolioObjective { get; set; }
		public string dematAccount { get; set; }
		public string uid { get; set; }
		public int levelNo { get; set; }
		public int isOutsideData { get; set; }
		public float avgCost { get; set; }
		public float CAGR { get; set; }
		public float currentAmount { get; set; }
		public float absoluteReturn { get; set; }
		public float oneDayChange { get; set; }
		public float share { get; set; }
	}
}
