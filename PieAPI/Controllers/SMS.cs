using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PieAPI.Repository.Interface;
using PieAPI.SMS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Xml.Serialization;
using ViewModel.ClientComms;
using ViewModel.Login;
using ViewModel.Reports;
using ViewModel.Shared;

namespace PieAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SMS : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public SMS(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("SendSMS")]
        public IActionResult SendSMS([FromBody] SMSHeader header)
        {
            //AuthController authController = new AuthController(_configuration);

            //var auth_details = authController.Get_API_Details(code);
            if (header.app_code == null || header.mobile == null || header.text == null || header.token == null || header.XAPIHeader == null)
            {
                return BadRequest("Header cannot be null.");
            }
            else
            {
                using (IDbConnection dbConnection = new SqlConnection("Data Source=" + Encryption_Decryption.EncryptionDecryption.Decrypt(_configuration.GetConnectionString("DataSource")) + "Initial Catalog=" + Encryption_Decryption.EncryptionDecryption.Decrypt(_configuration.GetConnectionString("InitialCatalog")) + "User ID=" + Encryption_Decryption.EncryptionDecryption.Decrypt(_configuration.GetConnectionString("UserID")) + "Password=" + Encryption_Decryption.EncryptionDecryption.Decrypt(_configuration.GetConnectionString("Password")) + ""))

                try
                {
                    dbConnection.Open();

                    var dp_params = new DynamicParameters();
                    dp_params.Add("app_code", header.app_code.ToString());
                    dp_params.Add("token", header.token.ToString());

                    psp_dsp_API_Details QueryOutput =
                        dbConnection.QueryFirst<psp_dsp_API_Details>("[dbo].[psp_dsp_check_api_Token]",
                        dp_params, commandType: CommandType.StoredProcedure, commandTimeout: 30);

                    var expiry_date = QueryOutput.Expire_time;
                    string app_code = header.app_code;

                    DateTime currentdate = DateTime.Now;

                    DateTime expirydate = DateTime.Parse(expiry_date.ToString());

                    if (QueryOutput.msg == "Authentication Successfull")
                    { 
                        if (expirydate < currentdate)
                        {
                            var authresponse = new AuthResponse { msg = "Token Expired" };

                            return Ok(authresponse);
                        }
                        else
                        {
                            header.text = header.text.Replace("&", "%26");
                            string URL = _configuration.GetValue<string>("SMS_URL") + header.text + "&to=" + header.mobile;

                            HttpClient client = new HttpClient();
                            string xml = "";
                            string Req_Id = "";
                            string Trans_Id = "";

                            try
                            {
                                var resultobj = System.Threading.Tasks.Task.Run(() => client.GetAsync(URL).Result);
                                resultobj.Wait();

                                if (resultobj.Result.IsSuccessStatusCode)
                                {
                                    xml = resultobj.Result.Content.ReadAsStringAsync().Result;

                                    smsXmlResponse xmlResponse = LoadFromXMLString(xml);

                                    Req_Id = xmlResponse.REQID;
                                    Trans_Id = xmlResponse.MID.TID;

                                    var smsresponse = new SMS_Response {Req_Id = Req_Id, Trans_Id = Trans_Id,  msg = "SMS Sent" };

                                    Insert_reporting_service(header.mobile, header.text, Req_Id, Trans_Id, header.XAPIHeader, "Y", "SMS Sent");

                                    return Ok(smsresponse);
                                }
                                else
                                { 
                                    Insert_reporting_service(header.mobile, header.text, Req_Id, Trans_Id, header.XAPIHeader, "E", "Error in SMS API");
                                    
                                    var smsresponse = new SMS_Response { msg = "Error in SMS API" };

                                    return Ok(smsresponse);
                                }
                            }
                            catch (Exception ex)
                            {

                                Insert_reporting_service(header.mobile, header.text, Req_Id, Trans_Id, header.XAPIHeader, "E", ex.ToString());

                                return BadRequest("Error in SMS API" + ex);
                            }

                        }
                    }
                    else
                    {
                        var authresponse = new AuthResponse { msg = QueryOutput.msg };
                        
                        return BadRequest(authresponse);
                    }
                }
                catch (Exception ex)
                {
                   return BadRequest(ex);     
                }
            }

        }
        public static smsXmlResponse LoadFromXMLString(string xmlText)
        {
            using (var stringReader = new System.IO.StringReader(xmlText))
            {
                var serializer = new XmlSerializer(typeof(smsXmlResponse));
                return serializer.Deserialize(stringReader) as smsXmlResponse;
            }
        }
        public object Insert_reporting_service(string report_to, string report_subject, string req_id, string trans_id, string XAPIHEADER, string send_status, string send_message)
        {
            using (IDbConnection dbConnection = new SqlConnection("Data Source=" + Encryption_Decryption.EncryptionDecryption.Decrypt(_configuration.GetConnectionString("DataSource")) + "Initial Catalog=" + Encryption_Decryption.EncryptionDecryption.Decrypt(_configuration.GetConnectionString("InitialCatalog")) + "User ID=" + Encryption_Decryption.EncryptionDecryption.Decrypt(_configuration.GetConnectionString("UserID")) + "Password=" + Encryption_Decryption.EncryptionDecryption.Decrypt(_configuration.GetConnectionString("Password")) + ""))
            try
            {
                dbConnection.Open();

                var dp_params = new DynamicParameters();
                dp_params.Add("report_id", 0);
                dp_params.Add("report_to", report_to, DbType.String);
                dp_params.Add("report_cc", "");
                dp_params.Add("report_export_format", "");
                dp_params.Add("report_parameters", "");
                dp_params.Add("report_subject", report_subject, DbType.String);
                dp_params.Add("report_body", "");
                dp_params.Add("message_type", "AS");
                dp_params.Add("profile_id", 2);
                dp_params.Add("priority", 1);
                dp_params.Add("XAPIHEADER", XAPIHEADER);
                dp_params.Add("req_id", req_id);
                dp_params.Add("trans_id", trans_id);
                dp_params.Add("send_status", send_status);
                dp_params.Add("send_message", send_message);

                List<psp_amd_reporting_service_reports> QueryOutput =
                        dbConnection.Query<psp_amd_reporting_service_reports>("[dbo].[psp_amd_reporting_service_reports_from_API]",
                        dp_params, commandType: CommandType.StoredProcedure, commandTimeout: 30).ToList();
                return QueryOutput;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
        //public object Generate_Token(string app_code)
        //{
        //    //Encryption_Decryption.EncryptionDecryption.Decrypt("vg/rwwHSqunr/zbMqZyrOmLmMhEiMoYTmc7bhLSAlP8=");

        //    using (IDbConnection dbConnection = new SqlConnection("Data Source=" + Encryption_Decryption.EncryptionDecryption.Decrypt(_configuration.GetConnectionString("DataSource")) + "Initial Catalog=" + Encryption_Decryption.EncryptionDecryption.Decrypt(_configuration.GetConnectionString("InitialCatalog")) + "User ID=" + Encryption_Decryption.EncryptionDecryption.Decrypt(_configuration.GetConnectionString("UserID")) + "Password=" + Encryption_Decryption.EncryptionDecryption.Decrypt(_configuration.GetConnectionString("Password")) + ""))

        //        try
        //        {
        //            dbConnection.Open();

        //            var dp_params = new DynamicParameters();
        //            dp_params.Add("app_code", app_code.ToString());
        //            //dp_params.Add("user", user.ToString());
        //            //dp_params.Add("password", password.ToString());

        //            List<psp_dsp_get_token> QueryOutput =
        //                    dbConnection.Query<psp_dsp_get_token>("[dbo].[psp_dsp_generate_token]",
        //                    dp_params, commandType: CommandType.StoredProcedure, commandTimeout: 30).ToList();
        //            return QueryOutput;
        //        }
        //        catch (Exception ex)
        //        {
        //            throw (ex);
        //        }
        //}
    }
}
