using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PieAPI.Encryption_Decryption;
using PieAPI.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks;
using ViewModel;
using ViewModel.ClientComms;
using ViewModel.IPO;
using ViewModel.Login;
using ViewModel.MINT;
using ViewModel.Reports;
using ViewModel.Reports.PMS;
using ViewModel.Reports.ResearchReport;
using ViewModel.Shared;

namespace PieAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServicesController : ControllerBase
    {
        private readonly IServices _locationLookup;
        private readonly IConfiguration _config;
        private readonly SmtpSettings _smtpSettingsObj;
        private readonly IDapperRepository _repository;

        public ServicesController(IServices locationLookup, IConfiguration configuration, IDapperRepository repository)
        {
            _locationLookup = locationLookup;
            _config = configuration;
            _repository = repository;

            SmtpSettings smtpSettingsObj = new SmtpSettings()
            {
                smtpHost = _config.GetValue<string>("SmtpHost"),
                smtpUid = _config.GetValue<string>("SmtpUid"),
                smtpPass = _config.GetValue<string>("SmtpPass"),
                smtpPort = _config.GetValue<Int32>("SmtpPort")

            };
            _smtpSettingsObj = smtpSettingsObj;
        }

        [HttpGet]
        [Route("Login")]
        public String Login(string endlogin)
        {
            return "get";
        }

        [HttpPost]
        [Route("AccountLogin")]
        public object AccountLogin(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            UserLogin serializeProfile = JsonConvert.DeserializeObject<UserLogin>(data);


            return _locationLookup.AccountLogin(serializeProfile);
        }


        [HttpPost]
        [Route("ChangePassword")]
        public object ChangePassword(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            dtoInputChangePassword serializee = JsonConvert.DeserializeObject<dtoInputChangePassword>(data);


            return _locationLookup.ChangePassword(serializee);
        }
        [HttpPost]
        [Route("LogOutDetails")]
        public object LogOutDetails(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            LogOut serializeProfile = JsonConvert.DeserializeObject<LogOut>(data);


            return _locationLookup.LogOutDetails(serializeProfile);

        }
        [HttpPost]
        [Route("ForgotPassword")]
        public object ForgotPassword(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            ForgotPassword serializeProfile = JsonConvert.DeserializeObject<ForgotPassword>(data);


            return _locationLookup.ForgotPassword(serializeProfile);

        }
        [HttpPost]
        [Route("OtpDetails")]
        public object OtpDetails(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            otp serializeProfile = JsonConvert.DeserializeObject<otp>(data);
            return _locationLookup.OtpDetails(serializeProfile);

        }

        [HttpPost]
        [Route("ForgotuserDetails")]
        public object ForgotuserDetails(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            InputForgotUser serializeProfile = JsonConvert.DeserializeObject<InputForgotUser>(data);
            return _locationLookup.ForgotuserDetails(serializeProfile);

        }

        [HttpPost]
        [Route("NewuserDetails")]
        public object NewuserDetails(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            ChangeUsername serializeProfile = JsonConvert.DeserializeObject<ChangeUsername>(data);
            return _locationLookup.NewuserDetails(serializeProfile);

        }

        [HttpPost]
        [Route("GetMobileDetails")]
        
        public object GetMobileDetails(EncryptData endlogin) //On the basis on PAN
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_mobile_pan serializeProfile = JsonConvert.DeserializeObject<psp_dsp_mobile_pan>(data);


            return _locationLookup.GetMobileDetails(serializeProfile);
        }

        [HttpPost]
        [Route("ResendMobileOTP")]

        public object ResendMobileOTP(EncryptData endlogin) //On the basis on PAN
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_mobile_pan serializeProfile = JsonConvert.DeserializeObject<psp_dsp_mobile_pan>(data);

            return _locationLookup.ResendMobileOTP(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_client_accounts")]
        public object psp_dsp_client_accounts(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_client_accounts serializeProfile = JsonConvert.DeserializeObject<psp_dsp_client_accounts>(data);

            return _locationLookup.psp_dsp_client_accounts(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_nri_client_list")]
        public object psp_dsp_client_kyc_attributes(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_nri_client_list serializeProfile = JsonConvert.DeserializeObject<psp_dsp_nri_client_list>(data);

            return _locationLookup.psp_dsp_nri_client_list(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_nri_category")]
        public object psp_dsp_nri_category(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_nri_category serializeProfile = JsonConvert.DeserializeObject<psp_dsp_nri_category>(data);

            return _locationLookup.psp_dsp_nri_category(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_calendar_year")]
        public object psp_dsp_calendar_year(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_calendar_year serializeProfile = JsonConvert.DeserializeObject<psp_dsp_calendar_year>(data);

            return _locationLookup.psp_dsp_calendar_year(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_nri_bank_details")]
        public object psp_dsp_nri_bank_details(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_nri_bank_details serializeProfile = JsonConvert.DeserializeObject<psp_dsp_nri_bank_details>(data);

            return _locationLookup.psp_dsp_nri_bank_details(serializeProfile);
        }
        [HttpPost]
        [Route("psp_amd_nri_client_data_entry")]
        public object psp_amd_nri_client_data_entry(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_amd_nri_client_data_entry serializeProfile = JsonConvert.DeserializeObject<psp_amd_nri_client_data_entry>(data);

            return _locationLookup.psp_amd_nri_client_data_entry(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_nri_bankmaster_details")]
        public object psp_dsp_nri_bankmaster_details(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_nri_bankmaster_details serializeProfile = JsonConvert.DeserializeObject<psp_dsp_nri_bankmaster_details>(data);

            return _locationLookup.psp_dsp_nri_bankmaster_details(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_nri_bank_account_type_list")]
        public object psp_dsp_nri_bank_account_type_list(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_nri_bank_account_type_list serializeProfile = JsonConvert.DeserializeObject<psp_dsp_nri_bank_account_type_list>(data);

            return _locationLookup.psp_dsp_nri_bank_account_type_list(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_nri_client_category_list")]
        public object psp_dsp_nri_client_category_list(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_nri_client_category_list serializeProfile = JsonConvert.DeserializeObject<psp_dsp_nri_client_category_list>(data);

            return _locationLookup.psp_dsp_nri_client_category_list(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_get_kyc_details")]
        public object psp_dsp_get_kyc_details(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_get_kyc_details serializeProfile = JsonConvert.DeserializeObject<psp_dsp_get_kyc_details>(data);

            return _locationLookup.psp_dsp_get_kyc_details(serializeProfile);
        }
        [HttpPost]
        [Route("psp_amd_equity_kyc_initiate_update_request")]
        public object psp_amd_equity_kyc_initiate_update_request(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            kyc_initiate_auth_resend_request serializeProfile = JsonConvert.DeserializeObject<kyc_initiate_auth_resend_request>(data);

            return _locationLookup.psp_amd_equity_kyc_initiate_update_request(serializeProfile);
        }
        [HttpPost]
        [Route("psp_amd_equity_kyc_authenticate_update_request")]
        public object psp_amd_equity_kyc_authenticate_update_request(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            kyc_initiate_auth_resend_request serializeProfile = JsonConvert.DeserializeObject<kyc_initiate_auth_resend_request>(data);

            return _locationLookup.psp_amd_equity_kyc_authenticate_update_request(serializeProfile);
        }
        [HttpPost]
        [Route("psp_amd_equity_kyc_resend_request")]
        public object psp_amd_equity_kyc_resend_request(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            kyc_initiate_auth_resend_request serializeProfile = JsonConvert.DeserializeObject<kyc_initiate_auth_resend_request>(data);

            return _locationLookup.psp_amd_equity_kyc_resend_request(serializeProfile);
        }
        [HttpPost]
        [Route("psp_amd_equity_kyc_capture_update_request")]
        public object psp_amd_equity_kyc_capture_update_request(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_amd_equity_kyc_capture_update_request serializeProfile = JsonConvert.DeserializeObject<psp_amd_equity_kyc_capture_update_request>(data);

            return _locationLookup.psp_amd_equity_kyc_capture_update_request(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_kyc_backoffice_requests")]
        public object psp_dsp_kyc_backoffice_requests(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_kyc_backoffice_requests serializeProfile = JsonConvert.DeserializeObject<psp_dsp_kyc_backoffice_requests>(data);

            return _locationLookup.psp_dsp_kyc_backoffice_requests(serializeProfile);
        }
        [HttpPost]
        [Route("psp_amd_kyc_backoffice_request_action")]
        public object psp_amd_kyc_backoffice_request_action(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_amd_kyc_backoffice_request_action serializeProfile = JsonConvert.DeserializeObject<psp_amd_kyc_backoffice_request_action>(data);

            return _locationLookup.psp_amd_kyc_backoffice_request_action(serializeProfile);
        }

        [HttpPost]
        [Route("psp_dsp_client_list")]
        public object psp_dsp_client_list(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_client_list serializeProfile = JsonConvert.DeserializeObject<psp_dsp_client_list>(data);

            return _locationLookup.psp_dsp_client_list(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_date_values")]
        public object psp_dsp_date_values(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_date_values serializeProfile = JsonConvert.DeserializeObject<psp_dsp_date_values>(data);

            return _locationLookup.psp_dsp_date_values(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_global_report_date_pills")]
        public object psp_dsp_global_report_date_pills(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_global_report_date_pills serializeProfile = JsonConvert.DeserializeObject<psp_dsp_global_report_date_pills>(data);

            return _locationLookup.psp_dsp_global_report_date_pills(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_get_kyc_update_history")]
        public object psp_dsp_get_kyc_update_history(EncryptData endData)
        {
            string data = EncryptionDecryption.Decrypt(endData.EncryptObject);
            psp_dsp_get_kyc_update_history serializeProfile = JsonConvert.DeserializeObject<psp_dsp_get_kyc_update_history>(data);

            return _locationLookup.psp_dsp_get_kyc_update_history(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_single_family_details")]
        public object psp_dsp_single_family_details(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_single_family_details serializeProfile = JsonConvert.DeserializeObject<psp_dsp_single_family_details>(data);

            return _locationLookup.psp_dsp_single_family_details(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_profile_details")]
        public object psp_dsp_profile_details(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_profile_details serializeProfile = JsonConvert.DeserializeObject<psp_dsp_profile_details>(data);

            return _locationLookup.psp_dsp_profile_details(serializeProfile);
        }
        [HttpPost]
        [Route("psp_amd_family_details")]
        public object psp_amd_family_details(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_amd_family_details serializeProfile = JsonConvert.DeserializeObject<psp_amd_family_details>(data);

            return _locationLookup.psp_amd_family_details(serializeProfile);
        }

        [HttpPost]
        [Route("psp_amd_profile_details")]
        public object psp_amd_profile_details(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_amd_profile_details serializeProfile = JsonConvert.DeserializeObject<psp_amd_profile_details>(data);

            return _locationLookup.psp_amd_profile_details(serializeProfile);
        }

        [HttpPost]
        [Route("psp_dsp_AIF_scheme")]
        public object psp_dsp_AIF_scheme(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_AIF_scheme serializeProfile = JsonConvert.DeserializeObject<psp_dsp_AIF_scheme>(data);

            return _locationLookup.psp_dsp_AIF_scheme(serializeProfile);
        }
        [HttpPost]
        [Route("psp_amd_AIF_data_entry")]
        public object psp_amd_AIF_data_entry(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_amd_AIF_data_entry serializeProfile = JsonConvert.DeserializeObject<psp_amd_AIF_data_entry>(data);

            return _locationLookup.psp_amd_AIF_data_entry(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_AIF_nav_data")]
        public object psp_dsp_AIF_nav_data(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_AIF_nav_data serializeProfile = JsonConvert.DeserializeObject<psp_dsp_AIF_nav_data>(data);

            return _locationLookup.psp_dsp_AIF_nav_data(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_survey_list")]
        public object psp_dsp_survey_list(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_survey_list serializeProfile = JsonConvert.DeserializeObject<psp_dsp_survey_list>(data);

            return _locationLookup.psp_dsp_survey_list(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_survey")]
        public object psp_dsp_survey(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_survey serializeProfile = JsonConvert.DeserializeObject<psp_dsp_survey>(data);

            return _locationLookup.psp_dsp_survey(serializeProfile);
        }
        [HttpPost]
        [Route("psp_amd_survey_response")]
        public object psp_amd_survey_response(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_amd_survey_response serializeProfile = JsonConvert.DeserializeObject<psp_amd_survey_response>(data);

            return _locationLookup.psp_amd_survey_response(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_survey_status")]
        public object psp_dsp_survey_status(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_survey_status serializeProfile = JsonConvert.DeserializeObject<psp_dsp_survey_status>(data);

            return _locationLookup.psp_dsp_survey_status(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_survey_family_list")]
        public object psp_dsp_survey_family_list(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_survey_family_list serializeProfile = JsonConvert.DeserializeObject<psp_dsp_survey_family_list>(data);

            return _locationLookup.psp_dsp_survey_family_list(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_captured_survey_responses")]
        public object psp_dsp_captured_survey_responses(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_captured_survey_responses serializeProfile = JsonConvert.DeserializeObject<psp_dsp_captured_survey_responses>(data);

            return _locationLookup.psp_dsp_captured_survey_responses(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_model_portfolio")]
        public object psp_dsp_model_portfolio(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_model_portfolio serializeProfile = JsonConvert.DeserializeObject<psp_dsp_model_portfolio>(data);

            return _locationLookup.psp_dsp_model_portfolio(serializeProfile);
        }

        [HttpPost]
        [Route("psp_amd_model_portfolio_cp")]
        public object psp_amd_model_portfolio_cp(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_amd_model_portfolio_cp serializeProfile = JsonConvert.DeserializeObject<psp_amd_model_portfolio_cp>(data);

            return _locationLookup.psp_amd_model_portfolio_cp(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_script_isin_cp")]
        public object psp_dsp_script_isin_cp(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_script_isin_cp serializeProfile = JsonConvert.DeserializeObject<psp_dsp_script_isin_cp>(data);

            return _locationLookup.psp_dsp_script_isin_cp(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_survey_respnse_dump")]
        public object psp_dsp_survey_respnse_dump(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_survey_respnse_dump serializeProfile = JsonConvert.DeserializeObject<psp_dsp_survey_respnse_dump>(data);

            return _locationLookup.psp_dsp_survey_respnse_dump(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_process_log")]
        public object psp_dsp_process_log(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_process_log serializeProfile = JsonConvert.DeserializeObject<psp_dsp_process_log>(data);

            return _locationLookup.psp_dsp_process_log(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_periodic_kyc_log")]
        public object psp_dsp_periodic_kyc_log(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_periodic_kyc_log serializeProfile = JsonConvert.DeserializeObject<psp_dsp_periodic_kyc_log>(data);

            return _locationLookup.psp_dsp_periodic_kyc_log(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_user_access_token")]
        public object psp_dsp_user_access_token(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_user_access_token serializeProfile = JsonConvert.DeserializeObject<psp_dsp_user_access_token>(data);

            return _locationLookup.psp_dsp_user_access_token(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_common_report_module_list")]
        public object psp_dsp_common_report_module_list(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_common_report_module_list serializeProfile = JsonConvert.DeserializeObject<psp_dsp_common_report_module_list>(data);

            return _locationLookup.psp_dsp_common_report_module_list(serializeProfile);
        }

        [HttpPost]
        [Route("psp_dsp_equity_research_reports")]
        public object psp_dsp_equity_research_reports(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_equity_research_reports serializeProfile = JsonConvert.DeserializeObject<psp_dsp_equity_research_reports>(data);

            return _locationLookup.psp_dsp_equity_research_reports(serializeProfile);
        }
        [HttpPost]
        [Route("psp_amd_equity_research_reco")]
        public object psp_amd_equity_research_reco(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_amd_equity_research_reco serializeProfile = JsonConvert.DeserializeObject<psp_amd_equity_research_reco>(data);

            return _locationLookup.psp_amd_equity_research_reco(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_search_client_cp")]
        public object psp_dsp_search_client_cp(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_search_client_cp serializeProfile = JsonConvert.DeserializeObject<psp_dsp_search_client_cp>(data);

            return _locationLookup.psp_dsp_search_client_cp(serializeProfile);
        }
		[HttpPost]
		[Route("psp_dsp_mint_mf_download_ReportCriteria")]
		public object psp_dsp_mint_mf_download_ReportCriteria(EncryptData endlogin)
		{
			string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
			psp_dsp_mint_mf_download_ReportCriteria serializeProfile = JsonConvert.DeserializeObject<psp_dsp_mint_mf_download_ReportCriteria>(data);

			return _locationLookup.psp_dsp_mint_mf_download_ReportCriteria(serializeProfile);
		}
		[HttpPost]
		[Route("psp_dsp_mint_mf_report_download_list")]
		public object psp_dsp_mint_mf_report_download_list(EncryptData endlogin)
		{
			string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
			psp_dsp_mint_mf_report_download_list serializeProfile = JsonConvert.DeserializeObject<psp_dsp_mint_mf_report_download_list>(data);

			return _locationLookup.psp_dsp_mint_mf_report_download_list(serializeProfile);
		}
		[HttpPost]
		[Route("psp_amd_mint_global_report_data")]
		public object psp_amd_mint_global_report_data(EncryptData endlogin)
		{
			string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
			psp_amd_mint_global_report_data serializeProfile = JsonConvert.DeserializeObject<psp_amd_mint_global_report_data>(data);

			return _locationLookup.psp_amd_mint_global_report_data(serializeProfile);
		}
        [HttpPost]
        [Route("psp_dsp_mint_mf_clients")]
        public object psp_dsp_mint_mf_clients(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_mint_mf_clients serializeProfile = JsonConvert.DeserializeObject<psp_dsp_mint_mf_clients>(data);

            return _locationLookup.psp_dsp_mint_mf_clients(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_family_mapping_fam_details")]
        public object psp_dsp_family_mapping_fam_details(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_family_mapping_fam_details serializeProfile = JsonConvert.DeserializeObject<psp_dsp_family_mapping_fam_details>(data);

            return _locationLookup.psp_dsp_family_mapping_fam_details(serializeProfile);
        }
        [HttpPost]
        [Route("psp_amd_family_mapping_change_family")]
        public object psp_amd_family_mapping_change_family(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_amd_family_mapping_change_family serializeProfile = JsonConvert.DeserializeObject<psp_amd_family_mapping_change_family>(data);

            return _locationLookup.psp_amd_family_mapping_change_family(serializeProfile);
        }
        [HttpPost]
        [Route("psp_amd_family_mapping_create_fam")]
        public object psp_amd_family_mapping_create_fam(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_amd_family_mapping_create_fam serializeProfile = JsonConvert.DeserializeObject<psp_amd_family_mapping_create_fam>(data);

            return _locationLookup.psp_amd_family_mapping_create_fam(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_family_mapping_branch_details")]
        public object psp_dsp_family_mapping_branch_details(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_family_mapping_branch_details serializeProfile = JsonConvert.DeserializeObject<psp_dsp_family_mapping_branch_details>(data);

            return _locationLookup.psp_dsp_family_mapping_branch_details(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_family_mapping_rm_details")]
        public object psp_dsp_family_mapping_rm_details(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_family_mapping_rm_details serializeProfile = JsonConvert.DeserializeObject<psp_dsp_family_mapping_rm_details>(data);

            return _locationLookup.psp_dsp_family_mapping_rm_details(serializeProfile);
        }
        [HttpPost]
        [Route("psp_amd_family_mapping_branch_update")]
        public object psp_amd_family_mapping_branch_update(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_amd_family_mapping_branch_update serializeProfile = JsonConvert.DeserializeObject<psp_amd_family_mapping_branch_update>(data);

            return _locationLookup.psp_amd_family_mapping_branch_update(serializeProfile);
        }
        [HttpPost]
        [Route("psp_amd_family_mapping_rm_update")]
        public object psp_amd_family_mapping_rm_update(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            PspAmdFamilyMappingRmUpdate serializeProfile = JsonConvert.DeserializeObject<PspAmdFamilyMappingRmUpdate>(data);

            return _locationLookup.psp_amd_family_mapping_rm_update(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_client_coms_dormant_account")]
        public object psp_dsp_client_coms_dormant_account(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_client_coms_dormant_account serializeProfile = JsonConvert.DeserializeObject<psp_dsp_client_coms_dormant_account>(data);

            return _locationLookup.psp_dsp_client_coms_dormant_account(serializeProfile);
        }

        [HttpPost]
        [Route("psp_dsp_client_coms_dormant_mail_content")]
        public object psp_dsp_client_coms_dormant_mail_content(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_client_coms_dormant_mail_content serializeProfile = JsonConvert.DeserializeObject<psp_dsp_client_coms_dormant_mail_content>(data);

            return _locationLookup.psp_dsp_client_coms_dormant_mail_content(serializeProfile);
        }

        [HttpPost]
        [Route("DormantAccountEmail")]
        public object DormantAccountEmail(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            SmtpSendEmail serializeProfile = JsonConvert.DeserializeObject<SmtpSendEmail>(data);

            Smtp.SendEmail(_smtpSettingsObj, serializeProfile);

            return _locationLookup.DormantAccountSMSUpdate(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_client_coms_history")]
        public object psp_dsp_client_coms_history(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_client_coms_history serializeProfile = JsonConvert.DeserializeObject<psp_dsp_client_coms_history>(data);

            return _locationLookup.psp_dsp_client_coms_history(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_client_coms_exceptions")]
        public object psp_dsp_client_coms_exceptions(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_client_coms_exceptions serializeProfile = JsonConvert.DeserializeObject<psp_dsp_client_coms_exceptions>(data);

            return _locationLookup.psp_dsp_client_coms_exceptions(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_client_coms_exception_list")]
        public object psp_dsp_client_coms_exception_list(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_client_coms_exception_list serializeProfile = JsonConvert.DeserializeObject<psp_dsp_client_coms_exception_list>(data);

            return _locationLookup.psp_dsp_client_coms_exception_list(serializeProfile);
        }
        //[HttpPost]
        //[Route("psp_dsp_client_coms_outstanding_debit_send_mail")]
        //public object psp_dsp_client_coms_outstanding_debit_send_mail(EncryptData endlogin)
        //{
        //    string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
        //    psp_dsp_client_coms_outstanding_debit_send_mail serializeProfile = JsonConvert.DeserializeObject<psp_dsp_client_coms_outstanding_debit_send_mail>(data);

        //    return _locationLookup.psp_amd_client_coms_outstanding_debit_send_mail(serializeProfile);
        //}
        [HttpPost]
        [Route("psp_dsp_client_coms_outstanding_debit_send_mail")]
        public object psp_dsp_client_coms_outstanding_debit_send_mail(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_client_coms_outstanding_debit_send_mail serializeProfile = JsonConvert.DeserializeObject<psp_dsp_client_coms_outstanding_debit_send_mail>(data);

            return _locationLookup.psp_dsp_client_coms_outstanding_debit_send_mail(serializeProfile);
        }
        [HttpPost]
        [Route("psp_amd_client_coms_outstanding_debit_send_mail")]
        public object psp_amd_client_coms_outstanding_debit_send_mail(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_client_coms_outstanding_debit_send_mail serializeProfile = JsonConvert.DeserializeObject<psp_dsp_client_coms_outstanding_debit_send_mail>(data);

            return _locationLookup.psp_amd_client_coms_outstanding_debit_send_mail(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_client_communication_log")]
        public object psp_dsp_client_communication_log(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_client_communication_log serializeProfile = JsonConvert.DeserializeObject<psp_dsp_client_communication_log>(data);

            return _locationLookup.psp_dsp_client_communication_log(serializeProfile);
        }
        [HttpPost]
        [Route("psp_test_multi_set")]
        public object New(EncryptData endlogin)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("login_id", 279195, DbType.Int32);

            var result = _repository.GetMultiResultSet(query: "[dbo].[psp_test_multi_set]", sp_params: dp_params,
                                                        commandType: CommandType.StoredProcedure);

            return result;
        }

        [HttpPost]
        [Route("psp_amd_equity_settlement_upload")]
        public object psp_amd_equity_settlement_upload(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_amd_equity_settlement_upload serializeProfile = JsonConvert.DeserializeObject<psp_amd_equity_settlement_upload>(data);

            return _locationLookup.psp_amd_equity_settlement_upload(serializeProfile);
        }

        [HttpPost]
        [Route("psp_amd_equity_research_push_mobile_notification")]
        public object psp_amd_equity_research_push_mobile_notification(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_amd_equity_research_push_mobile_notification serializeProfile = JsonConvert.DeserializeObject<psp_amd_equity_research_push_mobile_notification>(data);

            return _locationLookup.psp_amd_equity_research_push_mobile_notification(serializeProfile);
        }

        [HttpPost]
        [Route("One_Pager_Update")]
        public object One_Pager_Update(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            One_Pager serializeProfile = JsonConvert.DeserializeObject<One_Pager>(data);

            return _locationLookup.One_Pager(serializeProfile);
        }


    }
}
