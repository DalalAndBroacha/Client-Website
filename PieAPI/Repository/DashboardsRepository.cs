using Dapper;
using PieAPI.Encryption_Decryption;
using PieAPI.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ViewModel.Login;
using ViewModel.Reports;
using ViewModel.Shared;

namespace PieAPI.Repository
{
    public class DashboardsRepository :IDashboard
    {
        private readonly IDapperRepository _repository;
        public DashboardsRepository(IDapperRepository repository)
        {
            _repository = repository;
        }

        public object DeshboardFinYears(DashboardFinYears login)
        {
            var dp_params = new DynamicParameters();
            dp_params.Add("LoginId", login.Id, DbType.Int32);
            dp_params.Add("acces_token", login.access_token, DbType.String);
            
            var result = _repository.GetAll<List<DashboardFinYears>>("[dbo].[psp_dsp_mobile_get_finyear]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }
        public object FamilyDetails(FamilyList login)
        {
            var dp_params = new DynamicParameters();
            dp_params.Add("LoginId", login.Id, DbType.Int32);
            dp_params.Add("user_id", 0, DbType.Int32); //No use in current project but is Mandatory in Old PIE and SSRS

            var result = _repository.GetAll<List<FamilyList>>("[dbo].[psp_dsp_family_list]", dp_params, commandType: CommandType.StoredProcedure);
            //Old SP psp_dsp_mobile_get_familylist_new
            
            return result;

        }
        public object Familymenu(menu menus)
        {
            var dp_params = new DynamicParameters();
            //kinjaldoshi,Kinjal@1107,F7C43D9D1CFCA590BB26BE836FE275DD, "dokinm#p,oimp@["
            dp_params.Add("userid", menus.Id, DbType.Int32);

            var result = _repository.GetAll<List<menu>>("[dbo].[psp_dsp_get_menu_cp]", dp_params, commandType: CommandType.StoredProcedure);

            // session.SetString("LoginKey", JsonConvert.SerializeObject(result));
            return result;

        }

        public object FetchFevDetails(FetchFevourite fv)
        {
            var dp_params = new DynamicParameters();
            //kinjaldoshi,Kinjal@1107,F7C43D9D1CFCA590BB26BE836FE275DD, "dokinm#p,oimp@["  run
            dp_params.Add("userid", fv.Id, DbType.Int32);
            dp_params.Add("module_id", 0, DbType.Int32);
            dp_params.Add("flag", "D", DbType.String);
            var result = _repository.GetAll<FetchFevourite>("[dbo].[psp_amd_pie_favourite]", dp_params, commandType: CommandType.StoredProcedure);

            // session.SetString("LoginKey", JsonConvert.SerializeObject(result));
            return result;

        }

        public object UpdateFevouriteDetails(UpdateFevourite uv)
        {
            var dp_params = new DynamicParameters();
            //kinjaldoshi,Kinjal@1107,F7C43D9D1CFCA590BB26BE836FE275DD, "dokinm#p,oimp@["
            dp_params.Add("userid", uv.Id, DbType.String);
            dp_params.Add("module_id", uv.moduleid, DbType.Int32);
            dp_params.Add("flag", uv.flag, DbType.String);
            var result = _repository.Get<LogOut>("[dbo].[psp_amd_pie_favourite]", dp_params, commandType: CommandType.StoredProcedure);

            // session.SetString("LoginKey", JsonConvert.SerializeObject(result));
            return result;

        }

        public object AssetList(psp_dsp_client_portal_assets pdcpa)
        {
            var dp_params = new DynamicParameters();

            var result = _repository.GetAll<List<psp_dsp_client_portal_assets>>("[dbo].[psp_dsp_client_portal_assets]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }
        public object BranchList(psp_dsp_client_portal_branch pdcpb)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("login_id", pdcpb.Id, DbType.Int32);

            var result = _repository.GetAll<List<psp_dsp_client_portal_branch>>("[dbo].[psp_dsp_client_portal_branch]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }
        public object RMList(psp_rpt_ssrs_template_RM pdcpb)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("LoginId", pdcpb.Id, DbType.Int32);
            dp_params.Add("Branch", pdcpb.Branch, DbType.String);

            var result = _repository.GetAll<List<psp_rpt_ssrs_template_RM>>("[dbo].[psp_rpt_ssrs_template_RM]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }
        public object psp_rpt_scripwiseholding_scrip(psp_rpt_scripwiseholding_scrip pdcpb)
        {
            var dp_params = new DynamicParameters();

            
            dp_params.Add("rm", pdcpb.rm, DbType.String);
            dp_params.Add("Asset", pdcpb.Asset, DbType.String);

            var result = _repository.GetAll<List<psp_rpt_scripwiseholding_scrip>>("[dbo].[psp_rpt_scripwiseholding_scrip]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }
        public object psp_dsp_kyc_income_range(psp_dsp_kyc_income_range pdcpb)
        {
            var dp_params = new DynamicParameters();

            var result = _repository.GetAll<List<psp_dsp_kyc_income_range>>("[dbo].[psp_dsp_kyc_income_range]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }
        public object psp_dsp_kyc_relations(psp_dsp_kyc_relations pdcpb)
        {
            var dp_params = new DynamicParameters();

            var result = _repository.GetAll<List<psp_dsp_kyc_relations>>("[dbo].[psp_dsp_kyc_relations]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }
        public object psp_dsp_branch_list(psp_dsp_branch_list dataObj)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("LoginId", dataObj.LoginId, DbType.Int32);
            dp_params.Add("cat_type", dataObj.cat_type, DbType.Int32);

            var result = _repository.GetAll<List<psp_dsp_branch_list>>("[dbo].[psp_dsp_branch_list]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }

        public object psp_amd_survey_interaction(psp_amd_survey_interaction dataObj)
        {
            var dp_params = new DynamicParameters();
            
            dp_params.Add("login_id", dataObj.login_id, DbType.String);
            dp_params.Add("rec_id", dataObj.rec_id, DbType.String);
            dp_params.Add("survey_id", dataObj.survey_id, DbType.String);
            dp_params.Add("family_id", dataObj.family_id, DbType.String);
            dp_params.Add("survey_date", dataObj.survey_date, DbType.String);
            dp_params.Add("action", dataObj.action, DbType.String);
            dp_params.Add("status", dataObj.status, DbType.String);
            dp_params.Add("interact_date", dataObj.interact_date, DbType.String);
            dp_params.Add("remarks", dataObj.remarks, DbType.String);

            var result = _repository.Get<psp_amd_survey_interaction>("[dbo].[psp_amd_survey_interaction]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }
        public object psp_dsp_client_portal_dashboard_RM_details(psp_dsp_client_portal_dashboard_RM_details dataObj)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("family_token", dataObj.family_token, DbType.String);

            var result = _repository.GetAll<List<psp_dsp_client_portal_dashboard_RM_details>>("[dbo].[psp_dsp_client_portal_dashboard_RM_details]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }
        public object psp_dsp_client_portal_dashboard_client_list(psp_dsp_client_portal_dashboard_client_list dataObj)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("family_token", dataObj.family_token, DbType.String);

            var result = _repository.GetAll<List<psp_dsp_client_portal_dashboard_client_list>>("[dbo].[psp_dsp_client_portal_dashboard_client_list]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }
        public object psp_dsp_client_portal_dashboard_asset_allocation(psp_dsp_client_portal_dashboard_asset_allocation dataObj)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("family_token", dataObj.family_token, DbType.String);
            dp_params.Add("flag", dataObj.flag, DbType.Int32);

            var result = _repository.GetAll<List<psp_dsp_client_portal_dashboard_asset_allocation>>("[dbo].[psp_dsp_client_portal_dashboard_asset_allocation]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }
        public object psp_dsp_client_portal_dashboard_SIP_details(psp_dsp_client_portal_dashboard_SIP_details dataObj)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("family_token", dataObj.family_token, DbType.String);

            var result = _repository.GetAll<List<psp_dsp_client_portal_dashboard_SIP_details>>("[dbo].[psp_dsp_client_portal_dashboard_SIP_details]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }
        public object psp_dsp_client_portal_dashboard_xirr_details(psp_dsp_client_portal_dashboard_xirr_details dataObj)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("family_token", dataObj.family_token, DbType.String);

            var result = _repository.GetAll<List<psp_dsp_client_portal_dashboard_xirr_details>>("[dbo].[psp_dsp_client_portal_dashboard_xirr_details]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }
        public object psp_dsp_client_portal_dashboard_notification(psp_dsp_client_portal_dashboard_notification dataObj)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("family_token", dataObj.family_token, DbType.String);

            var result = _repository.GetAll<List<psp_dsp_client_portal_dashboard_notification>>("[dbo].[psp_dsp_client_portal_dashboard_notification]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }

        public object psp_dsp_current_holding_drill_down(psp_dsp_current_holding_drill_down dataObj)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("script_code", dataObj.script_code, DbType.Int32);
            dp_params.Add("client_Code", dataObj.client_Code, DbType.Int32);

            var result = _repository.GetAll<List<psp_dsp_current_holding_drill_down>>("[dbo].[psp_dsp_current_holding_drill_down]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }

        public object psp_dsp_searchable_family_list_client_portal(psp_dsp_searchable_family_list_client_portal dataObj)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("loginId", dataObj.loginId, DbType.Int32);
            dp_params.Add("search_term", dataObj.search_term, DbType.String);
            dp_params.Add("display_flag", dataObj.display_flag, DbType.Int32);

            var result = _repository.GetAll<List<psp_dsp_searchable_family_list_client_portal>>("[dbo].[psp_dsp_searchable_family_list_client_portal]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }

        public object psp_dsp_research_category(psp_dsp_research_category dataObj)
        {
            var dp_params = new DynamicParameters();

            var result = _repository.GetAll<List<psp_dsp_research_category>>("[dbo].[psp_dsp_research_category]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }

        public object psp_dsp_research_recommendation(psp_dsp_research_recommendation dataObj)
        {
            var dp_params = new DynamicParameters();

            var result = _repository.GetAll<List<psp_dsp_research_recommendation>>("[dbo].[psp_dsp_research_recommendation]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }
        public object psp_dsp_all_rm_list(psp_dsp_all_rm_list dataObj)
        {
            var dp_params = new DynamicParameters();
            dp_params.Add("loginId", dataObj.loginid, DbType.Int32);
            var result = _repository.GetAll<List<psp_dsp_all_rm_list>>("[dbo].[psp_dsp_all_rm_list]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }
        
    }
}
