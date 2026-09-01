using Grpc.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OfficeOpenXml.FormulaParsing.LexicalAnalysis;
using PieReports.Encryption_Decryption;
using PieReports.Filters;
using PieReports.Models;
using PieReports.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ViewModel.ClientComms;
using ViewModel.Login;
using ViewModel.Reports;
using ViewModel.Reports.PMS;
using ViewModel.Shared;

namespace PieReports.Controllers
{
    
    [Controller]
    public class PieController : Controller
    {
        private readonly ILogger<PieController> _logger;
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _clientFactory;
        public PieController(ILogger<PieController> logger,
           IConfiguration config, IHttpClientFactory clientFactory)
        {
            _logger = logger; 
            _config = config;
            _clientFactory = clientFactory;
        }

        public string FromBase64String(string inputdata)
        {
            byte[] decodedBytes = Convert.FromBase64String(inputdata);
            return Encoding.UTF8.GetString(decodedBytes);
        }
        public static smsXmlResponse LoadFromXMLString(string xmlText)
        {
            using (var stringReader = new System.IO.StringReader(xmlText))
            {
                var serializer = new XmlSerializer(typeof(smsXmlResponse));
                return serializer.Deserialize(stringReader) as smsXmlResponse;
            }
        }

        public IActionResult Dev_Test()
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

        public IActionResult GetCaptchaImage(string t)
        {
            int width = 180;
            int height = 50;
            var captchaCode = Captcha.GenerateCaptchaCode();
            var result = Captcha.GenerateCaptchaImage(width, height, captchaCode);
            HttpContext.Session.SetString("CaptchaCode", EncryptionDecryption.Encrypt(result.CaptchaCode));
            Stream s = new MemoryStream(result.CaptchaByteData);
            return new FileStreamResult(s, "image/png");
        }

        [ServiceFilter(typeof(SessionTimeoutFilter))]
        public IActionResult PieReport(bool dspFlag = false, string dspMessage = "")
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_user_access_token";

            psp_dsp_user_access_token postdata = new psp_dsp_user_access_token();

            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            UserLogin udata = JsonConvert.DeserializeObject<UserLogin>(dtoq);

            postdata.login_id = udata.Id;
            postdata.web_session_id = HttpContext.Session.GetString("web_session_id") ?? "0";

            bool validSession = EncryptionDecryption.AccessToken(postdata, url);

            if (HttpContext.Session.GetString("web_session_id") != null && validSession)
            {
                Dashboard dto = new Dashboard();
                dto = (Dashboard)Finyears(dto);
                dto.familyLists = FamilyDetails(dto);
                string v2 = JsonConvert.SerializeObject(dto);
                TempData["myDeshbordData"] = v2;

                dto.LoginModalDisplay = dspFlag;
                dto.LoginModalMessage = dspMessage;

                return View(dto);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        public IActionResult Contact_Us()
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
        public IActionResult Download_Reports()
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

        public IActionResult IndexAndCurrencyMaster()
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

        public IActionResult BackOffice_KYC()
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
        public IActionResult KYC_Update()
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
        public IActionResult UST_DataEntry()
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

        public IActionResult UST_Report()
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

        public IActionResult Bank_Master()
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
        public IActionResult KYC_History()
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
        public IActionResult KYC_Log()
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

        public IActionResult Family_Contact_Update()
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
        public IActionResult Profile_Update()
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
        public IActionResult UpdateAIF()
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
        public IActionResult Surveys()
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
        public IActionResult ClientSatisfactionSurvey()
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
        public IActionResult SurveyResponses()
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
        public IActionResult SurveyDump()
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

        public IActionResult ProcessLog()
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

        public IActionResult Family_Mapping()
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
                string branchurl = _config.GetValue<string>("APIKey") + "Dashboard/psp_dsp_branch_list";

                psp_dsp_branch_list branchObj = new psp_dsp_branch_list();
                branchObj.LoginId = "279195";
                branchObj.cat_type = "2";

                string serializeProfile = JsonConvert.SerializeObject(branchObj);

                string data = EncryptionDecryption.Encrypt(serializeProfile);
                EncryptData Endata = new EncryptData();
                Endata.EncryptObject = data;
                string data1 = HttpCall.HttpPostMethod(branchurl, Endata);
                List<psp_dsp_branch_list> BranchResult = JsonConvert.DeserializeObject<List<psp_dsp_branch_list>>(data1);


                string rmurl = _config.GetValue<string>("APIKey") + "Dashboard/psp_dsp_all_rm_list";

                psp_dsp_all_rm_list rmlistObj = new psp_dsp_all_rm_list();
                rmlistObj.loginid = "279195";

                string serializeProfile2 = JsonConvert.SerializeObject(rmlistObj);

                string data2 = EncryptionDecryption.Encrypt(serializeProfile);
                EncryptData Endata2 = new EncryptData();
                Endata2.EncryptObject = data2;
                string data3 = HttpCall.HttpPostMethod(rmurl, Endata);
                List<psp_dsp_all_rm_list> RMResult = JsonConvert.DeserializeObject<List<psp_dsp_all_rm_list>>(data3);

                familyMappingDisplay jsonDataBoj = new familyMappingDisplay { branchList = BranchResult, RMList = RMResult };

                return View(jsonDataBoj);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public IActionResult PMS_Reports()
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

        public IActionResult CommonReports()
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

        public IActionResult SearchClient()
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_user_access_token";

            psp_dsp_user_access_token postdata = new psp_dsp_user_access_token();

            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            UserLogin udata = JsonConvert.DeserializeObject<UserLogin>(dtoq);

            postdata.login_id = udata.Id;
            postdata.web_session_id = HttpContext.Session.GetString("web_session_id") ?? "0";

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
        

        public IActionResult WS_Report_Download()
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

        public IActionResult MF_Report_Download()
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

        public IActionResult DormantAccount()
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_user_access_token";

            psp_dsp_user_access_token postdata = new psp_dsp_user_access_token();

            if (TempData["myFinyearsdata"] != null)
            {
                var dtoq = TempData["myFinyearsdata"].ToString();
                TempData.Keep("myFinyearsdata");
                UserLogin udata = JsonConvert.DeserializeObject<UserLogin>(dtoq);

                postdata.login_id = udata.Id;
                postdata.web_session_id = HttpContext.Session.GetString("web_session_id");

                bool validSession = EncryptionDecryption.AccessToken(postdata, url);

                if (HttpContext.Session.GetString("web_session_id") != null && validSession)
                {
                    string dataurl = _config.GetValue<string>("APIKey") + "Services/psp_dsp_client_coms_dormant_account";

                    psp_dsp_client_coms_dormant_account rpt = new psp_dsp_client_coms_dormant_account()
                    {
                        loginid = udata.Id.ToString()
                    };

                    string serializeProfile = JsonConvert.SerializeObject(rpt);
                    string data = EncryptionDecryption.Encrypt(serializeProfile);
                    EncryptData Endata = new EncryptData();
                    Endata.EncryptObject = data;
                    string data1 = HttpCall.HttpPostMethod(dataurl, Endata);
                    List<psp_dsp_client_coms_dormant_account> Result = JsonConvert.DeserializeObject<List<psp_dsp_client_coms_dormant_account>>(data1);

                    return View(Result);
                }
                else
                {
                    return RedirectToAction("Index", "Login", new { msg = "Invalid Session." });
                }
            }
            else
            {
                return RedirectToAction("Index", "Login", new { msg = "Session Expired." });
            }
        }
        public IActionResult ClientCommsException()
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
                string dataurl = _config.GetValue<string>("APIKey") + "Services/psp_dsp_client_coms_exceptions";

                //psp_dsp_client_coms_exception_list

                psp_dsp_client_coms_exceptions rpt = new psp_dsp_client_coms_exceptions()
                {
                    loginid = udata.Id.ToString()
                };

                string serializeProfile = JsonConvert.SerializeObject(rpt);
                string data = EncryptionDecryption.Encrypt(serializeProfile);
                EncryptData Endata = new EncryptData();
                Endata.EncryptObject = data;
                string data1 = HttpCall.HttpPostMethod(dataurl, Endata);
                List<psp_dsp_client_coms_exceptions> Result = JsonConvert.DeserializeObject<List<psp_dsp_client_coms_exceptions>>(data1);

                string exceplisturl = _config.GetValue<string>("APIKey") + "Services/psp_dsp_client_coms_exception_list";

                string serializeProfileLi = JsonConvert.SerializeObject(rpt);
                string dataLi = EncryptionDecryption.Encrypt(serializeProfileLi);
                EncryptData EndataLi = new EncryptData();
                EndataLi.EncryptObject = data;
                string data1Li = HttpCall.HttpPostMethod(exceplisturl, EndataLi);
                List<psp_dsp_client_coms_exception_list> ResultLi = JsonConvert.DeserializeObject<List<psp_dsp_client_coms_exception_list>>(data1Li);

                coms_exceptions_model coms_Exceptions_Model = new coms_exceptions_model() { 
                    data_list = Result,
                    exception_reason_list = ResultLi
                };

                return View(coms_Exceptions_Model);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        public IActionResult ClientCommsLog()
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_user_access_token";

            psp_dsp_user_access_token postdata = new psp_dsp_user_access_token();

            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            UserLogin udata = JsonConvert.DeserializeObject<UserLogin>(dtoq);

            postdata.login_id = udata.Id;
            postdata.web_session_id = HttpContext.Session.GetString("web_session_id") ?? "0";

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

        public IActionResult OutstandingDebit()
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
                //string dataurl = _config.GetValue<string>("APIKey") + "Services/psp_dsp_client_coms_outstanding_debit_send_mail";

                ////psp_dsp_client_coms_exception_list

                //psp_dsp_client_coms_outstanding_debit_send_mail rpt = new psp_dsp_client_coms_outstanding_debit_send_mail()
                //{
                //    login_id = udata.Id.ToString()
                //};

                //string serializeProfile = JsonConvert.SerializeObject(rpt);
                //string data = EncryptionDecryption.Encrypt(serializeProfile);
                //EncryptData Endata = new EncryptData();
                //Endata.EncryptObject = data;
                //string data1 = HttpCall.HttpPostMethod(dataurl, Endata);
                //List<psp_dsp_client_coms_outstanding_debit_send_mail> Result = JsonConvert.DeserializeObject<List<psp_dsp_client_coms_outstanding_debit_send_mail>>(data1);

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [ServiceFilter(typeof(SessionTimeoutFilter))]
        public IActionResult EqSettlement()
        {
            return View();   
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> uploadEqSetData(EquitySettlement model)
        {
            IFormFile uploadedFile = model.reportFile;
            string fileGuid = Guid.NewGuid().ToString();
            string ogFileName = uploadedFile.FileName;

            //Getting file meta data
            string fileName = Path.GetFileName(uploadedFile.FileName);
            string newfilename = Guid.NewGuid().ToString() + Path.GetExtension(fileName);
            string Filestoragepath = _config.GetValue<string>("EqSettlementFileUploadPath");
            string filePath = Path.Combine(Filestoragepath, newfilename);
            
            try
            {
                List<EqSetDataModel> dataObj = ExcelUtil.ToList<EqSetDataModel>(ExcelUtil.ReadExcelFile(model.reportFile));

                string jsonString = JsonConvert.SerializeObject(dataObj);
                string dtoq = TempData["myFinyearsdata"].ToString();
                TempData.Keep("myFinyearsdata");
                UserLogin udata = JsonConvert.DeserializeObject<UserLogin>(dtoq);

                //Database entry
                psp_amd_equity_settlement_upload dto = new psp_amd_equity_settlement_upload()
                {
                    login_id = udata.Id.ToString(),
                    jsonString = jsonString,
                    file_ogName = ogFileName,
                    file_name = newfilename,
                    file_date = model.uploadDate.ToString(),
                    file_guid = fileGuid,
                };

                string url = _config.GetValue<string>("APIKey") + "Services/psp_amd_equity_settlement_upload";

                string serializeProfile = JsonConvert.SerializeObject(dto);
                string data = EncryptionDecryption.Encrypt(serializeProfile);
                EncryptData Endata = new EncryptData();
                Endata.EncryptObject = data;
                string data1 = HttpCall.HttpPostMethod(url, Endata);
                List<psp_amd_equity_settlement_upload> MobileResult = JsonConvert.DeserializeObject<List<psp_amd_equity_settlement_upload>>(data1);

                if (MobileResult.Count > 0)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.reportFile.CopyToAsync(stream);
                    }

                    //string smsMainUrl = _config.GetValue<string>("SMS_API_URL");
                    string smsMainUrl = _config.GetValue<string>("APIKey") + "SMS/SendSMS";

                    string SMS_API_USER = _config.GetValue<string>("SMS_API_USER");
                    string SMS_API_PASS = _config.GetValue<string>("SMS_API_PASS");
                    string SMS_API_APPCODE = _config.GetValue<string>("SMS_API_APPCODE");

                    string smsAppToken = getSmsApiToken();
                    string smsMainResponse;

                    foreach (var item in MobileResult)
                    {
                        var smsContent = new smsApiContent
                        {
                            app_code = SMS_API_APPCODE,
                            mobile = item.mobile,
                            text = item.sms_content,
                            token = smsAppToken,
                            XAPIHeader = item.comms_guid
                        };

                        var responseTokenTemplate = new
                        {
                            status = string.Empty,
                            token = string.Empty,
                            msg = string.Empty
                        };

                        smsMainResponse = HttpCall.HttpPostMethod(smsMainUrl, smsContent);
                        var mainResponseObject = JsonConvert.DeserializeAnonymousType(smsMainResponse, responseTokenTemplate);

                        if (mainResponseObject.msg == "Token Expired")
                        {
                            smsAppToken = getSmsApiToken();
                            smsContent.token = smsAppToken;
                            smsMainResponse = HttpCall.HttpPostMethod(smsMainUrl, smsContent);
                        }
                    }
                    return Json(new
                    {
                        status = "Success",
                        msg = "Request Processed."
                    });
                }
                else
                {
                    return Json(new
                    {
                        status = "Success",
                        msg = "No records found."
                    });
                }
            }
            catch (Exception)
            {
                return Json(new
                {
                    status = "Fail",
                    msg = "Error."
                });
            }
        }

        private string getSmsApiToken()
        {
            //string smsAuthUrl = _config.GetValue<string>("SMS_API_AuthURL");
            string smsAuthUrl = _config.GetValue<string>("APIKey") + "Auth/Authentication";

            var SMSCreds = new
            {
                app_code = "CP",
                user = _config.GetValue<string>("SMS_API_USER"),
                password = _config.GetValue<string>("SMS_API_PASS"),
            };

            string smsTokenResponse = HttpCall.HttpPostMethod(smsAuthUrl, SMSCreds);

            var smsTokenTemplate = new
            {
                status = string.Empty,
                token = string.Empty,
                msg = string.Empty
            };

            var deserializedObject = JsonConvert.DeserializeAnonymousType(smsTokenResponse, smsTokenTemplate);

            return deserializedObject.token;
        }


        [HttpPost]
        public IActionResult psp_dsp_client_coms_outstanding_debit_send_mail([FromBody] psp_dsp_client_coms_outstanding_debit_send_mail pampc)
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_client_coms_outstanding_debit_send_mail";

            string serializeProfile = JsonConvert.SerializeObject(pampc);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_client_coms_outstanding_debit_send_mail> Result = JsonConvert.DeserializeObject<List<psp_dsp_client_coms_outstanding_debit_send_mail>>(data1);

            return Json(Result);
        }

        [HttpPost]
        public IActionResult OutDebitEmail([FromBody] List<psp_dsp_client_coms_outstanding_debit_send_mail> pampc)
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_amd_client_coms_outstanding_debit_send_mail";

            string AccCodeComma, AmtConcat = string.Empty;

            AccCodeComma = string.Join(",", pampc.Select(x => x.account_code));
            AmtConcat = string.Join("$", pampc.Select(x => x.amount));

            var Dataobj = new 
            {
                login_id = pampc[0].login_id,
                as_on_date = pampc[0].as_on_date,
                str_acc_code = AccCodeComma,
                str_amt = AmtConcat
            };

            string serializeProfile = JsonConvert.SerializeObject(Dataobj);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            psp_dsp_client_coms_outstanding_debit_send_mail Result = JsonConvert.DeserializeObject<psp_dsp_client_coms_outstanding_debit_send_mail>(data1);
            return Json(Result);
        }

        [HttpPost]
        public IActionResult DormantAccountEmail([FromBody] List<psp_dsp_client_coms_dormant_mail_content> rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_client_coms_dormant_mail_content";

            string strCode, strCat = string.Empty;

            strCode = string.Join(",", rpt.Select(x => x.code));
            strCat = string.Join("$", rpt.Select(x => x.client_category));

            var Dataobj = new
            {
                login_id = rpt[0].login_id,
                content_id = rpt[0].content_id,
                code = strCode,
                client_category = strCat
            };

            string serializeProfile = JsonConvert.SerializeObject(Dataobj);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            psp_dsp_client_coms_dormant_mail_content Result = JsonConvert.DeserializeObject<psp_dsp_client_coms_dormant_mail_content>(data1);
            return Json(Result);

            /*
            foreach (var item in rpt)
            {
                string contentUrl = _config.GetValue<string>("APIKey") + "Services/psp_dsp_client_coms_dormant_mail_content";

                item.email_guid = Guid.NewGuid().ToString();

                string contentProfile = JsonConvert.SerializeObject(item);
                string contentData = EncryptionDecryption.Encrypt(contentProfile);
                EncryptData contentEndata = new EncryptData();
                contentEndata.EncryptObject = contentData;
                string contentData1 = HttpCall.HttpPostMethod(contentUrl, contentEndata);
                psp_dsp_client_coms_dormant_mail_content ContentResult = JsonConvert.DeserializeObject<psp_dsp_client_coms_dormant_mail_content>(contentData1);

                if (ContentResult.email_body != "" || ContentResult.email_body != null)
                {
                    List<string> attachList = null;

                    if (ContentResult.has_attachments == "Y")
                    {
                        attachList = ContentResult.attachments_path.Split(',').ToList();
                    }

                    SmtpSendEmail smtpSend = new SmtpSendEmail()
                    {
                        emailGuid = item.email_guid,
                        emailBody = ContentResult.email_body,
                        emailSubject = ContentResult.email_subject,
                        emailTo = ContentResult.email,
                        emailFrom = ContentResult.from_email,
                        emailDisplayName = ContentResult.email_display_name,
                        attachPath = attachList
                    };

                    //string mobile_no = ContentResult.mobile;

                    if (ContentResult.sms_content != null)
                    {
                        string mobile_no = "9930567046";

                        string smsUrl = "http://bulkpush.mytoday.com/BulkSms/SingleMsgApi?senderid=DNBEQT&username=9820568243&password=Pass@123&feedid=368826&text=" +
                            ContentResult.sms_content + "&to=" + mobile_no;

                        HttpClient client = new HttpClient();
                        string xml = "";

                        var task = Task.Run(() => client.GetAsync(smsUrl).Result);
                        task.Wait();
                        if (task.Result.IsSuccessStatusCode)
                        {
                            xml = task.Result.Content.ReadAsStringAsync().Result;
                        }

                        smsXmlResponse xmlResponse = LoadFromXMLString(xml);

                        smtpSend.smsREQID = xmlResponse.REQID;
                        smtpSend.smsTID = xmlResponse.MID.TID;
                    }

                    string url = _config.GetValue<string>("APIKey") + "Services/DormantAccountEmail";

                    string serializeProfile = JsonConvert.SerializeObject(smtpSend);
                    string data = EncryptionDecryption.Encrypt(serializeProfile);
                    EncryptData Endata = new EncryptData();
                    Endata.EncryptObject = data;
                    HttpCall.HttpPostMethod(url, Endata);
                }
            }
            //return Json(Result);

            return Json("{sqlmsg = 'Data'}");
            */
        }

        [HttpPost]
        public IActionResult psp_dsp_client_coms_history([FromBody] psp_dsp_client_coms_history rpt)
        {
            //using HttpClient client = _clientFactory.CreateClient();


            string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_client_coms_history";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_client_coms_history> Result = JsonConvert.DeserializeObject<List<psp_dsp_client_coms_history>>(data1);
            return Json(Result);
        }

        [HttpPost]
        public IActionResult AIFSchemeList([FromBody] psp_dsp_AIF_scheme rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_AIF_scheme";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_AIF_scheme> Result = JsonConvert.DeserializeObject<List<psp_dsp_AIF_scheme>>(data1);
            return Json(Result);
        }

        [HttpPost]
        public IActionResult psp_amd_AIF_data_entry([FromBody] psp_amd_AIF_data_entry rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_amd_AIF_data_entry";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            psp_amd_AIF_data_entry Result = JsonConvert.DeserializeObject<psp_amd_AIF_data_entry>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult AIFNavData([FromBody] psp_dsp_AIF_nav_data rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_AIF_nav_data";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_AIF_nav_data> Result = JsonConvert.DeserializeObject<List<psp_dsp_AIF_nav_data>>(data1);
            return Json(Result);
        }
        public object Finyears(Dashboard dto)
        {
           
            string url = _config.GetValue<string>("APIKey") + "Dashboard/DeshboardFinYears";
            Dashboard dashboard = new Dashboard();

            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            DashboardFinYears year = JsonConvert.DeserializeObject<DashboardFinYears>(dtoq);

            string serializeProfile = Newtonsoft.Json.JsonConvert.SerializeObject(year);

            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            dashboard.dashboardFinYears = JsonConvert.DeserializeObject<List<DashboardFinYears>>(data1);
            //dashboard = (Dashboard)FamilyDetails(dashboard);
            dashboard.Login_client_name = year.Login_client_name;
            dashboard.Id = year.Id;
            dashboard.row_guid = year.row_guid;
            dashboard.Role_Name = year.Role_Name;
            dashboard.Username = year.Username;
            dashboard.user = HttpContext.Session.GetString("username");
          
            dashboard = (Dashboard)Familymenu(dashboard);
            dashboard.menus = dashboard.menus;
            //dashboard = (Dashboard)FetchFev(dashboard);
            //dashboard.Fetchfev = dashboard.Fetchfev;
            return dashboard;
            //}
            //_logger.LogInformation("Dashboard Finyears Web Session and temp id null");
            //return null;
        }
        public List<FamilyList> FamilyDetails([FromBody] Dashboard dashboard)
        {
            //if (TempData["myFinyearsdata"] != null && HttpContext.Session.GetString("web_session_id") != null)
            //{
            string url = _config.GetValue<string>("APIKey") + "Dashboard/FamilyDetails";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            FamilyList year = JsonConvert.DeserializeObject<FamilyList>(dtoq);
            string serializeProfile = Newtonsoft.Json.JsonConvert.SerializeObject(year);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<FamilyList> familyLists = JsonConvert.DeserializeObject<List<FamilyList>>(data1);
            return familyLists;
            //}
            //_logger.LogInformation("Dashboard FamilyDetails Web Session and temp id null");
            //return null;
        }
        public object Familymenu(Dashboard dashboard)
        { 
            string url = _config.GetValue<string>("APIKey") + "Dashboard/Familymenu";

            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            FamilyList year = JsonConvert.DeserializeObject<FamilyList>(dtoq);

            string serializeProfile = Newtonsoft.Json.JsonConvert.SerializeObject(year);

            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            dashboard.menus = JsonConvert.DeserializeObject<List<menu>>(data1);

            List<menu> MenuObj = new List<menu>();
            MenuObj = JsonConvert.DeserializeObject<List<menu>>(data1);
            string v2 = JsonConvert.SerializeObject(MenuObj);
            TempData["Menus_Data"] = v2;
            TempData.Keep("Menus_Data");

            return dashboard;
            
        }

        public IActionResult SearchFamilyName(string display_flag, string loginID, string searchTerm = "")
      {
            string url = _config.GetValue<string>("APIKey") + "Dashboard/psp_dsp_searchable_family_list_client_portal";
            psp_dsp_searchable_family_list_client_portal newobj = new psp_dsp_searchable_family_list_client_portal();

            newobj.loginId = loginID;
            newobj.search_term = searchTerm;
            newobj.display_flag = display_flag;

            string serializeProfile = JsonConvert.SerializeObject(newobj);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_searchable_family_list_client_portal> Result = JsonConvert.DeserializeObject<List<psp_dsp_searchable_family_list_client_portal>>(data1);
            return Json(Result);
        }


        [HttpPost]
        public IActionResult FetchFev([FromBody] Dashboard dashboard)
        {
            
            string url = _config.GetValue<string>("APIKey") + "Dashboard/FetchFevDetails"; 

            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            FetchFevourite year = JsonConvert.DeserializeObject<FetchFevourite>(dtoq);

            string serializeProfile = Newtonsoft.Json.JsonConvert.SerializeObject(year);

            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string response = HttpCall.HttpPostMethod(url, Endata);
            dashboard.Fetchfev = JsonConvert.DeserializeObject<List<FetchFevourite>>(response);
            return Json(response);
        }

        [HttpPost]
        public IActionResult UpdateFev([FromBody] UpdateFevourite uv)
        {
            if (HttpContext.Session.GetString("web_session_id") != null)
            {
                WriteLog.WritewebLog("Start Application");
                if (TempData["myFinyearsdata"] != null && HttpContext.Session.GetString("web_session_id") != null)
                {
                    string url = _config.GetValue<string>("APIKey") + "Dashboard/UpdateFevouriteDetails";
                    string dtoq = TempData["myFinyearsdata"].ToString();
                    TempData.Keep("myFinyearsdata");
                    UpdateFevourite upd = JsonConvert.DeserializeObject<UpdateFevourite>(dtoq);
                    upd.web_session_id = HttpContext.Session.GetString("web_session_id");
                    upd.moduleid =int.Parse(uv.module_id);
                    upd.flag = "A";
                    string serializeProfile = Newtonsoft.Json.JsonConvert.SerializeObject(upd);
                    string data = EncryptionDecryption.Encrypt(serializeProfile);
                    EncryptData Endata = new EncryptData();
                    Endata.EncryptObject = data;
                    WriteLog.WritewebLog("Start Application");
                    string data1 = HttpCall.HttpPostMethod(url, Endata);
                    uv = JsonConvert.DeserializeObject<UpdateFevourite>(data1);

                    return Json(uv);
                }
                return Json(uv);
            }
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public IActionResult UpdateFevRemove([FromBody] UpdateFevourite uv)
        {
            if (HttpContext.Session.GetString("web_session_id") != null)
            {
                WriteLog.WritewebLog("Start Application");
                if (TempData["myFinyearsdata"] != null && HttpContext.Session.GetString("web_session_id") != null)
                {
                    string url = _config.GetValue<string>("APIKey") + "Dashboard/UpdateFevouriteDetails";
                    string dtoq = TempData["myFinyearsdata"].ToString();
                    TempData.Keep("myFinyearsdata");
                    UpdateFevourite upd = JsonConvert.DeserializeObject<UpdateFevourite>(dtoq);
                    upd.web_session_id = HttpContext.Session.GetString("web_session_id");
                    upd.moduleid = int.Parse(uv.module_id);
                    upd.flag = "R";
                    string serializeProfile = Newtonsoft.Json.JsonConvert.SerializeObject(upd);
                    string data = EncryptionDecryption.Encrypt(serializeProfile);
                    EncryptData Endata = new EncryptData();
                    Endata.EncryptObject = data;
                    WriteLog.WritewebLog("Start Application");
                    string data1 = HttpCall.HttpPostMethod(url, Endata);
                    uv = JsonConvert.DeserializeObject<UpdateFevourite>(data1);

                    return Json(uv);
                }
                return Json(uv);
            }
            return RedirectToAction("Index", "Login");
        }

        [HttpGet]
        public IActionResult getDocument(string FP = null)
        {
            FP = _config.GetValue<string>("DocPath") + FP;

            return Redirect(FP);
        }
        [HttpGet]
        public IActionResult AssetList()
        {
            string url = _config.GetValue<string>("APIKey") + "Dashboard/AssetList";

            psp_dsp_client_portal_assets newobj = new psp_dsp_client_portal_assets();

            string serializeProfile = JsonConvert.SerializeObject(newobj);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_client_portal_assets> Result = JsonConvert.DeserializeObject<List<psp_dsp_client_portal_assets>>(data1);
            return Json(Result);
        }
        [HttpGet]
        public IActionResult BranchList()
        {
            string url = _config.GetValue<string>("APIKey") + "Dashboard/BranchList";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            psp_dsp_client_portal_branch newobj = JsonConvert.DeserializeObject<psp_dsp_client_portal_branch>(dtoq);

            string serializeProfile = JsonConvert.SerializeObject(newobj);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_client_portal_branch> Result = JsonConvert.DeserializeObject<List<psp_dsp_client_portal_branch>>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult RMList([FromBody] psp_rpt_ssrs_template_RM rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Dashboard/RMList";
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            psp_rpt_ssrs_template_RM newobj = JsonConvert.DeserializeObject<psp_rpt_ssrs_template_RM>(dtoq);

            newobj.Branch = rpt.Branch;

            string serializeProfile = JsonConvert.SerializeObject(newobj);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_rpt_ssrs_template_RM> Result = JsonConvert.DeserializeObject<List<psp_rpt_ssrs_template_RM>>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult psp_rpt_scripwiseholding_scrip([FromBody] psp_rpt_scripwiseholding_scrip rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Dashboard/psp_rpt_scripwiseholding_scrip";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_rpt_scripwiseholding_scrip> Result = JsonConvert.DeserializeObject<List<psp_rpt_scripwiseholding_scrip>>(data1);
            return Json(Result);
        }
        
        [HttpGet]
        public IActionResult psp_dsp_kyc_income_range()
        {
            string url = _config.GetValue<string>("APIKey") + "Dashboard/psp_dsp_kyc_income_range";

            psp_dsp_kyc_income_range rpt = new psp_dsp_kyc_income_range();

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_kyc_income_range> Result = JsonConvert.DeserializeObject<List<psp_dsp_kyc_income_range>>(data1);
            return Json(Result);
        }
        [HttpGet]
        public IActionResult psp_dsp_kyc_relations()
        {
            string url = _config.GetValue<string>("APIKey") + "Dashboard/psp_dsp_kyc_relations";

            psp_dsp_kyc_income_range rpt = new psp_dsp_kyc_income_range();

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_kyc_relations> Result = JsonConvert.DeserializeObject<List<psp_dsp_kyc_relations>>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult psp_dsp_client_kyc_attributes([FromBody] psp_dsp_client_kyc_attributes rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Reports/psp_dsp_client_kyc_attributes";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            psp_dsp_client_kyc_attributes Result = JsonConvert.DeserializeObject<psp_dsp_client_kyc_attributes>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult GetNRIClientList([FromBody] psp_dsp_nri_client_list rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_nri_client_list";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_nri_client_list> Result = JsonConvert.DeserializeObject<List<psp_dsp_nri_client_list>>(data1);
            return Json(Result);
        }

        [HttpPost]
        public IActionResult psp_dsp_nri_category([FromBody] psp_dsp_nri_category rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_nri_category";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);



            List<psp_dsp_nri_category> Result = JsonConvert.DeserializeObject<List<psp_dsp_nri_category>>(data1);
            return Json(Result);
        }

        [HttpPost]
        public IActionResult psp_dsp_calendar_year([FromBody] psp_dsp_calendar_year rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_calendar_year";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_calendar_year> Result = JsonConvert.DeserializeObject<List<psp_dsp_calendar_year>>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult GetNRIClientBankAccountList([FromBody] psp_dsp_nri_bank_details rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_nri_bank_details";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_nri_bank_details> Result = JsonConvert.DeserializeObject<List<psp_dsp_nri_bank_details>>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult psp_amd_nri_client_data_entry([FromBody] psp_amd_nri_client_data_entry rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_amd_nri_client_data_entry";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            psp_amd_nri_client_data_entry Result = JsonConvert.DeserializeObject<psp_amd_nri_client_data_entry>(data1);
            return Json(Result);
        }

        [HttpPost]
        public IActionResult psp_dsp_nri_bankmaster_details([FromBody] psp_dsp_nri_bankmaster_details rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_nri_bankmaster_details";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_nri_bankmaster_details> Result = JsonConvert.DeserializeObject<List<psp_dsp_nri_bankmaster_details>>(data1);
            return Json(Result);
        }
        [HttpGet]
        public IActionResult psp_dsp_nri_bank_account_type_list()
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_nri_bank_account_type_list";

            psp_dsp_nri_bank_account_type_list rpt = new psp_dsp_nri_bank_account_type_list();

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_nri_bank_account_type_list> Result = JsonConvert.DeserializeObject<List<psp_dsp_nri_bank_account_type_list>>(data1);
            return Json(Result);
        }
        [HttpGet]
        public IActionResult psp_dsp_nri_client_category_list()
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_nri_client_category_list";

            psp_dsp_nri_client_category_list rpt = new psp_dsp_nri_client_category_list();

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_nri_client_category_list> Result = JsonConvert.DeserializeObject<List<psp_dsp_nri_client_category_list>>(data1);
            return Json(Result);
        }
     
        [HttpPost]
        public IActionResult psp_dsp_get_kyc_details([FromBody] psp_dsp_get_kyc_details rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_get_kyc_details";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_get_kyc_details> Result = JsonConvert.DeserializeObject<List<psp_dsp_get_kyc_details>>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult psp_amd_equity_kyc_initiate_update_request([FromBody] kyc_initiate_auth_resend_request rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_amd_equity_kyc_initiate_update_request";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            kyc_initiate_auth_resend_request Result = JsonConvert.DeserializeObject<kyc_initiate_auth_resend_request>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult psp_amd_equity_kyc_authenticate_update_request([FromBody] kyc_initiate_auth_resend_request rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_amd_equity_kyc_authenticate_update_request";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            kyc_initiate_auth_resend_request Result = JsonConvert.DeserializeObject<kyc_initiate_auth_resend_request>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult psp_amd_equity_kyc_resend_request([FromBody] kyc_initiate_auth_resend_request rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_amd_equity_kyc_resend_request";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            kyc_initiate_auth_resend_request Result = JsonConvert.DeserializeObject<kyc_initiate_auth_resend_request>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult psp_amd_equity_kyc_capture_update_request([FromBody] psp_amd_equity_kyc_capture_update_request rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_amd_equity_kyc_capture_update_request";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            psp_amd_equity_kyc_capture_update_request Result = JsonConvert.DeserializeObject<psp_amd_equity_kyc_capture_update_request>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult psp_dsp_kyc_backoffice_requests([FromBody] psp_dsp_kyc_backoffice_requests rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_kyc_backoffice_requests";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_kyc_backoffice_requests> Result = JsonConvert.DeserializeObject<List<psp_dsp_kyc_backoffice_requests>>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult psp_amd_kyc_backoffice_request_action([FromBody] psp_amd_kyc_backoffice_request_action rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_amd_kyc_backoffice_request_action";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            psp_amd_kyc_backoffice_request_action Result = JsonConvert.DeserializeObject<psp_amd_kyc_backoffice_request_action>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult psp_dsp_kyc_update_history([FromBody] psp_dsp_kyc_update_history rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_kyc_update_history";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_kyc_update_history> Result = JsonConvert.DeserializeObject<List<psp_dsp_kyc_update_history>>(data1);
            return Json(Result);
        }

        [HttpPost]
        public IActionResult psp_dsp_get_kyc_update_history([FromBody] psp_dsp_get_kyc_update_history rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_get_kyc_update_history";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_get_kyc_update_history> Result = JsonConvert.DeserializeObject<List<psp_dsp_get_kyc_update_history>>(data1);
            return Json(Result);
        }

        [HttpPost]
        public IActionResult psp_dsp_periodic_kyc_log([FromBody] psp_dsp_periodic_kyc_log rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_periodic_kyc_log";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);

            List<psp_dsp_periodic_kyc_log> Result = JsonConvert.DeserializeObject<List<psp_dsp_periodic_kyc_log>>(data1);

            return Json(Result);
        }

        [HttpPost]
        public IActionResult psp_dsp_client_list([FromBody] psp_dsp_client_list rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_client_list";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_client_list> Result = JsonConvert.DeserializeObject<List<psp_dsp_client_list>>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult psp_dsp_date_values([FromBody] psp_dsp_date_values rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_date_values";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            psp_dsp_date_values Result = JsonConvert.DeserializeObject<psp_dsp_date_values>(data1);
            return Json(Result);
        }

        [HttpPost]
        public IActionResult psp_dsp_global_report_date_pills([FromBody] psp_dsp_global_report_date_pills rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_global_report_date_pills";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            psp_dsp_global_report_date_pills Result = JsonConvert.DeserializeObject<psp_dsp_global_report_date_pills>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult psp_dsp_single_family_details([FromBody] psp_dsp_single_family_details rpt)
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


            string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_single_family_details";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            psp_dsp_single_family_details Result = JsonConvert.DeserializeObject<psp_dsp_single_family_details>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult psp_dsp_profile_details([FromBody] psp_dsp_profile_details rpt)
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

            string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_profile_details";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            psp_dsp_profile_details Result = JsonConvert.DeserializeObject<psp_dsp_profile_details>(data1);
            return Json(Result);
        }

        [HttpPost]
        public IActionResult psp_amd_family_details([FromBody] psp_amd_family_details rpt)
        {
            if (!ModelState.IsValid)
            {

                var allErrors = ModelState.Values.SelectMany(v => v.Errors.Select(b => b.ErrorMessage));

                int i = allErrors.Count();
                StringBuilder sb = new StringBuilder();

                foreach (string modelState in allErrors)
                {
                    sb.Append(modelState);
                }
                rpt.sql_msg = sb.ToString();
                rpt.sql_status = "Fail";
                return Json(rpt);

            }


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



            if (rpt.passFlag == "1" && rpt.newPassword != "")
            {
                rpt.plainPassword = rpt.newPassword;
                rpt.newPassword = EncryptionDecryption.EncryptMD5(rpt.newPassword);
                
            }

            //if (rpt.send_mail == "1")
            //{
            //    EncryptionDecryption objEncry = new EncryptionDecryption();

            //    rpt.plainPassword = EncryptionDecryption.RandomPassword();
            //    rpt.newPassword = EncryptionDecryption.EncryptMD5(rpt.plainPassword);
            //}

            string url = _config.GetValue<string>("APIKey") + "Services/psp_amd_family_details";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            psp_amd_family_details Result = JsonConvert.DeserializeObject<psp_amd_family_details>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult System_password()
        {
            string plainPassword = EncryptionDecryption.RandomPassword();

            var jsonresult = new
            {
                password = plainPassword,
            };

            return Json(plainPassword);
        }


        [HttpPost]
        public IActionResult psp_amd_profile_details([FromBody] psp_amd_profile_details rpt)
        {
            if (!ModelState.IsValid)
            {
                var allErrors = ModelState.Values.SelectMany(v => v.Errors.Select(b => b.ErrorMessage));

                StringBuilder sb = new StringBuilder();

                foreach (string modelState in allErrors)
                {
                    sb.Append(modelState);
                }
                rpt.sql_msg = sb.ToString();
                rpt.sql_status = "Fail";
                return Json(rpt);
            }

            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            UserLogin udata = JsonConvert.DeserializeObject<UserLogin>(dtoq);

            rpt.Login_Id = Convert.ToString(udata.Id);

            if (rpt.passFlag == "1" && rpt.newPassword != "")
            {
                rpt.newPassword = EncryptionDecryption.EncryptMD5(rpt.newPassword);
            }

            string url = _config.GetValue<string>("APIKey") + "Services/psp_amd_profile_details";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            psp_amd_profile_details Result = JsonConvert.DeserializeObject<psp_amd_profile_details>(data1);
            return Json(Result);

        }

        [HttpPost]
        public IActionResult psp_dsp_survey_list([FromBody] psp_dsp_survey_list rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_survey_list";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_survey_list> Result = JsonConvert.DeserializeObject<List<psp_dsp_survey_list>>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult psp_dsp_survey([FromBody] psp_dsp_survey rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_survey";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_survey> Result = JsonConvert.DeserializeObject<List<psp_dsp_survey>>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult psp_amd_survey_response([FromBody] psp_amd_survey_response rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_amd_survey_response";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            psp_amd_survey_response Result = JsonConvert.DeserializeObject<psp_amd_survey_response>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult psp_dsp_survey_status([FromBody] psp_dsp_survey_status rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_survey_status";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            psp_dsp_survey_status Result = JsonConvert.DeserializeObject<psp_dsp_survey_status>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult psp_dsp_survey_family_list([FromBody] psp_dsp_survey_family_list rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_survey_family_list";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_survey_family_list> Result = JsonConvert.DeserializeObject<List<psp_dsp_survey_family_list>>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult psp_dsp_captured_survey_responses([FromBody] psp_dsp_captured_survey_responses rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_captured_survey_responses";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_captured_survey_responses> Result = JsonConvert.DeserializeObject<List<psp_dsp_captured_survey_responses>>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult psp_dsp_survey_respnse_dump([FromBody] psp_dsp_survey_respnse_dump rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_survey_respnse_dump";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_survey_respnse_dump> Result = JsonConvert.DeserializeObject<List<psp_dsp_survey_respnse_dump>>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult psp_dsp_branch_list([FromBody] psp_dsp_branch_list pampc)
        {
            string url = _config.GetValue<string>("APIKey") + "Dashboard/psp_dsp_branch_list";

            string serializeProfile = JsonConvert.SerializeObject(pampc);

            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_branch_list> Result = JsonConvert.DeserializeObject<List<psp_dsp_branch_list>>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult psp_amd_survey_interaction([FromBody] psp_amd_survey_interaction pampc)
        {
            string url = _config.GetValue<string>("APIKey") + "Dashboard/psp_amd_survey_interaction";

            string serializeProfile = JsonConvert.SerializeObject(pampc);

            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            psp_amd_survey_interaction Result = JsonConvert.DeserializeObject<psp_amd_survey_interaction>(data1);
            return Json(Result);
        }

        [HttpPost]
        public IActionResult psp_dsp_client_portal_dashboard_RM_details([FromBody] psp_dsp_client_portal_dashboard_RM_details pampc)
        {
            string url = _config.GetValue<string>("APIKey") + "Dashboard/psp_dsp_client_portal_dashboard_RM_details";

            string serializeProfile = JsonConvert.SerializeObject(pampc);

            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_client_portal_dashboard_RM_details> Result = JsonConvert.DeserializeObject<List<psp_dsp_client_portal_dashboard_RM_details>>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult psp_dsp_client_portal_dashboard_client_list([FromBody] psp_dsp_client_portal_dashboard_client_list pampc)
        {
            string url = _config.GetValue<string>("APIKey") + "Dashboard/psp_dsp_client_portal_dashboard_client_list";

            string serializeProfile = JsonConvert.SerializeObject(pampc);

            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_client_portal_dashboard_client_list> Result = JsonConvert.DeserializeObject<List<psp_dsp_client_portal_dashboard_client_list>>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult psp_dsp_client_portal_dashboard_asset_allocation([FromBody] psp_dsp_client_portal_dashboard_asset_allocation pampc)
        {
            string url = _config.GetValue<string>("APIKey") + "Dashboard/psp_dsp_client_portal_dashboard_asset_allocation";

            string serializeProfile = JsonConvert.SerializeObject(pampc);

            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_client_portal_dashboard_asset_allocation> Result = JsonConvert.DeserializeObject<List<psp_dsp_client_portal_dashboard_asset_allocation>>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult psp_dsp_client_portal_dashboard_SIP_details([FromBody] psp_dsp_client_portal_dashboard_SIP_details pampc)
        {
            string url = _config.GetValue<string>("APIKey") + "Dashboard/psp_dsp_client_portal_dashboard_SIP_details";

            string serializeProfile = JsonConvert.SerializeObject(pampc);

            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_client_portal_dashboard_SIP_details> Result = JsonConvert.DeserializeObject<List<psp_dsp_client_portal_dashboard_SIP_details>>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult psp_dsp_client_portal_dashboard_xirr_details([FromBody] psp_dsp_client_portal_dashboard_xirr_details pampc)
        {
            string url = _config.GetValue<string>("APIKey") + "Dashboard/psp_dsp_client_portal_dashboard_xirr_details";

            string serializeProfile = JsonConvert.SerializeObject(pampc);

            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_client_portal_dashboard_xirr_details> Result = JsonConvert.DeserializeObject<List<psp_dsp_client_portal_dashboard_xirr_details>>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult psp_dsp_client_portal_dashboard_notification([FromBody] psp_dsp_client_portal_dashboard_notification pampc)
        {
            string url = _config.GetValue<string>("APIKey") + "Dashboard/psp_dsp_client_portal_dashboard_notification";

            string serializeProfile = JsonConvert.SerializeObject(pampc);

            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_client_portal_dashboard_notification> Result = JsonConvert.DeserializeObject<List<psp_dsp_client_portal_dashboard_notification>>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult psp_dsp_process_log([FromBody] psp_dsp_process_log pampc)
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_process_log";

            string serializeProfile = JsonConvert.SerializeObject(pampc);

            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_process_log> Result = JsonConvert.DeserializeObject<List<psp_dsp_process_log>>(data1);
            return Json(Result);
        }

        [HttpPost]
        public IActionResult psp_dsp_current_holding_drill_down([FromBody] psp_dsp_current_holding_drill_down pampc)
        {
            string url = _config.GetValue<string>("APIKey") + "Dashboard/psp_dsp_current_holding_drill_down";

            string serializeProfile = JsonConvert.SerializeObject(pampc);

            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_current_holding_drill_down> Result = JsonConvert.DeserializeObject<List<psp_dsp_current_holding_drill_down>>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult psp_dsp_common_report_module_list([FromBody] psp_dsp_common_report_module_list pampc)
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_common_report_module_list";

            string serializeProfile = JsonConvert.SerializeObject(pampc);

            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_common_report_module_list> Result = JsonConvert.DeserializeObject<List<psp_dsp_common_report_module_list>>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult psp_dsp_search_client_cp([FromBody] psp_dsp_search_client_cp pampc)
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_search_client_cp";

            string serializeProfile = JsonConvert.SerializeObject(pampc);

            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_search_client_cp> Result = JsonConvert.DeserializeObject<List<psp_dsp_search_client_cp>>(data1);
            return Json(Result);
        }
		[HttpPost]
		public IActionResult psp_dsp_client_accounts([FromBody] psp_dsp_client_accounts rpt)
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

			string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_client_accounts";

			string serializeProfile = JsonConvert.SerializeObject(rpt);
			string data = EncryptionDecryption.Encrypt(serializeProfile);
			EncryptData Endata = new EncryptData();
			Endata.EncryptObject = data;
			string data1 = HttpCall.HttpPostMethod(url, Endata);

			

			List<psp_dsp_client_accounts> Result = JsonConvert.DeserializeObject<List<psp_dsp_client_accounts>>(data1);
			return Json(Result);
		}

		[HttpPost]
		public IActionResult psp_dsp_mint_mf_download_ReportCriteria([FromBody] psp_dsp_mint_mf_download_ReportCriteria pampc)
		{
			string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_mint_mf_download_ReportCriteria";

			string serializeProfile = JsonConvert.SerializeObject(pampc);

			string data = EncryptionDecryption.Encrypt(serializeProfile);
			EncryptData Endata = new EncryptData();
			Endata.EncryptObject = data;
			string data1 = HttpCall.HttpPostMethod(url, Endata);
			List<psp_dsp_mint_mf_download_ReportCriteria> Result = JsonConvert.DeserializeObject<List<psp_dsp_mint_mf_download_ReportCriteria>>(data1);
			return Json(Result);
		}
		[HttpPost]
		public IActionResult psp_dsp_mint_mf_report_download_list([FromBody] psp_dsp_mint_mf_report_download_list pampc)
		{
			return Json(fetchMFreportdata());
		}

        private List<psp_dsp_mint_mf_report_download_list> fetchMFreportdata()
        {
			string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_mint_mf_report_download_list";

			string serializeProfile = JsonConvert.SerializeObject(new object { });

			string data = EncryptionDecryption.Encrypt(serializeProfile);
			EncryptData Endata = new EncryptData();
			Endata.EncryptObject = data;
			string data1 = HttpCall.HttpPostMethod(url, Endata);

			List<psp_dsp_mint_mf_report_download_list> Result = JsonConvert.DeserializeObject<List<psp_dsp_mint_mf_report_download_list>>(data1);

			return Result;
		}

        [HttpPost]
        public IActionResult psp_dsp_family_mapping_fam_details([FromBody] psp_dsp_family_mapping_fam_details pampc)
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_family_mapping_fam_details";

            string serializeProfile = JsonConvert.SerializeObject(pampc);

            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            psp_dsp_family_mapping_fam_details Result = JsonConvert.DeserializeObject<psp_dsp_family_mapping_fam_details>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult psp_amd_family_mapping_change_family([FromBody] psp_amd_family_mapping_change_family pampc)
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_amd_family_mapping_change_family";

            string serializeProfile = JsonConvert.SerializeObject(pampc);

            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            psp_amd_family_mapping_change_family Result = JsonConvert.DeserializeObject<psp_amd_family_mapping_change_family>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult psp_amd_family_mapping_create_fam([FromBody] psp_amd_family_mapping_create_fam pampc)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();

                var dataObj = new { errorList = errors, sql_status = "0" };

                return Json(dataObj);

            }
            string url = _config.GetValue<string>("APIKey") + "Services/psp_amd_family_mapping_create_fam";

            pampc.pass = EncryptionDecryption.RandomPassword();
            pampc.pass_en = EncryptionDecryption.EncryptMD5(pampc.pass);

            string serializeProfile = JsonConvert.SerializeObject(pampc);

            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            psp_amd_family_mapping_create_fam Result = JsonConvert.DeserializeObject<psp_amd_family_mapping_create_fam>(data1);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult FamilyMappingBranchRmDetails([FromBody] psp_dsp_family_mapping_branch_details pampc)
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_family_mapping_branch_details";

            string serializeProfile = JsonConvert.SerializeObject(pampc);

            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            psp_dsp_family_mapping_branch_details BranchResult = JsonConvert.DeserializeObject<psp_dsp_family_mapping_branch_details>(data1);

            psp_dsp_family_mapping_rm_details RmObjData = new psp_dsp_family_mapping_rm_details();

            RmObjData.family_token = pampc.family_token;

            List<psp_dsp_family_mapping_rm_details> RmResult = psp_dsp_family_mapping_rm_details(RmObjData);

            var dataObj = new { branchResult = BranchResult, rmResult = RmResult };

            return Json(dataObj);
        }

        public List<psp_dsp_family_mapping_rm_details> psp_dsp_family_mapping_rm_details(psp_dsp_family_mapping_rm_details objData)
        {
            string RMurl = _config.GetValue<string>("APIKey") + "Services/psp_dsp_family_mapping_rm_details";

            string RMserializeProfile = JsonConvert.SerializeObject(objData);

            string RMdata = EncryptionDecryption.Encrypt(RMserializeProfile);
            EncryptData RMEndata = new EncryptData();
            RMEndata.EncryptObject = RMdata;
            string RMdata1 = HttpCall.HttpPostMethod(RMurl, RMEndata);
            List<psp_dsp_family_mapping_rm_details> RmResult = JsonConvert.DeserializeObject<List<psp_dsp_family_mapping_rm_details>>(RMdata1);

            return RmResult;
        }
        [HttpPost]
        public IActionResult psp_amd_family_mapping_branch_update([FromBody] psp_amd_family_mapping_branch_update objData)
        {
            string RMurl = _config.GetValue<string>("APIKey") + "Services/psp_amd_family_mapping_branch_update";

            string RMserializeProfile = JsonConvert.SerializeObject(objData);

            string RMdata = EncryptionDecryption.Encrypt(RMserializeProfile);
            EncryptData RMEndata = new EncryptData();
            RMEndata.EncryptObject = RMdata;
            string RMdata1 = HttpCall.HttpPostMethod(RMurl, RMEndata);
            psp_amd_family_mapping_branch_update RmResult = JsonConvert.DeserializeObject<psp_amd_family_mapping_branch_update>(RMdata1);

            return Json(RmResult);
        }
        [HttpPost]
        public IActionResult PspAmdFamilyMappingRmUpdate([FromBody] PspAmdFamilyMappingRmUpdate pampc)
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_amd_family_mapping_rm_update";

            string serializeProfile = JsonConvert.SerializeObject(pampc);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            PspAmdFamilyMappingRmUpdate Result = JsonConvert.DeserializeObject<PspAmdFamilyMappingRmUpdate>(data1);
            return Json(Result);
        }

        [HttpPost]
        public IActionResult psp_dsp_client_communication_log([FromBody] psp_dsp_client_communication_log pampc)
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_client_communication_log";

            string serializeProfile = JsonConvert.SerializeObject(pampc);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_client_communication_log> Result = JsonConvert.DeserializeObject<List<psp_dsp_client_communication_log>>(data1);
            return Json(Result);
        }


        //psp_amd_equity_settlement_upload

        
    }
}

