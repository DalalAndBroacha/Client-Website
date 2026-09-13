using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PieReports.Encryption_Decryption;
using PieReports.Models;
using System;
using System.Collections.Generic;
using System.Net;
using ViewModel.Login;
using Wangkanai.Detection.Services;
using Recaptcha.Web.Mvc;
using Recaptcha.Web;
using DocumentFormat.OpenXml.InkML;
using System.Web.Providers.Entities;

namespace PieReports.Controllers
{
    public class LoginController : Controller
    {

        private readonly ILogger<LoginController> _logger;

        private readonly IDetectionService _detectionService;

        private IConfiguration _config;

        public const string SessionKeyAge = "_Age";
        public LoginController(ILogger<LoginController> logger,
            IDetectionService detectionService,
             IConfiguration config)
        {
            _logger = logger;
            _detectionService = detectionService;
            _config = config;
        }
        //Login Page


        public IActionResult TestIndex(Dashboard login)
        {
            // string passEn = EncryptionDecryption.DecryptMD5("0588FAAE03AC0F0D81EEE966E414291E");

            return View(login);
        }

        public IActionResult Index(UserLogin login)
        {
            WriteLog.WritewebLog("Start Application");
            // string passEn = EncryptionDecryption.DecryptMD5("0588FAAE03AC0F0D81EEE966E414291E");

            //HttpContext.Session.SetString("web_session_id", HttpContext.Session.Id);
            string guid = Guid.NewGuid().ToString();
            HttpContext.Session.SetString("web_session_id", guid);

            if (TempData["loginData"] != null)
            {
                string logins = TempData["loginData"].ToString();

                login = JsonConvert.DeserializeObject<UserLogin>(logins);
                return View(login);
            }
            return View(login);
        }
        // User Verify
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AccountLogin(UserLogin login)
        {
            if (HttpContext.Session.GetString("web_session_id") != null)
            {
                WriteLog.WritewebLog("Start Application");
                string url = _config.GetValue<string>("APIKey") + "Services/AccountLogin";
                string ShowCap = _config.GetValue<string>("ShowCap");
                string CaptchaType = _config.GetValue<string>("CaptchaType");

                // string url = "https://www.dalalbroacha.com/CPAPI/Services/AccountLogin"; _config.GetValue<string>("APIKey") +

                try
                {
                    if (ShowCap == "Yes")
                    {
                        if (CaptchaType == "G")
                        {
                            RecaptchaVerificationHelper recaptchaHelper = this.GetRecaptchaVerificationHelper();
                            if (String.IsNullOrEmpty(recaptchaHelper.Response))
                            {
                                login.msg = "Please validate Captcha.";
                                string v1 = JsonConvert.SerializeObject(login);
                                TempData["loginData"] = v1;
                                TempData.Keep("loginData");
                                return RedirectToAction("Index", "Login");
                            }
                            RecaptchaVerificationResult recaptchaResult = recaptchaHelper.VerifyRecaptchaResponse();
                            if (!recaptchaResult.Success)
                            {
                                login.msg = "Incorrect captcha answer.";
                                string v1 = JsonConvert.SerializeObject(login);
                                TempData["loginData"] = v1;
                                TempData.Keep("loginData");
                                return RedirectToAction("Index", "Login");
                            }
                        }
                        else
                        {
                            string Captcha = EncryptionDecryption.Decrypt(HttpContext.Session.GetString("CaptchaCode"));

                            if (login.CaptchaCode is null)
                            {
                                login.msg = "Please validate Captcha.";
                                string v1 = JsonConvert.SerializeObject(login);
                                TempData["loginData"] = v1;
                                TempData.Keep("loginData");

                                return RedirectToAction("Index", "Login");
                            }
                            else
                            {
                                if (login.CaptchaCode != Captcha)
                                {
                                    login.msg = "Incorrect Captcha.";
                                    string v1 = JsonConvert.SerializeObject(login);
                                    TempData["loginData"] = v1;
                                    TempData.Keep("loginData");

                                    return RedirectToAction("Index", "Login");
                                }
                                else
                                {
                                    HttpContext.Session.Remove("CaptchaCode");
                                }
                            }
                        }
                    }
                    HttpContext.Session.SetString("username", login.Username);
                    login.device_info = _detectionService.Device.Type.ToString();
                    login.browser_name = _detectionService.Browser.Name.ToString();
                    login.browser_version = _detectionService.Browser.Version.ToString();
                    login.opertation_system = _detectionService.Platform.Name.ToString();
                    login.geo_location = _detectionService.UserAgent.ToString();
                    login.web_session_id = HttpContext.Session.Id;
                    string guid = Guid.NewGuid().ToString();
                    login.web_session_id = guid;
                    HttpContext.Session.SetString("web_session_id", guid);
                    string passEn = EncryptionDecryption.EncryptMD5(login.Password);
                    login.Password = passEn;
                    string serializeProfile = Newtonsoft.Json.JsonConvert.SerializeObject(login);

                    string data = EncryptionDecryption.Encrypt(serializeProfile);
                    EncryptData Endata = new EncryptData();
                    Endata.EncryptObject = data;
                    WriteLog.WritewebLog("Start Application");
                    string data1 = HttpCall.HttpPostMethod(url, Endata);
                    login = JsonConvert.DeserializeObject<UserLogin>(data1);
                    login.Login_client_name = login.Login_name;
                    string v = JsonConvert.SerializeObject(login);
                    HttpContext.Session.SetString("mylogindata", v);
                    HttpContext.Session.SetString("ForgotUser", v);

                    if (login.password_type.ToUpper() == "U")
                    {
                        string v2 = JsonConvert.SerializeObject(login);
                        TempData["myFinyearsdata"] = v2;
                        //return RedirectToAction("PieReport", "Pie", login);  Removed since it exposes data in URL
                        return RedirectToAction("PieReport", "Pie", new { dspFlag = login.LoginModalDisplay , dspMessage = login.LoginModalMessage});
                    }
                    else if (login.password_type.ToUpper() == "S")
                    {
                        dtoInputChangePassword objN = new dtoInputChangePassword()
                            {
                                msg = "Change system generated password."
                            };
                        return RedirectToAction("ChangePassword", "Login", objN);
                    }
                    else if (login.password_type.ToUpper() == "E")
                    {
                        dtoInputChangePassword objN = new dtoInputChangePassword()
                        {
                            msg = "Password expired, kindly set new password."
                        };
                        return RedirectToAction("ChangePassword", "Login", objN);
                    }

                    else if (login.login_status == 0)
                    {
                        string v1 = JsonConvert.SerializeObject(login);
                        TempData["loginData"] = v1;
                        TempData.Keep("loginData");
                        return RedirectToAction("Index", "Login");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Login");
                    }
                }

                catch (Exception ex)
                {
                    _logger.LogInformation("Login", ex.Message);
                    throw;
                }
            }
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public IActionResult AccountLoginViaPANorUCC(UserLogin FromPost)
        {
            string url = _config.GetValue<string>("APIKey") + "Services/AccountLogin";
            UserLogin login = new UserLogin();
            try
            {
                login.otp_token = FromPost.otp_token;
                login.otp = FromPost.otp;
                login.device_info = _detectionService.Device.Type.ToString();
                login.browser_name = _detectionService.Browser.Name.ToString();
                login.browser_version = _detectionService.Browser.Version.ToString();
                login.opertation_system = _detectionService.Platform.Name.ToString();
                login.geo_location = _detectionService.UserAgent.ToString();
                string guid = Guid.NewGuid().ToString();
                login.web_session_id = guid;
                HttpContext.Session.SetString("web_session_id", guid);
                login.Password = "";
                login.Username = "";

                string serializeProfile = JsonConvert.SerializeObject(login);

                string data = EncryptionDecryption.Encrypt(serializeProfile);
                EncryptData Endata = new EncryptData();
                Endata.EncryptObject = data;

                string data1 = HttpCall.HttpPostMethod(url, Endata);
                login = JsonConvert.DeserializeObject<UserLogin>(data1);
                login.Login_client_name = login.Login_name;
                string v = JsonConvert.SerializeObject(login);
                HttpContext.Session.SetString("mylogindata", v);
                HttpContext.Session.SetString("ForgotUser", v);
                if (login.login_status == 1)
                {
                    string v2 = JsonConvert.SerializeObject(login);
                    TempData["myFinyearsdata"] = v2;
                    return RedirectToAction("PieReport", "Pie");
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Login", ex.Message);
                throw;
            }
        }
        
        //public IActionResult AccountLoginViaUCC(UserLogin login)
        //{
        //    login.Username = "Premal";
        //    login.Password = "Pass@123";
        //    return RedirectToAction("AccountLogin", "Login", login);
        //}

        public IActionResult ForgotPassword(ForgotPassword forgotPassword)
        {
            if (HttpContext.Session.GetString("web_session_id") != null)
            {
                WriteLog.WritewebLog("Start Application");
                otp otps = new otp();
                if (forgotPassword.Username != null && forgotPassword.EmailId != null)
                {
                    string url = _config.GetValue<string>("APIKey") + "Services/ForgotPassword";
                    string serializeProfile = Newtonsoft.Json.JsonConvert.SerializeObject(forgotPassword);
                    string data = EncryptionDecryption.Encrypt(serializeProfile);
                    EncryptData Endata = new EncryptData();
                    Endata.EncryptObject = data;
                    WriteLog.WritewebLog("Start Application");
                    string data1 = HttpCall.HttpPostMethod(url, Endata);
                    ForgotPassword forgot = JsonConvert.DeserializeObject<ForgotPassword>(data1);
                    if (forgot.sql_status == "Success")
                    {
                        string v = JsonConvert.SerializeObject(forgot);
                        HttpContext.Session.SetString("otpDetails", v);
                        HttpContext.Session.SetString("mylogindata", v);
                        otps.sql_message = forgot.sql_message;
                        forgot.otps = otps;
                        forgot.Username = forgotPassword.Username;
                        forgot.EmailId = forgotPassword.EmailId;
                        return View(forgot);
                    }
                   
                    return View(forgot);
                }

                return View(forgotPassword);
            }
            return RedirectToAction("Index", "Login");
        }

        public IActionResult ResentForgotPassword([FromBody] ForgotPassword forgotPassword)
        {
            if (HttpContext.Session.GetString("web_session_id") != null)
            {
                WriteLog.WritewebLog("Start Application");
                otp otps = new otp();
                string url = _config.GetValue<string>("APIKey") + "Services/ForgotPassword";
                string serializeProfile = Newtonsoft.Json.JsonConvert.SerializeObject(forgotPassword);
                string data = EncryptionDecryption.Encrypt(serializeProfile);
                EncryptData Endata = new EncryptData();
                Endata.EncryptObject = data;
                WriteLog.WritewebLog("Start Application");
                string data1 = HttpCall.HttpPostMethod(url, Endata);
                ForgotPassword forgot = JsonConvert.DeserializeObject<ForgotPassword>(data1);
                if (forgot.sql_status == "Success")
                {
                    string v = JsonConvert.SerializeObject(forgot);
                    HttpContext.Session.SetString("otpDetails", v);
                    HttpContext.Session.SetString("mylogindata", v);
                    otps.sql_message = forgot.sql_message;
                    forgot.otps = otps;
                    forgot.Username = forgotPassword.Username;
                    forgot.EmailId = forgotPassword.EmailId;
                    return Json(forgot);
                }
                return Json(forgot);
            }
            return RedirectToAction("Index", "Login");
        }

        public IActionResult AccountRecovery(InputForgotUser ifu)
        {
            if (HttpContext.Session.GetString("web_session_id") != null)
            {
                WriteLog.WritewebLog("Start Application");
                otp otps = new otp();
                if (ifu.email_id != null)
                {
                    string url = _config.GetValue<string>("APIKey") + "Services/ForgotuserDetails";
                    string serializeProfile = Newtonsoft.Json.JsonConvert.SerializeObject(ifu);
                    string data = EncryptionDecryption.Encrypt(serializeProfile);
                    EncryptData Endata = new EncryptData();
                    Endata.EncryptObject = data;
                    WriteLog.WritewebLog("Start Application");
                    string data1 = HttpCall.HttpPostMethod(url, Endata);
                    InputForgotUser forgot = JsonConvert.DeserializeObject<InputForgotUser>(data1);
                    if (forgot.sql_status == "Success")
                    {
                        string v = JsonConvert.SerializeObject(forgot);
                        HttpContext.Session.SetString("otpDetails", v);
                        HttpContext.Session.SetString("mylogindata", v);
                        HttpContext.Session.SetString("ForgotUser", v);
                        otps.sql_message = forgot.sql_message;
                        forgot.otps = otps;
                        forgot.pan_number = ifu.pan_number;
                        forgot.email_id = ifu.email_id;
                        forgot.account_code = ifu.account_code;
                        forgot.mobile_number = ifu.mobile_number;
                        return View(forgot);
                    }
                    return View(forgot);
                }

                return View(ifu);
            }
            return RedirectToAction("Index", "Login");
        }

        public IActionResult ChangePassword(dtoInputChangePassword dto)
        {
            if (HttpContext.Session.GetString("web_session_id") != null)
            {
                if (dto.NewPassword != null)
                {
                    string url = _config.GetValue<string>("APIKey") + "Services/ChangePassword";
                    string dtoq = HttpContext.Session.GetString("mylogindata");
                    string passEn = EncryptionDecryption.EncryptMD5(dto.NewPassword);
                    dto.NewPassword = passEn;
                    dtoInputChangePassword year = JsonConvert.DeserializeObject<dtoInputChangePassword>(dtoq);

                    if (year.Id != null)
                    {
                        dto.Id = year.Id;
                    }
                    else
                    {
                        dto.Id = Int32.Parse(year.Login_id);
                    }
                    if (year.access_token != null)
                    {
                        dto.access_token = year.access_token;
                    }
                    else
                    {
                        dto.access_token = year.token_no;
                    }
                    dto.access_token = year.access_token;
                    if (year.token_no != null)
                    {
                        dto.access_token = year.token_no;
                    }
                    dto.web_session_id = HttpContext.Session.GetString("web_session_id");
                    string serializeProfile = JsonConvert.SerializeObject(dto);
                    string data = EncryptionDecryption.Encrypt(serializeProfile);
                    EncryptData Endata = new EncryptData();
                    Endata.EncryptObject = data;
                    WriteLog.WritewebLog("Start Application");
                    string data1 = HttpCall.HttpPostMethod(url, Endata);
                    dtoInputChangePassword ot = JsonConvert.DeserializeObject<dtoInputChangePassword>(data1);
                    dto.sql_message = ot.sql_message;
                    if (ot.sql_message == "Password Updated successfully")
                    {
                        return RedirectToAction("Index", "Login");
                    }
                    
                    return View(dto);

                }
                return View(dto);
            }
            return RedirectToAction("Index", "Login");
        }
 
        /// <summary>
        /// Keep-alive ping for the session-expiry warning dialog.
        /// WCAG 2.2.1 Timing Adjustable requires the user to be able to
        /// extend a time limit; the dialog in _Layout.cshtml calls this when
        /// the user chooses "Stay signed in". Touching the session resets the
        /// sliding idle timeout without navigating away from the page, so no
        /// form data is lost.
        /// </summary>
        [HttpGet]
        public IActionResult KeepAlive()
        {
            // Reading and rewriting a session value is what refreshes the
            // sliding expiration on the session cookie.
            HttpContext.Session.SetString("a11y_last_activity",
                DateTime.UtcNow.ToString("O"));
            TempData.Keep();

            return Json(new { extended = true });
        }

        public IActionResult LogOut(Dashboard dashboard)
        {
            if (HttpContext.Session.GetString("web_session_id") != null)
            {
                if (TempData["myFinyearsdata"] != null && HttpContext.Session.GetString("web_session_id") != null)
                {
                    string url = _config.GetValue<string>("APIKey") + "Services/LogOutDetails";
                    string dtoq = TempData["myFinyearsdata"].ToString();
                    TempData.Keep("myFinyearsdata");
                    LogOut year = JsonConvert.DeserializeObject<LogOut>(dtoq);
                    year.web_session_id = HttpContext.Session.GetString("web_session_id");
                    string serializeProfile = Newtonsoft.Json.JsonConvert.SerializeObject(year);
                    string data = EncryptionDecryption.Encrypt(serializeProfile);
                    EncryptData Endata = new EncryptData();
                    Endata.EncryptObject = data;
                    WriteLog.WritewebLog("Start Application");
                    string data1 = HttpCall.HttpPostMethod(url, Endata);
                    LogOut logout = JsonConvert.DeserializeObject<LogOut>(data1);
                    if (logout.sql_message == "User Logged out")
                    {
                        HttpContext.Session.Clear();
                        TempData.Clear();

                        return RedirectToAction("Index", "Login", new { msg = "User Logged Out!"});
                    }
                    else if (logout.sql_message == "No session found")
                    {
                        HttpContext.Session.Clear();
                        TempData.Clear();

                        return RedirectToAction("Index", "Login");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
                return null;
            }
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public IActionResult otpVerify([FromBody] otp otps)
        {
            if (HttpContext.Session.GetString("web_session_id") != null)
            {
                WriteLog.WritewebLog("Start Application");
                // otp otps = new otp();
                string url = _config.GetValue<string>("APIKey") + "Services/OtpDetails";
                otps.finalotp = otps.otp1 + otps.otp2 + otps.otp3 + otps.otp4 + otps.otp5 + otps.otp6;
                string dtoq = HttpContext.Session.GetString("otpDetails");
                otp year = JsonConvert.DeserializeObject<otp>(dtoq);
                otps.token_no = year.token_no;
                otps.access_token = year.token_no;
                otps.web_session_id = HttpContext.Session.GetString("web_session_id");
                string serializeProfile = JsonConvert.SerializeObject(otps);
                string data = EncryptionDecryption.Encrypt(serializeProfile);
                EncryptData Endata = new EncryptData();
                Endata.EncryptObject = data;
                WriteLog.WritewebLog("Start Application");
                string data1 = HttpCall.HttpPostMethod(url, Endata);
                otp ot = JsonConvert.DeserializeObject<otp>(data1);
                return Json(ot);
                //if (ot.sql_status == "Success")
                //{
                //    return RedirectToAction("ChangePassword", "Login");
                //}else
                //{
                //    return 
                //}
            }
            return RedirectToAction("Index", "Login");
        }

        //public IActionResult FetchFev(FetchFevourite fv)
        //{
        //    if (HttpContext.Session.GetString("web_session_id") != null)
        //    {
        //        WriteLog.WritewebLog("Start Application");
        //        if (TempData["myFinyearsdata"] != null && HttpContext.Session.GetString("web_session_id") != null)
        //        {
        //            string url = _config.GetValue<string>("APIKey") + "Services/FetchFevDetails";
        //            string dtoq = TempData["myFinyearsdata"].ToString();
        //            TempData.Keep("myFinyearsdata");
        //            FetchFevourite year = JsonConvert.DeserializeObject<FetchFevourite>(dtoq);
        //            year.web_session_id = HttpContext.Session.GetString("web_session_id");
        //            string serializeProfile = Newtonsoft.Json.JsonConvert.SerializeObject(year);
        //            string data = EncryptionDecryption.Encrypt(serializeProfile);
        //            EncryptData Endata = new EncryptData();
        //            Endata.EncryptObject = data;
        //            WriteLog.WritewebLog("Start Application");
        //            string data1 = HttpCall.HttpPostMethod(url, Endata);
        //            LogOut logout = JsonConvert.DeserializeObject<LogOut>(data1);
        //        }
        //        return null;
        //    }
        //    return RedirectToAction("Index", "Login");
        //}


        public IActionResult ForgotUser([FromBody] ChangeUsername dto)
        {
            if (HttpContext.Session.GetString("web_session_id") != null)
            {
                WriteLog.WritewebLog("Start Application");
                if (dto.new_username != null)
                {
                    string url = _config.GetValue<string>("APIKey") + "Services/NewuserDetails";
                    string dtoq = HttpContext.Session.GetString("ForgotUser");
                    ChangeUsername year = JsonConvert.DeserializeObject<ChangeUsername>(dtoq);
                    if (year.Id != null)
                    {
                        dto.Id = year.Id;
                    }
                    else
                    {
                        dto.Id = Int32.Parse(year.LoginId);
                    }
                    // dto.Id = Int32.Parse(year.LoginId);
                    // dto.Id = Int32.Parse(year.Login_id);
                    dto.access_token = year.access_token;
                    if (year.token_no != null)
                    {
                        dto.access_token = year.token_no;
                    }
                    dto.web_session_id = HttpContext.Session.GetString("web_session_id");
                    string serializeProfile = JsonConvert.SerializeObject(dto);
                    string data = EncryptionDecryption.Encrypt(serializeProfile);
                    EncryptData Endata = new EncryptData();
                    Endata.EncryptObject = data;
                    WriteLog.WritewebLog("Start Application");
                    string data1 = HttpCall.HttpPostMethod(url, Endata);
                    dtoInputChangePassword ot = JsonConvert.DeserializeObject<dtoInputChangePassword>(data1);
                    dto.sql_message = ot.sql_message;
                    return Json(dto);
                }
                return Json(dto);
            }
            return RedirectToAction("Index", "Login");
        }

        public IActionResult ResentAccountOtp([FromBody] InputForgotUser ifu)
        {
            if (HttpContext.Session.GetString("web_session_id") != null)
            {
                WriteLog.WritewebLog("Start Application");
                otp otps = new otp();
                if (ifu.email_id != null)
                {
                    string url = _config.GetValue<string>("APIKey") + "Services/ForgotuserDetails";
                    string serializeProfile = Newtonsoft.Json.JsonConvert.SerializeObject(ifu);
                    string data = EncryptionDecryption.Encrypt(serializeProfile);
                    EncryptData Endata = new EncryptData();
                    Endata.EncryptObject = data;
                    WriteLog.WritewebLog("Start Application");
                    string data1 = HttpCall.HttpPostMethod(url, Endata);
                    InputForgotUser forgot = JsonConvert.DeserializeObject<InputForgotUser>(data1);
                    if (forgot.sql_status == "Success")
                    {
                        string v = JsonConvert.SerializeObject(forgot);
                        HttpContext.Session.SetString("otpDetails", v);
                        HttpContext.Session.SetString("mylogindata", v);
                        HttpContext.Session.SetString("ForgotUser", v);
                        otps.sql_message = forgot.sql_message;

                        forgot.otps = otps;

                        return Json(forgot);
                    }
                }

                return Json(ifu);
            }
            return RedirectToAction("Index", "Login");
        }


        public IActionResult DashChangePassword([FromBody] dtoInputChangePassword dto)
        {
            if (HttpContext.Session.GetString("web_session_id") != null)
            {
                WriteLog.WritewebLog("Start Application");
                string url = _config.GetValue<string>("APIKey") + "Services/ChangePassword";
                string dtoq = HttpContext.Session.GetString("mylogindata");
                string passEn = EncryptionDecryption.EncryptMD5(dto.NewPassword);
                dto.NewPassword = passEn;
                dtoInputChangePassword year = JsonConvert.DeserializeObject<dtoInputChangePassword>(dtoq);
                if (year.Id != null)
                {
                    dto.Id = year.Id;
                }
                else
                {
                    dto.Id = Int32.Parse(year.Login_id);
                }
                if (year.access_token != null)
                {
                    dto.access_token = year.access_token;
                }
                else
                {
                    dto.access_token = year.token_no;
                }
                dto.access_token = year.access_token;
                if (year.token_no != null)
                {
                    dto.access_token = year.token_no;
                }
                dto.web_session_id = HttpContext.Session.GetString("web_session_id");
                string serializeProfile = JsonConvert.SerializeObject(dto);
                string data = EncryptionDecryption.Encrypt(serializeProfile);
                EncryptData Endata = new EncryptData();
                Endata.EncryptObject = data;
                WriteLog.WritewebLog("Start Application");
                string data1 = HttpCall.HttpPostMethod(url, Endata);
                dtoInputChangePassword ot = JsonConvert.DeserializeObject<dtoInputChangePassword>(data1);
                dto.sql_message = ot.sql_message;
                return Json(dto);
            }
            return RedirectToAction("Index", "Login");
        }

        
        [HttpPost]
        public IActionResult GetMobileDetails([FromBody] psp_dsp_mobile_pan rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Services/GetMobileDetails";
            
            psp_dsp_mobile_pan rd = new psp_dsp_mobile_pan();

            rd.pan_number = rpt.pan_number;
            rd.Mob_No = rpt.Mob_No;

            if (rpt.Flag == "U")
            {
                rd.Asset_Class = Convert.ToInt32(rpt.JSON_Asset_Class);
                rd.Flag = rpt.Flag;
                rd.UCC_Value = rpt.UCC_Value;
            }
            else
            {
                rd.Asset_Class = Convert.ToInt32(0);
                rd.Flag = rpt.Flag;
                rd.UCC_Value = "";
            }

            string serializeProfile = JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_mobile_pan> pspdspmobilepan = JsonConvert.DeserializeObject<List<psp_dsp_mobile_pan>>(data1);
            return Json(pspdspmobilepan);
        }

        [HttpPost]
        public IActionResult ResendMobileOTP([FromBody] psp_dsp_mobile_pan rpt)
        {
            string url = _config.GetValue<string>("APIKey") + "Services/ResendMobileOTP";

            psp_dsp_mobile_pan rd = new psp_dsp_mobile_pan();

            rd.token_no = rpt.token_no;

            string serializeProfile = JsonConvert.SerializeObject(rd);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(url, Endata);
            List<psp_dsp_mobile_pan> pspdspmobilepan = JsonConvert.DeserializeObject<List<psp_dsp_mobile_pan>>(data1);
            return Json(pspdspmobilepan);
        }
    }
}
