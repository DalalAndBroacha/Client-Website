using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PieAPI.Encryption_Decryption;
using PieAPI.Repository.Interface;
using ViewModel.Login;
using ViewModel.Reports;
using ViewModel.Reports.Global_Report;
using ViewModel.Reports.Holding;
using ViewModel.Reports.IncomeReports;
using ViewModel.Reports.Liquiloan;
using ViewModel.Shared;

namespace PieAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IReports _locationLookup;

        public ReportsController(IReports locationLookup)
        {
            _locationLookup = locationLookup;
        }
        [HttpPost]
        [Route("CommonDocumentDetails")]
        public object CommonDocumentDetails(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            CommonDocumentReports serializeProfile = JsonConvert.DeserializeObject<CommonDocumentReports>(data);

            //TempData["loginData"] = v1;

            return _locationLookup.CommonDocumentDetails(serializeProfile);
        }

        [HttpPost]
        [Route("IncomePeriod")]
        public object IncomePeriod(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            Period serializeProfile = JsonConvert.DeserializeObject<Period>(data);

            return _locationLookup.IncomePeriod(serializeProfile);
        }

        [HttpPost]
        [Route("psp_dsp_equity_client_fy_factors")]
        public object psp_dsp_equity_client_fy_factors(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_equity_client_fy_factors serializeProfile = JsonConvert.DeserializeObject<psp_dsp_equity_client_fy_factors>(data);

            return _locationLookup.psp_dsp_equity_client_fy_factors(serializeProfile);
        }

        [HttpPost]
        [Route("SIPDetails")]
        public object SIPDetails(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            SIPDetail serializeProfile = JsonConvert.DeserializeObject<SIPDetail>(data);

            return _locationLookup.SIPDetails(serializeProfile);
        }

        [HttpPost]
        [Route("psp_dsp_mf_sip_dashboard")]
        public object psp_dsp_mf_sip_dashboard(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_mf_sip_dashboard serializeProfile = JsonConvert.DeserializeObject<psp_dsp_mf_sip_dashboard>(data);

            return _locationLookup.psp_dsp_mf_sip_dashboard(serializeProfile);
        }


        [HttpPost]
        [Route("DividendDetails")]
        public object DividendDetails(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            Dividend serializeProfile = JsonConvert.DeserializeObject<Dividend>(data);

            return _locationLookup.DividendDetails(serializeProfile);
        }

        [HttpPost]
        [Route("IncomeDetails")]
        public object IncomeDetails(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            IncomeStatement serializeProfile = JsonConvert.DeserializeObject<IncomeStatement>(data);

            return _locationLookup.IncomeDetails(serializeProfile);
        }

        [HttpPost]
        [Route("RealisedDetails")]
        public object RealisedDetails(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            RealisedGainLoss serializeProfile = JsonConvert.DeserializeObject<RealisedGainLoss>(data);
            return _locationLookup.RealisedDetails(serializeProfile);
        }
        [HttpPost]
        [Route("bindreferLink")]
        public object bindreferLink(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            xirr_trade_exception_link serializeProfile = JsonConvert.DeserializeObject<xirr_trade_exception_link>(data);
            return _locationLookup.bindreferLink(serializeProfile);
        }
        [HttpPost]
        [Route("gettradeexception")]
        public object gettradeexception(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_client_trade_exceptions serializeProfile = JsonConvert.DeserializeObject<psp_dsp_client_trade_exceptions>(data);
            return _locationLookup.gettradeexception(serializeProfile);
        }
        [HttpPost]
        [Route("EquityClientDetails")]
        public object EquityClientDetails(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            EquityClientSummary serializeProfile = JsonConvert.DeserializeObject<EquityClientSummary>(data);
            return _locationLookup.EquityClientDetails(serializeProfile);
        }

        [HttpPost]
        [Route("EquityClientFlowDetails")]
        public object EquityClientFlowDetails(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            EquityClientFlow serializeProfile = JsonConvert.DeserializeObject<EquityClientFlow>(data);
            return _locationLookup.EquityClientFlowDetails(serializeProfile);
        }

        [HttpPost]
        [Route("EquityInFlowOutFlowDetails")]
        public object EquityInFlowOutFlowDetails(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            EquityClientFlow serializeProfile = JsonConvert.DeserializeObject<EquityClientFlow>(data);
            return _locationLookup.EquityInFlowOutFlowDetails(serializeProfile);
        }

        [HttpPost]
        [Route("EquityClientFyFactorDetails")]
        public object EquityClientFyFactorDetails(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            EquityClientFyFactor serializeProfile = JsonConvert.DeserializeObject<EquityClientFyFactor>(data);
            return _locationLookup.EquityClientFyFactorDetails(serializeProfile);
        }

        [HttpPost]
        [Route("psp_dsp_equity_client_holding")]
        public object EquityClientHoldingDetails(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            EquityClientHolding serializeProfile = JsonConvert.DeserializeObject<EquityClientHolding>(data);
            return _locationLookup.EquityClientHoldingDetails(serializeProfile);
        }

        [HttpPost]
        [Route("pspdspequitydetails")]
        public object pspdspequitydetails(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            PSPDSPClientFlow serializeProfile = JsonConvert.DeserializeObject<PSPDSPClientFlow>(data);
            return _locationLookup.pspdspequitydetails(serializeProfile);
        }

        [HttpPost]
        [Route("PspDspOutFlowDetails")]
        public object PspDspOutFlowDetails(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            PSPDSPOUTFLOW serializeProfile = JsonConvert.DeserializeObject<PSPDSPOUTFLOW>(data);
            return _locationLookup.PspDspOutFlowDetails(serializeProfile);
        }

        [HttpPost]
        [Route("PSPDSPEQUITYCLIENTDetails")]
        public object PSPDSPEQUITYCLIENTDetails(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            PSPDSPEQUITYCLIENTS serializeProfile = JsonConvert.DeserializeObject<PSPDSPEQUITYCLIENTS>(data);
            return _locationLookup.PSPDSPEQUITYCLIENTDetails(serializeProfile);
        }

        [HttpPost]
        [Route("PSPDSPEQUITYDEALERTRACKTDetails")]
        public object PSPDSPEQUITYDEALERTRACKTDetails(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            Equitydealertracksheet serializeProfile = JsonConvert.DeserializeObject<Equitydealertracksheet>(data);
            return _locationLookup.PSPDSPEQUITYDEALERTRACKTDetails(serializeProfile);
        }

        [HttpPost]
        [Route("psprptperformanceholdingreportDetails")]
        public object psprptperformanceholdingreportDetails(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_rpt_performance_holding_report serializeProfile = JsonConvert.DeserializeObject<psp_rpt_performance_holding_report>(data);
            return _locationLookup.psprptperformanceholdingreportDetails(serializeProfile);
        }
        [HttpPost]
        [Route("psp_rpt_performance_holding_report_client_portal")]
        public object psp_rpt_performance_holding_report_client_portal(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_rpt_performance_holding_report serializeProfile = JsonConvert.DeserializeObject<psp_rpt_performance_holding_report>(data);
            return _locationLookup.psp_rpt_performance_holding_report_client_portal(serializeProfile);
        }
        
        [HttpPost]
        [Route("psprptperformanceholdingdirectequityreportDetails")]
        public object psprptperformanceholdingdirectequityreportDetails(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_rpt_performance_holding_direct_equity_report serializeProfile = JsonConvert.DeserializeObject<psp_rpt_performance_holding_direct_equity_report>(data);
            return _locationLookup.psprptperformanceholdingdirectequityreportDetails(serializeProfile);
        }

        [HttpPost]
        [Route("psprptperformanceholdingequitypmsreportnewformatDetails")]
        public object psprptperformanceholdingequitypmsreportnewformatDetails(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_rpt_performance_holding_equity_pms_report_new_format serializeProfile = JsonConvert.DeserializeObject<psp_rpt_performance_holding_equity_pms_report_new_format>(data);
            return _locationLookup.psprptperformanceholdingequitypmsreportnewformatDetails(serializeProfile);
        }

        [HttpPost]
        [Route("psprptperformanceholdingequitymfreportDetails")]
        public object psprptperformanceholdingequitymfreportDetails(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_rpt_performance_holding_equity_mf_report serializeProfile = JsonConvert.DeserializeObject<psp_rpt_performance_holding_equity_mf_report>(data);
            return _locationLookup.psprptperformanceholdingequitymfreportDetails(serializeProfile);
        }

        [HttpPost]
        [Route("pspdspmfsipstpdetailDetails")]
        public object pspdspmfsipstpdetailDetails(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_mf_sip_stp_detail serializeProfile = JsonConvert.DeserializeObject<psp_dsp_mf_sip_stp_detail>(data);
            return _locationLookup.pspdspmfsipstpdetailDetails(serializeProfile);
        }

        [HttpPost]
        [Route("pspdspmfschemeallocationDetails")]
        public object pspdspmfschemeallocationDetails(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_mf_scheme_allocation serializeProfile = JsonConvert.DeserializeObject<psp_dsp_mf_scheme_allocation>(data);
            return _locationLookup.pspdspmfschemeallocationDetails(serializeProfile);
        }

        [HttpPost]
        [Route("pspdspmfcategoryallocationDetails")]
        public object pspdspmfcategoryallocationDetails(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_mf_category_allocation serializeProfile = JsonConvert.DeserializeObject<psp_dsp_mf_category_allocation>(data);
            return _locationLookup.pspdspmfcategoryallocationDetails(serializeProfile);
        }

        [HttpPost]
        [Route("pspdspmfAMCallocationDetails")]
        public object pspdspmfAMCallocationDetails(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_mf_AMC_allocation serializeProfile = JsonConvert.DeserializeObject<psp_dsp_mf_AMC_allocation>(data);
            return _locationLookup.pspdspmfAMCallocationDetails(serializeProfile);
        }

        [HttpPost]
        [Route("psprptperformanceholdingdebtmfreportDetails")]
        public object psprptperformanceholdingdebtmfreportDetails(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_rpt_performance_holding_debt_mf_report serializeProfile = JsonConvert.DeserializeObject<psp_rpt_performance_holding_debt_mf_report>(data);
            return _locationLookup.psprptperformanceholdingdebtmfreportDetails(serializeProfile);
        }
        [HttpPost]
        [Route("psprptperformanceholdingbondsreportDetails")]
        public object psprptperformanceholdingbondsreportDetails(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_rpt_performance_holding_bonds_report serializeProfile = JsonConvert.DeserializeObject<psp_rpt_performance_holding_bonds_report>(data);
            return _locationLookup.psprptperformanceholdingbondsreportDetails(serializeProfile);
        }
        [HttpPost]
        [Route("psprptperformanceholdingbondsreportcashflowDetails")]
        public object psprptperformanceholdingbondsreportcashflowDetails(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_rpt_performance_holding_bonds_report_cashflow serializeProfile = JsonConvert.DeserializeObject<psp_rpt_performance_holding_bonds_report_cashflow>(data);
            return _locationLookup.psprptperformanceholdingbondsreportcashflowDetails(serializeProfile);
        }

        //[HttpPost]
        //[Route("Performanceholdingreportexport")]
        //public object Performanceholdingreportexport(EncryptData endlogin)
        //{
        //    string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
        //    psp_rpt_performance_holding_bonds_report_cashflow serializeProfile = JsonConvert.DeserializeObject<psp_rpt_performance_holding_bonds_report_cashflow>(data);
        //    return _locationLookup.psprptperformanceholdingbondsreportcashflowDetails(serializeProfile);
        //}

        [HttpPost]
        [Route("pspdspcurrentholdingdrilldownDetails")]
        public object pspdspcurrentholdingdrilldownDetails(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_current_holding_drill_down serializeProfile = JsonConvert.DeserializeObject<psp_dsp_current_holding_drill_down>(data);
            return _locationLookup.pspdspcurrentholdingdrilldownDetails(serializeProfile);
        }

        [HttpPost]
        [Route("pspdspreportpagesetupDetails")]
        public object pspdspreportpagesetupDetails(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_report_page_setup serializeProfile = JsonConvert.DeserializeObject<psp_dsp_report_page_setup>(data);
            return _locationLookup.pspdspreportpagesetupDetails(serializeProfile);
        }

        [HttpPost]
        [Route("pspdspinflowoutflowDetails")]
        public object pspdspinflowoutflowDetails(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_inflow_outflow_details serializeProfile = JsonConvert.DeserializeObject<psp_dsp_inflow_outflow_details>(data);
            return _locationLookup.pspdspinflowoutflowDetails(serializeProfile);
        }

        [HttpPost]
        [Route("psprptclientperformancecheckDetails")]
        public object psprptclientperformancecheckDetails(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_rpt_client_performance_check serializeProfile = JsonConvert.DeserializeObject<psp_rpt_client_performance_check>(data);
            return _locationLookup.psprptclientperformancecheckDetails(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_mf_transction_details")]
        public object psp_dsp_mf_transction_details(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_mf_transction_details serializeProfile = JsonConvert.DeserializeObject<psp_dsp_mf_transction_details>(data);
            return _locationLookup.psp_dsp_mf_transction_details(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_consolidated_income")]
        public object psp_dsp_consolidated_income(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_consolidated_income serializeProfile = JsonConvert.DeserializeObject<psp_dsp_consolidated_income>(data);
            return _locationLookup.psp_dsp_consolidated_income(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_liquiloan_investment")]
        public object psp_dsp_liquiloan_investment(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_liquiloan_investment serializeProfile = JsonConvert.DeserializeObject<psp_dsp_liquiloan_investment>(data);
            return _locationLookup.psp_dsp_liquiloan_investment(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_liquiloan_investor_dashboard")]
        public object psp_dsp_liquiloan_investor_dashboard(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_liquiloan_investor_dashboard serializeProfile = JsonConvert.DeserializeObject<psp_dsp_liquiloan_investor_dashboard>(data);
            return _locationLookup.psp_dsp_liquiloan_investor_dashboard(serializeProfile);
        }
        
        [HttpPost]
        [Route("psp_dsp_liquiloan_investor_ledger")]
        public object psp_dsp_liquiloan_investor_ledger(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_liquiloan_investor_ledger serializeProfile = JsonConvert.DeserializeObject<psp_dsp_liquiloan_investor_ledger>(data);
            return _locationLookup.psp_dsp_liquiloan_investor_ledger(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_client_portal_dividend_piechart")]
        public object psp_dsp_client_portal_dividend_piechart(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_client_portal_dividend_piechart serializeProfile = JsonConvert.DeserializeObject<psp_dsp_client_portal_dividend_piechart>(data);
            return _locationLookup.psp_dsp_client_portal_dividend_piechart(serializeProfile);
        }
        [HttpPost]
        [Route("psp_rpt_rm_details")]
        public object psp_rpt_rm_details(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_rpt_rm_details serializeProfile = JsonConvert.DeserializeObject<psp_rpt_rm_details>(data);
            return _locationLookup.psp_rpt_rm_details(serializeProfile);
        }
        [HttpPost]
        [Route("psp_rpt_scripwise_client_holding_cp")]
        public object psp_rpt_scripwise_client_holding_cp(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_rpt_scripwise_client_holding_cp serializeProfile = JsonConvert.DeserializeObject<psp_rpt_scripwise_client_holding_cp>(data);
            return _locationLookup.psp_rpt_scripwise_client_holding_cp(serializeProfile);
        }
        [HttpPost]
        [Route("psp_mis_rpt_branch_client_list")]
        public object psp_mis_rpt_branch_client_list(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_mis_rpt_branch_client_list serializeProfile = JsonConvert.DeserializeObject<psp_mis_rpt_branch_client_list>(data);
            return _locationLookup.psp_mis_rpt_branch_client_list(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_aum_break_up")]
        public object psp_dsp_aum_break_up(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_aum_break_up serializeProfile = JsonConvert.DeserializeObject<psp_dsp_aum_break_up>(data);
            return _locationLookup.psp_dsp_aum_break_up(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_client_kyc_attributes")]
        public object psp_dsp_client_kyc_attributes(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_client_kyc_attributes serializeProfile = JsonConvert.DeserializeObject<psp_dsp_client_kyc_attributes>(data);
            return _locationLookup.psp_dsp_client_kyc_attributes(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_nri_client_data_entry_taxwithheld_details")]
        public object psp_dsp_nri_client_data_entry_taxwithheld_details(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_nri_client_data_entry_taxwithheld_details serializeProfile = JsonConvert.DeserializeObject<psp_dsp_nri_client_data_entry_taxwithheld_details>(data);
            return _locationLookup.psp_dsp_nri_client_data_entry_taxwithheld_details(serializeProfile);
        }
        [HttpPost]
        [Route("psp_rpt_nri_client_details")]
        public object psp_rpt_nri_client_details(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_rpt_nri_client_details serializeProfile = JsonConvert.DeserializeObject<psp_rpt_nri_client_details>(data);
            return _locationLookup.psp_rpt_nri_client_details(serializeProfile);
        }
        [HttpPost]
        [Route("psp_rpt_nri_Highest_balance")]
        public object psp_rpt_nri_Highest_balance(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_rpt_nri_Highest_balance serializeProfile = JsonConvert.DeserializeObject<psp_rpt_nri_Highest_balance>(data);
            return _locationLookup.psp_rpt_nri_Highest_balance(serializeProfile);
        }
        [HttpPost]
        [Route("psp_rpt_nri_Highest_nav")]
        public object psp_rpt_nri_Highest_nav(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_rpt_nri_Highest_nav serializeProfile = JsonConvert.DeserializeObject<psp_rpt_nri_Highest_nav>(data);
            return _locationLookup.psp_rpt_nri_Highest_nav(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_eqt_current_fy_ledger")]
        public object psp_dsp_eqt_current_fy_ledger(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_eqt_current_fy_ledger serializeProfile = JsonConvert.DeserializeObject<psp_dsp_eqt_current_fy_ledger>(data);
            return _locationLookup.psp_dsp_eqt_current_fy_ledger(serializeProfile);
        }
        //[HttpPost]
        //[Route("psp_dsp_global_report_sub_category")]
        //public object psp_dsp_global_report_sub_category(EncryptData endlogin)
        //{
        //    string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
        //    psp_dsp_global_report_sub_category serializeProfile = JsonConvert.DeserializeObject<psp_dsp_global_report_sub_category>(data);
        //    return _locationLookup.psp_dsp_global_report_sub_category(serializeProfile);
        //}
        [HttpPost]
        [Route("psp_dsp_global_report_dividend")]
        public object psp_dsp_global_report_dividend(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_global_report_dividend serializeProfile = JsonConvert.DeserializeObject<psp_dsp_global_report_dividend>(data);
            return _locationLookup.psp_dsp_global_report_dividend(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_global_report_trades")]
        public object psp_dsp_global_report_trades(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_global_report_trades serializeProfile = JsonConvert.DeserializeObject<psp_dsp_global_report_trades>(data);
            return _locationLookup.psp_dsp_global_report_trades(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_global_report_gain_loss")]
        public object psp_dsp_global_report_gain_loss(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_global_report_gain_loss serializeProfile = JsonConvert.DeserializeObject<psp_dsp_global_report_gain_loss>(data);
            return _locationLookup.psp_dsp_global_report_gain_loss(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_global_report_ledger")]
        public object psp_dsp_global_report_ledger(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_global_report_ledger serializeProfile = JsonConvert.DeserializeObject<psp_dsp_global_report_ledger>(data);
            return _locationLookup.psp_dsp_global_report_ledger(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_client_contact_details")]
        public object psp_dsp_client_contact_details(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_client_contact_details serializeProfile = JsonConvert.DeserializeObject<psp_dsp_client_contact_details>(data);
            return _locationLookup.psp_dsp_client_contact_details(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_family_details")]
        public object psp_dsp_family_details(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_family_details serializeProfile = JsonConvert.DeserializeObject<psp_dsp_family_details>(data);
            return _locationLookup.psp_dsp_family_details(serializeProfile);
        }
        [HttpPost]
        [Route("psp_rpt_cdsl_holding_report_familyhead")]
        public object psp_rpt_cdsl_holding_report_familyhead(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_rpt_cdsl_holding_report_familyhead serializeProfile = JsonConvert.DeserializeObject<psp_rpt_cdsl_holding_report_familyhead>(data);
            return _locationLookup.psp_rpt_cdsl_holding_report_familyhead(serializeProfile);
        }
        [HttpPost]
        [Route("psp_rpt_client_bond_mismatch")]
        public object psp_rpt_client_bond_mismatch(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_rpt_client_equity_bond_mismatch serializeProfile = JsonConvert.DeserializeObject<psp_rpt_client_equity_bond_mismatch>(data);
            return _locationLookup.psp_rpt_client_bond_mismatch(serializeProfile);
        }
        [HttpPost]
        [Route("psp_rpt_client_script_mismatch")]
        public object psp_rpt_client_script_mismatch(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_rpt_client_equity_bond_mismatch serializeProfile = JsonConvert.DeserializeObject<psp_rpt_client_equity_bond_mismatch>(data);
            return _locationLookup.psp_rpt_client_script_mismatch(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_exchangewise_brokerage")]
        public object psp_dsp_exchangewise_brokerage(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            Brokerage_MIS serializeProfile = JsonConvert.DeserializeObject<Brokerage_MIS>(data);
            return _locationLookup.psp_dsp_exchangewise_brokerage(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_branchwise_brokerage")]
        public object psp_dsp_branchwise_brokerage(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            Brokerage_MIS serializeProfile = JsonConvert.DeserializeObject<Brokerage_MIS>(data);
            return _locationLookup.psp_dsp_branchwise_brokerage(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_brach_family_details")]
        public object psp_dsp_brach_family_details(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_brach_family_details serializeProfile = JsonConvert.DeserializeObject<psp_dsp_brach_family_details>(data);
            return _locationLookup.psp_dsp_brach_family_details(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_client_portal_mis_recent_client_activity")]
        public object psp_dsp_client_portal_mis_recent_client_activity(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_client_portal_mis_recent_client_activity serializeProfile = JsonConvert.DeserializeObject<psp_dsp_client_portal_mis_recent_client_activity>(data);
            return _locationLookup.psp_dsp_client_portal_mis_recent_client_activity(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_client_portfolio_interaction_history")]
        public object psp_dsp_client_portfolio_interaction_history(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_client_portfolio_interaction_history serializeProfile = JsonConvert.DeserializeObject<psp_dsp_client_portfolio_interaction_history>(data);
            return _locationLookup.psp_dsp_client_portfolio_interaction_history(serializeProfile);
        }

        [HttpPost]
        [Route("psp_amd_client_portfolio_interaction_add_remarks")]
        public object psp_amd_client_portfolio_interaction_add_remarks(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_amd_client_portfolio_interaction_add_remarks serializeProfile = JsonConvert.DeserializeObject<psp_amd_client_portfolio_interaction_add_remarks>(data);
            return _locationLookup.psp_amd_client_portfolio_interaction_add_remarks(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_client_script_trades")]
        public object psp_dsp_client_script_trades(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_client_script_trades serializeProfile = JsonConvert.DeserializeObject<psp_dsp_client_script_trades>(data);
            return _locationLookup.psp_dsp_client_script_trades(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_mst_index_cp")]
        public object psp_dsp_mst_index_cp(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_mst_index_cp serializeProfile = JsonConvert.DeserializeObject<psp_dsp_mst_index_cp>(data);
            return _locationLookup.psp_dsp_mst_index_cp(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_master_list_cp")]
        public object psp_dsp_master_list_cp(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_master_list_cp serializeProfile = JsonConvert.DeserializeObject<psp_dsp_master_list_cp>(data);
            return _locationLookup.psp_dsp_master_list_cp(serializeProfile);
        }
        [HttpPost]
        [Route("psp_amd_index_currency_master_cp")]
        public object psp_amd_index_currency_master_cp(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_amd_index_currency_master_cp serializeProfile = JsonConvert.DeserializeObject<psp_amd_index_currency_master_cp>(data);
            return _locationLookup.psp_amd_index_currency_master_cp(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_equity_client_fy_dividend")]
        public object psp_dsp_equity_client_fy_dividend(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_equity_client_fy_dividend serializeProfile = JsonConvert.DeserializeObject<psp_dsp_equity_client_fy_dividend>(data);
            return _locationLookup.psp_dsp_equity_client_fy_dividend(serializeProfile);
        }
        [HttpPost]
        [Route("psp_rpt_detailed_realised_gain_loss")]
        public object psp_rpt_detailed_realised_gain_loss(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_rpt_detailed_realised_gain_loss serializeProfile = JsonConvert.DeserializeObject<psp_rpt_detailed_realised_gain_loss>(data);
            return _locationLookup.psp_rpt_detailed_realised_gain_loss(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_ws_client_list")]
        public object psp_dsp_ws_client_list(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_ws_client_list serializeProfile = JsonConvert.DeserializeObject<psp_dsp_ws_client_list>(data);
            return _locationLookup.psp_dsp_ws_client_list(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_ws_download_report_list")]
        public object psp_dsp_ws_download_report_list(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_ws_download_report_list serializeProfile = JsonConvert.DeserializeObject<psp_dsp_ws_download_report_list>(data);
            return _locationLookup.psp_dsp_ws_download_report_list(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_download_ReportCriteria")]
        public object psp_dsp_download_ReportCriteria(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_download_ReportCriteria serializeProfile = JsonConvert.DeserializeObject<psp_dsp_download_ReportCriteria>(data);
            return _locationLookup.psp_dsp_download_ReportCriteria(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_other_pms_report_parameter")]
        public object psp_dsp_other_pms_report_parameter(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_other_pms_report_parameter serializeProfile = JsonConvert.DeserializeObject<psp_dsp_other_pms_report_parameter>(data);
            return _locationLookup.psp_dsp_other_pms_report_parameter(serializeProfile);
        }
        [HttpPost]
        [Route("psp_amd_WS_report_download")]
        public object psp_amd_WS_report_download(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_amd_WS_report_download serializeProfile = JsonConvert.DeserializeObject<psp_amd_WS_report_download>(data);
            return _locationLookup.psp_amd_WS_report_download(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_Nri_bank_interest_details_drill")]
        public object psp_dsp_Nri_bank_interest_details_drill(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_Nri_bank_interest_details_drill serializeProfile = JsonConvert.DeserializeObject<psp_dsp_Nri_bank_interest_details_drill>(data);
            return _locationLookup.psp_dsp_Nri_bank_interest_details_drill(serializeProfile);
        }
        [HttpPost]
        [Route("psp_rpt_nri_client_taxwithheld_drill")]
        public object psp_rpt_nri_client_taxwithheld_drill(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_rpt_nri_client_taxwithheld_drill serializeProfile = JsonConvert.DeserializeObject<psp_rpt_nri_client_taxwithheld_drill>(data);
            return _locationLookup.psp_rpt_nri_client_taxwithheld_drill(serializeProfile);
        }
        [HttpPost]
        [Route("psp_rpt_nri_client_dividend")]
        public object psp_rpt_nri_client_dividend(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_rpt_nri_client_dividend serializeProfile = JsonConvert.DeserializeObject<psp_rpt_nri_client_dividend>(data);
            return _locationLookup.psp_rpt_nri_client_dividend(serializeProfile);
        }
        [HttpPost]
        [Route("HoldingSummary")]
        public object HoldingSummary(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            HoldingSummary serializeProfile = JsonConvert.DeserializeObject<HoldingSummary>(data);
            return _locationLookup.HoldingSummary(serializeProfile);
        }
        [HttpPost]
        [Route("AssetAllocation")]
        public object AssetAllocation(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_rpt_asset_allocation serializeProfile = JsonConvert.DeserializeObject<psp_rpt_asset_allocation>(data);
            return _locationLookup.AssetAllocation(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_BH_RM_list")]
        public object psp_dsp_BH_RM_list(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_BH_RM_list serializeProfile = JsonConvert.DeserializeObject<psp_dsp_BH_RM_list>(data);
            return _locationLookup.psp_dsp_BH_RM_list(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_clientwise_holding_list")]
        public object psp_dsp_clientwise_holding_list(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_clientwise_holding_list serializeProfile = JsonConvert.DeserializeObject<psp_dsp_clientwise_holding_list>(data);
            return _locationLookup.psp_dsp_clientwise_holding_list(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_direct_equity_top_holding")]
        public object psp_dsp_direct_equity_top_holding(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_direct_equity_top_holding serializeProfile = JsonConvert.DeserializeObject<psp_dsp_direct_equity_top_holding>(data);
            return _locationLookup.psp_dsp_direct_equity_top_holding(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_top_clients_AUMwise")]
        public object psp_dsp_top_clients_AUMwise(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_top_clients_AUMwise serializeProfile = JsonConvert.DeserializeObject<psp_dsp_top_clients_AUMwise>(data);
            return _locationLookup.psp_dsp_top_clients_AUMwise(serializeProfile);
        }

        [HttpPost]
        [Route("psp_dsp_mapping_client_list")]
        public object psp_dsp_mapping_client_list(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_mapping_client_list serializeProfile = JsonConvert.DeserializeObject<psp_dsp_mapping_client_list>(data);
            return _locationLookup.psp_dsp_mapping_client_list(serializeProfile);
        }

        [HttpPost]
        [Route("psp_dsp_research_view_given_brokerage")]
        public object psp_dsp_research_view_given_brokerage(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_research_view_given_brokerage serializeProfile = JsonConvert.DeserializeObject<psp_dsp_research_view_given_brokerage>(data);
            return _locationLookup.psp_dsp_research_view_given_brokerage(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_capital_gain_report")]
        public object psp_dsp_capital_gain_report(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_capital_gain_report serializeProfile = JsonConvert.DeserializeObject<psp_dsp_capital_gain_report>(data);
            return _locationLookup.psp_dsp_capital_gain_report(serializeProfile);
        }

        [HttpPost]
        [Route("psp_dsp_family_equity_client_list")]
        public object psp_dsp_family_equity_client_list(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_family_equity_client_list serializeProfile = JsonConvert.DeserializeObject<psp_dsp_family_equity_client_list>(data);
            return _locationLookup.psp_dsp_family_equity_client_list(serializeProfile);
        }

        [HttpPost]
        [Route("psp_dsp_eq_client_scrip_list")]
        public object psp_dsp_eq_client_scrip_list(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_eq_client_scrip_list serializeProfile = JsonConvert.DeserializeObject<psp_dsp_eq_client_scrip_list>(data);
            return _locationLookup.psp_dsp_eq_client_scrip_list(serializeProfile);
        }

        

    }
}
