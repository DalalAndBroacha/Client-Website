using PieAPI.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using ViewModel.Reports;
using Dapper;
using ViewModel.Reports.IncomeReports;
using ViewModel.Reports.Liquiloan;
using ViewModel.Shared;
using ViewModel.Reports.Global_Report;
using ViewModel.Reports.Holding;
using System.Diagnostics;

namespace PieAPI.Repository
{
    public class ReportsRepository : IReports
    {
        private readonly IDapperRepository _repository;
        public ReportsRepository(IDapperRepository repository)
        {
            _repository = repository;
        }

        public object CommonDocumentDetails(CommonDocumentReports cdr)
        {
            var dp_params = new DynamicParameters();
            dp_params.Add("login_id", cdr.Id, DbType.Int32);
            var result = _repository.GetAll<List<CommonDocumentReports>>("[dbo].[psp_dsp_common_documents]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }
        public object SIPDetails(SIPDetail sd)

        {
            var dp_params = new DynamicParameters();
            dp_params.Add("loginid", sd.Id, DbType.Int32);
            dp_params.Add("family_id", sd.family_id, DbType.Int32);
            dp_params.Add("tokenId", sd.access_token, DbType.String);
            dp_params.Add("login_source", 'W');
            var result = _repository.GetAll<List<SIPDetail>>("[dbo].[psp_dsp_mf_sip_details]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }
        public object psp_dsp_mf_sip_dashboard(psp_dsp_mf_sip_dashboard pdmsd)

        {
            var dp_params = new DynamicParameters();
            dp_params.Add("loginid", pdmsd.Id, DbType.Int32);
            dp_params.Add("tokenId", pdmsd.access_token, DbType.String);
            dp_params.Add("login_source", 'W');
            var result = _repository.GetAll<List<psp_dsp_mf_sip_dashboard>>("[dbo].[psp_dsp_mf_sip_dashboard]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }

        public object DividendDetails(Dividend dividend)
        {
            var dp_params = new DynamicParameters();
            dp_params.Add("loginid", dividend.Id, DbType.Int32);
            dp_params.Add("family_id", dividend.family_id, DbType.Int32);
            dp_params.Add("finyr", dividend.main_client_id, DbType.String);
            dp_params.Add("finyr", dividend.finyr, DbType.Int32);
            dp_params.Add("tokenId", dividend.access_token, DbType.String);
            dp_params.Add("login_source", "W");
            dp_params.Add("sub_category", " ");
            var result = _repository.GetAll<List<Dividend>>("[dbo].[psp_dsp_dividend_report]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }
        public object IncomeDetails(IncomeStatement inc)
        {
            var dp_params = new DynamicParameters();
            dp_params.Add("login_id", inc.Id, DbType.Int32);
            dp_params.Add("family_id", inc.family_id, DbType.Int32);
            dp_params.Add("year", inc.year, DbType.Int32);
            dp_params.Add("rpt_period", inc.rpt_period, DbType.Int32);
            dp_params.Add("rpt_period_value", inc.rpt_period_value, DbType.Int32);
            dp_params.Add("login_source", "W");
            dp_params.Add("tokenId", inc.access_token, DbType.String);
            dp_params.Add("main_client_id", "");

            var result = _repository.GetAll<List<IncomeStatement>>("[dbo].[psp_dsp_income_statement]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }

        
        public object psp_dsp_consolidated_income(psp_dsp_consolidated_income pdci)
        {
            var dp_params = new DynamicParameters();
            dp_params.Add("fin_year", pdci.fin_year, DbType.Int32);
            dp_params.Add("fam_id", pdci.fam_id, DbType.String);
            dp_params.Add("loginId", pdci.Id, DbType.Int32);
            var result = _repository.GetAll<List<psp_dsp_consolidated_income>>("[dbo].[psp_dsp_consolidated_income]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }
        public object RealisedDetails(RealisedGainLoss rgl)
        {
            var dp_params = new DynamicParameters();
            dp_params.Add("FINYR", rgl.FINYR, DbType.Int32);
            dp_params.Add("Client", rgl.Client, DbType.String);
            dp_params.Add("sub_category", rgl.sub_category, DbType.String);
            dp_params.Add("rpt_period", rgl.rpt_period, DbType.Int32);
            dp_params.Add("rpt_period_value", rgl.rpt_period_value, DbType.String);
            var result = _repository.GetAll<List<RealisedGainLoss>>("[dbo].[psp_rpt_periodic_detailed_realised_gain_loss]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }
        public object bindreferLink(xirr_trade_exception_link ecs)
        {
            var dp_params = new DynamicParameters();
            dp_params.Add("LoginId", ecs.Id, DbType.Int32);
            dp_params.Add("account_code", ecs.account_code, DbType.Int32);
            var result = _repository.GetAll<List<xirr_trade_exception_link>>("[dbo].[psp_dsp_xirr_trade_exception_link]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }
        public object gettradeexception(psp_dsp_client_trade_exceptions ecs)
        {
            var dp_params = new DynamicParameters();
            //dp_params.Add("LoginId", ecs.Id, DbType.Int32);
            dp_params.Add("client_code", ecs.client_code, DbType.Int32);
            var result = _repository.GetAll<List<psp_dsp_client_trade_exceptions>>("[dbo].[psp_dsp_client_trade_exceptions]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }
        public object EquityClientDetails(EquityClientSummary ecs)
        {
            var dp_params = new DynamicParameters();
            dp_params.Add("LoginId", ecs.Id, DbType.Int32);
            dp_params.Add("account_code", ecs.account_code, DbType.Int32);
            var result = _repository.GetAll<List<EquityClientSummary>>("[dbo].[psp_dsp_equity_client_sector_summary]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }

        public object EquityClientFlowDetails(EquityClientFlow ecf)
        {
            var dp_params = new DynamicParameters();
            dp_params.Add("LoginId", ecf.Id, DbType.Int32);
            dp_params.Add("account_code", ecf.account_code, DbType.String);
            dp_params.Add("flow_type", ecf.flow_type, DbType.Int32);
            dp_params.Add("summary", ecf.summary, DbType.Int32);
            var result = _repository.Get<EquityClientFlow>("[dbo].[psp_dsp_equity_client_flow]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }

        public object EquityInFlowOutFlowDetails(EquityClientFlow ecf)
        {
            var dp_params = new DynamicParameters();
            dp_params.Add("LoginId", ecf.Id, DbType.Int32);
            dp_params.Add("account_code", ecf.account_code, DbType.String);
            dp_params.Add("flow_type", ecf.flow_type, DbType.Int32);
            dp_params.Add("summary", ecf.summary, DbType.Int32);
            var result = _repository.GetAll<List<EquityClientFlow>>("[dbo].[psp_dsp_equity_client_flow]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }
        public object psp_dsp_equity_client_fy_factors(psp_dsp_equity_client_fy_factors pdecff)
        {
            var dp_params = new DynamicParameters();
            dp_params.Add("account_code", pdecff.account_code, DbType.String);
            dp_params.Add("fin_year", pdecff.fin_year, DbType.Int32);
            dp_params.Add("rpt", 2, DbType.Int32);
            var result = _repository.GetAll<List<psp_dsp_equity_client_fy_factors>>("[dbo].[psp_dsp_equity_client_fy_factors]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }

        public object EquityClientFyFactorDetails(EquityClientFyFactor ecff)
        {
            var dp_params = new DynamicParameters();
            dp_params.Add("LoginId", ecff.Id, DbType.Int32);
            dp_params.Add("account_code", ecff.account_code, DbType.String);
            var result = _repository.GetAll<List<EquityClientFyFactor>>("[dbo].[psp_dsp_equity_client_all_fy_factors]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }

        public object EquityClientHoldingDetails(EquityClientHolding ech)
        {
            var dp_params = new DynamicParameters();
            dp_params.Add("LoginId", ech.Id, DbType.Int32);
            dp_params.Add("account_code", ech.account_code, DbType.String);
            dp_params.Add("holding_type", ech.holding_type, DbType.String);
            var result = _repository.GetAll<List<EquityClientHolding>>("[dbo].[psp_dsp_equity_client_holding]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }

        public object IncomePeriod(Period period)
        {
            var dp_params = new DynamicParameters();
            dp_params.Add("period_type", period.period_typeValue, DbType.Int32);
            dp_params.Add("Year", period.Year_typevalue, DbType.Int32);
            var result = _repository.GetAll<List<EquityClientHolding>>("[dbo].[psp_dsp_dd_period]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }

        public object pspdspequitydetails(PSPDSPClientFlow pdcf)
        {
            var dp_params = new DynamicParameters();
            dp_params.Add("LoginId", pdcf.LoginId, DbType.Int32);
            dp_params.Add("account_code", pdcf.account_code, DbType.String);
            dp_params.Add("flow_type", pdcf.flow_type, DbType.Int32);
            dp_params.Add("summary", pdcf.summary, DbType.Int32);
            var result = _repository.GetAll<List<PSPDSPClientFlow>>("[dbo].[psp_dsp_equity_client_flow]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }

        public object PspDspOutFlowDetails(PSPDSPOUTFLOW pdof)
        {
            var dp_params = new DynamicParameters();
            dp_params.Add("LoginId", pdof.Id, DbType.Int32);
            dp_params.Add("account_code", pdof.account_code, DbType.String);
            dp_params.Add("flow_type", Int32.Parse(pdof.flow_type), DbType.Int32);
            dp_params.Add("summary", Int32.Parse(pdof.summary), DbType.Int32);

            var result = _repository.GetAll<List<PSPDSPOUTFLOW>>("[dbo].[psp_dsp_equity_client_flow]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }

        public object PSPDSPEQUITYCLIENTDetails(PSPDSPEQUITYCLIENTS pdec)
        {
            var dp_params = new DynamicParameters();
            dp_params.Add("LoginId", pdec.LoginId, DbType.Int32);
            dp_params.Add("clientcode", pdec.clientcode, DbType.String);

            var result = _repository.GetAll<List<PSPDSPEQUITYCLIENTS>>("[dbo].[psp_dsp_equity_clients]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }

        public object PSPDSPEQUITYDEALERTRACKTDetails(Equitydealertracksheet edts)
        {
            var dp_params = new DynamicParameters();
            dp_params.Add("LoginId", edts.Id, DbType.Int32);
            dp_params.Add("branch_id", 0, DbType.Int32);
            dp_params.Add("show_value", "0", DbType.String);
            var result = _repository.GetAll<List<Equitydealertracksheet>>("[dbo].[psp_dsp_equity_dealer_tracksheet]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }

        public object psprptperformanceholdingreportDetails(psp_rpt_performance_holding_report prphr)
        {
            var dp_params = new DynamicParameters();
            dp_params.Add("LoginId", prphr.Id, DbType.Int32);
            dp_params.Add("Family", prphr.Family, DbType.String);
            dp_params.Add("FINYR", prphr.FINYR, DbType.Int32);
            var result = _repository.GetAll<List<psp_rpt_performance_holding_report>>("[dbo].[psp_rpt_performance_holding_report_client_portal]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }
        public object psp_rpt_performance_holding_report_client_portal(psp_rpt_performance_holding_report prphr)
        {
            var dp_params = new DynamicParameters();
            dp_params.Add("LoginId", prphr.Id, DbType.Int32);
            dp_params.Add("Family", prphr.Family, DbType.String);
            dp_params.Add("FINYR", prphr.FINYR, DbType.Int32);
            var result = _repository.GetAll<List<psp_rpt_performance_holding_report>>("[dbo].[psp_rpt_performance_holding_report_client_portal_new]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }

        public object psprptperformanceholdingdirectequityreportDetails(psp_rpt_performance_holding_direct_equity_report prphder)
        {
            var dp_params = new DynamicParameters();

			if (prphder.Subcategory == "Fixed Deposit")
			{
				dp_params.Add("fin_year", prphder.FINYRData, DbType.String);
				dp_params.Add("main_client_id", prphder.ClientID, DbType.String);
				var result = _repository.GetAll<List<psp_dsp_fd_details>>("[dbo].[psp_dsp_fd_details]", dp_params, commandType: CommandType.StoredProcedure);
				return result;
			}

			dp_params.Add("LoginId", prphder.Id, DbType.Int32);
            dp_params.Add("Family", prphder.FamilyID, DbType.String);
            dp_params.Add("Client", prphder.ClientID, DbType.String);
            dp_params.Add("FINYR", prphder.FINYRData, DbType.Int32);
            dp_params.Add("Subcategory", prphder.Subcategory, DbType.String);
            

            if (prphder.Subcategory == "Direct Equity" || prphder.Subcategory == "InvITs" || prphder.Subcategory == "ReITs")
            {
                dp_params.Add("source", "C", DbType.String);
                var result = _repository.GetAll<List<psp_rpt_performance_holding_direct_equity_report>>("[dbo].[psp_rpt_performance_holding_direct_equity_report]", dp_params, commandType: CommandType.StoredProcedure);
                return result;
            }
            else if (prphder.Subcategory == "Equity MF" || prphder.Subcategory == "Debt MF")
            {
                var result = _repository.GetAll<List<psp_rpt_performance_holding_equity_mf_report>>("[dbo].[psp_rpt_performance_holding_mf_report_client_portal]", dp_params, commandType: CommandType.StoredProcedure);
                return result;
            }
            else if (prphder.Subcategory == "Bonds")
            {
                var result = _repository.GetAll<List<psp_dsp_performance_holding_bonds_report_cp>>("[dbo].[psp_dsp_performance_holding_bonds_report_cp]", dp_params, commandType: CommandType.StoredProcedure);
                return result;
            }
            else if (prphder.Subcategory == "Equity PMS")
            {
                var result = _repository.GetAll<List<psp_rpt_performance_holding_equity_pms_report_new_format>>("[dbo].[psp_rpt_performance_holding_equity_pms_report_new_format]", dp_params, commandType: CommandType.StoredProcedure);
                return result;
            }
            else if(prphder.main_category == "Other PMS")
            {
                dp_params.Add("source", prphder.source, DbType.String);
                dp_params.Add("display_flag", "C", DbType.String);
                var result = _repository.GetAll<List<psp_rpt_performance_holding_other_pms_report>>("[dbo].[psp_rpt_performance_holding_other_pms_report]", dp_params, commandType: CommandType.StoredProcedure);
                return result;
            }
                //else if (prphder.Subcategory == "Moat And Special Situations Portfolio" || prphder.Subcategory == "Emerging Corporates India Portfolio")
                //{
                //    dp_params.Add("source", "Multiact", DbType.String);
                //    dp_params.Add("display_flag", "C", DbType.String);
                //    var result = _repository.GetAll<List<psp_rpt_performance_holding_other_pms_report>>("[dbo].[psp_rpt_performance_holding_other_pms_report]", dp_params, commandType: CommandType.StoredProcedure);
                //    return result;
                //}
                //else if (prphder.Subcategory == "Marcellus Consistent Compounders Portfolio" || prphder.Subcategory == "Marcellus Little Champs Portfolio" || prphder.Subcategory == "Marcellus Kings Of Capital Portfolio")
                //{
                //    dp_params.Add("source", "Marcellus", DbType.String);
                //    dp_params.Add("display_flag", "C", DbType.String);
                //    var result = _repository.GetAll<List<psp_rpt_performance_holding_other_pms_report>>("[dbo].[psp_rpt_performance_holding_other_pms_report]", dp_params, commandType: CommandType.StoredProcedure);
                //    return result;
                //}
                //else if (prphder.Subcategory == "Buoyant Opportunities Scheme" || prphder.Subcategory == "Buoyant Opportunities Strategy - Investor")
                //{
                //    dp_params.Add("source", "Buoyant", DbType.String);
                //    dp_params.Add("display_flag", "C", DbType.String);
                //    var result = _repository.GetAll<List<psp_rpt_performance_holding_other_pms_report>>("[dbo].[psp_rpt_performance_holding_other_pms_report]", dp_params, commandType: CommandType.StoredProcedure);
                //    return result;
                //}
                //else if (prphder.Subcategory == "GIRIK MULTICAP GROWTH EQUITY STRATEGY" || prphder.Subcategory == "GIRIK LIQUID STRATEGY")
                //{
                //    dp_params.Add("source", "Girik", DbType.String);
                //    dp_params.Add("display_flag", "C", DbType.String);
                //    var result = _repository.GetAll<List<psp_rpt_performance_holding_other_pms_report>>("[dbo].[psp_rpt_performance_holding_other_pms_report]", dp_params, commandType: CommandType.StoredProcedure);
                //    return result;
                //}
                //else if (prphder.Subcategory == "InCred Healthcare Portfolio")
                //{
                //    dp_params.Add("source", "InCred", DbType.String);
                //    dp_params.Add("display_flag", "C", DbType.String);
                //    var result = _repository.GetAll<List<psp_rpt_performance_holding_other_pms_report>>("[dbo].[psp_rpt_performance_holding_other_pms_report]", dp_params, commandType: CommandType.StoredProcedure);
                //    return result;
                //}
                //else if (prphder.Subcategory == "WHITE OAK INDIA PIONEERS EQUITY PORTFOLIO")
                //{
                //    dp_params.Add("source", "WhiteOak", DbType.String);
                //    dp_params.Add("display_flag", "C", DbType.String);
                //    var result = _repository.GetAll<List<psp_rpt_performance_holding_other_pms_report>>("[dbo].[psp_rpt_performance_holding_other_pms_report]", dp_params, commandType: CommandType.StoredProcedure);
                //    return result;
                //}
                return null;

        }

        public object psprptperformanceholdingequitypmsreportnewformatDetails(psp_rpt_performance_holding_equity_pms_report_new_format prpheprn)
        {
            var dp_params = new DynamicParameters();
            dp_params.Add("LoginId", prpheprn.LoginId, DbType.Int32);
            dp_params.Add("Family", prpheprn.Family, DbType.String);
            dp_params.Add("Client", prpheprn.Client, DbType.String);
            dp_params.Add("FINYR", prpheprn.FINYR, DbType.Int32);
            dp_params.Add("Subcategory", prpheprn.Subcategory, DbType.String);
            var result = _repository.GetAll<List<psp_rpt_performance_holding_equity_pms_report_new_format>>("[dbo].[psp_rpt_performance_holding_equity_pms_report_new_format]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }

        public object psprptperformanceholdingequitymfreportDetails(psp_rpt_performance_holding_equity_mf_report prphemr)
        {
            var dp_params = new DynamicParameters();
            dp_params.Add("LoginId", prphemr.LoginId, DbType.Int32);
            dp_params.Add("Family", prphemr.Family, DbType.String);
            dp_params.Add("Client", prphemr.Client, DbType.String);
            dp_params.Add("FINYR", prphemr.FINYR, DbType.Int32);
            dp_params.Add("Subcategory", prphemr.Subcategory, DbType.String);
            var result = _repository.GetAll<List<psp_rpt_performance_holding_equity_mf_report>>("[dbo].[psp_rpt_performance_holding_equity_mf_report]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }

        public object pspdspmfsipstpdetailDetails(psp_dsp_mf_sip_stp_detail pdms)
        {
            var dp_params = new DynamicParameters();
            dp_params.Add("loginid", pdms.Id, DbType.Int32);
            dp_params.Add("family_id", pdms.FamilyID, DbType.Int32);
            dp_params.Add("main_client_id", pdms.MainClientID, DbType.Int32);
            dp_params.Add("tran_type", pdms.tran_type, DbType.String);
            dp_params.Add("scheme_category", pdms.Subcategory, DbType.String);
            dp_params.Add("fin_year", pdms.FINYRData, DbType.Int32);
            dp_params.Add("source", "C", DbType.String);

            var result = _repository.GetAll<List<psp_dsp_mf_sip_stp_detail>>("[dbo].[psp_dsp_mf_sip_stp_detail]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }

        public object pspdspmfschemeallocationDetails(psp_dsp_mf_scheme_allocation pdmsa)
        {
            var dp_params = new DynamicParameters();
            dp_params.Add("login_id", pdmsa.Id, DbType.Int32);
            dp_params.Add("family_id", pdmsa.FamilyID, DbType.Int32);
            dp_params.Add("main_client_id", pdmsa.MainClientID, DbType.Int32);
            dp_params.Add("fund_type", pdmsa.Subcategory, DbType.String);
            dp_params.Add("fin_year", pdmsa.FINYRData, DbType.Int32);
            dp_params.Add("source", "C", DbType.String);

            var result = _repository.GetAll<List<psp_dsp_mf_scheme_allocation>>("[dbo].[psp_dsp_mf_scheme_allocation]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }

        public object pspdspmfcategoryallocationDetails(psp_dsp_mf_category_allocation pdmca)
        {
            var dp_params = new DynamicParameters();
            dp_params.Add("login_id", pdmca.Id, DbType.Int32);
            dp_params.Add("family_id", pdmca.FamilyID, DbType.Int32);
            dp_params.Add("main_client_id", pdmca.MainClientID, DbType.Int32);
            dp_params.Add("fund_type", pdmca.Subcategory, DbType.String);
            dp_params.Add("fin_year", pdmca.FINYRData, DbType.Int32);
            dp_params.Add("source", "C", DbType.String);

            var result = _repository.GetAll<List<psp_dsp_mf_category_allocation>>("[dbo].[psp_dsp_mf_category_allocation]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }

        public object pspdspmfAMCallocationDetails(psp_dsp_mf_AMC_allocation pdmcaa)
        {
            var dp_params = new DynamicParameters();
            dp_params.Add("login_id", pdmcaa.Id, DbType.Int32);
            dp_params.Add("family_id", pdmcaa.FamilyID , DbType.Int32);
            dp_params.Add("main_client_id", pdmcaa.MainClientID, DbType.Int32);
            dp_params.Add("fund_type", pdmcaa.Subcategory, DbType.String);
            dp_params.Add("fin_year", pdmcaa.FINYRData, DbType.Int32);
            dp_params.Add("source", "C", DbType.String);
            
            var result = _repository.GetAll<List<psp_dsp_mf_AMC_allocation>>("[dbo].[psp_dsp_mf_AMC_allocation]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }

        public object psprptperformanceholdingdebtmfreportDetails(psp_rpt_performance_holding_debt_mf_report prphdm)
        {
            var dp_params = new DynamicParameters();
            dp_params.Add("LoginId", prphdm.LoginId, DbType.Int32);
            dp_params.Add("Family", prphdm.Family, DbType.String);
            dp_params.Add("Client", prphdm.Client, DbType.String);
            dp_params.Add("FINYR", prphdm.FINYR, DbType.Int32);
            dp_params.Add("Subcategory", prphdm.Subcategory, DbType.String);
            var result = _repository.GetAll<List<psp_rpt_performance_holding_debt_mf_report>>("[dbo].[psp_rpt_performance_holding_debt_mf_report]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }
        public object psprptperformanceholdingbondsreportDetails(psp_rpt_performance_holding_bonds_report prphbr)
        {
            var dp_params = new DynamicParameters();
            dp_params.Add("LoginId", prphbr.Id, DbType.Int32);
            dp_params.Add("Family", prphbr.family_id, DbType.String);
            dp_params.Add("Client", prphbr.client_id, DbType.String);
            dp_params.Add("FINYR", prphbr.FINYR, DbType.Int32);
            dp_params.Add("Subcategory", prphbr.Subcategory, DbType.String);
            var result = _repository.GetAll<List<psp_rpt_performance_holding_bonds_report>>("[dbo].[psp_rpt_performance_holding_bonds_report]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }

        public object psprptperformanceholdingbondsreportcashflowDetails(psp_rpt_performance_holding_bonds_report_cashflow prphbrc)
        {
            var dp_params = new DynamicParameters();
            dp_params.Add("LoginId", prphbrc.Id, DbType.Int32);
            dp_params.Add("Family", prphbrc.FamilyID, DbType.String);
            dp_params.Add("Client", prphbrc.ClientID, DbType.String);
            dp_params.Add("FINYR", prphbrc.FINYRData, DbType.Int32);
            dp_params.Add("Subcategory", prphbrc.Subcategory, DbType.String);
            var result = _repository.GetAll<List<psp_rpt_performance_holding_bonds_report_cashflow>>("[dbo].[psp_rpt_performance_holding_bonds_report_cashflow]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }

        public object pspdspcurrentholdingdrilldownDetails(psp_dsp_current_holding_drill_down pdchd)
        {
            var dp_params = new DynamicParameters();
            dp_params.Add("client_Code", pdchd.client_Code, DbType.Int32);
            dp_params.Add("script_code", pdchd.script_code, DbType.String);
            var result = _repository.GetAll<List<psp_dsp_current_holding_drill_down>>("[dbo].[psp_dsp_current_holding_drill_down]", dp_params, commandType: CommandType.StoredProcedure);
            return result;
        }

        public object pspdspreportpagesetupDetails(psp_dsp_report_page_setup pdrps)
        {
            var dp_params = new DynamicParameters();
            var result = _repository.Get<psp_dsp_report_page_setup>("[dbo].[psp_dsp_report_page_setup]", dp_params, commandType: CommandType.StoredProcedure);
            return result;
        }

        public object pspdspinflowoutflowDetails(psp_dsp_inflow_outflow_details pdiod)
        {
            var dp_params = new DynamicParameters();
            dp_params.Add("LoginId", pdiod.Id, DbType.Int32);
            dp_params.Add("account_code", pdiod.account_code, DbType.String);
            dp_params.Add("trans_date", pdiod.trans_date, DbType.String);
            dp_params.Add("flow_type", Int32.Parse(pdiod.flow_type), DbType.Int32);
            var result = _repository.GetAll<List<psp_dsp_inflow_outflow_details>>("[dbo].[psp_dsp_inflow_outflow_details]", dp_params, commandType: CommandType.StoredProcedure);
            return result;
        }

        public object psprptclientperformancecheckDetails(psp_rpt_client_performance_check prcpc)
        {
            var dp_params = new DynamicParameters();
            dp_params.Add("FINYR", prcpc.FINYR, DbType.Int32);
            dp_params.Add("Client", prcpc.Client, DbType.String);
            dp_params.Add("Scrip_Code", prcpc.Scrip_Code, DbType.Int32);
            dp_params.Add("subcategory", prcpc.subcategory, DbType.String);
            dp_params.Add("account_code", prcpc.account_code, DbType.String);
            dp_params.Add("asset_code", prcpc.asset_code, DbType.String);

            var result = _repository.GetAll<List<psp_rpt_client_performance_check>>("[dbo].[psp_rpt_client_performance_check]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }

        public object psp_dsp_mf_transction_details(psp_dsp_mf_transction_details prcpc)
        {
            var dp_params = new DynamicParameters();
            
            dp_params.Add("main_client_code", prcpc.JSON_main_client_code, DbType.Int32);
            dp_params.Add("scrip_code", prcpc.JSON_scrip_code, DbType.Int32);
            dp_params.Add("sub_category", prcpc.JSON_sub_category, DbType.String);
            dp_params.Add("folio_no", prcpc.JSON_folio_no, DbType.String);

            var result = _repository.GetAll<List<psp_dsp_mf_transction_details>>("[dbo].[psp_dsp_mf_transction_details]", dp_params, commandType: CommandType.StoredProcedure);
            return result;
        }

        public object psp_dsp_liquiloan_investment(psp_dsp_liquiloan_investment prcpc)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("investor_id", prcpc.investor_id , DbType.String);

            var result = _repository.GetAll<List<psp_dsp_liquiloan_investment>>("[dbo].[psp_dsp_liquiloan_investment]", dp_params, commandType: CommandType.StoredProcedure);
            return result;
        }
        public object psp_dsp_liquiloan_investor_dashboard(psp_dsp_liquiloan_investor_dashboard prcpc)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("investor_id", prcpc.investor_id, DbType.String);

            var result = _repository.Get<psp_dsp_liquiloan_investor_dashboard>("[dbo].[psp_dsp_liquiloan_investor_dashboard]", dp_params, commandType: CommandType.StoredProcedure);
            return result;
        }
        public object psp_dsp_liquiloan_investor_ledger(psp_dsp_liquiloan_investor_ledger prcpc)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("investor_id", prcpc.investor_id, DbType.String);

            var result = _repository.GetAll<List<psp_dsp_liquiloan_investor_ledger>>("[dbo].[psp_dsp_liquiloan_investor_ledger]", dp_params, commandType: CommandType.StoredProcedure);
            return result;
        }

        public object psp_dsp_client_portal_dividend_piechart(psp_dsp_client_portal_dividend_piechart prcpc)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("family_id", prcpc.family_id, DbType.String);
            dp_params.Add("fin_year", prcpc.fin_year, DbType.Int32);

            var result = _repository.GetAll<List<psp_dsp_client_portal_dividend_piechart>>("[dbo].[psp_dsp_client_portal_dividend_piechart]", dp_params, commandType: CommandType.StoredProcedure);
            return result;
        }
        public object psp_rpt_rm_details(psp_rpt_rm_details prcpc)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("login_id", prcpc.Id, DbType.Int32);
            dp_params.Add("Asset", prcpc.Asset, DbType.String);
            dp_params.Add("RM", prcpc.RM, DbType.Int32);
            dp_params.Add("Branch", prcpc.Branch, DbType.Int32);

            var result = _repository.GetAll<List<psp_rpt_rm_details>>("[dbo].[psp_rpt_rm_details]", dp_params, commandType: CommandType.StoredProcedure);
            return result;
        }

        public object psp_rpt_scripwise_client_holding_cp(psp_rpt_scripwise_client_holding_cp prcpc)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("LoginId", prcpc.LoginId, DbType.Int32);
            dp_params.Add("Asset", prcpc.Asset, DbType.String);
            dp_params.Add("Script", prcpc.Script, DbType.String);

            var result = _repository.GetAll<List<psp_rpt_scripwise_client_holding_cp>>("[dbo].[psp_rpt_scripwise_client_holding_cp]", dp_params, commandType: CommandType.StoredProcedure);
            return result;
        }

        public object psp_dsp_mst_index_cp(psp_dsp_mst_index_cp prcpc)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("from_date", prcpc.from_date, DbType.Date);
            dp_params.Add("to_date", prcpc.to_date, DbType.Date);
            dp_params.Add("flag", prcpc.flag, DbType.Int32);

            var result = _repository.GetAll<List<psp_dsp_mst_index_cp>>("[dbo].[psp_dsp_mst_index_cp]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }
        public object psp_dsp_master_list_cp(psp_dsp_master_list_cp prcpc)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("flag", prcpc.flag, DbType.Int32);

            var result = _repository.GetAll<List<psp_dsp_master_list_cp>>("[dbo].[psp_dsp_master_list_cp]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }
        public object psp_amd_index_currency_master_cp(psp_amd_index_currency_master_cp prcpc)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("login_id", prcpc.login_id, DbType.Int32);
            dp_params.Add("rec_id", prcpc.rec_id, DbType.Int32);
            dp_params.Add("mod_flag", prcpc.mod_flag, DbType.String);
            dp_params.Add("table_id", prcpc.table_id, DbType.Int32);
            dp_params.Add("rate", prcpc.rate, DbType.Decimal);
            dp_params.Add("tr_date", prcpc.tr_date, DbType.Date);
            dp_params.Add("master_id", prcpc.master_id, DbType.Int32);

            var result = _repository.GetAll<List<psp_amd_index_currency_master_cp>>("[dbo].[psp_amd_index_currency_master_cp]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }

        

        public object psp_mis_rpt_branch_client_list(psp_mis_rpt_branch_client_list prcpc)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("login_id", prcpc.Id, DbType.Int32);

            var result = _repository.GetAll<List<psp_mis_rpt_branch_client_list>>("[dbo].[psp_mis_rpt_branch_client_list]", dp_params, commandType: CommandType.StoredProcedure);
            return result;
        }
        public object psp_dsp_aum_break_up(psp_dsp_aum_break_up prcpc)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("Loginid", prcpc.Id, DbType.Int32);

            var result = _repository.GetAll<List<psp_dsp_aum_break_up>>("[dbo].[psp_dsp_aum_break_up]", dp_params, commandType: CommandType.StoredProcedure);
            return result;
        }
        public object psp_dsp_client_kyc_attributes(psp_dsp_client_kyc_attributes prcpc)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("account_code", prcpc.account_code, DbType.String);

            var result = _repository.Get<psp_dsp_client_kyc_attributes>("[dbo].[psp_dsp_client_kyc_attributes]", dp_params, commandType: CommandType.StoredProcedure);
            return result;
        }
        public object psp_dsp_nri_client_data_entry_taxwithheld_details(psp_dsp_nri_client_data_entry_taxwithheld_details prcpc)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("main_client_id", prcpc.main_client_id, DbType.Int32);
            dp_params.Add("bank_account", prcpc.bank_account, DbType.String);
            dp_params.Add("cal_year", prcpc.cal_year, DbType.Int32);

            var result = _repository.GetAll<List<psp_dsp_nri_client_data_entry_taxwithheld_details>>("[dbo].[psp_dsp_nri_client_data_entry_taxwithheld_details]", dp_params, commandType: CommandType.StoredProcedure);
            return result;
        }
        public object psp_rpt_nri_client_details(psp_rpt_nri_client_details prcpc)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("main_client_id", prcpc.main_client_id, DbType.Int32);
            dp_params.Add("client_category", prcpc.client_category, DbType.Int32);
            dp_params.Add("cal_year", prcpc.cal_year, DbType.Int32);
            dp_params.Add("flag", prcpc.flag, DbType.String);

            var result = _repository.GetAll<List<psp_rpt_nri_client_details>>("[dbo].[psp_rpt_nri_client_details]", dp_params, commandType: CommandType.StoredProcedure);
            return result;
        }

        public object psp_rpt_nri_Highest_balance(psp_rpt_nri_Highest_balance prcpc)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("main_client_id", prcpc.main_client_id, DbType.Int32);
            dp_params.Add("client_category", prcpc.client_category, DbType.Int32);
            dp_params.Add("cal_year", prcpc.cal_year, DbType.Int32);

            var result = _repository.GetAll<List<psp_rpt_nri_Highest_balance>>("[dbo].[psp_rpt_nri_Highest_balance]", dp_params, commandType: CommandType.StoredProcedure);
            return result;
        }
        public object psp_rpt_nri_Highest_nav(psp_rpt_nri_Highest_nav prcpc)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("main_client_id", prcpc.main_client_id, DbType.Int32);
            dp_params.Add("client_category", prcpc.client_category, DbType.Int32);
            dp_params.Add("cal_year", prcpc.cal_year, DbType.Int32);

            var result = _repository.GetAll<List<psp_rpt_nri_Highest_nav>>("[dbo].[psp_rpt_nri_Highest_nav]", dp_params, commandType: CommandType.StoredProcedure);
            return result;
        }
        public object psp_dsp_eqt_current_fy_ledger(psp_dsp_eqt_current_fy_ledger prcpc)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("LoginId", prcpc.LoginId, DbType.Int32);
            dp_params.Add("Client", prcpc.Client, DbType.String);

            var result = _repository.GetAll<List<psp_dsp_eqt_current_fy_ledger>>("[dbo].[psp_dsp_eqt_current_fy_ledger]", dp_params, commandType: CommandType.StoredProcedure);
            return result;
        }

        public object psp_dsp_equity_client_fy_dividend(psp_dsp_equity_client_fy_dividend prcpc)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("account_code", prcpc.account_code, DbType.String);
            dp_params.Add("fin_year", prcpc.fin_year, DbType.Int32);

            var result = _repository.GetAll<List<psp_dsp_equity_client_fy_dividend>>("[dbo].[psp_dsp_equity_client_fy_dividend]", dp_params, commandType: CommandType.StoredProcedure);
            return result;
        }

        public object psp_rpt_detailed_realised_gain_loss(psp_rpt_detailed_realised_gain_loss prcpc)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("FINYR", prcpc.FINYR, DbType.Int32);
            dp_params.Add("Client", prcpc.Client, DbType.String);
            dp_params.Add("sub_category", "Direct Equity", DbType.String);
            dp_params.Add("source", "C", DbType.String);

            var result = _repository.GetAll<List<psp_rpt_detailed_realised_gain_loss>>("[dbo].[psp_rpt_detailed_realised_gain_loss]", dp_params, commandType: CommandType.StoredProcedure);
            return result;
        }


        

        //public object psp_dsp_global_report_sub_category(psp_dsp_global_report_sub_category dataObj)
        //{
        //    var dp_params = new DynamicParameters();

        //    dp_params.Add("main_client_id", dataObj.main_client_id, DbType.Int32);
        //    dp_params.Add("start_date", dataObj.start_date, DbType.Date);
        //    dp_params.Add("end_date", dataObj.end_date, DbType.Date);

        //    var result = _repository.GetAll<List<psp_dsp_global_report_sub_category>>("[dbo].[psp_dsp_global_report_sub_category]", dp_params, commandType: CommandType.StoredProcedure);

        //    return result;
        //}
        public object psp_dsp_global_report_dividend(psp_dsp_global_report_dividend dataObj)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("main_client_id", dataObj.main_client_id, DbType.Int32);
            dp_params.Add("from_date", dataObj.from_date, DbType.Date);
            dp_params.Add("to_date", dataObj.to_date, DbType.Date);
            dp_params.Add("flag", dataObj.flag, DbType.String);
            

            var result = _repository.GetAll<List<psp_dsp_global_report_dividend>>("[dbo].[psp_dsp_global_report_dividend]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }
        public object psp_dsp_global_report_trades(psp_dsp_global_report_trades dataObj)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("main_client_id", dataObj.main_client_id, DbType.Int32);
            dp_params.Add("from_date", dataObj.from_date, DbType.Date);
            dp_params.Add("to_date", dataObj.to_date, DbType.Date);

            var result = _repository.GetAll<List<psp_dsp_global_report_trades>>("[dbo].[psp_dsp_global_report_trades]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }
        public object psp_dsp_global_report_gain_loss(psp_dsp_global_report_gain_loss dataObj)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("main_client_id", dataObj.main_client_id, DbType.Int32);
            dp_params.Add("from_date", dataObj.from_date, DbType.Date);
            dp_params.Add("to_date", dataObj.to_date, DbType.Date);

            var result = _repository.GetAll<List<psp_dsp_global_report_gain_loss>>("[dbo].[psp_dsp_global_report_gain_loss]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }
        public object psp_dsp_global_report_ledger(psp_dsp_global_report_ledger dataObj)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("main_client_id", dataObj.main_client_id, DbType.Int32);
            dp_params.Add("from_date", dataObj.from_date, DbType.Date);
            dp_params.Add("to_date", dataObj.to_date, DbType.Date);

            var result = _repository.GetAll<List<psp_dsp_global_report_ledger>>("[dbo].[psp_dsp_global_report_ledger]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }
        public object psp_dsp_client_contact_details(psp_dsp_client_contact_details dataObj)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("family_id", dataObj.family_id, DbType.Int32);

            var result = _repository.GetAll<List<psp_dsp_client_contact_details>>("[dbo].[psp_dsp_client_contact_details]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }
        public object psp_dsp_family_details(psp_dsp_family_details dataObj)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("LoginId", dataObj.LoginId, DbType.Int32);

            var result = _repository.Get<psp_dsp_family_details>("[dbo].[psp_dsp_family_details]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }
        public object psp_rpt_cdsl_holding_report_familyhead(psp_rpt_cdsl_holding_report_familyhead dataObj)
        {

            var dp_params = new DynamicParameters();

            dp_params.Add("LoginId", dataObj.LoginHashId, DbType.String);
            dp_params.Add("access_token", dataObj.AccessToken, DbType.String);
            dp_params.Add("FamilyId", dataObj.FamilyId, DbType.String);
            dp_params.Add("main_client_id", dataObj.main_client_id, DbType.String);
            dp_params.Add("as_on_date", dataObj.as_on_date, DbType.Date);

            var result = _repository.GetAll<List<psp_rpt_cdsl_holding_report_familyhead>>("[dbo].[psp_rpt_cdsl_holding_report_familyhead_v1]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }
        public object psp_rpt_client_bond_mismatch(psp_rpt_client_equity_bond_mismatch dataObj)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("LoginId", 0, DbType.Int32);
            dp_params.Add("Branch", dataObj.Branch, DbType.String);
            dp_params.Add("RM", dataObj.RM, DbType.String);

            var result = _repository.GetAll<List<psp_rpt_client_equity_bond_mismatch>>("[dbo].[psp_rpt_client_bond_mismatch]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }
        public object psp_rpt_client_script_mismatch(psp_rpt_client_equity_bond_mismatch dataObj)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("LoginId", 0, DbType.Int32);
            dp_params.Add("Branch", dataObj.Branch, DbType.String);
            dp_params.Add("RM", dataObj.RM, DbType.String);

            var result = _repository.GetAll<List<psp_rpt_client_equity_bond_mismatch>>("[dbo].[psp_rpt_client_script_mismatch]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }
        public object psp_dsp_exchangewise_brokerage(Brokerage_MIS dataObj)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("as_on_date", dataObj.as_on_date, DbType.Date);
            
            var result = _repository.GetAll<List<Brokerage_MIS>>("[dbo].[psp_dsp_exchangewise_brokerage]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }
        public object psp_dsp_branchwise_brokerage(Brokerage_MIS dataObj)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("as_on_date", dataObj.as_on_date, DbType.Date);

            var result = _repository.GetAll<List<Brokerage_MIS>>("[dbo].[psp_dsp_branchwise_brokerage]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }
        public object psp_dsp_brach_family_details(psp_dsp_brach_family_details dataObj)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("login_id", dataObj.login_id, DbType.Int32);
            dp_params.Add("branch_id", dataObj.branch_id, DbType.Int32);

            var result = _repository.GetAll<List<psp_dsp_brach_family_details>>("[dbo].[psp_dsp_brach_family_details]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }
        public object psp_dsp_client_portal_mis_recent_client_activity(psp_dsp_client_portal_mis_recent_client_activity dataObj)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("login_id", dataObj.login_id, DbType.Int32);

            var result = _repository.GetAll<List<psp_dsp_client_portal_mis_recent_client_activity>>("[dbo].[psp_dsp_client_portal_mis_recent_client_activity]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }
        public object psp_dsp_client_portfolio_interaction_history(psp_dsp_client_portfolio_interaction_history dataObj)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("login_id", dataObj.login_id, DbType.Int32);
            dp_params.Add("main_client_id", dataObj.main_client_id, DbType.Int32);

            var result = _repository.GetAll<List<psp_dsp_client_portfolio_interaction_history>>("[dbo].[psp_dsp_client_portfolio_interaction_history]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }
        public object psp_amd_client_portfolio_interaction_add_remarks(psp_amd_client_portfolio_interaction_add_remarks dataObj)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("login_id", dataObj.login_id, DbType.Int32);
            dp_params.Add("main_client_id", dataObj.main_client_id, DbType.Int32);
            dp_params.Add("review_date", dataObj.review_date, DbType.Date);
            dp_params.Add("remarks", dataObj.remarks, DbType.String);

            var result = _repository.GetAll<List<psp_amd_client_portfolio_interaction_add_remarks>>("[dbo].[psp_amd_client_portfolio_interaction_add_remarks]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }
        public object psp_dsp_client_script_trades(psp_dsp_client_script_trades dataObj)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("client_Code", dataObj.client_Code, DbType.Int32);
            dp_params.Add("script_code", dataObj.script_code, DbType.Int32);
            
            var result = _repository.GetAll<List<psp_dsp_client_script_trades>>("[dbo].[psp_dsp_client_script_trades]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }
        public object psp_dsp_ws_client_list(psp_dsp_ws_client_list prcpc)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("Loginid", prcpc.LoginId, DbType.Int32);
            var result = _repository.GetAll<List<psp_dsp_ws_client_list>>("[dbo].[psp_dsp_ws_client_list]", dp_params, commandType: CommandType.StoredProcedure);
            return result;
        }
        public object psp_dsp_ws_download_report_list(psp_dsp_ws_download_report_list prcpc)
        {
            var dp_params = new DynamicParameters();

            //dp_params.Add("Loginid", prcpc.LoginId, DbType.Int32);
            var result = _repository.GetAll<List<psp_dsp_ws_download_report_list>>("[dbo].[psp_dsp_ws_download_report_list]", dp_params, commandType: CommandType.StoredProcedure);
            return result;
        }
        public object psp_dsp_download_ReportCriteria(psp_dsp_download_ReportCriteria prcpc)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("report_id", prcpc.report_id, DbType.Int32);
            //dp_params.Add("ws_client_id", prcpc.ws_client_id, DbType.Int32);
            var result = _repository.GetAll<List<psp_dsp_download_ReportCriteria>>("[dbo].[psp_dsp_download_ReportCriteria]", dp_params, commandType: CommandType.StoredProcedure);
            return result;
        }
        public object psp_dsp_other_pms_report_parameter(psp_dsp_other_pms_report_parameter prcpc)
        {
            var dp_params = new DynamicParameters();
            var result = _repository.GetAll<List<psp_dsp_other_pms_report_parameter>>("[dbo].[psp_dsp_other_pms_report_parameter]", dp_params, commandType: CommandType.StoredProcedure);
            return result;
        }
        public object psp_amd_WS_report_download(psp_amd_WS_report_download pawrd)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("login_id", pawrd.Login_id, DbType.Int32);
            dp_params.Add("report_id", pawrd.Report_id, DbType.Int32);
            dp_params.Add("json_parameter", pawrd.reportCriteria, DbType.String);
            dp_params.Add("description", pawrd.description, DbType.String);
            dp_params.Add("filename", pawrd.filename, DbType.String);

            var result = _repository.GetAll<List<psp_amd_WS_report_download>>("[dbo].[psp_amd_WS_report_download]", dp_params, commandType: CommandType.StoredProcedure);
            return result;
        }

        public object psp_dsp_Nri_bank_interest_details_drill(psp_dsp_Nri_bank_interest_details_drill prcpc)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("main_client_id", prcpc.main_client_id, DbType.Int32);
            dp_params.Add("cal_year", prcpc.cal_year, DbType.Int32);
            dp_params.Add("client_cat", prcpc.client_cat, DbType.Int32);
            dp_params.Add("flag", prcpc.flag, DbType.String);

            var result = _repository.GetAll<List<psp_dsp_Nri_bank_interest_details_drill>>("[dbo].[psp_dsp_Nri_bank_interest_details_drill]", dp_params, commandType: CommandType.StoredProcedure);
            return result;
        }
        public object psp_rpt_nri_client_taxwithheld_drill(psp_rpt_nri_client_taxwithheld_drill prcpc)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("main_client_id", prcpc.main_client_id, DbType.Int32);
            dp_params.Add("cal_year", prcpc.cal_year, DbType.Int32);
            dp_params.Add("client_cat", prcpc.client_cat, DbType.Int32);
            dp_params.Add("flag", "C", DbType.String);

            var result = _repository.GetAll<List<psp_rpt_nri_client_taxwithheld_drill>>("[dbo].[psp_rpt_nri_client_taxwithheld_drill]", dp_params, commandType: CommandType.StoredProcedure);
            return result;
        }
        public object psp_rpt_nri_client_dividend(psp_rpt_nri_client_dividend prcpc)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("main_client_id", prcpc.main_client_id, DbType.Int32);
            dp_params.Add("cal_year", prcpc.cal_year, DbType.Int32);
            dp_params.Add("client_category", prcpc.client_category, DbType.Int32);
            dp_params.Add("sub_category", prcpc.sub_category, DbType.String);


            var result = _repository.GetAll<List<psp_rpt_nri_client_dividend>>("[dbo].[psp_rpt_nri_client_dividend]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }
        public object HoldingSummary(HoldingSummary hs)
        {

            var dp_params = new DynamicParameters();

            dp_params.Add("main_client_id", hs.main_client_id, DbType.Int32);
            dp_params.Add("ToDate", hs.ToDate, DbType.Date);
            dp_params.Add("summary", hs.summaryFlag, DbType.String);
            dp_params.Add("FINYR", hs.FINYR, DbType.Int32);
            dp_params.Add("type", "C", DbType.String);

            var result = _repository.GetAll<List<HoldingSummary>>("[dbo].[psp_dsp_performance_holding_details]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }
        public object AssetAllocation(psp_rpt_asset_allocation ra)
        {

            var dp_params = new DynamicParameters();

            dp_params.Add("LoginId", ra.LoginId, DbType.Int32);
            dp_params.Add("acces_token", ra.acces_token, DbType.String);
            dp_params.Add("FamilyId",ra.family_id, DbType.String);
            dp_params.Add("fin_year", ra.fin_year, DbType.Int32);

            var result = _repository.GetAll<List<psp_rpt_asset_allocation>>("[dbo].[psp_rpt_asset_allocation]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }
        public object psp_dsp_BH_RM_list(psp_dsp_BH_RM_list serializeProfile)
        {

            var dp_params = new DynamicParameters();

            dp_params.Add("login_id", serializeProfile.login_id, DbType.Int32);

            var result = _repository.GetAll<List<psp_dsp_BH_RM_list>>("[dbo].[psp_dsp_BH_RM_list]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }
        public object psp_dsp_clientwise_holding_list(psp_dsp_clientwise_holding_list serializeProfile)
        {

            var dp_params = new DynamicParameters();

            dp_params.Add("LoginId", serializeProfile.LoginId, DbType.String);
            dp_params.Add("rm_id", serializeProfile.rm_id, DbType.Int32);
            dp_params.Add("scrip_code", serializeProfile.scrip_code, DbType.String);
            dp_params.Add("data", serializeProfile.data, DbType.Int32);

            var result = _repository.GetAll<List<psp_dsp_clientwise_holding_list>>("[dbo].[psp_dsp_clientwise_holding_list]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }

        public object psp_dsp_direct_equity_top_holding(psp_dsp_direct_equity_top_holding serializeProfile)
        {

            var dp_params = new DynamicParameters();

            dp_params.Add("login_id", serializeProfile.login_id, DbType.Int32);
            dp_params.Add("top_count", 0, DbType.Int32);

            var result = _repository.GetAll<List<psp_dsp_direct_equity_top_holding>>("[dbo].[psp_dsp_direct_equity_top_holding]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }

        public object psp_dsp_top_clients_AUMwise(psp_dsp_top_clients_AUMwise serializeProfile)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("login_id", serializeProfile.login_id, DbType.Int32);
            dp_params.Add("rm_id", serializeProfile.rm_id, DbType.Int32);
            dp_params.Add("from_rec", serializeProfile.from_rec, DbType.Int32);
            dp_params.Add("to_rec", serializeProfile.to_rec, DbType.Int32);

            var result = _repository.GetAll<List<psp_dsp_top_clients_AUMwise>>("[dbo].[psp_dsp_top_clients_AUMwise]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }

        public object psp_dsp_mapping_client_list(psp_dsp_mapping_client_list serializeProfile)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("login_id", serializeProfile.login_id, DbType.Int32);
            dp_params.Add("rm_id", serializeProfile.rm_id, DbType.Int32);

            var result = _repository.GetAll<List<psp_dsp_mapping_client_list>>("[dbo].[psp_dsp_mapping_client_list]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }

        public object psp_dsp_research_view_given_brokerage(psp_dsp_research_view_given_brokerage serializeProfile)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("from_date", serializeProfile.from_date, DbType.Date);
            dp_params.Add("to_date", serializeProfile.to_date, DbType.Date);
            dp_params.Add("flag", "3", DbType.Int32);
            dp_params.Add("scrip_code", serializeProfile.scrip_code, DbType.String);
            dp_params.Add("rm_id", serializeProfile.rm_id, DbType.Int32);

            var result = _repository.GetAll<List<psp_dsp_research_view_given_brokerage>>("[dbo].[psp_dsp_research_view_given_brokerage]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }


        public object psp_dsp_capital_gain_report(psp_dsp_capital_gain_report serializeProfile)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("LoginId", serializeProfile.LoginId, DbType.String);
            dp_params.Add("access_token", serializeProfile.access_token, DbType.String);
            dp_params.Add("FamilyId", serializeProfile.FamilyId, DbType.String);
            dp_params.Add("MainClientHash", serializeProfile.MainClientHash, DbType.String);
            dp_params.Add("from_date", serializeProfile.from_date, DbType.Date);
            dp_params.Add("to_date", serializeProfile.to_date, DbType.Date);

            var result = _repository.GetAll<List<psp_dsp_capital_gain_report>>("[dbo].[psp_dsp_capital_gain_report]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }

        public object psp_dsp_family_equity_client_list(psp_dsp_family_equity_client_list serializeProfile)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("family_hash", serializeProfile.family_hash, DbType.String);
            dp_params.Add("flag", serializeProfile.flag, DbType.Int32);
            
            var result = _repository.GetAll<List<psp_dsp_family_equity_client_list>>("[dbo].[psp_dsp_family_equity_client_list]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }

        public object psp_dsp_eq_client_scrip_list(psp_dsp_eq_client_scrip_list serializeProfile)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("client_id", serializeProfile.client_id, DbType.Int32);

            var result = _repository.GetAll<List<psp_dsp_eq_client_scrip_list>>("[dbo].[psp_dsp_eq_client_scrip_list]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }


        

        //psp_dsp_eq_client_scrip_list
    }
}
