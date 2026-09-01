using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PieAPI.Repository.Interface;
using PieAPI.SMS;
using PTUtility.Interfaces.Data;
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
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost("Authentication")]
        public IActionResult Authentication([FromBody] Authheader header)
        {
            //Encryption_Decryption.EncryptionDecryption.Decrypt("Rk/OEYRScsoI2/8Xp7d72UD/uxN4JSrbo7q04ddGpp0=");

            if (header.app_code == null || header.user == null || header.password == null)
            {
                return BadRequest("Header cannot be null.");
            }
            else
            {
            using (IDbConnection dbConnection = new SqlConnection("Data Source=" + Encryption_Decryption.EncryptionDecryption.Decrypt(_configuration.GetConnectionString("DataSource")) + "Initial Catalog=" + Encryption_Decryption.EncryptionDecryption.Decrypt(_configuration.GetConnectionString("InitialCatalog")) + "User ID=" + Encryption_Decryption.EncryptionDecryption.Decrypt(_configuration.GetConnectionString("UserID")) + "Password=" + Encryption_Decryption.EncryptionDecryption.Decrypt(_configuration.GetConnectionString("Password")) + ""))

                try
                {
                    dbConnection.Open();

                    var password = Encryption_Decryption.EncryptionDecryption.Encrypt(header.password);

                    var dp_params = new DynamicParameters();
                    dp_params.Add("app_code", header.app_code.ToString());
                    dp_params.Add("user", header.user.ToString());
                    dp_params.Add("password", password.ToString());

                    psp_dsp_API_Details QueryOutput =
                        dbConnection.QueryFirst<psp_dsp_API_Details>("[dbo].[psp_dsp_API_Details]",
                        dp_params, commandType: CommandType.StoredProcedure, commandTimeout: 30);

                    if (QueryOutput.msg == "Authentication Failed")
                    {
                        var authresponse = new AuthResponse { msg = QueryOutput.msg };
                        
                        return BadRequest(authresponse);
                    }
                    else
                    {
                        var authresponse = new AuthResponse { msg = QueryOutput.msg, Token = QueryOutput.Token };
                            
                        return Ok(authresponse);
                    }
                }

                catch (Exception ex)
                {
                    throw (ex);
                }
            }
        }
    }
}
