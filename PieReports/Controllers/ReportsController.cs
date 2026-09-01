using DocumentFormat.OpenXml.Math;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PieReports.Encryption_Decryption;
using PieReports.Filters;
using PieReports.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics; // To use Trace.
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices; // To use [CallerX] attributes
using System.Text;
using System.Web;
using ViewModel.Login;
using ViewModel.MINT;
using ViewModel.Reports;
using ViewModel.Reports.Global_Report;
using ViewModel.Reports.Holding;
using ViewModel.Reports.IncomeReports;
using ViewModel.Reports.Liquiloan;
using ViewModel.Reports.PMS;
using ViewModel.Reports.ResearchReport;
using ViewModel.Shared;

namespace PieReports.Controllers
{
    [Controller]
    [ServiceFilter(typeof(SessionTimeoutFilter))]
    public class ReportsController : Controller
    {
        private IConfiguration _config;

        public ReportsController(IConfiguration config)
        {
            _config = config;
		}
        /// <summary>
        /// Gets file for Report Controller
        /// </summary>
        /// <param name="FP">File Path</param>
        /// <returns>Redirects to File</returns>
        public IActionResult getDocument(string FP = null)
        {
            FP = _config.GetValue<string>("DocPath") + FP;

            return Redirect(FP);
        }
        public IActionResult CommonDocument(RPT dto)
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_user_access_token";

            psp_dsp_user_access_token postdata = new psp_dsp_user_access_token();

            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");

            UserLogin udata  = JsonConvert.DeserializeObject<UserLogin>(dtoq);

            postdata.login_id = udata.Id;
            postdata.web_session_id = HttpContext.Session.GetString("web_session_id");

            bool validSession = EncryptionDecryption.AccessToken(postdata, url);

            if (HttpContext.Session.GetString("web_session_id") != null && validSession)
            {
                dto = (RPT)CommonDocuments(dto);
                return View(dto);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        public IActionResult DividendDashboard()
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_user_access_token";

            psp_dsp_user_access_token postdata = new psp_dsp_user_access_token();

            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            UserLogin udata = JsonConvert.DeserializeObject<UserLogin>(dtoq);

            postdata.login_id = udata.Id;
            postdata.web_session_id = HttpContext.Session.GetString("web_session_id");

            bool validSession = EncryptionDecryption.AccessToken(postdata, url);

            if (HttpContext.Session.GetString("web_session_id") != null && validSession)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        public IActionResult ScriptwiseClientHolding()
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_user_access_token";

            psp_dsp_user_access_token postdata = new psp_dsp_user_access_token();

            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            UserLogin udata = JsonConvert.DeserializeObject<UserLogin>(dtoq);

            postdata.login_id = udata.Id;
            postdata.web_session_id = HttpContext.Session.GetString("web_session_id");

            bool validSession = EncryptionDecryption.AccessToken(postdata, url);

            if (HttpContext.Session.GetString("web_session_id") != null && validSession)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        public IActionResult MISFamilyDetails()
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_user_access_token";

            psp_dsp_user_access_token postdata = new psp_dsp_user_access_token();

            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            UserLogin udata = JsonConvert.DeserializeObject<UserLogin>(dtoq);

            postdata.login_id = udata.Id;
            postdata.web_session_id = HttpContext.Session.GetString("web_session_id");

            bool validSession = EncryptionDecryption.AccessToken(postdata, url);

            if (HttpContext.Session.GetString("web_session_id") != null && validSession)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        public IActionResult MISRMDetails()
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_user_access_token";

            psp_dsp_user_access_token postdata = new psp_dsp_user_access_token();

            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            UserLogin udata = JsonConvert.DeserializeObject<UserLogin>(dtoq);

            postdata.login_id = udata.Id;
            postdata.web_session_id = HttpContext.Session.GetString("web_session_id");

            bool validSession = EncryptionDecryption.AccessToken(postdata, url);

            if (HttpContext.Session.GetString("web_session_id") != null && validSession)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        public IActionResult AUMBreakup()
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_user_access_token";

            psp_dsp_user_access_token postdata = new psp_dsp_user_access_token();

            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            UserLogin udata = JsonConvert.DeserializeObject<UserLogin>(dtoq);

            postdata.login_id = udata.Id;
            postdata.web_session_id = HttpContext.Session.GetString("web_session_id");

            bool validSession = EncryptionDecryption.AccessToken(postdata, url);

            if (HttpContext.Session.GetString("web_session_id") != null && validSession)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public IActionResult CurrentFY_EquityLedger()
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_user_access_token";

            psp_dsp_user_access_token postdata = new psp_dsp_user_access_token();

            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            UserLogin udata = JsonConvert.DeserializeObject<UserLogin>(dtoq);

            postdata.login_id = udata.Id;
            postdata.web_session_id = HttpContext.Session.GetString("web_session_id");

            bool validSession = EncryptionDecryption.AccessToken(postdata, url);

            if (HttpContext.Session.GetString("web_session_id") != null && validSession)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public IActionResult GlobalReport(initGlobalReport dataobj = null)
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_user_access_token";

            psp_dsp_user_access_token postdata = new psp_dsp_user_access_token();

            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            UserLogin udata = JsonConvert.DeserializeObject<UserLogin>(dtoq);

            postdata.login_id = udata.Id;
            postdata.web_session_id = HttpContext.Session.GetString("web_session_id");

            bool validSession = EncryptionDecryption.AccessToken(postdata, url);

            if (HttpContext.Session.GetString("web_session_id") != null && validSession)
            {
                return View(dataobj);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public IActionResult CapitalGain()
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_user_access_token";

            psp_dsp_user_access_token postdata = new psp_dsp_user_access_token();

            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            UserLogin udata = JsonConvert.DeserializeObject<UserLogin>(dtoq);

            postdata.login_id = udata.Id;
            postdata.web_session_id = HttpContext.Session.GetString("web_session_id");

            bool validSession = EncryptionDecryption.AccessToken(postdata, url);

            if (HttpContext.Session.GetString("web_session_id") != null && validSession)
            {
                return View(udata);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public IActionResult GlobalReportMF(initGlobalReport dataobj = null)
		{
			string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_user_access_token";

			psp_dsp_user_access_token postdata = new psp_dsp_user_access_token();

			string dtoq = TempData["myFinyearsdata"].ToString();
			TempData.Keep("myFinyearsdata");
			UserLogin udata = JsonConvert.DeserializeObject<UserLogin>(dtoq);

			postdata.login_id = udata.Id;
			postdata.web_session_id = HttpContext.Session.GetString("web_session_id");

			bool validSession = EncryptionDecryption.AccessToken(postdata, url);

			if (HttpContext.Session.GetString("web_session_id") != null && validSession)
			{
				return View(dataobj);
			}
			else
			{
				return RedirectToAction("Index", "Login");
			}
		}

		public IActionResult MISClientActivity()
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_user_access_token";

            psp_dsp_user_access_token postdata = new psp_dsp_user_access_token();

            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            UserLogin udata = JsonConvert.DeserializeObject<UserLogin>(dtoq);

            postdata.login_id = udata.Id;
            postdata.web_session_id = HttpContext.Session.GetString("web_session_id");

            bool validSession = EncryptionDecryption.AccessToken(postdata, url);

            if (HttpContext.Session.GetString("web_session_id") != null && validSession)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        public IActionResult DepositoryHoldingReport()
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_user_access_token";

            psp_dsp_user_access_token postdata = new psp_dsp_user_access_token();

            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            UserLogin udata = JsonConvert.DeserializeObject<UserLogin>(dtoq);

            postdata.login_id = udata.Id;
            postdata.web_session_id = HttpContext.Session.GetString("web_session_id");

            bool validSession = EncryptionDecryption.AccessToken(postdata, url);

            if (HttpContext.Session.GetString("web_session_id") != null && validSession)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        public IActionResult ClientContactDetails()
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_user_access_token";

            psp_dsp_user_access_token postdata = new psp_dsp_user_access_token();

            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            UserLogin udata = JsonConvert.DeserializeObject<UserLogin>(dtoq);

            postdata.login_id = udata.Id;
            postdata.web_session_id = HttpContext.Session.GetString("web_session_id");

            bool validSession = EncryptionDecryption.AccessToken(postdata, url);

            if (HttpContext.Session.GetString("web_session_id") != null && validSession)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        public IActionResult BondHoldingException()
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_user_access_token";

            psp_dsp_user_access_token postdata = new psp_dsp_user_access_token();

            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            UserLogin udata = JsonConvert.DeserializeObject<UserLogin>(dtoq);

            postdata.login_id = udata.Id;
            postdata.web_session_id = HttpContext.Session.GetString("web_session_id");

            bool validSession = EncryptionDecryption.AccessToken(postdata, url);

            if (HttpContext.Session.GetString("web_session_id") != null && validSession)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        public IActionResult EqClientHoldingException()
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_user_access_token";

            psp_dsp_user_access_token postdata = new psp_dsp_user_access_token();

            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            UserLogin udata = JsonConvert.DeserializeObject<UserLogin>(dtoq);

            postdata.login_id = udata.Id;
            postdata.web_session_id = HttpContext.Session.GetString("web_session_id");

            bool validSession = EncryptionDecryption.AccessToken(postdata, url);

            if (HttpContext.Session.GetString("web_session_id") != null && validSession)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        public IActionResult BrokerageMIS()
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_user_access_token";

            psp_dsp_user_access_token postdata = new psp_dsp_user_access_token();

            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            UserLogin udata = JsonConvert.DeserializeObject<UserLogin>(dtoq);

            postdata.login_id = udata.Id;
            postdata.web_session_id = HttpContext.Session.GetString("web_session_id");

            bool validSession = EncryptionDecryption.AccessToken(postdata, url);

            if (HttpContext.Session.GetString("web_session_id") != null && validSession)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        public IActionResult TopPicksUpdate()
        {
            string Accessurl = _config.GetValue<string>("APIKey") + "Services/psp_dsp_user_access_token";

            psp_dsp_user_access_token postdata = new psp_dsp_user_access_token();

            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            UserLogin udata = JsonConvert.DeserializeObject<UserLogin>(dtoq);

            postdata.login_id = udata.Id;
            postdata.web_session_id = HttpContext.Session.GetString("web_session_id");

            bool validSession = EncryptionDecryption.AccessToken(postdata, Accessurl);

            if (HttpContext.Session.GetString("web_session_id") != null && validSession)
            {
                string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_model_portfolio";

                psp_dsp_model_portfolio rpt = new psp_dsp_model_portfolio();
                string serializeProfile = JsonConvert.SerializeObject(rpt);
                string data = EncryptionDecryption.Encrypt(serializeProfile);
                EncryptData Endata = new EncryptData();
                Endata.EncryptObject = data;
                string data1 = HttpCall.HttpPostMethod(url, Endata);

                List<psp_dsp_model_portfolio> Result = JsonConvert.DeserializeObject<List<psp_dsp_model_portfolio>>(data1);
                return View(Result);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public IActionResult MisEqtyScripHoldingClientList(DisplayEqScripHolCliList jsonDataBoj = null)
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_user_access_token";

            psp_dsp_user_access_token postdata = new psp_dsp_user_access_token();

            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            UserLogin udata = JsonConvert.DeserializeObject<UserLogin>(dtoq);

            postdata.login_id = udata.Id;
            postdata.web_session_id = HttpContext.Session.GetString("web_session_id");

            bool validSession = EncryptionDecryption.AccessToken(postdata, url);

            if (HttpContext.Session.GetString("web_session_id") != null && validSession)
            {
                string rmurl = _config.GetValue<string>("APIKey") + "Reports/psp_dsp_BH_RM_list";

                psp_dsp_BH_RM_list rmlistObj = new psp_dsp_BH_RM_list();
                rmlistObj.login_id = udata.Id.ToString();

                string serializeProfile2 = JsonConvert.SerializeObject(rmlistObj);

                string data2 = EncryptionDecryption.Encrypt(serializeProfile2);
                EncryptData Endata2 = new EncryptData();
                Endata2.EncryptObject = data2;
                string data3 = HttpCall.HttpPostMethod(rmurl, Endata2);
                List<psp_dsp_BH_RM_list> RMResult = JsonConvert.DeserializeObject<List<psp_dsp_BH_RM_list>>(data3);

                string scripurl = _config.GetValue<string>("APIKey") + "Dashboard/psp_rpt_scripwiseholding_scrip";

                psp_rpt_scripwiseholding_scrip scriprpt = new psp_rpt_scripwiseholding_scrip
                {
                    rm = _config.GetValue<string>("MasterTM_Id"),
                    Asset = "1"
                };

                string serializeProfileScrip = JsonConvert.SerializeObject(scriprpt);
                string Scripdata = EncryptionDecryption.Encrypt(serializeProfileScrip);
                EncryptData ScripEndata = new EncryptData();
                ScripEndata.EncryptObject = Scripdata;
                string Scripdata1 = HttpCall.HttpPostMethod(scripurl, ScripEndata);
                List<psp_rpt_scripwiseholding_scrip> ScripResult = JsonConvert.DeserializeObject<List<psp_rpt_scripwiseholding_scrip>>(Scripdata1);

                jsonDataBoj.BH_RM_list = RMResult;
                jsonDataBoj.script_list = ScripResult;

                return View(jsonDataBoj);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public IActionResult MisEqScripSummaryHolding()
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_user_access_token";

            psp_dsp_user_access_token postdata = new psp_dsp_user_access_token();

            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            UserLogin udata = JsonConvert.DeserializeObject<UserLogin>(dtoq);

            postdata.login_id = udata.Id;
            postdata.web_session_id = HttpContext.Session.GetString("web_session_id");

            bool validSession = EncryptionDecryption.AccessToken(postdata, url);

            if (HttpContext.Session.GetString("web_session_id") != null && validSession)
            {
                string rmurl = _config.GetValue<string>("APIKey") + "Reports/psp_dsp_BH_RM_list";

                psp_dsp_BH_RM_list rmlistObj = new psp_dsp_BH_RM_list();
                rmlistObj.login_id = udata.Id.ToString();

                string serializeProfile2 = JsonConvert.SerializeObject(rmlistObj);

                string data2 = EncryptionDecryption.Encrypt(serializeProfile2);
                EncryptData Endata2 = new EncryptData();
                Endata2.EncryptObject = data2;
                string data3 = HttpCall.HttpPostMethod(rmurl, Endata2);
                List<psp_dsp_BH_RM_list> RMResult = JsonConvert.DeserializeObject<List<psp_dsp_BH_RM_list>>(data3);

                DisplayEqScripHolCliList jsonDataBoj = new DisplayEqScripHolCliList { BH_RM_list = RMResult };

                return View(jsonDataBoj);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        public IActionResult MisEqTopClients()
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_user_access_token";

            psp_dsp_user_access_token postdata = new psp_dsp_user_access_token();

            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            UserLogin udata = JsonConvert.DeserializeObject<UserLogin>(dtoq);

            postdata.login_id = udata.Id;
            postdata.web_session_id = HttpContext.Session.GetString("web_session_id");

            bool validSession = EncryptionDecryption.AccessToken(postdata, url);

            if (HttpContext.Session.GetString("web_session_id") != null && validSession)
            {
                string rmurl = _config.GetValue<string>("APIKey") + "Reports/psp_dsp_BH_RM_list";

                psp_dsp_BH_RM_list rmlistObj = new psp_dsp_BH_RM_list();
                rmlistObj.login_id = udata.Id.ToString();

                string serializeProfile2 = JsonConvert.SerializeObject(rmlistObj);

                string data2 = EncryptionDecryption.Encrypt(serializeProfile2);
                EncryptData Endata2 = new EncryptData();
                Endata2.EncryptObject = data2;
                string data3 = HttpCall.HttpPostMethod(rmurl, Endata2);
                List<psp_dsp_BH_RM_list> RMResult = JsonConvert.DeserializeObject<List<psp_dsp_BH_RM_list>>(data3);

                DisplayEqScripHolCliList jsonDataBoj = new DisplayEqScripHolCliList { BH_RM_list = RMResult };

                return View(jsonDataBoj);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        public IActionResult MisEqtyClientMappingList()
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_user_access_token";

            psp_dsp_user_access_token postdata = new psp_dsp_user_access_token();

            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            UserLogin udata = JsonConvert.DeserializeObject<UserLogin>(dtoq);

            postdata.login_id = udata.Id;
            postdata.web_session_id = HttpContext.Session.GetString("web_session_id");

            bool validSession = EncryptionDecryption.AccessToken(postdata, url);

            if (HttpContext.Session.GetString("web_session_id") != null && validSession)
            {
                string rmurl = _config.GetValue<string>("APIKey") + "Reports/psp_dsp_BH_RM_list";

                psp_dsp_BH_RM_list rmlistObj = new psp_dsp_BH_RM_list();
                rmlistObj.login_id = udata.Id.ToString();

                string serializeProfile2 = JsonConvert.SerializeObject(rmlistObj);

                string data2 = EncryptionDecryption.Encrypt(serializeProfile2);
                EncryptData Endata2 = new EncryptData();
                Endata2.EncryptObject = data2;
                string data3 = HttpCall.HttpPostMethod(rmurl, Endata2);
                List<psp_dsp_BH_RM_list> RMResult = JsonConvert.DeserializeObject<List<psp_dsp_BH_RM_list>>(data3);

                DisplayEqScripHolCliList jsonDataBoj = new DisplayEqScripHolCliList { BH_RM_list = RMResult };

                return View(jsonDataBoj);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public IActionResult MisEqtyVolumeBrokerage(DisplayEqScripHolCliList jsonDataObj = null)
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_user_access_token";

            psp_dsp_user_access_token postdata = new psp_dsp_user_access_token();

            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            UserLogin udata = JsonConvert.DeserializeObject<UserLogin>(dtoq);

            postdata.login_id = udata.Id;
            postdata.web_session_id = HttpContext.Session.GetString("web_session_id");

            bool validSession = EncryptionDecryption.AccessToken(postdata, url);

            if (HttpContext.Session.GetString("web_session_id") != null && validSession)
            {
                string rmurl = _config.GetValue<string>("APIKey") + "Reports/psp_dsp_BH_RM_list";

                psp_dsp_BH_RM_list rmlistObj = new psp_dsp_BH_RM_list();
                rmlistObj.login_id = udata.Id.ToString();

                string serializeProfile2 = JsonConvert.SerializeObject(rmlistObj);

                string data2 = EncryptionDecryption.Encrypt(serializeProfile2);
                EncryptData Endata2 = new EncryptData();
                Endata2.EncryptObject = data2;
                string data3 = HttpCall.HttpPostMethod(rmurl, Endata2);
                List<psp_dsp_BH_RM_list> RMResult = JsonConvert.DeserializeObject<List<psp_dsp_BH_RM_list>>(data3);

                string scripurl = _config.GetValue<string>("APIKey") + "Dashboard/psp_rpt_scripwiseholding_scrip";

                psp_rpt_scripwiseholding_scrip scriprpt = new psp_rpt_scripwiseholding_scrip
                {
                    rm = _config.GetValue<string>("MasterTM_Id"),
                    Asset = "1"
                };

                string serializeProfileScrip = JsonConvert.SerializeObject(scriprpt);
                string Scripdata = EncryptionDecryption.Encrypt(serializeProfileScrip);
                EncryptData ScripEndata = new EncryptData();
                ScripEndata.EncryptObject = Scripdata;
                string Scripdata1 = HttpCall.HttpPostMethod(scripurl, ScripEndata);
                List<psp_rpt_scripwiseholding_scrip> ScripResult = JsonConvert.DeserializeObject<List<psp_rpt_scripwiseholding_scrip>>(Scripdata1);

                jsonDataObj.BH_RM_list = RMResult;
                jsonDataObj.script_list = ScripResult;

                return View(jsonDataObj);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public IActionResult TransactionReport()
        {
            return View();
        }

        public object CommonDocuments(RPT rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/CommonDocumentDetails";
            // Dashboard dashboard = new Dashboard();

            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            CommonDocumentReports rd = JsonConvert.DeserializeObject<CommonDocumentReports>(dtoq);

            string serializeProfile = JsonConvert.SerializeObject(rd);

            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            rpt.commdocrpt = JsonConvert.DeserializeObject<List<CommonDocumentReports>>(data1);

            return rpt;

        }

        public IActionResult TestSIPFormat(RPT dto)
        {
            if (HttpContext.Session.GetString("web_session_id") != null)
            {
                //   dto = (RPT)SIPDetails(dto);

                string url = _config.GetValue<string>("APIKey") + "Reports/SIPDetails"; //psp_dsp_mf_sip_details
                string dtoq = TempData["myFinyearsdata"].ToString();
                TempData.Keep("myFinyearsdata");
                SIPDetail rd = JsonConvert.DeserializeObject<SIPDetail>(dtoq);
                string serializeProfile = Newtonsoft.Json.JsonConvert.SerializeObject(rd);
                string data = EncryptionDecryption.Encrypt(serializeProfile);
                EncryptData Endata = new EncryptData();
                Endata.EncryptObject = data;
                string data1 = HttpCall.HttpPostMethod(url, Endata);

                List<SIPDetail> pspdsp = JsonConvert.DeserializeObject<List<SIPDetail>>(data1);
                string v2 = JsonConvert.SerializeObject(pspdsp);
                TempData["AllSIPData"] = v2;

                List<SIPDetail> newSIPObj = new List<SIPDetail>();
                newSIPObj = pspdsp.Where(x => x.SIP_Categpry == "T").ToList();
                v2 = JsonConvert.SerializeObject(newSIPObj);
                TempData["TerminatingSIPData"] = v2;

                newSIPObj = pspdsp.Where(x => x.SIP_Categpry == "R").ToList();
                v2 = JsonConvert.SerializeObject(newSIPObj);
                TempData["RecentSIPData"] = v2;

                return View(dto);
            }
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public IActionResult TestSIPDashboard([FromBody] psp_dsp_mf_sip_dashboard rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/psp_dsp_mf_sip_dashboard";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            psp_dsp_mf_sip_dashboard rd = JsonConvert.DeserializeObject<psp_dsp_mf_sip_dashboard>(dtoq);
            
            string serializeProfile = JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_mf_sip_dashboard> psp_dsp_mf_sip_dashboard = JsonConvert.DeserializeObject<List<psp_dsp_mf_sip_dashboard>>(data1);
            return Json(psp_dsp_mf_sip_dashboard);
        }

        //public IActionResult SIPReports(RPT dto)
        //{
        //    if (HttpContext.Session.GetString("web_session_id") != null)
        //    {
        //        //   dto = (RPT)SIPDetails(dto);

                

        //        return View(dto);
        //    }
        //    return RedirectToAction("Index", "Login");
        //}

        //public object SIPDetails(RPT rpt)
        //{
        //    string html = "";
        //    string url = _config.GetValue<string>("APIKey") + "Reports/SIPDetails";
        //    string dtoq = TempData["myFinyearsdata"].ToString();
        //    TempData.Keep("myFinyearsdata");
        //    SIPDetail rd = JsonConvert.DeserializeObject<SIPDetail>(dtoq);
        //    string serializeProfile = Newtonsoft.Json.JsonConvert.SerializeObject(rd);
        //    string data = EncryptionDecryption.Encrypt(serializeProfile);
        //    EncryptData Endata = new EncryptData();
        //    Endata.EncryptObject = data;
        //    string data1 = HttpCall.HttpPostMethod(url, Endata);
        //    rpt.sd = JsonConvert.DeserializeObject<List<SIPDetail>>(data1);
        //    // rpt.totamt= rpt.sd.Sum(x => x.Amount);
        //    int i = 1;
        //    int j = 1;
        //    int r = 1;
        //    foreach (var d in rpt.sd.GroupBy(x => x.Login_Name))
        //    {
        //        html = html + "<div class='panel-group' id='accordion" + i + "'><div class='panel panel-default'> <div id='bot' style='background-color: #ffb3b3;' class='panel-heading'><h4 class='panel-title'>" +
        //            "<a class='table-title1' data-toggle='collapse' style='font-size:16px;max-width:auto;position: relative;text-decoration: underline;' data-parent='#accordion" + i + "' href='#collapseThree" + j + "'>" + d.Key + "</a>" + "<a class='table-title2' style='font-size:16px;margin-left:15px;position: relative;'> No of Clients : " + d.Select(x => x.main_client_name).Distinct().Count() + "</a>" + " <a class='table-title2' style='font-size:16px;margin-left:15px;position: relative;'> Total SIPs : " + d.Count() + "</a>" + "<a class='table-title2' style='font-size:16px;margin-left:15px;position: relative;'> Expiring SIPs : " + d.Where(x => x.terminating == "1").Count() + "</a>" + "<a style='font-size:16px;margin-left:15px;position: relative;'> Total Amount : " + d.Sum(x => x.Amount).ToString("N") + "</a>" +
        //            "<i class='indicator glyphicon glyphicon-chevron-down  pull-left' style='margin-right: 10px; color: white;'></i> </h4> </div>";
        //        foreach (var item in d.GroupBy(y => y.main_client_name))
        //        {
        //            html = html + "<div id='collapseThree" + j + "' class='panel-collapse collapse'><div class='panel-body'><div class='panel-group' id='accordion" + j + "'><div class='panel panel-default'><div style='background-color: #ffe6e6;' class='panel-heading'><h4 class='panel-title'>" +
        //            "<a class='table-title1' data-toggle='collapse' style='font-size:16px;max-width:auto;position: relative;text-decoration: underline;' data-parent='#accordion" + j + "' href='#collapseThreeOne" + r + "'>" + item.Key + "</a>" + "<a class='table-title2' style='font-size:16px;margin-left:15px;position: relative;'> Total SIPs : " + item.Count() + "</a>" + "<a class='table-title2' style='font-size:16px;margin-left:15px;position: relative;'> Expiring SIPs : " + item.Where(x => x.terminating == "1").Count() + " </a>" + "<a style='font-size:16px;margin-left:15px;position: relative;'> Amount : " + item.Sum(x => x.Amount).ToString("N") + "</a>" +
        //            "<i class='indicator glyphicon glyphicon-chevron-down  pull-left' style='margin-right: 10px; color: black;'></i> " +
        //            "</h4></div><div id='collapseThreeOne" + r + "' class='panel-collapse collapse'><div class='panel-body table-responsive'><table class='table w-auto'>" +
        //            "<tr><th><p style='font-size:15px;font-weight: bold;text-align:left;'>Scheme Name</p><p style='font-size:12px;font-weight: bold;text-align:left;'> Folio No</p></th><th><p style='font-size:15px;font-weight: bold;'>End Date</p><p style='font-size:12px;font-weight: bold;'>Start Date</p></th><th><p style='font-size:15px;font-weight: bold;'>Amount</p><p style='font-size:12px;font-weight: bold;'>Frequency</p></th><th><p style='font-size:15px;font-weight: bold;'>Bank Name</p><p style='font-size:12px;font-weight: bold;'>Account No</p></th><th><p style='font-size:15px;font-weight: bold;'>Expiring <br> in Days</p></th></tr>";
        //            foreach (var da in item)
        //            {
        //                html = html + "<tr><td><p style='font-size:15px;'>" + da.Scheme_Name + "</p><p style='font-size:12px;'>" + da.Folio_no + "</p></td><td><p style='font-size:15px;text-align:center;'>" + da.End_Date.ToString("dd-MMM-yyyy") + "</p><p style='font-size:12px;text-align:center;'>" + da.Start_Date.ToString("dd-MMM-yyyy") + "</p></td><td><p style='font-size:15px;text-align:right;'>" + da.Amount.ToString("N") + "</p><p style='font-size:12px;text-align:right;'>" + da.Frequency + "</p></td><td><p style='font-size:15px;'>" + da.Bank_Name + "<p/><p style='font-size:12px;'>" + da.Ac_No + "</p></td><td><p style='font-size:15px;float:right;color:red;'>" + da.terminating + "<p/></td></tr>";
        //            }
        //            r++;


        //            html = html + "</table></div></div></div></div></div></div>";

        //        }
        //        j++;
        //        i++;

        //        html = html + "</div></div>";

        //    }

        //    // html = html + "";

        //    return Json(html);

        //    // return rpt;
        //}

        //public object SIPDetails(SIPDetail rpt)
        //{
        //    string url = _config.GetValue<string>("APIKey") + "Reports/SIPDetails";
        //    string dtoq = TempData["myFinyearsdata"].ToString();
        //    TempData.Keep("myFinyearsdata");
        //    SIPDetail rd = JsonConvert.DeserializeObject<SIPDetail>(dtoq);
        //    string serializeProfile = Newtonsoft.Json.JsonConvert.SerializeObject(rd);
        //    string data = EncryptionDecryption.Encrypt(serializeProfile);
        //    EncryptData Endata = new EncryptData();
        //    Endata.EncryptObject = data;
        //    string data1 = HttpCall.HttpPostMethod(url, Endata);

        //    List<SIPDetail> pspdsp = JsonConvert.DeserializeObject<List<SIPDetail>>(data1);
        //    string v2 = JsonConvert.SerializeObject(pspdsp);
        //    TempData["AllSIPData"] = v2;

        //    List<SIPDetail> newSIPObj = new List<SIPDetail>();
        //    newSIPObj = pspdsp.Where(x => x.SIP_Categpry == "T").ToList();
        //    v2 = JsonConvert.SerializeObject(newSIPObj);
        //    TempData["TerminatingSIPData"] = v2;

        //    newSIPObj = pspdsp.Where(x => x.SIP_Categpry != "R").ToList();
        //    v2 = JsonConvert.SerializeObject(newSIPObj);
        //    TempData["RecentSIPData"] = v2;


        //    return Json(pspdsp);
        //}

        public IActionResult SIPSarchDetails([FromBody] RPT rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/SIPDetails";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            SIPDetail rd = JsonConvert.DeserializeObject<SIPDetail>(dtoq);
            //  rd.finyr = Int32.Parse(rpt.yearID);
            //  rd.family_id = Int32.Parse(rpt.familyListID);
            string serializeProfile = Newtonsoft.Json.JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<SIPDetail> sip = JsonConvert.DeserializeObject<List<SIPDetail>>(data1);
            //sip = sip.OrderBy(x => x.Login_Name).ThenBy(y => y.main_client_name).ThenBy(z => z.Scheme_Name).ThenBy(a => a.terminating).ToList();
            sip = sip.OrderBy(x => x.terminating).ThenBy(y => y.Login_Name).ThenBy(z => z.main_client_name).ThenBy(a => a.Scheme_Name).ToList();
            // return sip;
            return Json(sip);
        }


        public IActionResult DividendReports(RPT dto)
        {
            if (HttpContext.Session.GetString("web_session_id") != null)
            {
                // dto = (RPT)DividendDetails(dto);
                return View(dto);
            }
            return RedirectToAction("Index", "Login");
        }
        [HttpPost]
        public IActionResult DividendDetails([FromBody] RPT rpt)
        {
            string html = "";
            if (rpt.yearID != null)
            {
                string url = _config.GetValue<string>("APIKey") + "Reports/DividendDetails";
                string dtoq = TempData["myFinyearsdata"].ToString();
                TempData.Keep("myFinyearsdata");
                Dividend rd = JsonConvert.DeserializeObject<Dividend>(dtoq);
                rd.finyr = Int32.Parse(rpt.yearID);
                rd.family_id = 0;  //Int32.Parse(rpt.familyListID);
                string serializeProfile = Newtonsoft.Json.JsonConvert.SerializeObject(rd);
                string data = EncryptionDecryption.Encrypt(serializeProfile);
                EncryptData Endata = new EncryptData();
                Endata.EncryptObject = data;
                string data1 = HttpCall.HttpPostMethod(url, Endata);
                rpt.dividend = JsonConvert.DeserializeObject<List<Dividend>>(data1);
                int i = 1;
                int j = 1;
                int k = 1;
                int l = 1;
                foreach (var d in rpt.dividend.GroupBy(x => x.family_name))
                {
                    html = html + "<div class='panel-group' id='accordion" + i + "'><div class='panel panel-default'> <div id='bot' style='background-color: #ffb3b3;' class='panel-heading'>" +
                        "<h4 class='panel-title'> <a class='table-title1' data-toggle='collapse' style='font-size:16px;text-decoration: underline;' data-parent='#accordion" + i + "' href='#collapseFirst" + j + "'>" + d.Key + "</a>" + "<a class='table-title2' style='font-size:16px;margin-left:15px;position: relative;'> Clients : " + d.Select(x => x.client_name).Distinct().Count() + "</a>" + "<a class='table-title2' style='font-size:16px;margin-left:15px;position: relative;'> Bonds Int : " + d.Where(x => x.sub_category == "Bonds").Sum(x => x.value).ToString("N") + " </a> " + "<a class='table-title2' style='font-size:16px;margin-left:15px;position: relative;'> EQ Div : " + d.Where(x => x.sub_category == "Equity").Sum(x => x.value).ToString("N") + "</a>" + "<a class='table-title2' style='font-size:16px;margin-left:15px;position: relative;'> MF Div : " + d.Where(x => x.sub_category == "MF").Sum(x => x.value).ToString("N") + "</a>" + "<a class='table-title2' style='font-size:16px;margin-left:15px;position: relative;'> Total Div/Int : " + d.Sum(x => x.value).ToString("N") + "</a>" +
                        "<i class='indicator glyphicon glyphicon-chevron-down  pull-left' style='margin-right: 10px; color: white;'></i> </h4> </div>";
                    foreach (var item in d.GroupBy(y => y.client_name))
                    {
                        html = html + "<div id='collapseFirst" + j + "' class='panel-collapse collapse'><div class='panel-body'><div class='panel-group' id='accordion" + j + "'>" +
                        "<div class='panel panel-default'><div style='background-color: #ffcccc;' class='panel-heading'><h4 class='panel-title'>" +
                        "<a class='table-title1' data-toggle='collapse' style='font-size:15px;text-decoration: underline;' data-parent='#accordion" + j + "' href='#collapseSecond" + k + "'>" + item.Key + "</a>" + "<a class='table-title2' style='font-size:16px;margin-left:15px;position: relative;'> Bonds Int : " + item.Where(x => x.sub_category == "Bonds").Sum(x => x.value).ToString("N") + "</a>" + "<a class='table-title2' style='font-size:16px;margin-left:15px;position: relative;'> EQ Div : " + item.Where(x => x.sub_category == "Equity").Sum(x => x.value).ToString("N") + "</a>" + "<a class='table-title2' style='font-size:16px;margin-left:15px;position: relative;'>  MF Div : " + item.Where(x => x.sub_category == "MF").Sum(x => x.value).ToString("N") + "</a>" + "<a class='table-title2' style='font-size:16px;margin-left:15px;position: relative;'> Total Div/Int : " + item.Sum(x => x.value).ToString("N") + "</a>" +
                        "<i class='indicator glyphicon glyphicon-chevron-down  pull-left' style='margin-right: 10px; color: black;'></i> " +
                        "</h4></div>";
                        foreach (var cat in item.GroupBy(y => y.category))
                        {
                            html = html + "<div id='collapseSecond" + k + "' class='panel-collapse collapse'><div class='panel-body'><div class='panel-group' id='accordion" + k + "'>" +
                            "<div class='panel panel-default'><div style='background-color: #ffe6e6;' class='panel-heading'><h4 class='panel-title'>" +
                            "<a class='table-title1' data-toggle='collapse' style='font-size:14px;text-decoration: underline;' data-parent='#accordion" + k + "' href='#collapseThird" + l + "'>" + cat.Key + "</a>" + "<a class='table-title2' style='font-size:16px;margin-left:15px;position: relative;'> Div/Int : " + cat.Where(x => x.sub_category == x.sub_category).Count() + "</a>" + "<a class='table-title2' style='font-size:16px;margin-left:15px;position: relative;'> Total Div/Int : " + cat.Sum(x => x.value).ToString("N") + "</a>" +
                            "<i class='indicator glyphicon glyphicon-chevron-down  pull-left' style='margin-right: 10px; color: black;'></i> " +
                            "</h4></div><div id='collapseThird" + l + "' class='panel-collapse collapse'><div class='panel-body'><table class='table w-auto' style='font-family:Roboto, sans-serif;'>" +
                            "<tr><th><p style='font-size:14px;font-family:Roboto, sans-serif;font-weight: bold;'>Date</p></th><th><p style='font-size:14px;font-family:Roboto, sans-serif;font-weight: bold;'>ISIN</p></th><th><p style='font-size:14px;font-family:Roboto, sans-serif;font-weight: bold;'>Scrip Name</p></th><th><p style='font-size:14px;font-family:Roboto, sans-serif;font-weight: bold;float:right;'>Amount</p></th></tr>";
                            foreach (var da in cat.OrderBy(x => x.dividend_date))
                            {
                                html = html + "<tr><td><p style='font-size:14px;font-family:Roboto,sans-serif;'>" + da.dividend_date.ToString("dd-MMM-yyyy") + "</p></td><td><p style='font-size:14px;font-family:Roboto,sans-serif;'>" + da.ISIN + "</p></td><td><p style='font-size:14px;font-family:Roboto,sans-serif;word-wrap: break-word;'>" + da.scrip_name + "</p></td><td><p style='font-size:14px;float:right;font-family:Roboto, sans-serif;'>" + da.value.ToString("N") + "</p></td></tr> ";
                            }
                            l++;
                            html = html + " </table></div></div></div></div></div></div>";
                        }
                        k++;
                        html = html + "</div></div></div></div>";
                    }

                    j++;
                    i++;
                    html = html + "</div></div>";


                }

                return Json(html);
            }
            return Json(rpt);
        }

        public List<Dividend> DividendSearDetails([FromBody] RPT rpt)
        {
            //string html = "";
            //if (rpt.yearID != null)
            //{
            string url = _config.GetValue<string>("APIKey") + "Reports/DividendDetails";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            Dividend rd = JsonConvert.DeserializeObject<Dividend>(dtoq);
            rd.finyr = Int32.Parse(rpt.yearID);
            rd.family_id = 0;// Int32.Parse(rpt.familyListID);
            string serializeProfile = Newtonsoft.Json.JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<Dividend> dividend = JsonConvert.DeserializeObject<List<Dividend>>(data1);
            //Dividend dividend1 = new Dividend();
            //foreach (var divi in dividend)
            //{
            //    dividend1.family_name=divi.family_name.OrderByDescending(divi)
            //}

            dividend = dividend.OrderBy(x => x.family_name).ThenBy(y => y.client_name).ThenBy(d => d.dividend_date).ThenBy(s => s.scrip_name).ToList();

            return dividend;
            //}
            //return rpt;
        }
        public IActionResult IncomeReports(RPT dto)
        {
            if (HttpContext.Session.GetString("web_session_id") != null)
            {
                //dto = (RPT)IncomeDetails(dto);
                return View(dto);
            }
            return RedirectToAction("Index", "Login");
        }
        public IActionResult ConsolidatedIncome()
        {
            if (HttpContext.Session.GetString("web_session_id") != null)
            {
                
                return View();
            }
            return RedirectToAction("Index", "Login");
        }
        [HttpPost]
        public IActionResult ConsolidatedIncomeDetails([FromBody] psp_dsp_consolidated_income rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/psp_dsp_consolidated_income";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            psp_dsp_consolidated_income rd = JsonConvert.DeserializeObject<psp_dsp_consolidated_income>(dtoq);
            if (rpt.fam_id == null)
            {
                rd.fam_id = "254580";
            }
            else
            {
                rd.fam_id = rpt.fam_id;
            }

            rd.Login_id = rpt.Id.ToString();
            rd.fin_year = rpt.fin_year;

            // rd.main_client_id = rpt.main_client_id;
            string serializeProfile = JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_consolidated_income> income = JsonConvert.DeserializeObject<List<psp_dsp_consolidated_income>>(data1);

            return Json(income);
        }
        [HttpPost]
        public IActionResult psp_dsp_equity_client_fy_factors([FromBody] psp_dsp_equity_client_fy_factors rpt)
        {

            string url = _config.GetValue<string>("APIKey") + "Reports/psp_dsp_equity_client_fy_factors";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            psp_dsp_equity_client_fy_factors rd = JsonConvert.DeserializeObject<psp_dsp_equity_client_fy_factors>(dtoq);

            rd.fin_year = rpt.fin_year;
            rd.account_code = rpt.account_code;

            string serializeProfile = JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_equity_client_fy_factors> equityClientSummary = JsonConvert.DeserializeObject<List<psp_dsp_equity_client_fy_factors>>(data1);

            if (rd.fin_year == "0")
            {
                return Json(equityClientSummary);
            }
            else
            {
                equityClientSummary = equityClientSummary.Where(x => x.sort_order != "2").ToList();
                return Json(equityClientSummary);
            }
        }
        [HttpPost]
        public IActionResult IncomeDetails([FromBody] RPT rpt)
        {
            // dtoOutoutInDetails incomeDetails = new dtoOutoutInDetails();
            string url = _config.GetValue<string>("APIKey") + "Reports/IncomeDetails";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            IncomeStatement rd = JsonConvert.DeserializeObject<IncomeStatement>(dtoq);

            if (rpt.familyListID == null)
            {
                rd.family_id = 254580;
                rd.year = 2021;
                rd.rpt_period_value = 2021;
            }
            else
            {
                rd.family_id = Int32.Parse(rpt.familyListID);
                rd.year = Int32.Parse(rpt.yearID);
                rd.rpt_period_value = Int32.Parse(rpt.yearID);
            }
            rd.rpt_period = 1;
            // rd.main_client_id = rpt.main_client_id;
            string serializeProfile = JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<IncomeStatement> income = JsonConvert.DeserializeObject<List<IncomeStatement>>(data1);

            return Json(income);
        }

        [HttpGet]
        public IActionResult IncomePeriodDetails(string period_type, string Year_value_data)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/IncomePeriod";
            Period period = new Period();
            if (period != null)
            {
                period.period_typeValue = Int32.Parse(period_type);
                period.Year_typevalue = Int32.Parse(Year_value_data);
            }
            else
            {
                period.period_typeValue = 0;
                period.Year_typevalue = 0;
            }

            string serializeProfile = JsonConvert.SerializeObject(period);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<Period> inPeriod = JsonConvert.DeserializeObject<List<Period>>(data1);
            return Json(inPeriod);
        }
        public IActionResult RealisedGainLossReports(RPT dto)
        {
            if (HttpContext.Session.GetString("web_session_id") != null)
            {
                //dto = (RPT)IncomeDetails(dto);
                return View(dto);
            }
            return RedirectToAction("Index", "Login");
        }
        [HttpPost]
        public IActionResult RealisedDetails([FromBody] InputRealisedGainLoss rpt)
        {

            string url = _config.GetValue<string>("APIKey") + "Reports/RealisedDetails";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            RealisedGainLoss rd = JsonConvert.DeserializeObject<RealisedGainLoss>(dtoq);
            rd.FINYR = Int32.Parse(rpt.FINYEAR);
            rd.sub_category = rpt.Subcategory;
            rd.Client = rpt.ClientID;
            rd.rpt_period = Int32.Parse(rpt.Rtpperiod);
            rd.rpt_period_value = rpt.Rptperiod_value;
            string serializeProfile = JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<RealisedGainLoss> realisedgainloss = JsonConvert.DeserializeObject<List<RealisedGainLoss>>(data1);
            return Json(realisedgainloss);
        }

        [HttpPost]
        public IActionResult RealisedDividendDetails([FromBody] RPT rpt)
        {
            if (rpt.yearID != null)
            {
                string url = _config.GetValue<string>("APIKey") + "Reports/DividendDetails";
                string dtoq = TempData["myFinyearsdata"].ToString();
                TempData.Keep("myFinyearsdata");
                Dividend rd = JsonConvert.DeserializeObject<Dividend>(dtoq);
                rd.finyr = Int32.Parse(rpt.yearID);
                rd.family_id = Int32.Parse(rpt.familyListID);
                rd.main_client_id = rpt.main_client_id;
                string serializeProfile = Newtonsoft.Json.JsonConvert.SerializeObject(rd);
                string data = EncryptionDecryption.Encrypt(serializeProfile);
                EncryptData Endata = new EncryptData();
                Endata.EncryptObject = data;
                string data1 = HttpCall.HttpPostMethod(url, Endata);
                List<Dividend> dividend = JsonConvert.DeserializeObject<List<Dividend>>(data1);

                return Json(dividend);
            }
            return Json(rpt);
        }


        public IActionResult EquityClientSummaryReports(RPT dto)
        {
            if (HttpContext.Session.GetString("web_session_id") != null)
            {
                //dto = (RPT)IncomeDetails(dto);
                return View(dto);
            }
            return RedirectToAction("Index", "Login");
        }
        [HttpPost]
        public IActionResult bindreferLink([FromBody] EquityClientSummary rpt)
        {

            string url = _config.GetValue<string>("APIKey") + "Reports/bindreferLink";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            xirr_trade_exception_link rd = JsonConvert.DeserializeObject<xirr_trade_exception_link>(dtoq);
            rd.account_code = rpt.account_code;
            string serializeProfile = JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<xirr_trade_exception_link> xirr_trade_exception_link = JsonConvert.DeserializeObject<List<xirr_trade_exception_link>>(data1);
            return Json(xirr_trade_exception_link);
        }
        [HttpPost]
        public JsonResult gettradeexception([FromBody] psp_dsp_client_trade_exceptions rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/gettradeexception";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            psp_dsp_client_trade_exceptions rd = JsonConvert.DeserializeObject<psp_dsp_client_trade_exceptions>(dtoq);
            rd.client_code = rpt.client_code;
            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_client_trade_exceptions> psp_dsp_client_trade_exceptions = JsonConvert.DeserializeObject<List<psp_dsp_client_trade_exceptions>>(data1);
            return Json(psp_dsp_client_trade_exceptions);
        }

        [HttpPost]
        public IActionResult EquityClientDetails([FromBody] EquityClientSummary rpt)
        {

            string url = _config.GetValue<string>("APIKey") + "Reports/EquityClientDetails";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            EquityClientSummary rd = JsonConvert.DeserializeObject<EquityClientSummary>(dtoq);
            rd.account_code = rpt.account_code;
            string serializeProfile = JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<EquityClientSummary> equityClientSummary = JsonConvert.DeserializeObject<List<EquityClientSummary>>(data1);
            return Json(equityClientSummary);
        }

        public IActionResult EquityClientFlowReports(RPT dto)
        {
            if (HttpContext.Session.GetString("web_session_id") != null)
            {
                //dto = (RPT)IncomeDetails(dto);
                return View(dto);
            }
            return RedirectToAction("Index", "Login");
        }
        [HttpPost]
        public IActionResult EquityClientFlowDetails([FromBody] EquityClientFlow rpt)
        {

            string url = _config.GetValue<string>("APIKey") + "Reports/EquityClientFlowDetails";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            EquityClientFlow rd = JsonConvert.DeserializeObject<EquityClientFlow>(dtoq);
            rd.account_code = rpt.account_code;
            rd.flow_type = "0";
            rd.summary = 0;
            string serializeProfile = JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            EquityClientFlow equityClientflow = JsonConvert.DeserializeObject<EquityClientFlow>(data1);
            return Json(equityClientflow);
        }

        [HttpPost]
        public IActionResult EquityClientInFlowOutFlowDetails([FromBody] EquityClientFlow rpt)
        {

            string url = _config.GetValue<string>("APIKey") + "Reports/EquityInFlowOutFlowDetails";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            EquityClientFlow rd = JsonConvert.DeserializeObject<EquityClientFlow>(dtoq);
            rd.account_code = rpt.account_code;
            rd.flow_type = rpt.flow_type;
            rd.summary = 1;
            string serializeProfile = JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<EquityClientFlow> equityClientflow = JsonConvert.DeserializeObject<List<EquityClientFlow>>(data1);
            return Json(equityClientflow);
        }
        public IActionResult EquityClientFyFactorReports(RPT dto)
        {
            if (HttpContext.Session.GetString("web_session_id") != null)
            {
                //dto = (RPT)IncomeDetails(dto);
                return View(dto);
            }
            return RedirectToAction("Index", "Login");
        }
        [HttpPost]
        public IActionResult EquityClientFyFactorDetails([FromBody] EquityClientFyFactor rpt)
        {

            string url = _config.GetValue<string>("APIKey") + "Reports/EquityClientFyFactorDetails";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            EquityClientFyFactor rd = JsonConvert.DeserializeObject<EquityClientFyFactor>(dtoq);
            rd.account_code = rpt.account_code;
            string serializeProfile = JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<EquityClientFyFactor> equityClientflow = JsonConvert.DeserializeObject<List<EquityClientFyFactor>>(data1);
            return Json(equityClientflow);
        }
        public IActionResult EquityClientHoldingReports(RPT dto)
        {
            if (HttpContext.Session.GetString("web_session_id") != null)
            {
                //dto = (RPT)IncomeDetails(dto);
                return View(dto);
            }
            return RedirectToAction("Index", "Login");
        }
        [HttpPost]
        public IActionResult EquityClientHoldingDetails([FromBody] EquityClientHolding rpt)
        {

            string url = _config.GetValue<string>("APIKey") + "Reports/EquityClientHoldingDetails";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            EquityClientHolding rd = JsonConvert.DeserializeObject<EquityClientHolding>(dtoq);

            rd.account_code = rpt.account_code;
            rd.holding_type = "2";
            string serializeProfile = JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<EquityClientHolding> equityClientholding = JsonConvert.DeserializeObject<List<EquityClientHolding>>(data1);
            return Json(equityClientholding);
        }

        public IActionResult pspdspequitydetails(PSPDSPClientFlow dto)
        {
            if (HttpContext.Session.GetString("web_session_id") != null)
            {
                //dto = (RPT)IncomeDetails(dto);
                return View(dto);
            }
            return RedirectToAction("Index", "Login");
        }
        [HttpPost]
        public IActionResult pspdspequitydetails([FromBody] RPT rpt)
        {

            string url = _config.GetValue<string>("APIKey") + "Reports/pspdspequitydetails";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            PSPDSPClientFlow rd = JsonConvert.DeserializeObject<PSPDSPClientFlow>(dtoq);
            string serializeProfile = JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<PSPDSPClientFlow> pdcflow = JsonConvert.DeserializeObject<List<PSPDSPClientFlow>>(data1);
            return Json(data1);
        }

        [HttpPost]
        public IActionResult PspDspOutFlowDetails([FromBody] PSPDSPOUTFLOW rpt)
        {

            string url = _config.GetValue<string>("APIKey") + "Reports/PspDspOutFlowDetails";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            PSPDSPOUTFLOW rd = JsonConvert.DeserializeObject<PSPDSPOUTFLOW>(dtoq);
            rd.flow_type = rpt.flow_type;
            rd.summary = "1";
            rd.account_code = rpt.account_code;
            string serializeProfile = JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<PSPDSPOUTFLOW> pdoutflow = JsonConvert.DeserializeObject<List<PSPDSPOUTFLOW>>(data1);
            return Json(pdoutflow);
        }

      
        [HttpPost]
        public IActionResult PSPDSPEQUITYCLIENTDetails([FromBody] PSPDSPEQUITYCLIENTS rpt)
        {

            string url = _config.GetValue<string>("APIKey") + "Reports/PSPDSPEQUITYCLIENTDetails";
            

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<PSPDSPEQUITYCLIENTS> pdequityclient = JsonConvert.DeserializeObject<List<PSPDSPEQUITYCLIENTS>>(data1);
            return Json(pdequityclient);
        }

        [HttpPost]
        public IActionResult psp_dsp_eqt_current_fy_ledger([FromBody] psp_dsp_eqt_current_fy_ledger rpt)
        {

            string url = _config.GetValue<string>("APIKey") + "Reports/psp_dsp_eqt_current_fy_ledger";


            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_eqt_current_fy_ledger> pdequityclient = JsonConvert.DeserializeObject<List<psp_dsp_eqt_current_fy_ledger>>(data1);
            return Json(pdequityclient);
        }



        public IActionResult XIRRReports(RPT dto)
        {
            if (HttpContext.Session.GetString("web_session_id") != null)
            {
                //dto = (RPT)IncomeDetails(dto);
                return View("XIRR", dto);
                //return View(dto);
            }
            return RedirectToAction("Index", "Login");
        }
        public IActionResult XIRR_Summary(string acc_code = "0")
        {
            if (HttpContext.Session.GetString("web_session_id") != null)
            {
                ViewBag.Acc = acc_code;
                return View("XIRR_Summary");
            }
            return RedirectToAction("Index", "Login");
        }
        [HttpPost]
        public IActionResult PSPDSPEQUITYDEALERTRACKTDetails([FromBody] Equitydealertracksheet rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/PSPDSPEQUITYDEALERTRACKTDetails";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            Equitydealertracksheet rd = JsonConvert.DeserializeObject<Equitydealertracksheet>(dtoq);
            string serializeProfile = JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<Equitydealertracksheet> Equitydealertracksheet = JsonConvert.DeserializeObject<List<Equitydealertracksheet>>(data1);
            return Json(Equitydealertracksheet);
        }
        public IActionResult psp_dsp_equity_client_holding([FromBody] EquityClientHolding rpt)
        {

            string url = _config.GetValue<string>("APIKey") + "Reports/psp_dsp_equity_client_holding";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            EquityClientHolding rd = JsonConvert.DeserializeObject<EquityClientHolding>(dtoq);
            rd.account_code = rpt.account_code;
            rd.holding_type = rpt.holding_type;
            string serializeProfile = JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<EquityClientHolding> OpBal = JsonConvert.DeserializeObject<List<EquityClientHolding>>(data1);
            return Json(OpBal);
        }

        public IActionResult PerformanceReports(RPT dto)
        {
            if (HttpContext.Session.GetString("web_session_id") != null)
            {
                //dto = (RPT)IncomeDetails(dto);
                return View(dto);
            }
            return RedirectToAction("Index", "Login");
        }
        [HttpPost]
        public IActionResult psprptperformanceholdingreportDetails([FromBody] psp_rpt_performance_holding_report rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/psprptperformanceholdingreportDetails";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            psp_rpt_performance_holding_report rd = JsonConvert.DeserializeObject<psp_rpt_performance_holding_report>(dtoq);
            rd.FINYR = rpt.FINYR;
            rd.Family = rpt.Family;
            string serializeProfile = JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_rpt_performance_holding_report> psprptperformanceholding = JsonConvert.DeserializeObject<List<psp_rpt_performance_holding_report>>(data1);
            return Json(psprptperformanceholding);
        }
        public IActionResult PerformanceReportsNew(RPT dto)
        {
            if (HttpContext.Session.GetString("web_session_id") != null)
            {
                //dto = (RPT)IncomeDetails(dto);
                return View(dto);
            }
            return RedirectToAction("Index", "Login");
        }
        [HttpPost]
        public IActionResult psp_rpt_performance_holding_report_client_portal([FromBody] psp_rpt_performance_holding_report rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/psp_rpt_performance_holding_report_client_portal";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_rpt_performance_holding_report> psprptperformanceholding = JsonConvert.DeserializeObject<List<psp_rpt_performance_holding_report>>(data1);
            return Json(psprptperformanceholding);
        }
        [HttpPost]
        public IActionResult SetFamilyID([FromBody] FamilyList FamL)
        {
            string url = _config.GetValue<string>("APIKey") + "Exports/GetFamilyID";
            string serializeProfile = JsonConvert.SerializeObject(FamL);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            FamilyList FamID = JsonConvert.DeserializeObject<FamilyList>(data1);

            return Json(FamID);
        }

        [HttpPost]
        public IActionResult psprptperformanceholdingdirectequityreportDetails([FromBody] psp_rpt_performance_holding_direct_equity_report rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/psprptperformanceholdingdirectequityreportDetails";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            psp_rpt_performance_holding_direct_equity_report rd = JsonConvert.DeserializeObject<psp_rpt_performance_holding_direct_equity_report>(dtoq);

            rd.FamilyID = rpt.FamilyID;
            rd.FINYRData = rpt.FINYRData;
            rd.Subcategory = rpt.Subcategory;
            rd.ClientID = rpt.ClientID;
            rd.main_category = rpt.main_category;
            rd.source = rpt.source;
            string serializeProfile = JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);

            if (rpt.Subcategory == "Direct Equity" || rpt.Subcategory == "InvITs" || rpt.Subcategory == "ReITs")
            {
                List<psp_rpt_performance_holding_direct_equity_report> psprptperformanceholdingdirectequity = JsonConvert.DeserializeObject<List<psp_rpt_performance_holding_direct_equity_report>>(data1);
                return Json(psprptperformanceholdingdirectequity);
            }
            else if (rpt.Subcategory == "Equity MF" || rpt.Subcategory == "Debt MF")
            {
                List<psp_rpt_performance_holding_equity_mf_report> psprptperformanceholdingdirectequity = JsonConvert.DeserializeObject<List<psp_rpt_performance_holding_equity_mf_report>>(data1);
                return Json(psprptperformanceholdingdirectequity);
            }
            else if (rpt.Subcategory == "Bonds")
            {
                List<psp_dsp_performance_holding_bonds_report_cp> psprptperformanceholdingdirectequity = JsonConvert.DeserializeObject<List<psp_dsp_performance_holding_bonds_report_cp>>(data1);
                return Json(psprptperformanceholdingdirectequity);
            }
            else if (rpt.Subcategory == "Equity PMS")
            {
                List<psp_rpt_performance_holding_equity_pms_report_new_format> psprptperformanceholdingdirectequity = JsonConvert.DeserializeObject<List<psp_rpt_performance_holding_equity_pms_report_new_format>>(data1);
                return Json(psprptperformanceholdingdirectequity);
            }
            else if (rpt.main_category == "Other PMS")
            {
                List<psp_rpt_performance_holding_other_pms_report> psprptperformanceholdingdirectequity = JsonConvert.DeserializeObject<List<psp_rpt_performance_holding_other_pms_report>>(data1);
                return Json(psprptperformanceholdingdirectequity);
            }
            //else if (rpt.Subcategory == "Moat And Special Situations Portfolio" || rpt.Subcategory == "Emerging Corporates India Portfolio" || rpt.Subcategory == "Marcellus Consistent Compounders Portfolio" ||
            //        rpt.Subcategory == "Marcellus Little Champs Portfolio" || rpt.Subcategory == "Marcellus Kings Of Capital Portfolio" || rpt.Subcategory == "Buoyant Opportunities Scheme" ||
            //        rpt.Subcategory == "Buoyant Opportunities Strategy - Investor" || rpt.Subcategory == "GIRIK MULTICAP GROWTH EQUITY STRATEGY" || rpt.Subcategory == "GIRIK LIQUID STRATEGY" ||
            //        rpt.Subcategory == "InCred Healthcare Portfolio" || rpt.Subcategory == "WHITE OAK INDIA PIONEERS EQUITY PORTFOLIO")
            //{
            //    List<psp_rpt_performance_holding_other_pms_report> psprptperformanceholdingdirectequity = JsonConvert.DeserializeObject<List<psp_rpt_performance_holding_other_pms_report>>(data1);
            //    return Json(psprptperformanceholdingdirectequity);
            //}
            else if (rpt.Subcategory == "Fixed Deposit")
			{
				List<psp_dsp_fd_details> psprptperformanceholdingdirectequity = JsonConvert.DeserializeObject<List<psp_dsp_fd_details>>(data1);
				return Json(psprptperformanceholdingdirectequity);
			}
			return null;

        }

        [HttpPost]
        public IActionResult psprptperformanceholdingequitypmsreportnewformatDetails([FromBody] psp_rpt_performance_holding_equity_pms_report_new_format rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/psprptperformanceholdingequitypmsreportnewformatDetails";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            psp_rpt_performance_holding_equity_pms_report_new_format rd = JsonConvert.DeserializeObject<psp_rpt_performance_holding_equity_pms_report_new_format>(dtoq);

            string serializeProfile = JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_rpt_performance_holding_equity_pms_report_new_format> psprptperformanceholdingdirectequitypms = JsonConvert.DeserializeObject<List<psp_rpt_performance_holding_equity_pms_report_new_format>>(data1);
            return Json(psprptperformanceholdingdirectequitypms);
        }

        [HttpPost]
        public IActionResult psprptperformanceholdingequitymfreportDetails([FromBody] psp_rpt_performance_holding_equity_mf_report rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/psprptperformanceholdingequitymfreportDetails";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            psp_rpt_performance_holding_equity_mf_report rd = JsonConvert.DeserializeObject<psp_rpt_performance_holding_equity_mf_report>(dtoq);

            string serializeProfile = JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_rpt_performance_holding_equity_mf_report> psprptperformanceholdingdirectequitymf = JsonConvert.DeserializeObject<List<psp_rpt_performance_holding_equity_mf_report>>(data1);
            return Json(psprptperformanceholdingdirectequitymf);
        }

        [HttpPost]
        public IActionResult pspdspmfsipstpdetailDetails([FromBody] psp_dsp_mf_sip_stp_detail rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/pspdspmfsipstpdetailDetails";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            psp_dsp_mf_sip_stp_detail rd = JsonConvert.DeserializeObject<psp_dsp_mf_sip_stp_detail>(dtoq);

            rd.FamilyID = rpt.FamilyID;
            rd.FINYRData = rpt.FINYRData;
            rd.MainClientID = rpt.MainClientID;
            rd.tran_type = rpt.tran_type;

            if (rpt.Subcategory == "Equity MF")
            {
                rd.Subcategory = "Equity";
            }
            else if (rpt.Subcategory == "Debt MF")
            {
                rd.Subcategory = "Debt";
            }

            string serializeProfile = JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_mf_sip_stp_detail> pspdspmfsip = JsonConvert.DeserializeObject<List<psp_dsp_mf_sip_stp_detail>>(data1);
            return Json(pspdspmfsip);
        }

        [HttpPost]
        public IActionResult pspdspmfschemeallocationDetails([FromBody] psp_dsp_mf_scheme_allocation rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/pspdspmfschemeallocationDetails";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            psp_dsp_mf_scheme_allocation rd = JsonConvert.DeserializeObject<psp_dsp_mf_scheme_allocation>(dtoq);

            rd.FamilyID = rpt.FamilyID;
            rd.FINYRData = rpt.FINYRData;
            rd.MainClientID = rpt.MainClientID;
            if (rpt.Subcategory == "Equity MF")
            {
                rd.Subcategory = "Equity";
            }
            else if (rpt.Subcategory == "Debt MF")
            {
                rd.Subcategory = "Debt";
            }

            string serializeProfile = JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_mf_scheme_allocation> pspdspmfsschemeallocation = JsonConvert.DeserializeObject<List<psp_dsp_mf_scheme_allocation>>(data1);
            return Json(pspdspmfsschemeallocation);
        }

        [HttpPost]
        public IActionResult pspdspmfcategoryallocationDetails([FromBody] psp_dsp_mf_category_allocation rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/pspdspmfcategoryallocationDetails";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            psp_dsp_mf_category_allocation rd = JsonConvert.DeserializeObject<psp_dsp_mf_category_allocation>(dtoq);

            rd.FamilyID = rpt.FamilyID;
            rd.FINYRData = rpt.FINYRData;
            rd.MainClientID = rpt.MainClientID;
            if (rpt.Subcategory == "Equity MF")
            {
                rd.Subcategory = "Equity";
            }
            else if (rpt.Subcategory == "Debt MF")
            {
                rd.Subcategory = "Debt";
            }

            string serializeProfile = JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_mf_category_allocation> pspdspmfcategoryallocation = JsonConvert.DeserializeObject<List<psp_dsp_mf_category_allocation>>(data1);
            return Json(pspdspmfcategoryallocation);
        }

        [HttpPost]
        public IActionResult pspdspmfAMCallocationDetails([FromBody] psp_dsp_mf_AMC_allocation rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/pspdspmfAMCallocationDetails";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            psp_dsp_mf_AMC_allocation rd = JsonConvert.DeserializeObject<psp_dsp_mf_AMC_allocation>(dtoq);

            rd.FamilyID =  rpt.FamilyID;
            rd.FINYRData = rpt.FINYRData;
            rd.MainClientID = rpt.MainClientID;
            if(rpt.Subcategory == "Equity MF")
            {
                rd.Subcategory = "Equity";
            }
            else if(rpt.Subcategory == "Debt MF")
            {
                rd.Subcategory = "Debt";
            }

            string serializeProfile = JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_mf_AMC_allocation> pspdspmfamcallocation = JsonConvert.DeserializeObject<List<psp_dsp_mf_AMC_allocation>>(data1);
            return Json(pspdspmfamcallocation);
        }
        [HttpPost]
        public IActionResult psprptperformanceholdingdebtmfreportDetails([FromBody] psp_rpt_performance_holding_debt_mf_report rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/psprptperformanceholdingdebtmfreportDetails";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            psp_rpt_performance_holding_debt_mf_report rd = JsonConvert.DeserializeObject<psp_rpt_performance_holding_debt_mf_report>(dtoq);

            string serializeProfile = JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_rpt_performance_holding_debt_mf_report> psprptperformance = JsonConvert.DeserializeObject<List<psp_rpt_performance_holding_debt_mf_report>>(data1);
            return Json(psprptperformance);
        }
        [HttpPost]
        public IActionResult psprptperformanceholdingbondsreportDetails([FromBody] psp_rpt_performance_holding_bonds_report rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/psprptperformanceholdingbondsreportDetails";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            psp_rpt_performance_holding_bonds_report rd = JsonConvert.DeserializeObject<psp_rpt_performance_holding_bonds_report>(dtoq);

            string serializeProfile = JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_rpt_performance_holding_bonds_report> psprptperformanceholdingbonds = JsonConvert.DeserializeObject<List<psp_rpt_performance_holding_bonds_report>>(data1);
            return Json(psprptperformanceholdingbonds);
        }

        [HttpPost]
        public IActionResult psprptperformanceholdingbondsreportcashflowDetails([FromBody] psp_rpt_performance_holding_bonds_report_cashflow rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/psprptperformanceholdingbondsreportcashflowDetails";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            psp_rpt_performance_holding_bonds_report_cashflow rd = JsonConvert.DeserializeObject<psp_rpt_performance_holding_bonds_report_cashflow>(dtoq);

            rd.FamilyID = rpt.FamilyID;
            rd.FINYRData = rpt.FINYRData;
            rd.Subcategory = rpt.Subcategory;

            if (rpt.ClientID == "0")
            {
                rd.ClientID = "All Clients";
            }
            else
            {
                rd.ClientID = rpt.ClientID;
            }

            string serializeProfile = JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_rpt_performance_holding_bonds_report_cashflow> psprptperformanceholdingbondscashflow = JsonConvert.DeserializeObject<List<psp_rpt_performance_holding_bonds_report_cashflow>>(data1);
            return Json(psprptperformanceholdingbondscashflow);
        }

        [HttpPost]
        public IActionResult pspdspcurrentholdingdrilldownDetails([FromBody] psp_dsp_current_holding_drill_down rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/pspdspcurrentholdingdrilldownDetails";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            psp_dsp_current_holding_drill_down rd = JsonConvert.DeserializeObject<psp_dsp_current_holding_drill_down>(dtoq);
            rd.client_Code = rpt.client_Code;
            rd.script_code = rpt.script_code;
            string serializeProfile = JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_current_holding_drill_down> pspDspCurrentHoldingDrillDowns = JsonConvert.DeserializeObject<List<psp_dsp_current_holding_drill_down>>(data1);
            return Json(pspDspCurrentHoldingDrillDowns);
        }

        [HttpPost]
        public IActionResult pspdspinflowoutflowDetails([FromBody] psp_dsp_inflow_outflow_details rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/pspdspinflowoutflowDetails";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            psp_dsp_inflow_outflow_details rd = JsonConvert.DeserializeObject<psp_dsp_inflow_outflow_details>(dtoq);
            rd.trans_date1 = Convert.ToDateTime(rpt.trans_date);
            rd.trans_date = rd.trans_date1.ToString("yyyy-MM-dd");
            rd.flow_type = rpt.flow_type;
            rd.account_code = rpt.account_code;
            string serializeProfile = JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_inflow_outflow_details> pspdspinflowoutflow = JsonConvert.DeserializeObject<List<psp_dsp_inflow_outflow_details>>(data1);
            return Json(pspdspinflowoutflow);
        }

        [HttpPost]
        public IActionResult psprptclientperformancecheckDetails([FromBody] psp_rpt_client_performance_check rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/psprptclientperformancecheckDetails";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            psp_rpt_client_performance_check rd = JsonConvert.DeserializeObject<psp_rpt_client_performance_check>(dtoq);
            rd.FINYR = rpt.FINYR;
            rd.Client = rpt.Client;
            rd.Scrip_Code = rpt.Scrip_Code;
            rd.subcategory = rpt.subcategory;
            rd.asset_code = rpt.asset_code;
            rd.account_code = rpt.account_code;
            string serializeProfile = JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_rpt_client_performance_check> psprptclientperformancecheck = JsonConvert.DeserializeObject<List<psp_rpt_client_performance_check>>(data1);
            return Json(psprptclientperformancecheck);
        }
        [HttpPost]
        public IActionResult psp_dsp_mf_transction_details([FromBody] psp_dsp_mf_transction_details rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/psp_dsp_mf_transction_details";
            
            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_mf_transction_details> psprptclientperformancecheck = JsonConvert.DeserializeObject<List<psp_dsp_mf_transction_details>>(data1);
            return Json(psprptclientperformancecheck);
        }

        public IActionResult psp_dsp_liquiloan_investment([FromBody] psp_dsp_liquiloan_investment rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/psp_dsp_liquiloan_investment";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_liquiloan_investment> LiquiLoanInvest = JsonConvert.DeserializeObject<List<psp_dsp_liquiloan_investment>>(data1);
            return Json(LiquiLoanInvest);
        }
        
        public IActionResult psp_dsp_liquiloan_investor_dashboard([FromBody] psp_dsp_liquiloan_investor_dashboard rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/psp_dsp_liquiloan_investor_dashboard";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            psp_dsp_liquiloan_investor_dashboard LiquiLoanInvest = JsonConvert.DeserializeObject<psp_dsp_liquiloan_investor_dashboard>(data1);
            return Json(LiquiLoanInvest);
        }
        public IActionResult psp_dsp_liquiloan_investor_ledger([FromBody] psp_dsp_liquiloan_investor_ledger rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/psp_dsp_liquiloan_investor_ledger";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_liquiloan_investor_ledger> LiquiLoanInvest = JsonConvert.DeserializeObject<List<psp_dsp_liquiloan_investor_ledger>>(data1);
            return Json(LiquiLoanInvest);
        }
        public IActionResult psp_dsp_client_portal_dividend_piechart([FromBody] psp_dsp_client_portal_dividend_piechart rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/psp_dsp_client_portal_dividend_piechart";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_client_portal_dividend_piechart> Result = JsonConvert.DeserializeObject<List<psp_dsp_client_portal_dividend_piechart>>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult GetRMDetails([FromBody] psp_rpt_rm_details rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/psp_rpt_rm_details";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            psp_rpt_rm_details newobj = JsonConvert.DeserializeObject<psp_rpt_rm_details>(dtoq);

            newobj.Branch = rpt.Branch;
            newobj.Asset = rpt.Asset;
            newobj.RM = rpt.RM;

            string serializeProfile = JsonConvert.SerializeObject(newobj);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_rpt_rm_details> Result = JsonConvert.DeserializeObject<List<psp_rpt_rm_details>>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult psp_rpt_scripwise_client_holding_cp([FromBody] psp_rpt_scripwise_client_holding_cp rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/psp_rpt_scripwise_client_holding_cp";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_rpt_scripwise_client_holding_cp> Result = JsonConvert.DeserializeObject<List<psp_rpt_scripwise_client_holding_cp>>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult GetFamDetails([FromBody] psp_mis_rpt_branch_client_list rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/psp_mis_rpt_branch_client_list";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            psp_mis_rpt_branch_client_list newobj = JsonConvert.DeserializeObject<psp_mis_rpt_branch_client_list>(dtoq);

            newobj.Branch = rpt.Branch;

            string serializeProfile = JsonConvert.SerializeObject(newobj);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_mis_rpt_branch_client_list> Result = JsonConvert.DeserializeObject<List<psp_mis_rpt_branch_client_list>>(data1);
            return Json(Result);
        }
        [HttpGet]
        public IActionResult psp_dsp_aum_break_up()
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/psp_dsp_aum_break_up";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            psp_mis_rpt_branch_client_list newobj = JsonConvert.DeserializeObject<psp_mis_rpt_branch_client_list>(dtoq);

            string serializeProfile = JsonConvert.SerializeObject(newobj);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_aum_break_up> Result = JsonConvert.DeserializeObject<List<psp_dsp_aum_break_up>>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult psp_dsp_nri_client_data_entry_taxwithheld_details([FromBody] psp_dsp_nri_client_data_entry_taxwithheld_details rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/psp_dsp_nri_client_data_entry_taxwithheld_details";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_nri_client_data_entry_taxwithheld_details> Result = JsonConvert.DeserializeObject<List<psp_dsp_nri_client_data_entry_taxwithheld_details>>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult psp_rpt_nri_client_details([FromBody] psp_rpt_nri_client_details rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/psp_rpt_nri_client_details";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_rpt_nri_client_details> Result = JsonConvert.DeserializeObject<List<psp_rpt_nri_client_details>>(data1);
            return Json(Result);
        }

        [HttpPost]
        public IActionResult psp_rpt_nri_Highest_balance([FromBody] psp_rpt_nri_Highest_balance rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/psp_rpt_nri_Highest_balance";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_rpt_nri_Highest_balance> Result = JsonConvert.DeserializeObject<List<psp_rpt_nri_Highest_balance>>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult psp_rpt_nri_Highest_nav([FromBody] psp_rpt_nri_Highest_nav rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/psp_rpt_nri_Highest_nav";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_rpt_nri_Highest_nav> Result = JsonConvert.DeserializeObject<List<psp_rpt_nri_Highest_nav>>(data1);
            return Json(Result);
        }

        [HttpPost]
        public IActionResult psp_dsp_global_report_sub_category([FromBody] psp_dsp_global_report_sub_category rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/psp_dsp_global_report_sub_category";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_global_report_sub_category> Result = JsonConvert.DeserializeObject<List<psp_dsp_global_report_sub_category>>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult psp_dsp_global_report_dividend([FromBody] psp_dsp_global_report_dividend rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/psp_dsp_global_report_dividend";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_global_report_dividend> Result = JsonConvert.DeserializeObject<List<psp_dsp_global_report_dividend>>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult psp_dsp_global_report_trades([FromBody] psp_dsp_global_report_trades rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/psp_dsp_global_report_trades";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_global_report_trades> Result = JsonConvert.DeserializeObject<List<psp_dsp_global_report_trades>>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult psp_dsp_global_report_gain_loss([FromBody] psp_dsp_global_report_gain_loss rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/psp_dsp_global_report_gain_loss";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_global_report_gain_loss> Result = JsonConvert.DeserializeObject<List<psp_dsp_global_report_gain_loss>>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult psp_dsp_global_report_ledger([FromBody] psp_dsp_global_report_ledger rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/psp_dsp_global_report_ledger";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_global_report_ledger> Result = JsonConvert.DeserializeObject<List<psp_dsp_global_report_ledger>>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult psp_dsp_client_contact_details([FromBody] psp_dsp_client_contact_details rpt)
        {
            FamilyList Fml = new FamilyList();
            Fml.Family_Token = rpt.family_id;


            string Famurl = _config.GetValue<string>("APIKey") + "Exports/GetFamilyID";
            string FamProfile = JsonConvert.SerializeObject(Fml);
            string Famdata = EncryptionDecryption.Encrypt(FamProfile);
            EncryptData FamEndata = new EncryptData();
            FamEndata.EncryptObject = Famdata;
            string Famdataout = HttpCall.HttpPostMethod(Famurl, FamEndata);
            FamilyList FamResult = JsonConvert.DeserializeObject<FamilyList>(Famdataout);

            rpt.family_id = FamResult.Family_Id;


            string url = _config.GetValue<string>("APIKey") + "Reports/psp_dsp_client_contact_details";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_client_contact_details> Result = JsonConvert.DeserializeObject<List<psp_dsp_client_contact_details>>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult psp_dsp_family_details([FromBody] psp_dsp_family_details rpt)
        {
            FamilyList Fml = new FamilyList();
            Fml.Family_Token = rpt.LoginId;


            string Famurl = _config.GetValue<string>("APIKey") + "Exports/GetFamilyID";
            string FamProfile = JsonConvert.SerializeObject(Fml);
            string Famdata = EncryptionDecryption.Encrypt(FamProfile);
            EncryptData FamEndata = new EncryptData();
            FamEndata.EncryptObject = Famdata;
            string Famdataout = HttpCall.HttpPostMethod(Famurl, FamEndata);
            FamilyList FamResult = JsonConvert.DeserializeObject<FamilyList>(Famdataout);

            rpt.LoginId = FamResult.Family_Id;


            string url = _config.GetValue<string>("APIKey") + "Reports/psp_dsp_family_details";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            psp_dsp_family_details Result = JsonConvert.DeserializeObject<psp_dsp_family_details>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult psp_rpt_cdsl_holding_report_familyhead([FromBody] psp_rpt_cdsl_holding_report_familyhead rpt)
        {
            string Accessurl = _config.GetValue<string>("APIKey") + "Services/psp_dsp_user_access_token";

            psp_dsp_user_access_token postdata = new psp_dsp_user_access_token();

            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            UserLogin udata = JsonConvert.DeserializeObject<UserLogin>(dtoq);

            postdata.login_id = udata.Id;
            postdata.web_session_id = HttpContext.Session.GetString("web_session_id");

            bool validSession = EncryptionDecryption.AccessToken(postdata, Accessurl);

            if (HttpContext.Session.GetString("web_session_id") != null && validSession)
            {
                rpt.AccessToken = HttpContext.Session.GetString("web_session_id");

                string url = _config.GetValue<string>("APIKey") + "Reports/psp_rpt_cdsl_holding_report_familyhead";

                string serializeProfile = JsonConvert.SerializeObject(rpt);
                string data = EncryptionDecryption.Encrypt(serializeProfile);
                EncryptData Endata = new EncryptData();
                Endata.EncryptObject = data;
                string data1 = HttpCall.HttpPostMethod(url, Endata);
                List<psp_rpt_cdsl_holding_report_familyhead> Result = JsonConvert.DeserializeObject<List<psp_rpt_cdsl_holding_report_familyhead>>(data1);
                return Json(Result);
            }
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public IActionResult psp_rpt_client_bond_mismatch([FromBody] psp_rpt_client_equity_bond_mismatch rpt) //Same Model for both SPs
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/psp_rpt_client_bond_mismatch";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_rpt_client_equity_bond_mismatch> Result = JsonConvert.DeserializeObject<List<psp_rpt_client_equity_bond_mismatch>>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult psp_rpt_client_script_mismatch([FromBody] psp_rpt_client_equity_bond_mismatch rpt) //Same Model for both SPs
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/psp_rpt_client_script_mismatch";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_rpt_client_equity_bond_mismatch> Result = JsonConvert.DeserializeObject<List<psp_rpt_client_equity_bond_mismatch>>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult psp_dsp_exchangewise_brokerage([FromBody] Brokerage_MIS rpt) //Same Model for both SPs
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/psp_dsp_exchangewise_brokerage";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<Brokerage_MIS> Result = JsonConvert.DeserializeObject<List<Brokerage_MIS>>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult psp_dsp_branchwise_brokerage([FromBody] Brokerage_MIS rpt) //Same Model for both SPs
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/psp_dsp_branchwise_brokerage";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<Brokerage_MIS> Result = JsonConvert.DeserializeObject<List<Brokerage_MIS>>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult psp_amd_model_portfolio_cp([FromBody] psp_amd_model_portfolio_cp pampc)
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_amd_model_portfolio_cp";

            string serializeProfile = JsonConvert.SerializeObject(pampc);

            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            psp_amd_model_portfolio_cp Result = JsonConvert.DeserializeObject<psp_amd_model_portfolio_cp>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult psp_dsp_script_isin_cp([FromBody] psp_dsp_script_isin_cp pampc)
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_script_isin_cp";

            string serializeProfile = JsonConvert.SerializeObject(pampc);

            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            psp_dsp_script_isin_cp Result = JsonConvert.DeserializeObject<psp_dsp_script_isin_cp>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult psp_dsp_brach_family_details([FromBody] psp_dsp_brach_family_details pampc)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/psp_dsp_brach_family_details";

            string serializeProfile = JsonConvert.SerializeObject(pampc);

            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_brach_family_details> Result = JsonConvert.DeserializeObject<List<psp_dsp_brach_family_details>>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult psp_dsp_client_portal_mis_recent_client_activity([FromBody] psp_dsp_client_portal_mis_recent_client_activity pampc)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/psp_dsp_client_portal_mis_recent_client_activity";

            string serializeProfile = JsonConvert.SerializeObject(pampc);

            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_client_portal_mis_recent_client_activity> Result = JsonConvert.DeserializeObject<List<psp_dsp_client_portal_mis_recent_client_activity>>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult psp_dsp_client_portfolio_interaction_history([FromBody] psp_dsp_client_portfolio_interaction_history pampc)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/psp_dsp_client_portfolio_interaction_history";

            string serializeProfile = JsonConvert.SerializeObject(pampc);

            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_client_portfolio_interaction_history> Result = JsonConvert.DeserializeObject<List<psp_dsp_client_portfolio_interaction_history>>(data1);
            return Json(Result);
        }

        [HttpPost]
        public IActionResult psp_amd_client_portfolio_interaction_add_remarks([FromBody] psp_amd_client_portfolio_interaction_add_remarks pampc)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/psp_amd_client_portfolio_interaction_add_remarks";

            pampc.remarks = HttpUtility.HtmlEncode(pampc.remarks);

            string serializeProfile = JsonConvert.SerializeObject(pampc);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_amd_client_portfolio_interaction_add_remarks> Result = JsonConvert.DeserializeObject<List<psp_amd_client_portfolio_interaction_add_remarks>>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult psp_dsp_client_script_trades([FromBody] psp_dsp_client_script_trades pampc)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/psp_dsp_client_script_trades";

            string serializeProfile = JsonConvert.SerializeObject(pampc);

            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_client_script_trades> Result = JsonConvert.DeserializeObject<List<psp_dsp_client_script_trades>>(data1);
            return Json(Result);
        }

        [HttpPost]
        public IActionResult psp_dsp_mst_index_cp([FromBody] psp_dsp_mst_index_cp pampc)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/psp_dsp_mst_index_cp";

            string serializeProfile = JsonConvert.SerializeObject(pampc);

            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_mst_index_cp> Result = JsonConvert.DeserializeObject<List<psp_dsp_mst_index_cp>>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult psp_dsp_master_list_cp([FromBody] psp_dsp_master_list_cp pampc)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/psp_dsp_master_list_cp";

            string serializeProfile = JsonConvert.SerializeObject(pampc);

            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_master_list_cp> Result = JsonConvert.DeserializeObject<List<psp_dsp_master_list_cp>>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult psp_amd_index_currency_master_cp([FromBody] psp_amd_index_currency_master_cp pampc)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/psp_amd_index_currency_master_cp";

            string serializeProfile = JsonConvert.SerializeObject(pampc);

            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_amd_index_currency_master_cp> Result = JsonConvert.DeserializeObject<List<psp_amd_index_currency_master_cp>>(data1);
            return Json(Result);
        }

        [HttpPost]
        public IActionResult psp_dsp_equity_client_fy_dividend([FromBody] psp_dsp_equity_client_fy_dividend pampc)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/psp_dsp_equity_client_fy_dividend";

            string serializeProfile = JsonConvert.SerializeObject(pampc);

            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_equity_client_fy_dividend> Result = JsonConvert.DeserializeObject<List<psp_dsp_equity_client_fy_dividend>>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult psp_rpt_detailed_realised_gain_loss([FromBody] psp_rpt_detailed_realised_gain_loss pampc)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/psp_rpt_detailed_realised_gain_loss";

            string serializeProfile = JsonConvert.SerializeObject(pampc);

            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_rpt_detailed_realised_gain_loss> Result = JsonConvert.DeserializeObject<List<psp_rpt_detailed_realised_gain_loss>>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult psp_dsp_ws_client_list([FromBody] psp_dsp_ws_client_list rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/psp_dsp_ws_client_list";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_ws_client_list> Result = JsonConvert.DeserializeObject<List<psp_dsp_ws_client_list>>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult psp_dsp_ws_download_report_list([FromBody] psp_dsp_ws_download_report_list rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/psp_dsp_ws_download_report_list";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_ws_download_report_list> Result = JsonConvert.DeserializeObject<List<psp_dsp_ws_download_report_list>>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult psp_dsp_download_ReportCriteria([FromBody] psp_dsp_download_ReportCriteria rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/psp_dsp_download_ReportCriteria";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_download_ReportCriteria> Result = JsonConvert.DeserializeObject<List<psp_dsp_download_ReportCriteria>>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult psp_dsp_other_pms_report_parameter([FromBody] psp_dsp_other_pms_report_parameter rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/psp_dsp_other_pms_report_parameter";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_other_pms_report_parameter> Result = JsonConvert.DeserializeObject<List<psp_dsp_other_pms_report_parameter>>(data1);

            string apiURL = Result[0].APIURL;
            string key = Result[0].API_Key;
            string reportcriteria = rpt.reportCriteria;
            string Filepath = rpt.downloadedFilepath;
            string report_id = rpt.reportID;
            string LoginId = rpt.Login_Id;
            string client_name = rpt.client_name;
            string Report_name = rpt.Report_name;

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            HttpClient client = new HttpClient(clientHandler);
            client.BaseAddress = new Uri(apiURL);

            //client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", key);

            var requestJson = reportcriteria;
            var content = new StringContent(requestJson, Encoding.UTF8, "application/json");
            var executeresponse = client.PostAsync("reports/executeDynamic", content).Result;
            string jsonString = executeresponse.Content.ReadAsStringAsync().Result;

            var message = JsonConvert.DeserializeObject<executeReportResponse>(jsonString).msg.ToString();
            var status = JsonConvert.DeserializeObject<executeReportResponse>(jsonString).status.ToString();

            if (message == "Success" && status == "true")
            {
                var responsfile = JsonConvert.DeserializeObject<executeReportResponse>(jsonString).data.ToString();
                var filename = responsfile.ToString();

                string param = "fileName=" + filename;
                //Filepath = @"D:\\Projects\\WSAPI\\WSAPI\\bin\\Debug\\Report\";
                Filepath = _config.GetValue<string>("WSReportDownloadpath");
                HttpResponseMessage ReportdownloadresponseData = client.GetAsync("download?" + param, HttpCompletionOption.ResponseContentRead).Result;

                WebClient webClient = new WebClient();
                webClient.Headers.Add("Authorization", "Bearer " + key);

                string guid = Guid.NewGuid().ToString();
                string Filename = guid;

                webClient.DownloadFile(apiURL + "download?" + param, Filepath + guid + ".pdf");

                psp_amd_WS_report_download pawr = new psp_amd_WS_report_download();

                string url3 = _config.GetValue<string>("APIKey") + "Reports/psp_amd_WS_report_download";

                pawr.description = Report_name + " for " + client_name;
                pawr.Report_id = report_id;
                pawr.Login_id = LoginId;
                pawr.reportCriteria = reportcriteria;
                pawr.filename = Filename + ".pdf";


                string serializeProfile1 = JsonConvert.SerializeObject(pawr);

                string data3 = EncryptionDecryption.Encrypt(serializeProfile1);
                EncryptData Endata1 = new EncryptData();
                Endata1.EncryptObject = data3;
                string data4 = HttpCall.HttpPostMethod(url3, Endata1);
                List<psp_amd_WS_report_download> Result1 = JsonConvert.DeserializeObject<List<psp_amd_WS_report_download>>(data4);

                psp_amd_WS_report_download objfile = new psp_amd_WS_report_download();
                objfile.filename = Filename;

                return Json(objfile);
            }
            else
            {
                psp_amd_WS_report_download objmsg = new psp_amd_WS_report_download();
                objmsg.Error_msg = message;
                objmsg.status = status;
                return Json(objmsg);
            }
        }

        [HttpPost]
        public IActionResult psp_dsp_Nri_bank_interest_details_drill([FromBody] psp_dsp_Nri_bank_interest_details_drill pampc)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/psp_dsp_Nri_bank_interest_details_drill";

            string serializeProfile = JsonConvert.SerializeObject(pampc);

            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_Nri_bank_interest_details_drill> Result = JsonConvert.DeserializeObject<List<psp_dsp_Nri_bank_interest_details_drill>>(data1);
            return Json(Result);
        }

        [HttpPost]
        public IActionResult psp_rpt_nri_client_taxwithheld_drill([FromBody] psp_rpt_nri_client_taxwithheld_drill pampc)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/psp_rpt_nri_client_taxwithheld_drill";

            string serializeProfile = JsonConvert.SerializeObject(pampc);

            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_rpt_nri_client_taxwithheld_drill> Result = JsonConvert.DeserializeObject<List<psp_rpt_nri_client_taxwithheld_drill>>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult psp_rpt_nri_client_dividend([FromBody] psp_rpt_nri_client_dividend pampc)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/psp_rpt_nri_client_dividend";

            string serializeProfile = JsonConvert.SerializeObject(pampc);

            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_rpt_nri_client_dividend> Result = JsonConvert.DeserializeObject<List<psp_rpt_nri_client_dividend>>(data1);
            return Json(Result);
        }

        public IActionResult HoldingReport(RPT dto)
        {
            if (HttpContext.Session.GetString("web_session_id") != null)
            {
                //dto = (RPT)IncomeDetails(dto);
                return View(dto);
            }
            return RedirectToAction("Index", "Login");
        }
        [HttpPost]
        public IActionResult HoldingSummary([FromBody] HoldingSummary rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/HoldingSummary";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<HoldingSummary> Result = JsonConvert.DeserializeObject<List<HoldingSummary>>(data1);
            return Json(Result);
        }
		public IActionResult AssetAllocationReport(RPT dto)
		{
			if (HttpContext.Session.GetString("web_session_id") != null)
			{
				//dto = (RPT)IncomeDetails(dto);
				return View(dto);
			}
			return RedirectToAction("Index", "Login");
		}
        [HttpPost]
        public IActionResult AssetAllocation([FromBody] psp_rpt_asset_allocation rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/AssetAllocation";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_rpt_asset_allocation> Result = JsonConvert.DeserializeObject<List<psp_rpt_asset_allocation>>(data1);

            if (Result.Count > 0)
            {
                List<psp_rpt_asset_allocation> SummaryData = Result.GroupBy(x => x.category)
                  .Select(y =>
                  new psp_rpt_asset_allocation
                  {
                      display_category = y.First().display_category_summary,
                      //display_category = (y.First().display_category.IndexOf("-") >= 0) ? y.First().display_category.Substring(0, y.First().display_category.IndexOf("-")) : y.First().display_category,
                      Market_value = y.Sum(s => s.Market_value),
                      display_order = y.First().display_order,
                      PercentageOfPortfolio = y.Sum(s => s.PercentageOfPortfolio)
                  }
                  ).ToList();
                SummaryData.Add(new psp_rpt_asset_allocation
                {
                    display_category = "Total",
                    Market_value = SummaryData.Sum(s => s.Market_value),
                    display_order = "99",
                    PercentageOfPortfolio = SummaryData.Sum(s => s.PercentageOfPortfolio)
                });

                List<psp_rpt_asset_allocation> ClientData = Result.GroupBy(x => x.main_client_id)
                  .Select(y =>
                  new psp_rpt_asset_allocation
                  {
                      client_name = y.First().client_name,
                      pan = y.First().pan,
                      Market_value = y.Sum(s => s.Market_value),
                      PercentageOfPortfolio = y.Sum(s => s.PercentageOfPortfolio)
                  }
                  ).ToList();

                ClientData.Add(new psp_rpt_asset_allocation
                {
                    client_name = "Total",
                    Market_value = ClientData.Sum(s => s.Market_value),
                    PercentageOfPortfolio = ClientData.Sum(s => s.PercentageOfPortfolio)
                });

                List<psp_rpt_asset_allocation> ClientCategoryData = Result.GroupBy(x => new { x.client_name, x.main_category, x.display_category })
                  .Select(y =>
                  new psp_rpt_asset_allocation
                  {
                      Mint_client_name = y.First().Mint_client_name,
                      category = y.First().category,
                      main_category = y.First().main_category,
                      client_id = y.First().client_id,
                      account_code = y.First().account_code,
                      main_client_id = y.First().main_client_id,
                      pan = y.First().pan,
                      client_name = y.First().client_name,
                      display_category = y.First().display_category,
                      display_order = y.First().display_order,
                      source = y.First().source,
                      Market_value = y.Sum(s => s.Market_value),
                      hld_per_clientwise = y.Sum(s => s.hld_per_clientwise)
                  }
                  ).ToList();

                List<psp_rpt_asset_allocation> ClientCategoryDataTotal = ClientCategoryData.GroupBy(x => new { x.client_name, x.main_category })
                  .Select(y =>
                  new psp_rpt_asset_allocation
                  {
                      client_name = y.First().client_name,
                      display_category = y.First().main_category + " Total",
                      main_category = "ZZZ",
                      display_order = y.First().display_order,
                      Market_value = y.Sum(s => s.Market_value),
                      hld_per_clientwise = y.Sum(s => s.hld_per_clientwise)
                  }
                  ).ToList();

                List<psp_rpt_asset_allocation> ClientCategoryDataGrandTotal = ClientCategoryData.GroupBy(x => new { x.client_name })
                 .Select(y =>
                 new psp_rpt_asset_allocation
                 {
                     client_name = y.First().client_name,
                     display_category = "Grand Total",
                     main_category = "XXX",
                     display_order = "99",
                     Market_value = y.Sum(s => s.Market_value),
                     hld_per_clientwise = y.Sum(s => s.hld_per_clientwise)
                 }
                 ).ToList();

                List<psp_rpt_asset_allocation> newLi = ClientCategoryData.Concat(ClientCategoryDataTotal).Concat(ClientCategoryDataGrandTotal).ToList();

                rpt.sql_message = "Data Found.";
                rpt.sql_status = "Success";
                var jsonresult = new
                {
                    family = Result[0].family_name,
                    family_id = Result[0].family_id,
                    SummaryData = SummaryData.OrderBy(x => x.display_order),
                    ClientData = ClientData,
                    ClientCategoryData = newLi.OrderBy(x => x.client_name).ThenBy(x => x.display_order).ThenBy(x => x.main_category),
                    sql_message = rpt.sql_message,
                    sql_status = rpt.sql_status
                };


                return Json(jsonresult);
            }
            else
            {
                rpt.sql_message = "No Data Found.";
                rpt.sql_status = "Fail";
                return Json(rpt);
            }
        }

        public IActionResult psp_dsp_clientwise_holding_list([FromBody] psp_dsp_clientwise_holding_list rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/psp_dsp_clientwise_holding_list";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_clientwise_holding_list> Result = JsonConvert.DeserializeObject<List<psp_dsp_clientwise_holding_list>>(data1);
            return Json(Result);
        }
        public IActionResult psp_dsp_direct_equity_top_holding([FromBody] psp_dsp_direct_equity_top_holding rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/psp_dsp_direct_equity_top_holding";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_direct_equity_top_holding> Result = JsonConvert.DeserializeObject<List<psp_dsp_direct_equity_top_holding>>(data1);
            return Json(Result);
        }
        public IActionResult psp_dsp_top_clients_AUMwise([FromBody] psp_dsp_top_clients_AUMwise rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/psp_dsp_top_clients_AUMwise";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_top_clients_AUMwise> Result = JsonConvert.DeserializeObject<List<psp_dsp_top_clients_AUMwise>>(data1);
            return Json(Result);
        }

        public IActionResult psp_dsp_mapping_client_list([FromBody] psp_dsp_mapping_client_list rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/psp_dsp_mapping_client_list";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_mapping_client_list> Result = JsonConvert.DeserializeObject<List<psp_dsp_mapping_client_list>>(data1);
            return Json(Result);
        }

        [HttpPost]
        public IActionResult psp_dsp_research_view_given_brokerage([FromBody] psp_dsp_research_view_given_brokerage rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/psp_dsp_research_view_given_brokerage";

            

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_research_view_given_brokerage> Result = JsonConvert.DeserializeObject<List<psp_dsp_research_view_given_brokerage>>(data1);
            return Json(Result);
        }

        [HttpPost]
        public IActionResult psp_dsp_capital_gain_report([FromBody] psp_dsp_capital_gain_report rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/psp_dsp_capital_gain_report";


            rpt.access_token = HttpContext.Session.GetString("web_session_id");

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_capital_gain_report> Result = JsonConvert.DeserializeObject<List<psp_dsp_capital_gain_report>>(data1);
            return Json(Result);
        }

        [HttpPost]
        public IActionResult psp_dsp_family_equity_client_list([FromBody] psp_dsp_family_equity_client_list rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/psp_dsp_family_equity_client_list";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_family_equity_client_list> Result = JsonConvert.DeserializeObject<List<psp_dsp_family_equity_client_list>>(data1);
            return Json(Result);
        }

        [HttpPost]
        public IActionResult psp_dsp_eq_client_scrip_list([FromBody] psp_dsp_eq_client_scrip_list rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/psp_dsp_eq_client_scrip_list";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_eq_client_scrip_list> Result = JsonConvert.DeserializeObject<List<psp_dsp_eq_client_scrip_list>>(data1);
            return Json(Result);
        }



        static void LogSourceDetails(bool condition, [CallerMemberName] string member = "", [CallerFilePath] string filepath = "",
            [CallerLineNumber] int line = 0, [CallerArgumentExpression(nameof(condition))] string expression = "")
        {
            Debug.WriteLine(string.Format(
            "[{0}]\n {1} on line {2}. Expression: {3}",
            filepath, member, line, expression));
            Debug.Close();
        }
    }
}
