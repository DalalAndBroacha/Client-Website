using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.IPO
{
    public class BidRequest
    {
        public string scripid { get; set; }
        public string applicationno { get; set; }
        public string category { get; set; }
        public string applicantname { get; set; }
        public string depository { get; set; }
        public string dpid { get; set; }
        public string clientbenfid { get; set; }
        public string chequereceivedflag { get; set; }
        public string chequeamount { get; set; }
        public string panno { get; set; }
        public string bankname { get; set; }
        public string location { get; set; }
        public string accountnumber_upiid { get; set; }
        public string ifsccode { get; set; }
        public string referenceno { get; set; }

        public string asba_upiid { get; set; }

        public List<BidsObj> bids { get; set; }

        public string statuscode { get; set; }
        public string statusmessage { get; set; }
        public string errorcode { get; set; }
        public string errormessage { get; set; }

    }
}
