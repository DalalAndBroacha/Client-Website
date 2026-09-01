using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PieReports.Encryption_Decryption;
using PieReports.Models;
using System.Collections.Generic;
using System.Net.Http;
using System;
using ViewModel.Login;
using ViewModel.MINT;
using ViewModel.Reports.Global_Report;
using ViewModel.Shared;
using static Org.BouncyCastle.Math.EC.ECCurve;
using System.Net.Http.Headers;
using System.Text;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.IO;
using System.Security.Policy;
using DocumentFormat.OpenXml.Bibliography;
using ViewModel.Reports;

namespace PieReports.Controllers
{
	public class MintController : Controller
	{
		private IConfiguration _config;
		private static string MintBaseUrl, MintUserId, MintPassword;

		public MintController(IConfiguration config)
		{
			_config = config;
			MintBaseUrl = _config.GetValue<string>("mintApiBaseUrl");
			MintUserId = _config.GetValue<string>("mintApiUser");
			MintPassword = _config.GetValue<string>("mintApiPassword");
		}

		public async static Task<string> GetMintAuthorizationToken()
		{
			string authURL = MintBaseUrl + "/auth/getAuthorizationToken";
			InvestwellCreds objInvestwell = new InvestwellCreds();

			objInvestwell.authName = MintUserId;
			objInvestwell.password = MintPassword;

			HttpClient _objHttp = new HttpClient();

			_objHttp.BaseAddress = new Uri(authURL);

			_objHttp.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			var request = JsonConvert.SerializeObject(objInvestwell);
			var content = new StringContent(request, Encoding.UTF8, "application/json");

			try
			{
				var Response = await _objHttp.PostAsync(authURL, content);

				if (Response.IsSuccessStatusCode)
				{

					string jsonString = Response.Content.ReadAsStringAsync().Result;

					//string jsonString = Response.Result.Content.ReadAsStringAsync().Result.ToString();

					InvestwellToken objData = JsonConvert.DeserializeObject<InvestwellToken>(jsonString);

					if (objData.GetType().GetProperty("result") != null)
					{
						return objData.result.token;
					}
					else
					{
						return null;
					}
				}
				else
				{
					return null;
				}
			}
			catch (Exception)
			{
				return null;
			}
		}
		[HttpPost]
		public IActionResult psp_dsp_mint_mf_clients([FromBody] psp_dsp_mint_mf_clients pampc)
		{
			string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_mint_mf_clients";

			string serializeProfile = JsonConvert.SerializeObject(pampc);

			string data = EncryptionDecryption.Encrypt(serializeProfile);
			EncryptData Endata = new EncryptData();
			Endata.EncryptObject = data;
			string data1 = HttpCall.HttpPostMethod(url, Endata);

			TempData["mintClientData"] = data1;
			TempData.Keep("mintClientData");

			List<psp_dsp_mint_mf_clients> Result = JsonConvert.DeserializeObject<List<psp_dsp_mint_mf_clients>>(data1);

			return Json(Result);
		}

		[HttpPost]
		public IActionResult FetchMFGlobalReport([FromBody] GlobalReportMF grmf)
		{
			string MintToken = GetMintAuthorizationToken().Result;
			if (MintToken == null)
			{
                MintGlobalReportResponse jsonFailResult = new MintGlobalReportResponse{ 
					message = "Service is temporarily unavailable, please try again later.",
					status = "Fail" };
				return Json(jsonFailResult);
			}
            string DbToken = Guid.NewGuid().ToString();
            string strJsonPortfolioReturn, strJsonTransaction, strJsonPnL;
			strJsonPortfolioReturn = strJsonTransaction = strJsonPnL = null;

			List<PortfolioReturnScheme> objSchemeLiData = new List<PortfolioReturnScheme>();
			List<Transaction> objTransDataLi = new List<Transaction>();
			List<Transaction> objDiviDataLi = new List<Transaction>();
            //List<MFGlobalPnLDisplay> mFGlobalPnLDisplay = new List<MFGlobalPnLDisplay>();            

            string dtoq = TempData["mintClientData"].ToString();
			TempData.Keep("mintClientData");
			List<psp_dsp_client_accounts> mintClientData = JsonConvert.DeserializeObject<List<psp_dsp_client_accounts>>(dtoq);
			string clientPAN = mintClientData.Find(x => x.main_client_id == grmf.main_client_id).pan_no;
			string mintClientName = mintClientData.Find(x => x.main_client_id == grmf.main_client_id).mint_clientname;

			/* PNL
			if (grmf.showPnL == "Yes")
            {
                string portfolioReturnUrl = MintBaseUrl + $"/reports/getRealizedCapitalGain?" +
                                $"filters=[{{\"pan\":\"{clientPAN}\"}},{{\"investorExactName\":\"{mintClientName}\"}}," +
								$"{{\"panWiseData\":true}}]&year={grmf.FINYR}&token="
                                + MintToken;

                HttpClient HttpPortfolioReturn = new HttpClient();
                HttpPortfolioReturn.BaseAddress = new Uri(portfolioReturnUrl);
                var ResponsePortfolioReturn = HttpPortfolioReturn.GetAsync(portfolioReturnUrl).Result;

                string jsondata = ResponsePortfolioReturn.Content.ReadAsStringAsync().Result.ToString();

                MintApiRealizedGainLoss objData = JsonConvert.DeserializeObject<MintApiRealizedGainLoss>(jsondata);

                MintPnLShorttermDebtFifo[] debtShortData = objData.result.debt.shortTerm.fifo;
				MintPnLLongtermDebtFifo[] debtLongData = objData.result.debt.longTerm.fifo;
                MintPnLEquityShorttermFifo[] equiShortData = objData.result.equity.shortTerm.fifo;
                Longtermafterjan18Fifo[] equiLongPreJan18Data = objData.result.equity.longTermBeforeJan18.fifo;
				Longtermafterjan18Fifo[] equiLongPostJan18Data = objData.result.equity.longTermAfterJan18.fifo;

                #region Debt Short Term
                foreach (var item in debtShortData)
				{
                    foreach (var fifoL2 in item.fifo)
                    {
						mFGlobalPnLDisplay.Add(new MFGlobalPnLDisplay
						{
                            units = fifoL2.units,
							folioNo = fifoL2.folioNo,
							isinNo	= fifoL2.isinNo,
							investorName = fifoL2.investorName,
							schemeName = fifoL2.schemeName,
							purchaseDate = fifoL2.purchaseDate,
							purchaseAmount = fifoL2.purchaseAmount,
							sellDate = fifoL2.sellDate,
							sellAmount = fifoL2.sellAmount,
							adjustedPurchaseAmount = fifoL2.adjustedPurchaseAmount,
							taxableGain	= fifoL2.taxableGain
                        });
                    }
                }
                #endregion

                #region Debt Long Term
                foreach (var item in debtLongData)
                {
                    foreach (var fifoL2 in item.fifo)
                    {
                        mFGlobalPnLDisplay.Add(new MFGlobalPnLDisplay
                        {
                            units = fifoL2.units,
                            folioNo = fifoL2.folioNo,
                            isinNo = fifoL2.isinNo,
                            investorName = fifoL2.investorName,
                            schemeName = fifoL2.schemeName,
                            purchaseDate = fifoL2.purchaseDate,
                            purchaseAmount = fifoL2.purchaseAmount,
                            sellDate = fifoL2.sellDate,
                            sellAmount = fifoL2.sellAmount,
                            adjustedPurchaseAmount = fifoL2.adjustedPurchaseAmount,
                            taxableGain = fifoL2.taxableGain
                        });
                    }
                }
                #endregion

                #region Equi Long *PreJan18* Data
                foreach (var item in equiShortData)
                {
                    foreach (var fifoL2 in item.fifo)
                    {
                        mFGlobalPnLDisplay.Add(new MFGlobalPnLDisplay
                        {
                            units = fifoL2.units,
                            folioNo = fifoL2.folioNo,
                            isinNo = fifoL2.isinNo,
                            investorName = fifoL2.investorName,
                            schemeName = fifoL2.schemeName,
                            purchaseDate = fifoL2.purchaseDate,
                            purchaseAmount = fifoL2.purchaseAmount,
                            sellDate = fifoL2.sellDate,
                            sellAmount = fifoL2.sellAmount,
                            adjustedPurchaseAmount = fifoL2.adjustedPurchaseAmount,
                            taxableGain = fifoL2.taxableGain
                        });
                    }
                }
                #endregion

                #region Equi Long *PreJan18* Data
                foreach (var item in equiLongPreJan18Data)
                {
                    foreach (var fifoL2 in item.fifo)
                    {
                        mFGlobalPnLDisplay.Add(new MFGlobalPnLDisplay
                        {
                            units = fifoL2.units,
                            folioNo = fifoL2.folioNo,
                            isinNo = fifoL2.isinNo,
                            investorName = fifoL2.investorName,
                            schemeName = fifoL2.schemeName,
                            purchaseDate = fifoL2.purchaseDate,
                            purchaseAmount = fifoL2.purchaseAmount,
                            sellDate = fifoL2.sellDate,
                            sellAmount = fifoL2.sellAmount,
                            isGrandfathered = fifoL2.isGrandfathered,
                            adjustedPurchasePrice = fifoL2.adjustedPurchasePrice,
                            adjustedPurchaseNav = fifoL2.adjustedPurchaseNav,
                            adjustedPurchaseAmount = fifoL2.adjustedPurchaseAmount,
                            taxableGain = fifoL2.taxableGain
                        });
                    }
                }
                #endregion

                #region Equi Long *PostJan18* Data
                foreach (var item in equiLongPostJan18Data)
                {
                    foreach (var fifoL2 in item.fifo)
                    {
                        mFGlobalPnLDisplay.Add(new MFGlobalPnLDisplay
                        {
                            units = fifoL2.units,
                            folioNo = fifoL2.folioNo,
                            isinNo = fifoL2.isinNo,
                            investorName = fifoL2.investorName,
                            schemeName = fifoL2.schemeName,
                            purchaseDate = fifoL2.purchaseDate,
                            purchaseAmount = fifoL2.purchaseAmount,
                            sellDate = fifoL2.sellDate,
                            sellAmount = fifoL2.sellAmount,
                            isGrandfathered = fifoL2.isGrandfathered,
                            adjustedPurchasePrice = fifoL2.adjustedPurchasePrice,
                            adjustedPurchaseNav = fifoL2.adjustedPurchaseNav,
                            adjustedPurchaseAmount = fifoL2.adjustedPurchaseAmount,
                            taxableGain = fifoL2.taxableGain
                        });
                    }
                }
                #endregion


                mFGlobalPnLDisplay.OrderBy(x => x.schemeName);
                strJsonPnL = JsonConvert.SerializeObject(mFGlobalPnLDisplay); 

            }
			*/

            if (grmf.showHolding == "Yes")
			{
				

				string portfolioReturnUrl = MintBaseUrl + $"/reports/getPortfolioReturns?" +
								$"filters=[{{\"endDate\":\"{grmf.to_date}\"}},{{\"pan\":\"{clientPAN}\"}}," +
								$"{{\"investorExactName\":\"{mintClientName}\"}}]" +
								$"&group=schid";

				HttpClient HttpPortfolioReturn = new HttpClient();

				HttpPortfolioReturn.DefaultRequestHeaders.Add("token", MintToken);


                HttpPortfolioReturn.BaseAddress = new Uri(portfolioReturnUrl);
				var ResponsePortfolioReturn = HttpPortfolioReturn.GetAsync(portfolioReturnUrl).Result;

				string jsondata = ResponsePortfolioReturn.Content.ReadAsStringAsync().Result.ToString();

				PortfolioReturn objData = JsonConvert.DeserializeObject<PortfolioReturn>(jsondata);
				if (objData.result != null)
				{
					
					PortfolioReturnScheme[] objSchemeData = objData.result.data;
                    objSchemeLiData = objData.result.data.ToList();

					if (objSchemeLiData.Count > 0)
					{
						objSchemeLiData.Add(new PortfolioReturnScheme
						{
							schemeName = "Total",
							gain = (float)objSchemeLiData.Sum(s => Math.Round(s.gain, 2)),
							currentAmount = (float)objSchemeLiData.Sum(s => Math.Round(s.currentAmount, 2)),
							purchaseValue = (float)objSchemeLiData.Sum(s => Math.Round(s.purchaseValue, 2))
						});
					}

					strJsonPortfolioReturn = JsonConvert.SerializeObject(objSchemeData);
                }
			}
			if (grmf.showTrades == "Yes" || grmf.showDivi == "Yes")
			{
				string TransactionUrl = MintBaseUrl + $"/reports/getClientTransactions?filters=[{{\"assetType\":\"M\"}}," +
										$"{{\"panWiseData\":1}},{{\"pan\":\"{clientPAN}\"}},{{\"investorExactName\":\"{mintClientName}\"}}," +
										$"{{\"txnTypeIn\":[\"NRP\",\"SIP\",\"STI\",\"SWI\",\"DIR\",\"BON\",\"NRS\",\"STO\",\"SWO\",\"SWP\",\"DVP\"]}}," +
										$"{{\"selectedDateFrom\":\"{grmf.from_date}\"}},{{\"selectedDateTo\":\"{grmf.to_date}\"}}]";

				HttpClient HttpClientTrans = new HttpClient();

				HttpClientTrans.DefaultRequestHeaders.Add("token", MintToken);


                HttpClientTrans.BaseAddress = new Uri(TransactionUrl);
				var ResponseClientTrans = HttpClientTrans.GetAsync(TransactionUrl).Result;


				string jsondata = ResponseClientTrans.Content.ReadAsStringAsync().Result.ToString();

				ClientTransactions objData = JsonConvert.DeserializeObject<ClientTransactions>(jsondata);

				if (objData.result != null)
                {
                    strJsonTransaction = JsonConvert.SerializeObject(objData.result);

                    if (grmf.showTrades == "Yes")
					{
						objTransDataLi = objData.result.Where(x => (x.txnType != "DVP") && (x.txnType != "DIR"))
							.OrderBy(x => x.navDate).ThenBy(x => x.schemeName).ToList();
						
					}
					if (grmf.showDivi == "Yes")
					{
						objDiviDataLi = objData.result.Where(x => (x.txnType == "DVP") || (x.txnType == "DIR"))
							.OrderBy(x => x.navDate).ThenBy(x => x.schemeName).ToList();
						if (objDiviDataLi.Count > 0)
						{
							objDiviDataLi.Add(new Transaction
							{
								schemeName = "Total",
								totalAmount = (float)objDiviDataLi.Sum(s => Math.Round(s.totalAmount, 2))
							});
						}
					}
				}
			}


            string url = _config.GetValue<string>("APIKey") + "Services/psp_amd_mint_global_report_data";
            psp_amd_mint_global_report_data resJsonObj = new psp_amd_mint_global_report_data
			{
				token_no = DbToken,
				ReturnSchemejsonstring = strJsonPortfolioReturn ?? "", //Holding
				GainLossjsonstring = strJsonPnL ?? "", //PnL
				Tradesjsonstring = strJsonTransaction ?? "" //Dividend and Trades
			};
            string serializeProfile = JsonConvert.SerializeObject(resJsonObj);
			string data = EncryptionDecryption.Encrypt(serializeProfile);
			EncryptData Endata = new EncryptData();
			Endata.EncryptObject = data;
			string data1 = HttpCall.HttpPostMethod(url, Endata);

			var jsonResult = new
			{
				PortfolioReturnData = objSchemeLiData,
				TransactionData = objTransDataLi,
				DiviData = objDiviDataLi,
                //PnLData = mFGlobalPnLDisplay,
                message = "Success",
                status = "Success"
            };

			return Json(jsonResult);
		}

        [HttpPost]
        public async Task<IActionResult> generateMfReport([FromBody] mint_reports_download mrd)
        {
            string baseURL = _config.GetValue<string>("mintApiBaseUrl");

            string MintToken = GetMintAuthorizationToken().Result;
			if(MintToken == null)
			{
				var jsonResult = new { mfPostUrl = "", Message = "Service is temporarily unavailable, please try again later.", Status = "Fail" };
				return Json(jsonResult);
			}


			string dtoq = TempData["mintClientData"].ToString();
            TempData.Keep("mintClientData");
            List<psp_dsp_client_accounts> mintClientData = JsonConvert.DeserializeObject<List<psp_dsp_client_accounts>>(dtoq);
            string clientPAN = mintClientData.Find(x => x.main_client_id == mrd.ddlClientList).pan_no;
            string MintClientName = mintClientData.Find(x => x.main_client_id == mrd.ddlClientList).mint_clientname;

            string ReportDownloadUrl = "";

            if (mrd.ddlReportList == "1") //Portfolio Summary
            {
                ReportDownloadUrl = baseURL + "/reports/downloadCompletePortfolioSummary?" +
                    $"filters=[{{\"endDate\":\"{mrd.inpAsOnDatePortSummary}\",\"pan\":\"{clientPAN}\"," +
                    $"\"investorExactName\":\"{MintClientName}\",\"panWiseData\":1}}]";
            }
            else if (mrd.ddlReportList == "2") //Portfolio Return
            {
                ReportDownloadUrl = baseURL + "/reports/getPortfolioReport?" +
                    $"filters=[{{\"endDate\":\"{mrd.inpAsOnDatePortReturn}\",\"pan\":\"{clientPAN}\"," +
                    $"\"investorExactName\":\"{MintClientName}\",\"panWiseData\":1}}]";
            }
            else if (mrd.ddlReportList == "3") //Detailed Capital Gain Realized 
            {
                ReportDownloadUrl = baseURL + "/reports/downloadRealizedCapitalGainPDF?"
                + $"filters=[{{\"casArnOption\":\"0\",\"panWiseData\":true,\"investorExactName\":\"{MintClientName}\",\"pan\":\"{clientPAN}\"}}]" +
                $"+&year={Convert.ToInt32(mrd.ddlFinYearList) + 1}&pdfType=detailed&clubTxns=false";
            }
            else if (mrd.ddlReportList == "4") //Summary Capital Gain Realized
            {
                ReportDownloadUrl = baseURL + "/reports/downloadRealizedCapitalGainPDF?" +
                $"filters=[{{\"casArnOption\":\"0\",\"panWiseData\":true,\"investorExactName\":\"{MintClientName}\",\"pan\":\"{clientPAN}\"}}]" +
                $"+&year={Convert.ToInt32(mrd.ddlFinYearList) + 1}&pdfType=summary&clubTxns=false";
            }
			/* Test for Dummy Client
			//ReportDownloadUrl = baseURL + "/reports/downloadRealizedCapitalGainPDF?"
			//	+ $"filters=[{{\"casArnOption\":\"0\",\"panWiseData\":true,\"pan\":\"AAAAA1212Z\"}}]" +
			//	$"+&year={Convert.ToInt32(mrd.ddlFinYearList) + 1}&pdfType=summary&clubTxns=false&token={MintToken}";
			*/
			//WebClient webClient = new WebClient();
            string FilePath = _config.GetValue<string>("WSReportDownloadpath");
            string FileName = Guid.NewGuid().ToString() + ".pdf";
            string FileLocation = FilePath + FileName;

            bool FlieDownloaded = false;

			try
            {
				FlieDownloaded = await DownloadAndSave(ReportDownloadUrl, MintToken, FilePath, FileName);
				//webClient.DownloadFile(ReportDownloadUrl, FileLocation);			
			}
			catch (Exception)
            {
                throw;
            }

            if (FlieDownloaded)
            {
                var jsonResult = new { mfPostUrl = FileName, Message = "File Downloaded", Status = "Success" };
                return Json(jsonResult);
            }
            else
            {
                var jsonResult = new { mfPostUrl = "", Message = "Unable to process your request.", Status = "Fail" };
                return Json(jsonResult);
            }
        }

		async Task<bool> DownloadAndSave(string sourceFile, string token, string destinationFolder, string destinationFileName)
		{
			Stream fileStream = await GetFileStream(sourceFile, token);

			if (fileStream != Stream.Null)
			{
				await SaveStream(fileStream, destinationFolder, destinationFileName);
				return true;
			}
			else
			{
				return false;
			}
		}

		async Task<Stream> GetFileStream(string fileUrl, string token)
		{
			HttpClient httpClient = new HttpClient();
			try
			{
                httpClient.DefaultRequestHeaders.Add("token", token);

				var response = await httpClient.GetAsync(fileUrl, HttpCompletionOption.ResponseHeadersRead);
				if (response != null)
				{
					if (response.StatusCode == HttpStatusCode.OK)
					{
						Stream fileStream = await response.Content.ReadAsStreamAsync();
						return fileStream;
					}
					else
					{
						return Stream.Null;
					}
				}
				else
				{
					return Stream.Null;
				}
			}
			catch (Exception ex)
			{
				return Stream.Null;
			}
		}

		async Task SaveStream(Stream fileStream, string destinationFolder, string destinationFileName)
		{
			if (!Directory.Exists(destinationFolder))
				Directory.CreateDirectory(destinationFolder);

			string path = Path.Combine(destinationFolder, destinationFileName);

			using (FileStream outputFileStream = new FileStream(path, FileMode.CreateNew))
			{
				await fileStream.CopyToAsync(outputFileStream);
			}
		}

        [HttpPost]
        public async Task<IActionResult> DownloadMintReport([FromBody] psp_rpt_asset_allocation rpt)
        {
            string baseURL = _config.GetValue<string>("mintApiBaseUrl");
            string MintToken = GetMintAuthorizationToken().Result;

            string ReportDownloadUrl = "";

            if (MintToken == null)
            {
                var jsonResult = new { mfPostUrl = "", Message = "Service is temporarily unavailable, please try again later.", Status = "Fail" };
                return Json(jsonResult);
            }
            else
            {
                ReportDownloadUrl = baseURL + "/reports/getPortfolioReport?" +
                  $"filters=[{{\"endDate\":\"{rpt.AsOnDatePortReturn}\",\"pan\":\"{rpt.pan}\",\"category\":\"{rpt.category}\",\"" +
                  $"panWiseData\":1,\"investorExactName\":\"{rpt.Mint_client_name}\"}}]";

                string FilePath = _config.GetValue<string>("WSReportDownloadpath");
                string FileName = Guid.NewGuid().ToString() + ".pdf";

                bool FlieDownloaded = false;

                try
                {
                    FlieDownloaded = await DownloadAndSave(ReportDownloadUrl, MintToken, FilePath, FileName);
                }
                catch (Exception ex)
                {
                    throw;
                }

                if (FlieDownloaded)
                {
                    var jsonResult = new { mfPostUrl = FileName, Message = "File Downloaded", Status = "Success" };
                    return Json(jsonResult);
                }
                else
                {
                    var jsonResult = new { mfPostUrl = "", Message = "Unable to process your request.", Status = "Fail" };
                    return Json(jsonResult);
                }
            }
        }
    }
}
