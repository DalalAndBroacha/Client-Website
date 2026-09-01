using System;
using System.Collections.Generic;
using System.Linq;
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

namespace PieAPI.Repository.Interface
{
   public interface IServices
    {
        object AccountLogin(UserLogin login);

        object ChangePassword(dtoInputChangePassword dto);

        object LogOutDetails(LogOut logout);

        object ForgotPassword(ForgotPassword logout);

        object OtpDetails(otp ot);

        object ForgotuserDetails(InputForgotUser ifu);

        object NewuserDetails(ChangeUsername changeuser);
        object GetMobileDetails(psp_dsp_mobile_pan serializeProfile);

        object ResendMobileOTP(psp_dsp_mobile_pan serializeProfile);
        object psp_dsp_client_accounts(psp_dsp_client_accounts serializeProfile);
        object psp_dsp_nri_client_list(psp_dsp_nri_client_list serializeProfile);
        object psp_dsp_calendar_year(psp_dsp_calendar_year serializeProfile);
        object psp_dsp_nri_bank_details(psp_dsp_nri_bank_details serializeProfile);
        object psp_amd_nri_client_data_entry(psp_amd_nri_client_data_entry serializeProfile);
        object psp_dsp_nri_bankmaster_details(psp_dsp_nri_bankmaster_details serializeProfile);
        object psp_dsp_nri_bank_account_type_list(psp_dsp_nri_bank_account_type_list serializeProfile);
        object psp_dsp_nri_client_category_list(psp_dsp_nri_client_category_list serializeProfile);
        object psp_dsp_get_kyc_details(psp_dsp_get_kyc_details serializeProfile);
        object psp_amd_equity_kyc_initiate_update_request(kyc_initiate_auth_resend_request serializeProfile);
        object psp_amd_equity_kyc_authenticate_update_request(kyc_initiate_auth_resend_request serializeProfile);
        object psp_amd_equity_kyc_resend_request(kyc_initiate_auth_resend_request serializeProfile);
        object psp_amd_equity_kyc_capture_update_request(psp_amd_equity_kyc_capture_update_request serializeProfile);
        object psp_dsp_kyc_backoffice_requests(psp_dsp_kyc_backoffice_requests serializeProfile);
        object psp_amd_kyc_backoffice_request_action(psp_amd_kyc_backoffice_request_action serializeProfile);
        object psp_dsp_client_list(psp_dsp_client_list serializeProfile);
        object psp_dsp_date_values(psp_dsp_date_values serializeProfile);
        object psp_dsp_global_report_date_pills(psp_dsp_global_report_date_pills serializeProfile);
        object psp_dsp_get_kyc_update_history(psp_dsp_get_kyc_update_history serializeProfile);
        object psp_dsp_single_family_details(psp_dsp_single_family_details serializeProfile);

        public object psp_amd_family_details(psp_amd_family_details dataObj);

        object psp_dsp_AIF_scheme(psp_dsp_AIF_scheme serializeProfile);
        object psp_amd_AIF_data_entry(psp_amd_AIF_data_entry serializeProfile);
        object psp_dsp_AIF_nav_data(psp_dsp_AIF_nav_data serializeProfile);
        object psp_dsp_survey_list(psp_dsp_survey_list serializeProfile);
        object psp_dsp_survey(psp_dsp_survey serializeProfile);
        object psp_amd_survey_response(psp_amd_survey_response serializeProfile);
        object psp_dsp_survey_status(psp_dsp_survey_status serializeProfile);
        object psp_dsp_survey_family_list(psp_dsp_survey_family_list serializeProfile);
        object psp_dsp_captured_survey_responses(psp_dsp_captured_survey_responses serializeProfile);
        object psp_dsp_model_portfolio(psp_dsp_model_portfolio serializeProfile);
        object psp_amd_model_portfolio_cp(psp_amd_model_portfolio_cp serializeProfile);
        object psp_dsp_script_isin_cp(psp_dsp_script_isin_cp serializeProfile);
        object psp_dsp_survey_respnse_dump(psp_dsp_survey_respnse_dump serializeProfile);
        object psp_dsp_profile_details(psp_dsp_profile_details serializeProfile);
        object psp_amd_profile_details(psp_amd_profile_details serializeProfile);
        object psp_dsp_process_log(psp_dsp_process_log serializeProfile);
        object psp_dsp_periodic_kyc_log(psp_dsp_periodic_kyc_log serializeProfile);
        object psp_dsp_user_access_token(psp_dsp_user_access_token serializeProfile);
        object psp_dsp_nri_category(psp_dsp_nri_category serializeProfile);
        object psp_dsp_common_report_module_list(psp_dsp_common_report_module_list serializeProfile);
        object psp_dsp_equity_research_reports(psp_dsp_equity_research_reports serializeProfile);
        object psp_amd_equity_research_reco(psp_amd_equity_research_reco serializeProfile);
        object psp_dsp_search_client_cp(psp_dsp_search_client_cp serializeProfile);
		object psp_dsp_mint_mf_download_ReportCriteria(psp_dsp_mint_mf_download_ReportCriteria serializeProfile);
		object psp_dsp_mint_mf_report_download_list(psp_dsp_mint_mf_report_download_list serializeProfile);
		object psp_amd_mint_global_report_data(psp_amd_mint_global_report_data serializeProfile);
        object psp_dsp_mint_mf_clients(psp_dsp_mint_mf_clients serializeProfile);
        object psp_dsp_family_mapping_fam_details(psp_dsp_family_mapping_fam_details serializeProfile);
        object psp_amd_family_mapping_change_family(psp_amd_family_mapping_change_family serializeProfile);
        object psp_amd_family_mapping_create_fam(psp_amd_family_mapping_create_fam serializeProfile);
        object psp_dsp_family_mapping_branch_details(psp_dsp_family_mapping_branch_details serializeProfile);
        object psp_dsp_family_mapping_rm_details(psp_dsp_family_mapping_rm_details serializeProfile);
        object psp_amd_family_mapping_branch_update(psp_amd_family_mapping_branch_update serializeProfile);
        object psp_amd_family_mapping_rm_update(PspAmdFamilyMappingRmUpdate serializeProfile);
        object psp_dsp_client_coms_dormant_account(psp_dsp_client_coms_dormant_account serializeProfile);
        object psp_dsp_client_coms_dormant_mail_content(psp_dsp_client_coms_dormant_mail_content serializeProfile);
        object psp_dsp_client_coms_history(psp_dsp_client_coms_history serializeProfile);
        object DormantAccountSMSUpdate(SmtpSendEmail serializeProfile);
        object psp_dsp_client_coms_exceptions(psp_dsp_client_coms_exceptions serializeProfile);
        object psp_dsp_client_coms_exception_list(psp_dsp_client_coms_exception_list serializeProfile);
        object psp_dsp_client_coms_outstanding_debit_send_mail(psp_dsp_client_coms_outstanding_debit_send_mail serializeProfile);
        object psp_amd_client_coms_outstanding_debit_send_mail(psp_dsp_client_coms_outstanding_debit_send_mail serializeProfile);
        object psp_dsp_client_communication_log(psp_dsp_client_communication_log serializeProfile);
        object psp_amd_equity_settlement_upload(psp_amd_equity_settlement_upload serializeProfile);
        object psp_amd_equity_research_push_mobile_notification(psp_amd_equity_research_push_mobile_notification serializeProfile);
        object One_Pager(One_Pager serializeProfile);
    }
}
