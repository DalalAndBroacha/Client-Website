using Dapper;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using PieAPI.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using ViewModel;
using ViewModel.Login;
using ViewModel.Reports;
using ViewModel.Shared;
using ViewModel.Reports.PMS;
using ViewModel.Reports.ResearchReport;
using ViewModel.IPO;
using ViewModel.MINT;
using System.Web;
using ViewModel.UtilityClasses;
using ViewModel.ClientComms;


namespace PieAPI.Repository
{
    public class ServicesRepository : IServices
    {

        //ISession session;
        private readonly IDapperRepository _repository;
        public ServicesRepository(IDapperRepository repository)
        {
            _repository = repository;
        }
        public object AccountLogin(UserLogin login)
        {

            var dp_params = new DynamicParameters();
            //kinjaldoshi,Kinjal@1107,F7C43D9D1CFCA590BB26BE836FE275DD, "dokinm#p,F7C43D9D1CFCA590BB26BE836FE275D,oimp@["
            dp_params.Add("username", login.Username, DbType.String);
            dp_params.Add("password", login.Password, DbType.String);
            dp_params.Add("web_session_id", login.web_session_id, DbType.String);
            dp_params.Add("login_source", "W", DbType.String);
            dp_params.Add("geo_location", login.geo_location, DbType.String);
            dp_params.Add("browser_name", login.browser_name, DbType.String);
            dp_params.Add("browser_version", login.browser_version, DbType.String);
            dp_params.Add("operation_system", login.opertation_system, DbType.String);
            dp_params.Add("device_info", login.device_info, DbType.String);
            dp_params.Add("otp_token", login.otp_token, DbType.String);
            dp_params.Add("otp", login.otp, DbType.String);

            var result = _repository.Get<UserLogin>("[dbo].[psp_dsp_mobile_user_login]", dp_params, commandType: CommandType.StoredProcedure);

            // session.SetString("LoginKey", JsonConvert.SerializeObject(result));
            return result;

        }
        public object ChangePassword(dtoInputChangePassword dto)
        {

            var dp_params = new DynamicParameters();
            //kinjaldoshi,Kinjal@1107,F7C43D9D1CFCA590BB26BE836FE275DD, "dokinm#p,oimp@["
            dp_params.Add("login_id", dto.Id, DbType.Int32);
            dp_params.Add("tokenId", dto.access_token, DbType.String);
            dp_params.Add("login_source", "W", DbType.String);
            //dp_params.Add("web_session_id", dto.web_session_id, DbType.String);
            dp_params.Add("new_password", dto.NewPassword, DbType.String);
            var result = _repository.Get<dtoInputChangePassword>("[dbo].[psp_amd_change_password]", dp_params, commandType: CommandType.StoredProcedure);

            // session.SetString("LoginKey", JsonConvert.SerializeObject(result));
            return result;

        }
        public object LogOutDetails(LogOut logout)
        {
            var dp_params = new DynamicParameters();
            //kinjaldoshi,Kinjal@1107,F7C43D9D1CFCA590BB26BE836FE275DD, "dokinm#p,oimp@["
            dp_params.Add("login_id", logout.Id, DbType.Int32);
            dp_params.Add("tokenId", logout.access_token, DbType.String);
            dp_params.Add("web_session_id", logout.web_session_id, DbType.String);
            dp_params.Add("login_source", "W", DbType.String);
            var result = _repository.Get<LogOut>("[dbo].[psp_dsp_mobile_user_logout]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }

        public object ForgotPassword(ForgotPassword logout)
        {
            var dp_params = new DynamicParameters();
            //kinjaldoshi,Kinjal@1107,F7C43D9D1CFCA590BB26BE836FE275DD, "dokinm#p,oimp@["
            dp_params.Add("username", logout.Username, DbType.String);
            dp_params.Add("email_id", logout.EmailId, DbType.String);
            var result = _repository.Get<LogOut>("[dbo].[psp_amd_forgot_password]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }

        public object OtpDetails(otp ot)
        {
            var dp_params = new DynamicParameters();
            //kinjaldoshi,Kinjal@1107,F7C43D9D1CFCA590BB26BE836FE275DD, "dokinm#p,oimp@["
            dp_params.Add("otp", ot.finalotp, DbType.String);
            dp_params.Add("token_no", ot.token_no, DbType.String);
            var result = _repository.Get<LogOut>("[dbo].[psp_amd_verify_otp]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }

        public object ForgotuserDetails(InputForgotUser ifu)
        {
            var dp_params = new DynamicParameters();
            //kinjaldoshi,Kinjal@1107,F7C43D9D1CFCA590BB26BE836FE275DD, "dokinm#p,oimp@["
            dp_params.Add("account_code", ifu.account_code, DbType.String);
            dp_params.Add("pan_number", ifu.pan_number, DbType.String);
            dp_params.Add("mobile_number", ifu.mobile_number, DbType.String);
            dp_params.Add("email_id", ifu.email_id, DbType.String);
            var result = _repository.Get<InputForgotUser>("[dbo].[psp_amd_forgot_user_credential]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }

        public object NewuserDetails(ChangeUsername changeuser)
        {
            var dp_params = new DynamicParameters();
            //kinjaldoshi,Kinjal@1107,F7C43D9D1CFCA590BB26BE836FE275DD, "dokinm#p,oimp@["
            dp_params.Add("login_id", changeuser.Id, DbType.String);
            dp_params.Add("tokenId", changeuser.access_token, DbType.String);
            dp_params.Add("login_source", "W", DbType.String);
            dp_params.Add("new_username", changeuser.new_username, DbType.String);
           
            var result = _repository.Get<ChangeUsername>("[dbo].[psp_amd_change_username]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }

        public object GetMobileDetails(psp_dsp_mobile_pan serializeProfile)
        {
            var dp_params = new DynamicParameters();
            
            dp_params.Add("pan_number", serializeProfile.pan_number, DbType.String);
            dp_params.Add("mobile_number", serializeProfile.Mob_No, DbType.String);
            dp_params.Add("flag", serializeProfile.Flag, DbType.String);
            dp_params.Add("asset_class", serializeProfile.Asset_Class, DbType.Int32);
            dp_params.Add("UCC_value", serializeProfile.UCC_Value, DbType.String);

            var result = _repository.GetAll<List<psp_dsp_mobile_pan>>("[dbo].[psp_dsp_mobile_pan]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }

        public object ResendMobileOTP(psp_dsp_mobile_pan serializeProfile)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("token_no", serializeProfile.token_no, DbType.String);

            var result = _repository.GetAll<List<psp_dsp_mobile_pan>>("[dbo].[psp_dsp_resend_otp]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }
        public object psp_dsp_client_accounts(psp_dsp_client_accounts serializeProfile)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("family_id", serializeProfile.family_id, DbType.Int32);
            dp_params.Add("asset_class", serializeProfile.asset_class, DbType.String);
            dp_params.Add("pan_no", serializeProfile.pan_no, DbType.String);
            dp_params.Add("level", serializeProfile.level, DbType.Int32);

            var result = _repository.GetAll<List<psp_dsp_client_accounts>>("[dbo].[psp_dsp_client_accounts]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }
        public object psp_dsp_nri_client_list(psp_dsp_nri_client_list serializeProfile)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("country", serializeProfile.country, DbType.String);
            
            var result = _repository.GetAll<List<psp_dsp_nri_client_list>>("[dbo].[psp_dsp_nri_client_list]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }
        public object psp_dsp_nri_category(psp_dsp_nri_category pdnc)
        {
            var dp_params = new DynamicParameters();
            dp_params.Add("Client", pdnc.main_client_id, DbType.Int32);

            var result = _repository.GetAll<List<psp_dsp_nri_category>>("[dbo].[psp_dsp_nri_category]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }
        public object psp_dsp_calendar_year(psp_dsp_calendar_year serializeProfile)
        {
            var dp_params = new DynamicParameters();

            var result = _repository.GetAll<List<psp_dsp_calendar_year>>("[dbo].[psp_dsp_calendar_year]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }
        public object psp_dsp_nri_bank_details(psp_dsp_nri_bank_details serializeProfile)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("main_client_id", serializeProfile.main_client_id, DbType.String);
            dp_params.Add("bank_account", serializeProfile.bank_account_no, DbType.String);
            dp_params.Add("data_level", serializeProfile.data_level, DbType.Int32);

            var result = _repository.GetAll<List<psp_dsp_nri_bank_details>>("[dbo].[psp_dsp_nri_bank_details]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }
        public object psp_amd_nri_client_data_entry(psp_amd_nri_client_data_entry serializeProfile)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("loginid", serializeProfile.loginid, DbType.Int32);
            dp_params.Add("rec_id", serializeProfile.rec_id, DbType.Int32);
            dp_params.Add("main_client_id", serializeProfile.main_client_id, DbType.Int32);
            dp_params.Add("bank_account", serializeProfile.bank_account, DbType.String);
            dp_params.Add("client_cat", serializeProfile.client_cat, DbType.Int32);
            dp_params.Add("trans_date", serializeProfile.trans_date, DbType.Date);
            dp_params.Add("segment", serializeProfile.segment, DbType.Int32);
            dp_params.Add("trans_type", serializeProfile.trans_type, DbType.Int32);
            dp_params.Add("narration", serializeProfile.narration.HtmlEncode(), DbType.String);
            dp_params.Add("amount", serializeProfile.amount, DbType.Decimal);
            dp_params.Add("flag", serializeProfile.flag, DbType.Int32);

            var result = _repository.Get<psp_amd_nri_client_data_entry>("[dbo].[psp_amd_nri_client_data_entry]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }
        public object psp_dsp_nri_bankmaster_details(psp_dsp_nri_bankmaster_details serializeProfile)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("main_client_id", serializeProfile.main_client_id, DbType.Int32);

            var result = _repository.GetAll<List<psp_dsp_nri_bankmaster_details>>("[dbo].[psp_dsp_nri_bankmaster_details]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }
        public object psp_dsp_nri_bank_account_type_list(psp_dsp_nri_bank_account_type_list serializeProfile)
        {
            var dp_params = new DynamicParameters();

            var result = _repository.GetAll<List<psp_dsp_nri_bank_account_type_list>>("[dbo].[psp_dsp_nri_bank_account_type_list]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }
        public object psp_dsp_nri_client_category_list(psp_dsp_nri_client_category_list serializeProfile)
        {
            var dp_params = new DynamicParameters();

            var result = _repository.GetAll<List<psp_dsp_nri_client_category_list>>("[dbo].[psp_dsp_nri_client_category_list]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }
        public object psp_dsp_get_kyc_details(psp_dsp_get_kyc_details serializeProfile)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("main_client_id", serializeProfile.main_client_id, DbType.Int32);

            var result = _repository.GetAll<List<psp_dsp_get_kyc_details>>("[dbo].[psp_dsp_get_kyc_details]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }
        public object psp_amd_equity_kyc_initiate_update_request(kyc_initiate_auth_resend_request serializeProfile)
        {
            var dp_params = new DynamicParameters();
            
            dp_params.Add("account_code", serializeProfile.account_code, DbType.String);
            dp_params.Add("login_id", serializeProfile.login_id, DbType.Int32);

            var result = _repository.Get<kyc_initiate_auth_resend_request>("[dbo].[psp_amd_equity_kyc_initiate_update_request]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }
        public object psp_amd_equity_kyc_authenticate_update_request(kyc_initiate_auth_resend_request serializeProfile)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("token_no", serializeProfile.token_no, DbType.String);
            dp_params.Add("otp", serializeProfile.otp, DbType.String);
            dp_params.Add("request_stage", serializeProfile.request_stage, DbType.Int32);
            

            var result = _repository.Get<kyc_initiate_auth_resend_request>("[dbo].[psp_amd_equity_kyc_authenticate_update_request]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }
        public object psp_amd_equity_kyc_resend_request(kyc_initiate_auth_resend_request serializeProfile)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("token_no", serializeProfile.token_no, DbType.String);
            dp_params.Add("stage", serializeProfile.stage, DbType.String);

            var result = _repository.Get<kyc_initiate_auth_resend_request>("[dbo].[psp_amd_equity_kyc_resend_request]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }
        public object psp_amd_equity_kyc_capture_update_request(psp_amd_equity_kyc_capture_update_request serializeProfile)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("OTP1_Token", serializeProfile.OTP1_Token, DbType.String);
            dp_params.Add("email_update", serializeProfile.email_update, DbType.String);
            dp_params.Add("email_address", serializeProfile.email_address, DbType.String);
            dp_params.Add("email_relation", serializeProfile.email_relation, DbType.String);
            dp_params.Add("mobile_update", serializeProfile.mobile_update, DbType.String);
            dp_params.Add("mobile_number", serializeProfile.mobile_number, DbType.String);
            dp_params.Add("mobile_relation", serializeProfile.mobile_relation, DbType.String);
            dp_params.Add("income_update", serializeProfile.income_update, DbType.String);
            dp_params.Add("income_range", serializeProfile.income_range, DbType.String);
            dp_params.Add("networth", serializeProfile.networth, DbType.String);
            dp_params.Add("networth_date", serializeProfile.networth_date, DbType.Date);

            var result = _repository.Get<psp_amd_equity_kyc_capture_update_request>("[dbo].[psp_amd_equity_kyc_capture_update_request]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }
        public object psp_dsp_kyc_backoffice_requests(psp_dsp_kyc_backoffice_requests serializeProfile)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("login_id", serializeProfile.login_id, DbType.Int32);

            var result = _repository.GetAll<List<psp_dsp_kyc_backoffice_requests>>("[dbo].[psp_dsp_kyc_backoffice_requests]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }
        public object psp_amd_kyc_backoffice_request_action(psp_amd_kyc_backoffice_request_action serializeProfile)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("request_id", serializeProfile.request_id, DbType.Int32);
            dp_params.Add("account_code", serializeProfile.account_code, DbType.String);
            dp_params.Add("status", serializeProfile.status, DbType.String);
            dp_params.Add("remarks", serializeProfile.remarks, DbType.String);

            var result = _repository.Get<psp_amd_kyc_backoffice_request_action>("[dbo].[psp_amd_kyc_backoffice_request_action]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }

        public object psp_dsp_client_list(psp_dsp_client_list serializeProfile)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("family_id", serializeProfile.family_id, DbType.String);

            var result = _repository.GetAll<List<psp_dsp_client_list>>("[dbo].[psp_dsp_client_list]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }
        public object psp_dsp_date_values(psp_dsp_date_values serializeProfile)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("date", serializeProfile.date, DbType.Date);

            var result = _repository.Get<psp_dsp_date_values>("[dbo].[psp_dsp_date_values]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }
        public object psp_dsp_global_report_date_pills(psp_dsp_global_report_date_pills serializeProfile)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("date", serializeProfile.date, DbType.Date);

            var result = _repository.Get<psp_dsp_global_report_date_pills>("[dbo].[psp_dsp_global_report_date_pills]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }
        public object psp_dsp_get_kyc_update_history(psp_dsp_get_kyc_update_history serializeProfile)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("main_client_id", serializeProfile.main_client_id, DbType.String);

            var result = _repository.GetAll<List<psp_dsp_get_kyc_update_history>>("[dbo].[psp_dsp_get_kyc_update_history]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }
        public object psp_dsp_single_family_details(psp_dsp_single_family_details serializeProfile)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("user_id", "0", DbType.Int32);
            dp_params.Add("LoginId", serializeProfile.LoginId, DbType.Int32);
            dp_params.Add("family_id", serializeProfile.family_id, DbType.Int32);

            var result = _repository.Get<psp_dsp_single_family_details>("[dbo].[psp_dsp_single_family_details]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }
        public object psp_amd_family_details(psp_amd_family_details serializeProfile)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("Login_Id", serializeProfile.Login_Id, DbType.Int32);
            dp_params.Add("family_name", serializeProfile.family_name, DbType.String);
            dp_params.Add("family_id", serializeProfile.family_id, DbType.Int32);
            dp_params.Add("contact_No", serializeProfile.contact_No, DbType.String);
            dp_params.Add("email_Id", serializeProfile.email_Id, DbType.String);
            dp_params.Add("passFlag", serializeProfile.passFlag, DbType.Int32);
            dp_params.Add("send_mail", serializeProfile.send_mail, DbType.Int32);
            dp_params.Add("newPassword", serializeProfile.newPassword, DbType.String);
            dp_params.Add("plainPassword", serializeProfile.plainPassword, DbType.String);
            dp_params.Add("passtype", serializeProfile.passtype, DbType.String);


            var result = _repository.Get<psp_amd_family_details>("[dbo].[psp_amd_family_details]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }

        public object psp_amd_profile_details(psp_amd_profile_details serializeProfile)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("Login_Id", serializeProfile.Login_Id, DbType.Int32);
            dp_params.Add("family_name", serializeProfile.family_name.HtmlEncode(), DbType.String);
            dp_params.Add("nickname", serializeProfile.nickname.HtmlEncode(), DbType.String);
            dp_params.Add("contact_No", serializeProfile.contact_No, DbType.String);
            dp_params.Add("email_Id", serializeProfile.email_Id, DbType.String);
            dp_params.Add("passFlag", serializeProfile.passFlag, DbType.Int32);
            dp_params.Add("newPassword", serializeProfile.newPassword, DbType.String);
            

            var result = _repository.Get<psp_amd_profile_details>("[dbo].[psp_amd_profile_details]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }

        public object psp_dsp_AIF_scheme(psp_dsp_AIF_scheme pdas)
        {
            var dp_params = new DynamicParameters();

            var result = _repository.GetAll<List<psp_dsp_AIF_scheme>>("[dbo].[psp_dsp_AIF_scheme]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }
        public object psp_amd_AIF_data_entry(psp_amd_AIF_data_entry pdas)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("rec_id", pdas.rec_id, DbType.Int32);
            dp_params.Add("user", pdas.user, DbType.String);
            dp_params.Add("scheme_id", pdas.scheme_id, DbType.String);
            dp_params.Add("nav_date", pdas.nav_date, DbType.Date);
            dp_params.Add("nav", pdas.para_nav, DbType.String);
            dp_params.Add("action", pdas.action, DbType.String);

            var result = _repository.Get<psp_amd_AIF_data_entry>("[dbo].[psp_amd_AIF_data_entry]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }
        public object psp_dsp_AIF_nav_data(psp_dsp_AIF_nav_data pdas)
        {
            var dp_params = new DynamicParameters();

            var result = _repository.GetAll<List<psp_dsp_AIF_nav_data>>("[dbo].[psp_dsp_AIF_nav_data]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }
        public object psp_dsp_survey_list(psp_dsp_survey_list pdas)
        {
            var dp_params = new DynamicParameters();

            var result = _repository.GetAll<List<psp_dsp_survey_list>>("[dbo].[psp_dsp_survey_list]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }
        public object psp_dsp_survey(psp_dsp_survey pdas)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("survey_id", pdas.survey_id, DbType.Int32);

            var result = _repository.GetAll<List<psp_dsp_survey>>("[dbo].[psp_dsp_survey]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }
        public object psp_amd_survey_response(psp_amd_survey_response pdas)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("login_id", pdas.login_id, DbType.Int32);
            dp_params.Add("family_id", pdas.family_id, DbType.String);
            dp_params.Add("survey_id", pdas.survey_id, DbType.Int32);
            dp_params.Add("response", pdas.response, DbType.String);

            var result = _repository.Get<psp_amd_survey_response>("[dbo].[psp_amd_survey_response]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }
        public object psp_dsp_survey_status(psp_dsp_survey_status pdas)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("family_id", pdas.family_id, DbType.String);
            dp_params.Add("survey_id", pdas.survey_id, DbType.Int32);            

            var result = _repository.Get<psp_dsp_survey_status>("[dbo].[psp_dsp_survey_status]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }

        public object psp_dsp_survey_family_list(psp_dsp_survey_family_list pdas)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("login_id", pdas.login_id, DbType.Int32);
            dp_params.Add("survey_id", pdas.survey_id, DbType.Int32);

            var result = _repository.GetAll<List<psp_dsp_survey_family_list>>("[dbo].[psp_dsp_survey_family_list]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }
        public object psp_dsp_captured_survey_responses(psp_dsp_captured_survey_responses pdas)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("family_id", pdas.family_id, DbType.Int32);
            dp_params.Add("survey_id", pdas.survey_id, DbType.Int32);

            var result = _repository.GetAll<List<psp_dsp_captured_survey_responses>>("[dbo].[psp_dsp_captured_survey_responses]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }
        public object psp_dsp_model_portfolio(psp_dsp_model_portfolio serializeProfile)
        {
            var dp_params = new DynamicParameters();

            var result = _repository.GetAll<List<psp_dsp_model_portfolio>>("[dbo].[psp_dsp_model_portfolio]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }
        public object psp_amd_model_portfolio_cp(psp_amd_model_portfolio_cp pdas)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("action", pdas.action, DbType.String);
            dp_params.Add("rec_id", pdas.rec_id, DbType.Int32);
            dp_params.Add("reco_date", pdas.reco_date, DbType.Date);
            dp_params.Add("isin", pdas.isin, DbType.String);
            dp_params.Add("52_wk_low", pdas.wk_low, DbType.String);
            dp_params.Add("52_wk_high", pdas.wk_high, DbType.String);
            dp_params.Add("eps1", pdas.eps1, DbType.String);
            dp_params.Add("eps2", pdas.eps2, DbType.String);
            dp_params.Add("price_target", pdas.price_target, DbType.String);
            dp_params.Add("risk_profile", pdas.risk_profile, DbType.String);
            dp_params.Add("portfolio_type", pdas.portfolio_type, DbType.String);
            dp_params.Add("inv_rationale", pdas.inv_rationale, DbType.String);
            dp_params.Add("active", pdas.active, DbType.String);
            dp_params.Add("actionable", pdas.actionable, DbType.String);
            dp_params.Add("amd_user", pdas.user, DbType.String);


            var result = _repository.Get<psp_amd_model_portfolio_cp>("[dbo].[psp_amd_model_portfolio_cp]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }
        public object psp_dsp_script_isin_cp(psp_dsp_script_isin_cp pdsi)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("isin", pdsi.Isin_Code, DbType.String);

            var result = _repository.Get<psp_dsp_script_isin_cp>("[dbo].[psp_dsp_script_isin_cp]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }

        public object psp_dsp_survey_respnse_dump(psp_dsp_survey_respnse_dump serializeProfile)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("survey_id", serializeProfile.survey_id, DbType.Int32);
            dp_params.Add("login_id", serializeProfile.login_id, DbType.Int32);
            dp_params.Add("flag", serializeProfile.flag, DbType.Int16);

            var result = _repository.GetAll<List<psp_dsp_survey_respnse_dump>>("[dbo].[psp_dsp_survey_respnse_dump]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }
        public object psp_dsp_profile_details(psp_dsp_profile_details pdsi)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("LoginId", pdsi.LoginId, DbType.Int32);

            var result = _repository.Get<psp_dsp_profile_details>("[dbo].[psp_dsp_profile_details]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }
        public object psp_dsp_process_log(psp_dsp_process_log pdsi)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("log", pdsi.log, DbType.String);

            var result = _repository.GetAll<psp_dsp_process_log>("[dbo].[psp_dsp_process_log]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }

        public object psp_dsp_periodic_kyc_log(psp_dsp_periodic_kyc_log pdpkl)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("start_date", pdpkl.start_date, DbType.Date);
            dp_params.Add("end_date", pdpkl.end_date, DbType.Date);

            var result = _repository.GetAll<psp_dsp_periodic_kyc_log>("[dbo].[psp_dsp_periodic_kyc_log]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }
        public object psp_dsp_user_access_token(psp_dsp_user_access_token pdsi)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("login_id", pdsi.login_id, DbType.Int32);
            dp_params.Add("web_session", pdsi.web_session_id, DbType.String);

            var result = _repository.Get<psp_dsp_user_access_token>("[dbo].[psp_dsp_user_access_token]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }
        public object psp_dsp_common_report_module_list(psp_dsp_common_report_module_list pdpkl)
        {
            var dp_params = new DynamicParameters();

            var result = _repository.GetAll<psp_dsp_common_report_module_list>("[dbo].[psp_dsp_common_report_module_list]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }
        public object psp_dsp_equity_research_reports(psp_dsp_equity_research_reports pdpkl)
        {
            var dp_params = new DynamicParameters();

            var result = _repository.GetAll<psp_dsp_equity_research_reports>("[dbo].[psp_dsp_equity_research_reports]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }
        public object psp_amd_equity_research_reco(psp_amd_equity_research_reco pdsi)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("Login_Id", pdsi.Login_Id, DbType.Int32);
            dp_params.Add("rec_id", pdsi.rec_id, DbType.Int32);
            dp_params.Add("repo_title", pdsi.repo_title, DbType.String);
            dp_params.Add("category", pdsi.category, DbType.String);
            dp_params.Add("scrip_code", pdsi.scrip_code ?? "0", DbType.Int32);
            dp_params.Add("recommendation", pdsi.recommendation ?? "-", DbType.String);
            dp_params.Add("target_price", pdsi.target_price ?? 0, DbType.Decimal);
            dp_params.Add("url", pdsi.url, DbType.String);
            dp_params.Add("flag", pdsi.flag, DbType.String);
            dp_params.Add("dsp_web", pdsi.dsp_web, DbType.String);

            var result = _repository.Get<psp_amd_equity_research_reco>("[dbo].[psp_amd_equity_research_reco]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }

        public object psp_dsp_search_client_cp(psp_dsp_search_client_cp serializeProfile)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("login_id", serializeProfile.login_id, DbType.Int32);
            dp_params.Add("search_str", serializeProfile.search_str , DbType.String);

            var result = _repository.GetAll<psp_dsp_search_client_cp>("[dbo].[psp_dsp_search_client_cp]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }

		public object psp_dsp_mint_mf_download_ReportCriteria(psp_dsp_mint_mf_download_ReportCriteria serializeProfile)
		{
			var dp_params = new DynamicParameters();

			dp_params.Add("report_id", serializeProfile.report_id, DbType.Int32);

			var result = _repository.GetAll<psp_dsp_mint_mf_download_ReportCriteria>("[dbo].[psp_dsp_mint_mf_download_ReportCriteria]", dp_params, commandType: CommandType.StoredProcedure);

			return result;

		}
		public object psp_dsp_mint_mf_report_download_list(psp_dsp_mint_mf_report_download_list serializeProfile)
		{
			var dp_params = new DynamicParameters();

			var result = _repository.GetAll<psp_dsp_mint_mf_report_download_list>("[dbo].[psp_dsp_mint_mf_report_download_list]", dp_params, commandType: CommandType.StoredProcedure);

			return result;

		}

		public object psp_amd_mint_global_report_data(psp_amd_mint_global_report_data serializeProfile)
		{
			var dp_params = new DynamicParameters();

			dp_params.Add("token_no", serializeProfile.token_no, DbType.String);
			dp_params.Add("GainLossjsonstring", serializeProfile.GainLossjsonstring, DbType.String);
			dp_params.Add("ReturnSchemejsonstring", serializeProfile.ReturnSchemejsonstring, DbType.String);
			dp_params.Add("Tradesjsonstring", serializeProfile.Tradesjsonstring, DbType.String);

			var result = _repository.GetAll<psp_amd_mint_global_report_data>("[dbo].[psp_amd_mint_global_report_data]", dp_params, commandType: CommandType.StoredProcedure);

			return result;

		}
        public object psp_dsp_mint_mf_clients(psp_dsp_mint_mf_clients serializeProfile)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("family_id", serializeProfile.family_id, DbType.String);

            var result = _repository.GetAll<psp_dsp_mint_mf_clients>("[dbo].[psp_dsp_mint_mf_clients]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }
        public object psp_dsp_family_mapping_fam_details(psp_dsp_family_mapping_fam_details serializeProfile)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("pan", serializeProfile.pan, DbType.String);

            var result = _repository.Get<psp_dsp_family_mapping_fam_details>("[dbo].[psp_dsp_family_mapping_fam_details]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }
        public object psp_amd_family_mapping_change_family(psp_amd_family_mapping_change_family serializeProfile)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("login_id", serializeProfile.login_id, DbType.Int32);
            dp_params.Add("pan", serializeProfile.pan, DbType.String);
            dp_params.Add("new_fam_guid", serializeProfile.new_fam_guid, DbType.String);
            dp_params.Add("old_fam_id", serializeProfile.old_fam_id, DbType.Int32);

            var result = _repository.Get<psp_amd_family_mapping_change_family>("[dbo].[psp_amd_family_mapping_change_family]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }
        public object psp_amd_family_mapping_create_fam(psp_amd_family_mapping_create_fam serializeProfile)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("login_id", serializeProfile.login_id, DbType.Int32);
            dp_params.Add("pan", serializeProfile.pan, DbType.String);
            dp_params.Add("pass", serializeProfile.pass, DbType.String);
            dp_params.Add("pass_en", serializeProfile.pass_en, DbType.String);
            dp_params.Add("family_name", serializeProfile.family_name.HtmlEncode(), DbType.String);
            dp_params.Add("mobile", serializeProfile.mobile.HtmlEncode(), DbType.String);
            dp_params.Add("email_id", serializeProfile.email_id.HtmlEncode(), DbType.String);
            dp_params.Add("branch", serializeProfile.branch, DbType.Int32);
            dp_params.Add("rm_list", serializeProfile.rm_list, DbType.String);

            var result = _repository.Get<psp_amd_family_mapping_create_fam>("[dbo].[psp_amd_family_mapping_create_fam]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }
        public object psp_dsp_family_mapping_branch_details(psp_dsp_family_mapping_branch_details serializeProfile)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("family_token", serializeProfile.family_token, DbType.String);

            var result = _repository.Get<psp_dsp_family_mapping_branch_details>("[dbo].[psp_dsp_family_mapping_branch_details]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }
        public object psp_dsp_family_mapping_rm_details(psp_dsp_family_mapping_rm_details serializeProfile)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("family_token", serializeProfile.family_token, DbType.String);

            var result = _repository.GetAll<psp_dsp_family_mapping_rm_details>("[dbo].[psp_dsp_family_mapping_rm_details]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }
        public object psp_amd_family_mapping_branch_update(psp_amd_family_mapping_branch_update serializeProfile)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("login_id", serializeProfile.login_id, DbType.String);
            dp_params.Add("family_token", serializeProfile.family_token, DbType.String);
            dp_params.Add("new_branch_id", serializeProfile.new_branch_id, DbType.String);

            var result = _repository.Get<psp_amd_family_mapping_branch_update>("[dbo].[psp_amd_family_mapping_branch_update]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }
        public object psp_amd_family_mapping_rm_update(PspAmdFamilyMappingRmUpdate serializeProfile)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("login_id", serializeProfile.login_id, DbType.String);
            dp_params.Add("family_token", serializeProfile.family_token, DbType.String);
            dp_params.Add("rm_id", serializeProfile.rm_id, DbType.String);
            dp_params.Add("amd_flag", serializeProfile.amd_flag, DbType.String);

            var result = _repository.Get<PspAmdFamilyMappingRmUpdate>("[dbo].[psp_amd_family_mapping_rm_update]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }
        public object psp_dsp_client_coms_dormant_account(psp_dsp_client_coms_dormant_account serializeProfile)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("loginid", serializeProfile.loginid, DbType.Int32);
            //dp_params.Add("months", serializeProfile.months, DbType.Int32);

            var result = _repository.GetAll<psp_dsp_client_coms_dormant_account>("[dbo].[psp_dsp_client_coms_dormant_account]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }

        public object psp_dsp_client_coms_dormant_mail_content(psp_dsp_client_coms_dormant_mail_content serializeProfile)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("login_id", serializeProfile.login_id, DbType.Int32);
            dp_params.Add("content_id", serializeProfile.content_id, DbType.Int32);
            dp_params.Add("code", serializeProfile.code, DbType.String);
            dp_params.Add("client_category", serializeProfile.client_category, DbType.String);

            var result = _repository.Get<psp_dsp_client_coms_dormant_mail_content>("[dbo].[psp_dsp_client_coms_dormant_mail_content]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }

        public object psp_dsp_client_coms_history(psp_dsp_client_coms_history serializeProfile)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("client_code", serializeProfile.client_code, DbType.String);
            dp_params.Add("content_id", serializeProfile.content_id, DbType.Int32);

            var result = _repository.GetAll<psp_dsp_client_coms_history>("[dbo].[psp_dsp_client_coms_history]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }

        public object DormantAccountSMSUpdate(SmtpSendEmail serializeProfile)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("smsREQID", serializeProfile.smsREQID, DbType.String);
            dp_params.Add("smsTID", serializeProfile.smsTID, DbType.String);
            dp_params.Add("emailGuid", serializeProfile.emailGuid, DbType.String);

            var result = _repository.Get<SmtpSendEmail>("[dbo].[psp_amd_cleint_comms_sms_details]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }
        public object psp_dsp_client_coms_exceptions(psp_dsp_client_coms_exceptions serializeProfile)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("loginid", serializeProfile.loginid, DbType.Int32);
            dp_params.Add("excep_reason", "All", DbType.String);

            var result = _repository.GetAll<psp_dsp_client_coms_exceptions>("[dbo].[psp_dsp_client_coms_exceptions]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }
        public object psp_dsp_client_coms_exception_list(psp_dsp_client_coms_exception_list serializeProfile)
        {
            var dp_params = new DynamicParameters();

            var result = _repository.GetAll<psp_dsp_client_coms_exception_list>("[dbo].[psp_dsp_client_coms_exception_list]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }
        public object psp_dsp_client_coms_outstanding_debit_send_mail(psp_dsp_client_coms_outstanding_debit_send_mail serializeProfile)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("login_id", serializeProfile.login_id, DbType.Int32);
            dp_params.Add("trdate", serializeProfile.trdate, DbType.Date);
            //dp_params.Add("trdate", "2025-02-02", DbType.Date);

            var result = _repository.GetAll<psp_dsp_client_coms_outstanding_debit_send_mail>("[dbo].[psp_dsp_client_coms_outstanding_debit_send_mail]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }
        public object psp_amd_client_coms_outstanding_debit_send_mail(psp_dsp_client_coms_outstanding_debit_send_mail serializeProfile)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("login_id", serializeProfile.login_id, DbType.Int32);
            dp_params.Add("str_acc_code", serializeProfile.str_acc_code, DbType.String);
            dp_params.Add("str_amt", serializeProfile.str_amt, DbType.String);
            dp_params.Add("as_on_date", serializeProfile.as_on_date, DbType.Date);

            var result = _repository.Get<psp_dsp_client_coms_outstanding_debit_send_mail>("[dbo].[psp_amd_client_coms_outstanding_debit_send_mail]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }
        public object psp_dsp_client_communication_log(psp_dsp_client_communication_log serializeProfile)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("login_id", serializeProfile.login_id, DbType.Int32);
            dp_params.Add("from_date", serializeProfile.from_date, DbType.Date);
            dp_params.Add("to_date", serializeProfile.to_date, DbType.Date);

            var result = _repository.GetAll<psp_dsp_client_communication_log>("[dbo].[psp_dsp_client_communication_log]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }

        public object psp_amd_equity_settlement_upload(psp_amd_equity_settlement_upload serializeProfile)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("login_id", serializeProfile.login_id, DbType.Int32);
            dp_params.Add("jsonString", serializeProfile.jsonString, DbType.String);
            dp_params.Add("file_name", serializeProfile.file_name, DbType.String);
            dp_params.Add("file_ogName", serializeProfile.file_ogName, DbType.String);
            dp_params.Add("file_date", serializeProfile.file_date, DbType.Date);
            dp_params.Add("file_guid", serializeProfile.file_guid, DbType.String);

            var result = _repository.GetAll<psp_amd_equity_settlement_upload>("[dbo].[psp_amd_equity_settlement_upload]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }

        public object psp_amd_equity_research_push_mobile_notification(psp_amd_equity_research_push_mobile_notification serializeProfile)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("Login_Id", serializeProfile.Login_Id, DbType.Int32);
            dp_params.Add("rec_id", serializeProfile.rec_id, DbType.Int32);

            var result = _repository.Get<psp_amd_equity_research_push_mobile_notification>("[dbo].[psp_amd_equity_research_push_mobile_notification]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }

        public object One_Pager(One_Pager op)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("login_id", op.Login_id, DbType.Int32);
            dp_params.Add("report_name", op.Repo_name, DbType.String);
            dp_params.Add("title", op.Repo_title, DbType.String);
            dp_params.Add("subtitle", op.Repo_subtitle, DbType.String);
            dp_params.Add("reco_price", op.Reco_price, DbType.Int32);
            dp_params.Add("Target_price", op.target_price, DbType.Int32);
            dp_params.Add("analyst", op.Analyst, DbType.String);
            dp_params.Add("jsonObjSec", op.jsonObjSec, DbType.String);
            dp_params.Add("jsonObjSubSec", op.jsonObjSubSec, DbType.String);


            var result = _repository.Get<One_Pager>("[dbo].[psp_amd_one_pager]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }
    }
}
