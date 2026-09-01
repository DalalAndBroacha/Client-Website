using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PieReports.Encryption_Decryption;
using PieReports.Models;
using System;
using System.Web;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ViewModel.Login;
using ViewModel.Shared;
using ViewModel.IPO;
using ViewModel.Reports.Holding;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;

namespace PieReports.Controllers
{
    public class IPOController : Controller
    {
        private IConfiguration _config;

        private static readonly string generalAPIBaseUrl = "https://ibbs.bseindia.com/ibbsmsgapi/iBBSWebBroadcastApi.svc/";

        private static string Simulation = "https://uat.bseindia.in/ibbsmsgapi/iBBSWebBroadcastApi.svc/";
        private static readonly string biddingAPIBaseUrl = "https://IBBSAPI.BSEINDIA.COM/IBBSAPI/IBBSAPISERVICE.SVC/";

        string LoginUrl = generalAPIBaseUrl + "v1/login";


        public IPOController(IConfiguration config)
        {
            _config = config;
        }


        public List<OpenIPOIssues> IPOData()
        {
            string url = _config.GetValue<string>("APIKey") + "IPO/psp_dsp_ipo_openissue";

            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = "";
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<OpenIPOIssues> Result = JsonConvert.DeserializeObject<List<OpenIPOIssues>>(data1);

            if (Result != null)
            {
                TempData["liveIPOData"] = data1;
            }

            return Result;
        }


        public IActionResult Index()
        {
            List<OpenIPOIssues> Result = IPOData();

            return View(Result);
        }

        
        [HttpPost]
        public IActionResult psp_dsp_ipo_clients_list([FromBody] psp_dsp_ipo_clients_list rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "IPO/psp_dsp_ipo_clients_list";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_ipo_clients_list> Result = JsonConvert.DeserializeObject<List<psp_dsp_ipo_clients_list>>(data1);
            return Json(Result);
        }

        [HttpPost]
        public IActionResult GetIPODetails([FromBody] OpenIPOIssues rpt)
        {

            string IpoData = TempData["liveIPOData"].ToString();
            TempData.Keep("liveIPOData");

            List<OpenIPOIssues> TempIPOData = JsonConvert.DeserializeObject<List<OpenIPOIssues>>(IpoData);

            if(TempIPOData != null)
            {
                OpenIPOIssues selectedIPO = TempIPOData.Find(x => x.isin == rpt.isin);

                return Json(selectedIPO);
            }
            else
            {
                string url = _config.GetValue<string>("APIKey") + "IPO/psp_dsp_ipo_clients_list";

                string serializeProfile = JsonConvert.SerializeObject(rpt);
                string data = EncryptionDecryption.Encrypt(serializeProfile);
                EncryptData Endata = new EncryptData();
                Endata.EncryptObject = data;
                string data1 = HttpCall.HttpPostMethod(url, Endata);
                List<OpenIPOIssues> Result = JsonConvert.DeserializeObject<List<OpenIPOIssues>>(data1);

                OpenIPOIssues selectedIPO = Result.Find(x => x.isin == rpt.isin);

                return Json(selectedIPO);
            }   
        }

        [HttpPost]
        public IActionResult psp_amd_ipo_requests([FromBody] psp_amd_ipo_requests rpt)
        {

            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            UserLogin udata = JsonConvert.DeserializeObject<UserLogin>(dtoq);

            rpt.login_id = udata.Id.ToString();

            //OTP, applicationno & orderno Generation

            rpt.otp = Captcha.GenerateOTPCode("123467890", 6);
            rpt.otp_encrypt = EncryptionDecryption.EncryptMD5(rpt.otp);

            rpt.applicationno = rpt.symbol.Substring(0,3) + Captcha.GenerateOTPCode("ABCDEFGHIJKLMNOPQRSTUVWXYZ", 3) + DateTime.Now.Day.ToString()
                                + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
            rpt.orderno = rpt.applicationno;

            //OTP, applicationno & orderno Generation

            string url = _config.GetValue<string>("APIKey") + "IPO/psp_amd_ipo_requests";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            psp_amd_ipo_requests Result = JsonConvert.DeserializeObject<psp_amd_ipo_requests>(data1);

            return Json(Result);
        }

        [HttpPost]
        public IActionResult psp_verify_ipo_otp([FromBody] psp_verify_ipo_otp rpt)
        {
            string userOTP = EncryptionDecryption.EncryptMD5(rpt.otp);

            string url = _config.GetValue<string>("APIKey") + "IPO/psp_verify_ipo_otp";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            psp_amd_ipo_requests Result = JsonConvert.DeserializeObject<psp_amd_ipo_requests>(data1);

            psp_amd_ipo_requests Result2 = new psp_amd_ipo_requests();
            EncryptData Endata2 = new EncryptData();
            string data3;

            if (Result.otp_encrypt == userOTP)
            {
                rpt.flag = "S";

                //Result.bid_actioncode = "N"; //N: -NEW,M: -MODIFY,D: -CANCEL For API
                Result.bid_actioncode = rpt.bid_actioncode;
                
                BidRequest bidresobjData = submitBid(Result);

                rpt.ibbs_remarks = bidresobjData.bids[0].message;

                string serializeProfile2 = JsonConvert.SerializeObject(rpt);
                string data2 = EncryptionDecryption.Encrypt(serializeProfile2);
                Endata.EncryptObject = data2;
                data3 = HttpCall.HttpPostMethod(url, Endata);
                Result2 = JsonConvert.DeserializeObject<psp_amd_ipo_requests>(data3);

                Result2.sql_msg = bidresobjData.bids[0].message;
            }
            else
            {
                rpt.flag = "F";
                string serializeProfile2 = JsonConvert.SerializeObject(rpt);
                string data2 = EncryptionDecryption.Encrypt(serializeProfile2);

                Endata.EncryptObject = data2;
                data3 = HttpCall.HttpPostMethod(url, Endata);
                Result2 = JsonConvert.DeserializeObject<psp_amd_ipo_requests>(data3);
            }

            return Json(Result2);
        }

        private BidRequest submitBid(psp_amd_ipo_requests dataObj)
        {
            IPOCreds objIpo = new IPOCreds();

            objIpo.membercode = "162";
            objIpo.loginid = "IPO1";
            objIpo.password = "BSe@3849";
            objIpo.ibbsid = "2FEMTDLO4Y";

            string bidLoginURL = biddingAPIBaseUrl + "v1/login";
            string bidURL = biddingAPIBaseUrl + "v1/Ipoorder";

            HttpClient objHttpBid = new HttpClient();
            objHttpBid.BaseAddress = new Uri(bidLoginURL);

            objHttpBid.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var bidLoginReq = JsonConvert.SerializeObject(objIpo);
            var bidLogincontent = new StringContent(bidLoginReq, Encoding.UTF8, "application/json");
            var bidLoginResponse = objHttpBid.PostAsync(bidLoginURL, bidLogincontent).Result;

            string bidLoginjsonString = bidLoginResponse.Content.ReadAsStringAsync().Result.ToString();
            IPOCreds bidLoginObjData = JsonConvert.DeserializeObject<IPOCreds>(bidLoginjsonString);


            objHttpBid.DefaultRequestHeaders.Add("Membercode", bidLoginObjData.membercode);
            objHttpBid.DefaultRequestHeaders.Add("Login", bidLoginObjData.loginid);
            objHttpBid.DefaultRequestHeaders.Add("Token", bidLoginObjData.token);

            BidRequest BidReqObj = new BidRequest();
            
            BidReqObj.scripid = dataObj.symbol;
            BidReqObj.applicationno = dataObj.applicationno; //Needs further discussion
            BidReqObj.category = "ind"; //Hard Code
            //BidReqObj.applicantname = "Kinjal Devang Doshi";
            BidReqObj.depository = "CDSL"; //Hard Coded as per API Document
            BidReqObj.dpid = "0"; //Hard Coded as per API Document
            BidReqObj.clientbenfid = dataObj.dp_id;
            BidReqObj.chequereceivedflag = "Y"; //Hard Coded as per API Document
            BidReqObj.chequeamount = Math.Round(Convert.ToDecimal(dataObj.bid_ttl_amount), 2).ToString();
            BidReqObj.panno = dataObj.pan_number;
            BidReqObj.bankname = "8888"; //Hard Coded as per API Document
            BidReqObj.location = "upiidl"; //Hard Coded as per API Document
            BidReqObj.accountnumber_upiid = "devanggdoshi@okaxis";
            BidReqObj.ifsccode = ""; //Hard Coded as per API Document
            BidReqObj.referenceno = "12345"; //Needs further discussion
            BidReqObj.asba_upiid = "1"; //Hard Coded as per API Document

            BidsObj bid1 = new BidsObj();
            bid1.bidid = ""; //VALID BID ID GENERATED BY THE IBBS  0 for New
            bid1.quantity = Math.Round(Convert.ToDecimal(dataObj.bid_qty), 2).ToString();
            bid1.rate = Math.Round(Convert.ToDecimal(dataObj.bid_price), 2).ToString();
            bid1.cuttoffflag = dataObj.bid_cutoff == "Y" ? "1" : "0"; //CONTAINS 0 OR 1, “1” FOR CUT OFF ELSE MENTION 0 FOR PRICE BID
            bid1.orderno = dataObj.orderno; //***Needs further discussion*** VENDOR’S UNIQUE ID 
            bid1.actioncode = dataObj.bid_actioncode; //N: -NEW,M: -MODIFY,D: -CANCEL

            List<BidsObj> bidList = new List<BidsObj>();
            bidList.Add(bid1);
            BidReqObj.bids = bidList;

            /*
            BidReqObj.scripid = "BHARTIHEXA";
            BidReqObj.applicationno = "test2"; //Hard Code
            BidReqObj.category = "ind"; //Hard Code
            BidReqObj.applicantname = "Kinjal Devang Doshi";
            BidReqObj.depository = "CDSL"; //Hard Code
            BidReqObj.dpid = "0"; //Hard Code
            BidReqObj.clientbenfid = "1201170000191394";
            BidReqObj.chequereceivedflag = "Y"; //Hard Code
            BidReqObj.chequeamount = "14820";
            BidReqObj.panno = "AJLPS4646Q";
            BidReqObj.bankname = "8888"; //Hard Code
            BidReqObj.location = "upiidl"; //Hard Code
            BidReqObj.accountnumber_upiid = "devanggdoshi-2@okaxis";
            BidReqObj.ifsccode = ""; //Hard Code
            BidReqObj.referenceno = "12345"; //Hard Code
            BidReqObj.asba_upiid =  "1"; //Hard Code

            BidsObj bid1 = new BidsObj();
            bid1.bidid = ""; //VALID BID ID GENERATED BY THE IBBS  Blank for New
            bid1.quantity = "52";
            bid1.rate = "570";
            bid1.cuttoffflag = "1"; //CONTAINS 0 OR 1, “1” FOR CUT OFF ELSE MENTION 0 FOR PRICE BID
            bid1.orderno = "test2"; //VENDOR’S UNIQUE ID
            bid1.actioncode = dataObj.bid_actioncode; //N: -NEW,M: -MODIFY,D: -CANCEL

            List<BidsObj> bidList = new List<BidsObj>();
            bidList.Add(bid1);
            BidReqObj.bids = bidList;
            */
            var bidrequest = JsonConvert.SerializeObject(BidReqObj);
            var bidcontent = new StringContent(bidrequest, Encoding.UTF8, "application/json");
            var bidResponse = objHttpBid.PostAsync(bidURL, bidcontent).Result;

            string bidjsonString = bidResponse.Content.ReadAsStringAsync().Result.ToString();
            BidRequest bidresobjData = JsonConvert.DeserializeObject<BidRequest>(bidjsonString);

            return bidresobjData;
        }
    }
}


//BidReqObj.scripid = "khazanchi";
//BidReqObj.applicationno = "appno1"; Hard Code
//BidReqObj.category = "ind"; Hard Code
//BidReqObj.applicantname = "Kinjal Devang Doshi";
//BidReqObj.depository = "CDSL"; Hard Code
//BidReqObj.dpid = "0"; Hard Code
//BidReqObj.clientbenfid = "1201170000191394";
//BidReqObj.chequereceivedflag = "Y"; Hard Code
//BidReqObj.chequeamount = "14000";
//BidReqObj.panno = "AJLPS4646Q";
//BidReqObj.bankname = "8888"; Hard Code
//BidReqObj.location = "upiidl"; Hard Code
//BidReqObj.accountnumber_upiid = "devanggdoshi@okaxis";
//BidReqObj.ifsccode = ""; Hard Code
//BidReqObj.referenceno = "12345"; Hard Code
//BidReqObj.asba_upiid = "1"; Hard Code

//BidsObj bid1 = new BidsObj();
//bid1.bidid = "0"; VALID BID ID GENERATED BY THE IBBS  0 for New
//bid1.quantity = "1000";
//bid1.rate = "140";
//bid1.cuttoffflag = "1"; CONTAINS 0 OR 1, “1” FOR CUT OFF ELSE MENTION 0 FOR PRICE BID
//bid1.orderno = "1234"; VENDOR’S UNIQUE ID
//bid1.actioncode = "N"; N:- NEW,M:- MODIFY,D:-CANCEL

//List<BidsObj> bidList = new List<BidsObj>();
//bidList.Add(bid1);

//BidReqObj.bids = bidList;

//var bidrequest = JsonConvert.SerializeObject(BidReqObj);
//var bidcontent = new StringContent(bidrequest, Encoding.UTF8, "application/json");
//var bidResponse = objHttpBid.PostAsync(bidURL, bidcontent).Result;

//string bidjsonString = bidResponse.Content.ReadAsStringAsync().Result.ToString();
//BidRequest bidresobjData = JsonConvert.DeserializeObject<BidRequest>(bidjsonString);