using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModel.Login;
using ViewModel.Reports;
using ViewModel.Shared;

namespace PieAPI.Repository.Interface
{
  public  interface IDashboard
    {
        object DeshboardFinYears(DashboardFinYears endlogin);

        object FamilyDetails(FamilyList endlogin);

        object Familymenu(menu endlogin);

        object FetchFevDetails(FetchFevourite fv);

        object UpdateFevouriteDetails(UpdateFevourite uv);

        object AssetList(psp_dsp_client_portal_assets pdcpa);

        object BranchList(psp_dsp_client_portal_branch pdcpb);

        object RMList(psp_rpt_ssrs_template_RM prstRM);

        object psp_rpt_scripwiseholding_scrip(psp_rpt_scripwiseholding_scrip pmsp);
        object psp_dsp_kyc_income_range(psp_dsp_kyc_income_range serializeProfile);
        object psp_dsp_kyc_relations(psp_dsp_kyc_relations serializeProfile);
        object psp_dsp_branch_list(psp_dsp_branch_list serializeProfile);
        object psp_amd_survey_interaction(psp_amd_survey_interaction serializeProfile);
        object psp_dsp_client_portal_dashboard_RM_details(psp_dsp_client_portal_dashboard_RM_details serializeProfile);
        object psp_dsp_client_portal_dashboard_client_list(psp_dsp_client_portal_dashboard_client_list serializeProfile);
        object psp_dsp_client_portal_dashboard_asset_allocation(psp_dsp_client_portal_dashboard_asset_allocation serializeProfile);
        object psp_dsp_client_portal_dashboard_SIP_details(psp_dsp_client_portal_dashboard_SIP_details serializeProfile);
        object psp_dsp_client_portal_dashboard_xirr_details(psp_dsp_client_portal_dashboard_xirr_details serializeProfile);
        object psp_dsp_client_portal_dashboard_notification(psp_dsp_client_portal_dashboard_notification serializeProfile);
        object psp_dsp_current_holding_drill_down(psp_dsp_current_holding_drill_down serializeProfile);
        object psp_dsp_searchable_family_list_client_portal(psp_dsp_searchable_family_list_client_portal serializeProfile);
        object psp_dsp_research_category(psp_dsp_research_category serializeProfile);
        object psp_dsp_research_recommendation(psp_dsp_research_recommendation serializeProfile);
        object psp_dsp_all_rm_list(psp_dsp_all_rm_list serializeProfile);
    }
}
