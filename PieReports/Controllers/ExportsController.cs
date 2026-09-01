using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PieReports.Encryption_Decryption;
using PieReports.Models;
using ViewModel.Exports;
using ViewModel.Login;

using Syncfusion.Pdf;
using Syncfusion.Pdf.Parsing;
using System.IO;
using Syncfusion.Pdf.Security;
using Syncfusion.Drawing;
using Syncfusion.Pdf.Graphics;
using System.Text;

namespace PieReports.Controllers
{

    public class ExportsController : Controller
    {
        private IConfiguration _config;
        private string strAPIpath;
        public ExportsController(IConfiguration config)
        {
            _config = config;
            strAPIpath = EncryptionDecryption.Decrypt(_config.GetValue<string>("EncryptedAPIKey"));
        }

        public string FromBase64String(string inputdata)
        {
            byte[] decodedBytes = Convert.FromBase64String(inputdata);
            return Encoding.UTF8.GetString(decodedBytes);

        }

        [HttpPost]
        public IActionResult SetFamilyID([FromBody] FamilyList rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Exports/GetFamilyID";
            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            FamilyList pspdspmobilepan = JsonConvert.DeserializeObject<FamilyList>(data1);

            return Json(pspdspmobilepan);
        }

        [HttpPost]
        public IActionResult psp_dsp_client_portal_downloads_details([FromBody] psp_dsp_client_portal_downloads_details rpt)
        {
            //string some = rpt.report_name;
            //string Decrypt = FromBase64String(some);

            string url = _config.GetValue<string>("APIKey") + "Exports/psp_dsp_client_portal_downloads_details";
            string serializeProfile = JsonConvert.SerializeObject(rpt);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_client_portal_downloads_details> result = JsonConvert.DeserializeObject<List<psp_dsp_client_portal_downloads_details>>(data1);

            return Json(result);
        }

        

        public IActionResult getPDF()
        {
            //Open a new PDF document.

            FileStream docStream = new FileStream("D:\\Snehank\\PieReports\\Daily Equity Brokerage MIS.pdf", FileMode.Open, FileAccess.Read);

            PdfLoadedDocument document = new PdfLoadedDocument(docStream);

            //PDF document security 

            PdfSecurity security = document.Security;

            //Specifies encryption key size, algorithm and permission. 

            security.KeySize = PdfEncryptionKeySize.Key256Bit;

            security.Algorithm = PdfEncryptionAlgorithm.AES;

            //Provide owner and user password.

            security.OwnerPassword = "ownerpassword";

            security.UserPassword = "userpassword";

            //Save the document into stream.

            MemoryStream stream = new MemoryStream();

            document.Save(stream);

            stream.Position = 0;

            //Close the documents.

            document.Close(true);

            //Defining the ContentType for pdf file.

            string contentType = "application/pdf";

            //Define the file name.

            string fileName = "Output.pdf";

            //Creates a FileContentResult object by using the file contents, content type, and file name.

            return File(stream, contentType, fileName);
        }

        [HttpPost]
        public IActionResult EmailMainReport([FromBody] psp_amd_send_report_client_portal rpt)
        {

            //FamilyList Fml = new FamilyList();
            //Fml.Family_Token = rpt.family_id;


            //string Famurl = _config.GetValue<string>("APIKey") + "Exports/GetFamilyID";
            //string FamProfile = JsonConvert.SerializeObject(Fml);
            //string Famdata = EncryptionDecryption.Encrypt(FamProfile);
            //EncryptData FamEndata = new EncryptData();
            //FamEndata.EncryptObject = Famdata;
            //string Famdataout = HttpCall.HttpPostMethod(Famurl, FamEndata);
            //FamilyList FamResult = JsonConvert.DeserializeObject<FamilyList>(Famdataout);

            //rpt.family_id = FamResult.Family_Id;

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

                rpt.parameters = rpt.parameters.Replace("[AccessToken]", HttpContext.Session.GetString("web_session_id"));

                string url = _config.GetValue<string>("APIKey") + "Exports/EmailReport";

                string serializeProfile = JsonConvert.SerializeObject(rpt);
                string data = EncryptionDecryption.Encrypt(serializeProfile);
                EncryptData Endata = new EncryptData();
                Endata.EncryptObject = data;
                string data1 = HttpCall.HttpPostMethod(url, Endata);
                List<psp_amd_send_report_client_portal> pspdspmobilepan = JsonConvert.DeserializeObject<List<psp_amd_send_report_client_portal>>(data1);
                return Json(pspdspmobilepan);
            }
            return RedirectToAction("Index", "Login");
        }

        


    }
}
