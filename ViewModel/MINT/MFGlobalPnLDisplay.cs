using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.MINT
{
	public class MFGlobalPnLDisplay
	{
		public float units { get; set; }
		public string folioNo { get; set; }
		public string isinNo { get; set; }
		public string investorName { get; set; }

		public string schemeName { get; set; }
		public string purchaseDate { get; set; }
		public float purchaseAmount { get; set; }

		public float sellAmount { get; set; }
		public string sellDate { get; set; }

		public float actualGain { get; set; }
        public float jan18Nav { get; set; }
        public float days { get; set; }

        public float jan18PurchaseAmount { get; set; }
        public float isGrandfathered { get; set; }
        public float adjustedPurchasePrice { get; set; }

        public float taxableGain { get; set; }
        public float adjustedPurchaseNav { get; set; }
        public float adjustedPurchaseAmount { get; set; }


    }
}
