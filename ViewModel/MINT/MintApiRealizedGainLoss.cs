using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.MINT
{
    public class MintApiRealizedGainLoss
    {
        public int status { get; set; }
        public string message { get; set; }
        public MintPnLResult result { get; set; }
        
    }

    public class MintPnLResult
    {
        public MintPnLDebt debt { get; set; }
        public MintPnLEquity equity { get; set; }
    }

    public class MintPnLDebt
    {
        public MintPnLShorttermDebt shortTerm { get; set; }
        public MintPnLLongtermDebt longTerm { get; set; }
    }
    public class MintPnLEquity
    {
        public MintPnLEquityShortterm shortTerm { get; set; }
        public Longtermbeforejan18 longTermBeforeJan18 { get; set; }
        public Longtermafterjan18 longTermAfterJan18 { get; set; }
        public MintPnLEquityLongtermsummary longTermSummary { get; set; }
    }
    public class MintPnLEquityLongtermsummary
    {
        public float sellAmount { get; set; }
        public float purchaseAmount { get; set; }
        public float adjustedPurchaseAmount { get; set; }
        public float jan18PurchaseAmount { get; set; }
        public float taxableGain { get; set; }
        public float actualGain { get; set; }
        public float STT { get; set; }
    }
    public class Longtermafterjan18
    {
        public Longtermafterjan18Fifo[] fifo { get; set; }
        public Longtermafterjan18Periodwisegain periodWiseGain { get; set; }
        public Longtermafterjan18Summary summary { get; set; }
    }
    public class Longtermafterjan18Summary
    {
        public float sellAmount { get; set; }
        public float purchaseAmount { get; set; }
        public float adjustedPurchaseAmount { get; set; }
        public float jan18PurchaseAmount { get; set; }
        public float taxableGain { get; set; }
        public float actualGain { get; set; }
        public float STT { get; set; }
        public float TDS { get; set; }
    }
    public class Longtermafterjan18Periodwisegain
    {
        public float first { get; set; }
        public float second { get; set; }
        public float third { get; set; }
        public float fourth { get; set; }
        public float fifth { get; set; }
    }

    public class Longtermafterjan18Fifo
    {
        public string folioNo { get; set; }
        public string folioid { get; set; }
        public string schemeName { get; set; }
        public string schid { get; set; }
        public string investorName { get; set; }
        public string isinNo { get; set; }
        public float jan18Nav { get; set; }
        public Longtermafterjan18FifoSummary summary { get; set; }
        public Longtermafterjan18Fifolvl2[] fifo { get; set; }
    }
    public class Longtermafterjan18Fifolvl2
    {
        public string folioid { get; set; }
        public string sellid { get; set; }
        public string purchaseid { get; set; }
        public float strikedUnits { get; set; }
        public DateTime sellNavDate { get; set; }
        public float sellNav { get; set; }
        public DateTime purchaseNavDate { get; set; }
        public float purchaseNav { get; set; }
        public string schemeName { get; set; }
        public string folioNo { get; set; }
        public string investmentType { get; set; }
        public string taxStat { get; set; }
        public string taxStat2 { get; set; }
        public string schid { get; set; }
        public float STT { get; set; }
        public string sellType { get; set; }
        public string purchaseType { get; set; }
        public string isinNo { get; set; }
        public string investorName { get; set; }
        public float adjustAmount { get; set; }
        public object orgFolioid { get; set; }
        public float units { get; set; }
        public float tdsAmount { get; set; }
        public float actualGain { get; set; }
        public float taxableGain { get; set; }
        public float adjustedPurchaseNav { get; set; }
        public float adjustedPurchaseAmount { get; set; }
        public string sellDate { get; set; }
        public string purchaseDate { get; set; }
        public float days { get; set; }
        public float purchaseAmount { get; set; }
        public float sellAmount { get; set; }
        public float jan18Nav { get; set; }
        public float jan18PurchaseAmount { get; set; }
        public float isGrandfathered { get; set; }
        public float adjustedPurchasePrice { get; set; }
    }
    public class Longtermafterjan18FifoSummary
    {
        public float strikedUnits { get; set; }
        public float sellAmount { get; set; }
        public float purchaseAmount { get; set; }
        public float disallowedLoss { get; set; }
        public float actualGain { get; set; }
        public float taxableGain { get; set; }
        public float STT { get; set; }
        public float TDS { get; set; }
        public float adjustedCost { get; set; }
        public float adjustedPurchaseAmount { get; set; }
        public float jan18PurchaseAmount { get; set; }
        public string lastPurchaseDate { get; set; }
        public string lastSellDate { get; set; }
    }
    public class Longtermbeforejan18
    {
        public Longtermafterjan18Fifo[] fifo { get; set; }
        public Longtermbeforejan18Periodwisegain periodWiseGain { get; set; }
        public Longtermbeforejan18Summary summary { get; set; }
    }
    public class Longtermbeforejan18Summary
    {
        public float sellAmount { get; set; }
        public float purchaseAmount { get; set; }
        public float adjustedPurchaseAmount { get; set; }
        public float jan18PurchaseAmount { get; set; }
        public float taxableGain { get; set; }
        public float actualGain { get; set; }
        public float STT { get; set; }
        public float TDS { get; set; }
    }
    public class Longtermbeforejan18Periodwisegain
    {
        public float first { get; set; }
        public float second { get; set; }
        public float third { get; set; }
        public float fourth { get; set; }
        public float fifth { get; set; }
    }
    public class MintPnLEquityShortterm
    {
        public MintPnLEquityShorttermFifo[] fifo { get; set; }
        public MintPnLEquityPeriodwisegain periodWiseGain { get; set; }
        public MintPnLEquityShorttermSummary summary { get; set; }
    }
	public class MintPnLEquityShorttermFifo
	{
		public string folioNo { get; set; }
		public string folioid { get; set; }
		public string schemeName { get; set; }
		public string schid { get; set; }
		public string investorName { get; set; }
		public string isinNo { get; set; }
		public int jan18Nav { get; set; }
		public MintPnLEquityShorttermSummaryL2 summary { get; set; }
		public MintPnLEquityShorttermFifoL2[] fifo { get; set; }
	}
	public class MintPnLEquityShorttermFifoL2
	{
		public string folioid { get; set; }
		public string sellid { get; set; }
		public string purchaseid { get; set; }
		public float strikedUnits { get; set; }
		public DateTime sellNavDate { get; set; }
		public float sellNav { get; set; }
		public DateTime purchaseNavDate { get; set; }
		public float purchaseNav { get; set; }
		public string schemeName { get; set; }
		public string folioNo { get; set; }
		public string investmentType { get; set; }
		public string taxStat { get; set; }
		public string taxStat2 { get; set; }
		public string schid { get; set; }
		public float STT { get; set; }
		public string sellType { get; set; }
		public string purchaseType { get; set; }
		public string isinNo { get; set; }
		public string investorName { get; set; }
		public float adjustAmount { get; set; }
		public object orgFolioid { get; set; }
		public float units { get; set; }
		public int tdsAmount { get; set; }
		public float actualGain { get; set; }
		public string sellDate { get; set; }
		public string purchaseDate { get; set; }
		public int days { get; set; }
		public float purchaseAmount { get; set; }
		public float sellAmount { get; set; }
		public float adjustedPurchaseAmount { get; set; }
		public int disallowedLoss { get; set; }
		public float taxableGain { get; set; }
	}
	public class MintPnLEquityShorttermSummaryL2
	{
		public float strikedUnits { get; set; }
		public float sellAmount { get; set; }
		public float purchaseAmount { get; set; }
		public int disallowedLoss { get; set; }
		public float actualGain { get; set; }
		public float taxableGain { get; set; }
		public float STT { get; set; }
		public int TDS { get; set; }
		public int adjustedCost { get; set; }
		public float adjustedPurchaseAmount { get; set; }
		public int jan18PurchaseAmount { get; set; }
		public string lastPurchaseDate { get; set; }
		public string lastSellDate { get; set; }
	}

	public class MintPnLEquityShorttermSummary
    {
        public float sellAmount { get; set; }
        public float purchaseAmount { get; set; }
        public float disallowedLoss { get; set; }
        public float taxableGain { get; set; }
        public float actualGain { get; set; }
        public float STT { get; set; }
        public float TDS { get; set; }
        public float adjustedCost { get; set; }
    }
    public class MintPnLEquityPeriodwisegain
    {
        public float first { get; set; }
        public float second { get; set; }
        public float third { get; set; }
        public float fourth { get; set; }
        public float fifth { get; set; }
    }
    public class MintPnLShorttermDebt
    {
        public MintPnLShorttermDebtFifo[] fifo { get; set; }
        public MintPnLShorttermDebtPeriodwisegain periodWiseGain { get; set; }
        public MintPnLShorttermDebtSummary2 summary { get; set; }
    }
    public class MintPnLShorttermDebtSummary2
    {
        public float sellAmount { get; set; }
        public float purchaseAmount { get; set; }
        public float disallowedLoss { get; set; }
        public float taxableGain { get; set; }
        public float actualGain { get; set; }
        public float TDS { get; set; }
        public float adjustedCost { get; set; }
    }
    public class MintPnLShorttermDebtPeriodwisegain
    {
        public float first { get; set; }
        public float second { get; set; }
        public float third { get; set; }
        public float fourth { get; set; }
        public float fifth { get; set; }
    }
    public class MintPnLShorttermDebtFifo
    {
        public string folioNo { get; set; }
        public string folioid { get; set; }
        public string schemeName { get; set; }
        public string schid { get; set; }
        public string investorName { get; set; }
        public string isinNo { get; set; }
        public float jan18Nav { get; set; }
        public MintPnLShorttermDebtSummary summary { get; set; }
        public MintPnLShorttermDebtFifolvl2[] fifo { get; set; }
    }

    public class MintPnLShorttermDebtFifolvl2
    {
        public string folioid { get; set; }
        public string sellid { get; set; }
        public string purchaseid { get; set; }
        public float strikedUnits { get; set; }
        public DateTime sellNavDate { get; set; }
        public float sellNav { get; set; }
        public DateTime purchaseNavDate { get; set; }
        public float purchaseNav { get; set; }
        public string schemeName { get; set; }
        public string folioNo { get; set; }
        public string investmentType { get; set; }
        public string taxStat { get; set; }
        public string taxStat2 { get; set; }
        public string schid { get; set; }
        public float STT { get; set; }
        public string sellType { get; set; }
        public string purchaseType { get; set; }
        public string isinNo { get; set; }
        public string investorName { get; set; }
        public float adjustAmount { get; set; }
        public object orgFolioid { get; set; }
        public float units { get; set; }
        public float tdsAmount { get; set; }
        public float actualGain { get; set; }
        public string sellDate { get; set; }
        public string purchaseDate { get; set; }
        public float days { get; set; }
        public float purchaseAmount { get; set; }
        public float sellAmount { get; set; }
        public float adjustedPurchaseAmount { get; set; }
        public float disallowedLoss { get; set; }
        public float taxableGain { get; set; }
    }

    public class MintPnLShorttermDebtSummary
    {
        public float strikedUnits { get; set; }
        public float sellAmount { get; set; }
        public float purchaseAmount { get; set; }
        public float disallowedLoss { get; set; }
        public float actualGain { get; set; }
        public float taxableGain { get; set; }
        public float STT { get; set; }
        public float TDS { get; set; }
        public float adjustedCost { get; set; }
        public float adjustedPurchaseAmount { get; set; }
        public float jan18PurchaseAmount { get; set; }
        public string lastPurchaseDate { get; set; }
        public string lastSellDate { get; set; }
    }

    public class MintPnLLongtermDebt
    {
        public MintPnLLongtermDebtFifo[] fifo { get; set; }
        public MintPnLLongtermDebtPeriodwisegain periodWiseGain { get; set; }
        public MintPnLLongtermDebtSummary summary { get; set; }
    }
    public class MintPnLLongtermDebtSummary
    {
        public float sellAmount { get; set; }
        public float purchaseAmount { get; set; }
        public float adjustedCost { get; set; }
        public float taxableGain { get; set; }
        public float actualGain { get; set; }
        public float TDS { get; set; }
    }
    public class MintPnLLongtermDebtPeriodwisegain
    {
        public float first { get; set; }
        public float second { get; set; }
        public float third { get; set; }
        public float fourth { get; set; }
        public float fifth { get; set; }
    }
    public class MintPnLLongtermDebtFifo
    {
        public string folioNo { get; set; }
        public string folioid { get; set; }
        public string schemeName { get; set; }
        public string schid { get; set; }
        public string investorName { get; set; }
        public string isinNo { get; set; }
        public float jan18Nav { get; set; }
        public MintPnLLongtermDebtFifoSummary summary { get; set; }
        public MintPnLLongtermDebtFifoFifo[] fifo { get; set; }
    }
    public class MintPnLLongtermDebtFifoFifo
    {
        public string folioid { get; set; }
        public string sellid { get; set; }
        public string purchaseid { get; set; }
        public float strikedUnits { get; set; }
        public DateTime sellNavDate { get; set; }
        public float sellNav { get; set; }
        public DateTime purchaseNavDate { get; set; }
        public float purchaseNav { get; set; }
        public string schemeName { get; set; }
        public string folioNo { get; set; }
        public string investmentType { get; set; }
        public string taxStat { get; set; }
        public string taxStat2 { get; set; }
        public string schid { get; set; }
        public float STT { get; set; }
        public string sellType { get; set; }
        public string purchaseType { get; set; }
        public string isinNo { get; set; }
        public string investorName { get; set; }
        public float adjustAmount { get; set; }
        public object orgFolioid { get; set; }
        public float units { get; set; }
        public float tdsAmount { get; set; }
        public float actualGain { get; set; }
        public float taxableGain { get; set; }
        public float adjustedCost { get; set; }
        public string sellDate { get; set; }
        public string purchaseDate { get; set; }
        public float days { get; set; }
        public float purchaseAmount { get; set; }
        public float sellAmount { get; set; }
        public float adjustedPurchasePrice { get; set; }
        public float adjustedPurchaseAmount { get; set; }
    }
    public class MintPnLLongtermDebtFifoSummary
    {
        public float strikedUnits { get; set; }
        public float sellAmount { get; set; }
        public float purchaseAmount { get; set; }
        public float disallowedLoss { get; set; }
        public float actualGain { get; set; }
        public float taxableGain { get; set; }
        public float STT { get; set; }
        public float TDS { get; set; }
        public float adjustedCost { get; set; }
        public float adjustedPurchaseAmount { get; set; }
        public float jan18PurchaseAmount { get; set; }
        public string lastPurchaseDate { get; set; }
        public string lastSellDate { get; set; }
    }

}
