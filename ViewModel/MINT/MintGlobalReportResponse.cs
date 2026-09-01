using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.MINT
{
    public class MintGlobalReportResponse
    {
        public string PortfolioReturnData { get; set; }
        public string TransactionData { get; set; }
        public string DiviData { get; set; }

        public string status { get; set; }
        public string message { get; set; }
    }
}
