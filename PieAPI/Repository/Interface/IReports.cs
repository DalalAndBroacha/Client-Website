using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModel.Reports;
using ViewModel.Reports.Global_Report;
using ViewModel.Reports.Holding;
using ViewModel.Reports.IncomeReports;
using ViewModel.Reports.Liquiloan;
using ViewModel.Shared;

namespace PieAPI.Repository.Interface
{
   public interface IReports
    {
        object CommonDocumentDetails(CommonDocumentReports cdr);
        //object SIPSummaryDetails(SIPSummary ss);
        object SIPDetails(SIPDetail sd);
        //object SIPTerminatingDetails(SIPTerminating st);
        object DividendDetails(Dividend dividend);
        object IncomeDetails(IncomeStatement inc);
        object RealisedDetails(RealisedGainLoss rgl);
        object EquityClientDetails(EquityClientSummary ecs);

        object EquityClientFlowDetails(EquityClientFlow ecf);

        object EquityClientFyFactorDetails(EquityClientFyFactor ecff);
        object EquityClientHoldingDetails(EquityClientHolding ech);

        object IncomePeriod(Period period);

        object pspdspequitydetails(PSPDSPClientFlow pdcf);

        object PspDspOutFlowDetails(PSPDSPOUTFLOW pdof);

        object PSPDSPEQUITYCLIENTDetails(PSPDSPEQUITYCLIENTS pdec);

        object PSPDSPEQUITYDEALERTRACKTDetails(Equitydealertracksheet edts);

        object psprptperformanceholdingreportDetails(psp_rpt_performance_holding_report prphr);

        object psprptperformanceholdingdirectequityreportDetails(psp_rpt_performance_holding_direct_equity_report prphder);

        object psprptperformanceholdingequitypmsreportnewformatDetails(psp_rpt_performance_holding_equity_pms_report_new_format prpheprn);

        object psprptperformanceholdingequitymfreportDetails(psp_rpt_performance_holding_equity_mf_report prphemr);

        object pspdspmfsipstpdetailDetails(psp_dsp_mf_sip_stp_detail pdms);

        object pspdspmfschemeallocationDetails(psp_dsp_mf_scheme_allocation pdmsa);
        object psp_dsp_equity_client_fy_factors(psp_dsp_equity_client_fy_factors serializeProfile);
        object pspdspmfcategoryallocationDetails(psp_dsp_mf_category_allocation pdmca);

        object pspdspmfAMCallocationDetails(psp_dsp_mf_AMC_allocation pdmcaa);

        object psprptperformanceholdingdebtmfreportDetails(psp_rpt_performance_holding_debt_mf_report prphdm);
        object psp_dsp_mf_sip_dashboard(psp_dsp_mf_sip_dashboard serializeProfile);
        object psprptperformanceholdingbondsreportDetails(psp_rpt_performance_holding_bonds_report prphbr);

        object psprptperformanceholdingbondsreportcashflowDetails(psp_rpt_performance_holding_bonds_report_cashflow prphbrc);

        object pspdspcurrentholdingdrilldownDetails(psp_dsp_current_holding_drill_down pdchd);

        object pspdspreportpagesetupDetails(psp_dsp_report_page_setup pdrps);
        object pspdspinflowoutflowDetails(psp_dsp_inflow_outflow_details pdiod);

        object psprptclientperformancecheckDetails(psp_rpt_client_performance_check prcpc);
        object psp_dsp_consolidated_income(psp_dsp_consolidated_income serializeProfile);
        object EquityInFlowOutFlowDetails(EquityClientFlow serializeProfile);

        object psp_dsp_mf_transction_details(psp_dsp_mf_transction_details prcpc);
        object psp_dsp_liquiloan_investment(psp_dsp_liquiloan_investment pdli);
        object psp_dsp_liquiloan_investor_dashboard(psp_dsp_liquiloan_investor_dashboard pdlid);

        object psp_dsp_liquiloan_investor_ledger(psp_dsp_liquiloan_investor_ledger pdlil);
        object psp_dsp_client_portal_dividend_piechart(psp_dsp_client_portal_dividend_piechart pdcpdp);
        object psp_rpt_rm_details(psp_rpt_rm_details prrd);
        object psp_mis_rpt_branch_client_list(psp_mis_rpt_branch_client_list serializeProfile);
        object psp_dsp_aum_break_up(psp_dsp_aum_break_up serializeProfile);
        object psp_dsp_client_kyc_attributes(psp_dsp_client_kyc_attributes serializeProfile);
        object psp_dsp_nri_client_data_entry_taxwithheld_details(psp_dsp_nri_client_data_entry_taxwithheld_details serializeProfile);
        object psp_dsp_eqt_current_fy_ledger(psp_dsp_eqt_current_fy_ledger serializeProfile);
        //object psp_dsp_global_report_sub_category(psp_dsp_global_report_sub_category serializeProfile);
        object psp_dsp_global_report_dividend(psp_dsp_global_report_dividend serializeProfile);
        object psp_dsp_global_report_trades(psp_dsp_global_report_trades serializeProfile);
        object psp_dsp_global_report_gain_loss(psp_dsp_global_report_gain_loss serializeProfile);
        object psp_dsp_global_report_ledger(psp_dsp_global_report_ledger serializeProfile);
        object psp_dsp_client_contact_details(psp_dsp_client_contact_details serializeProfile);
        object psp_dsp_family_details(psp_dsp_family_details serializeProfile);
        object psp_rpt_cdsl_holding_report_familyhead(psp_rpt_cdsl_holding_report_familyhead serializeProfile);
        object psp_rpt_client_bond_mismatch(psp_rpt_client_equity_bond_mismatch serializeProfile);
        object psp_rpt_client_script_mismatch(psp_rpt_client_equity_bond_mismatch serializeProfile);
        object psp_dsp_exchangewise_brokerage(Brokerage_MIS serializeProfile);
        object psp_dsp_branchwise_brokerage(Brokerage_MIS serializeProfile);
        object psp_dsp_brach_family_details(psp_dsp_brach_family_details serializeProfile);
        object psp_dsp_client_portal_mis_recent_client_activity(psp_dsp_client_portal_mis_recent_client_activity serializeProfile);
        object psp_dsp_client_portfolio_interaction_history(psp_dsp_client_portfolio_interaction_history serializeProfile);
        object psp_amd_client_portfolio_interaction_add_remarks(psp_amd_client_portfolio_interaction_add_remarks serializeProfile);
        object psp_dsp_client_script_trades(psp_dsp_client_script_trades serializeProfile);
        object psp_rpt_scripwise_client_holding_cp(psp_rpt_scripwise_client_holding_cp serializeProfile);
        object psp_dsp_mst_index_cp(psp_dsp_mst_index_cp serializeProfile);
        object psp_dsp_master_list_cp(psp_dsp_master_list_cp serializeProfile);
        object psp_amd_index_currency_master_cp(psp_amd_index_currency_master_cp serializeProfile);
        object psp_rpt_nri_client_details(psp_rpt_nri_client_details serializeProfile);
        object psp_rpt_nri_Highest_balance(psp_rpt_nri_Highest_balance serializeProfile);
        object psp_rpt_nri_Highest_nav(psp_rpt_nri_Highest_nav serializeProfile);
        object psp_dsp_equity_client_fy_dividend(psp_dsp_equity_client_fy_dividend serializeProfile);
        object psp_rpt_detailed_realised_gain_loss(psp_rpt_detailed_realised_gain_loss serializeProfile);
        object psp_amd_WS_report_download(psp_amd_WS_report_download serializeProfile);
        object psp_dsp_other_pms_report_parameter(psp_dsp_other_pms_report_parameter serializeProfile);
        object psp_dsp_download_ReportCriteria(psp_dsp_download_ReportCriteria serializeProfile);
        object psp_dsp_ws_download_report_list(psp_dsp_ws_download_report_list serializeProfile);
        object psp_dsp_ws_client_list(psp_dsp_ws_client_list serializeProfile);
        object psp_dsp_Nri_bank_interest_details_drill(psp_dsp_Nri_bank_interest_details_drill serializeProfile);
        object psp_rpt_nri_client_taxwithheld_drill(psp_rpt_nri_client_taxwithheld_drill serializeProfile);
        object psp_rpt_nri_client_dividend(psp_rpt_nri_client_dividend serializeProfile);
        object HoldingSummary(HoldingSummary serializeProfile);
        object psp_rpt_performance_holding_report_client_portal(psp_rpt_performance_holding_report serializeProfile);
        object AssetAllocation(psp_rpt_asset_allocation serializeProfile);
        object psp_dsp_BH_RM_list(psp_dsp_BH_RM_list serializeProfile);
        object psp_dsp_clientwise_holding_list(psp_dsp_clientwise_holding_list serializeProfile);
        object psp_dsp_direct_equity_top_holding(psp_dsp_direct_equity_top_holding serializeProfile);
        object psp_dsp_top_clients_AUMwise(psp_dsp_top_clients_AUMwise serializeProfile);
        object psp_dsp_mapping_client_list(psp_dsp_mapping_client_list serializeProfile);
        object psp_dsp_research_view_given_brokerage(psp_dsp_research_view_given_brokerage serializeProfile);
        object psp_dsp_capital_gain_report(psp_dsp_capital_gain_report serializeProfile);
        object psp_dsp_family_equity_client_list(psp_dsp_family_equity_client_list serializeProfile);
        object psp_dsp_eq_client_scrip_list(psp_dsp_eq_client_scrip_list serializeProfile);
        object bindreferLink(xirr_trade_exception_link serializeProfile);
        object gettradeexception(psp_dsp_client_trade_exceptions serializeProfile);
    }
}
