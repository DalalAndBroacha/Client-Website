using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PieReports.Encryption_Decryption;
using PieReports.Filters;
using PieReports.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ViewModel.Login;
using ViewModel.Reports;
using ViewModel.Reports.ResearchReport;
using ViewModel.Shared;

namespace PieReports.Controllers
{
    
    public class ResearchController : Controller
    {

        private readonly ILogger<ResearchController> _logger;
        private IConfiguration _config;
        public ResearchController(ILogger<ResearchController> logger,
           IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        public IActionResult Index()
        {
            List<psp_dsp_equity_research_reports> Result = ReportsData("Web");

            return View(Result);
        }
        [ServiceFilter(typeof(SessionTimeoutFilter))]
        public IActionResult UploadReport()
		{
            Upload_Research_Report objModel = new Upload_Research_Report();
            objModel.script_data = scripData();
            objModel.cat_data = catData();
            objModel.reco_data = recosData();
            objModel.reports_data = ReportsData();

			return View(objModel);

		}

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(SessionTimeoutFilter))]
        public IActionResult uploadData(Upload_Research_Report model)
        {
            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            UserLogin udata = JsonConvert.DeserializeObject<UserLogin>(dtoq);

            psp_amd_equity_research_reco dto = new psp_amd_equity_research_reco();

            dto.Login_Id = udata.Id.ToString();
            dto.repo_title = model.RepoTitle;
                

            research_category catData = new research_category();

            catData = JsonConvert.DeserializeObject<research_category>(model.category);

            dto.category = catData.category_name;

            if (model.chkDspWeb == true)
            {
                dto.dsp_web = "Y";
            }
            else
            {
                dto.dsp_web = "N";
            }

            if (catData.stock_related_flag == "N")
            {
                dto.scrip_code = "0";
                dto.recommendation = "-";
                dto.target_price = 0;
            }
            else
            {
                dto.scrip_code = model.scrip_code;
                dto.recommendation = model.Recomandation;
                dto.target_price = model.targetPrice;
            }

            dto.rec_id = model.rec_id;
            dto.flag = model.flag;

            if (model.flag == "A")
            {

                var img = model.reportFile;

                //Getting file meta data
                var fileName = Path.GetFileName(img.FileName);
                //var contentType = model.reportFile.ContentType;

                //string newfilename = GetUniqueFileName(fileName);
                string newfilename = Guid.NewGuid().ToString() + Path.GetExtension(fileName);

                string Filestoragepath = _config.GetValue<string>("ResearchUploadPath");

                    
                var filePath = Path.Combine(Filestoragepath, newfilename);
                dto.url = newfilename;
                try
                {
                    model.reportFile.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                catch (Exception)
                {
                    throw;
                }
                    
            }
            else if (model.flag == "U")
            {
                dto.url = model.fileURL;

                string appendUrl = _config.GetValue<string>("ResearchPath");
                string filePath = _config.GetValue<string>("ResearchUploadPath") + model.fileURL.Replace(appendUrl, "");

                    
                if (model.updateReportFile != null)
                {
                    model.updateReportFile.CopyTo(new FileStream(filePath, FileMode.Create));
                }
            }

            string postFileurl = _config.GetValue<string>("APIKey") + "Services/psp_amd_equity_research_reco";
            string postFileProfile = JsonConvert.SerializeObject(dto);
            string postFiledata = EncryptionDecryption.Encrypt(postFileProfile);
            EncryptData postFileEndata = new EncryptData();
            postFileEndata.EncryptObject = postFiledata;
            string Famdataout = HttpCall.HttpPostMethod(postFileurl, postFileEndata);
            psp_amd_equity_research_reco FamResult = JsonConvert.DeserializeObject<psp_amd_equity_research_reco>(Famdataout);
    
            return RedirectToAction("UploadReport", "Research");

  
        }

        [HttpPost]
        [ServiceFilter(typeof(SessionTimeoutFilter))]
        public IActionResult deleteReport([FromBody] psp_amd_equity_research_reco rpt)
        {
            psp_dsp_user_access_token postdata = new psp_dsp_user_access_token();

            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            UserLogin udata = JsonConvert.DeserializeObject<UserLogin>(dtoq);

            string appendUrl = _config.GetValue<string>("ResearchPath");
            string filePath = _config.GetValue<string>("ResearchUploadPath") + rpt.url.Replace(appendUrl, "");
            try
            {
                System.IO.File.Delete(filePath);
            }
            catch (Exception)
            {
                throw;
            }

            rpt.Login_Id = udata.Id.ToString();

            string url = _config.GetValue<string>("APIKey") + "Services/psp_amd_equity_research_reco";

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            psp_amd_equity_research_reco objData = JsonConvert.DeserializeObject<psp_amd_equity_research_reco>(data1);

            return Json(objData.sql_msg);
        }

        [HttpPost]
        [ServiceFilter(typeof(SessionTimeoutFilter))]
        public IActionResult pushMobileNotification([FromBody] psp_amd_equity_research_push_mobile_notification rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_amd_equity_research_push_mobile_notification";

            string dtoq = TempData["myFinyearsdata"].ToString();
            TempData.Keep("myFinyearsdata");
            UserLogin udata = JsonConvert.DeserializeObject<UserLogin>(dtoq);

            rpt.Login_Id = udata.Id.ToString();

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            psp_amd_equity_research_push_mobile_notification Result = JsonConvert.DeserializeObject<psp_amd_equity_research_push_mobile_notification>(data1);
            
            return Json(Result);
        }

        public List<psp_rpt_scripwiseholding_scrip> scripData()
        {
            string url = _config.GetValue<string>("APIKey") + "Dashboard/psp_rpt_scripwiseholding_scrip";

            psp_rpt_scripwiseholding_scrip rpt = new psp_rpt_scripwiseholding_scrip
            {
                rm = _config.GetValue<string>("MasterTM_Id"),
                Asset = "1"
            };

            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_rpt_scripwiseholding_scrip> Result = JsonConvert.DeserializeObject<List<psp_rpt_scripwiseholding_scrip>>(data1);

            return Result;
        }

        public List<psp_dsp_research_category> catData()
        {
            string url = _config.GetValue<string>("APIKey") + "Dashboard/psp_dsp_research_category";

            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = "";

            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_research_category> Result = JsonConvert.DeserializeObject<List<psp_dsp_research_category>>(data1);

            return Result;
        }

        public List<psp_dsp_research_recommendation> recosData()
        {
            string url = _config.GetValue<string>("APIKey") + "Dashboard/psp_dsp_research_recommendation";

            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = "";

            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_research_recommendation> Result = JsonConvert.DeserializeObject<List<psp_dsp_research_recommendation>>(data1);

            return Result;
        }

        public List<psp_dsp_equity_research_reports> ReportsData(string dsp = "All")
        {
            string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_equity_research_reports";

            string appendUrl = _config.GetValue<string>("ResearchPath");

            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = "";
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_equity_research_reports> Result = JsonConvert.DeserializeObject<List<psp_dsp_equity_research_reports>>(data1);

            foreach (var item in Result)
            {
                item.URL = appendUrl + item.URL;
            }

            if (dsp == "All")
            {
                return Result;
            }
            else
            {
                return Result.Where(x => x.Display_on_web == "Y").ToList();
            }
        }

        public IActionResult One_Pager_Update()
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

        [HttpPost]
        public IActionResult One_Pager_Update([FromBody] One_Pager pampc)
        {
            string url = _config.GetValue<string>("APIKey") + "Services/One_Pager_Update";

            string serializeProfile = JsonConvert.SerializeObject(pampc);

            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            One_Pager Result = JsonConvert.DeserializeObject<One_Pager>(data1);
            return Json(Result);
        }

        //private string GetUniqueFileName(string fileName)
        //{
        //    return Path.GetFileNameWithoutExtension(fileName)
        //              + "_"
        //              + Guid.NewGuid().ToString()
        //              + Path.GetExtension(fileName);
        //}
    }
}
