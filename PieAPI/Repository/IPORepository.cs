using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using PieAPI.Repository.Interface;
using ViewModel.IPO;
using static System.Net.Mime.MediaTypeNames;

namespace PieAPI.Repository
{
    public class IPORepository : IIPO
    {
        private readonly IDapperRepository _repository;
        public IPORepository(IDapperRepository repository)
        {
            _repository = repository;
        }

        public object PopulateIPOData(string serializeProfile)
        {
            var dp_params = new DynamicParameters();
            
            dp_params.Add("jsonString", serializeProfile);
            var result = _repository.GetAll<List<OpenIPOIssues>>("[dbo].[psp_amd_ipo_openissue]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }

        public object psp_dsp_ipo_openissue(string serializeProfile)
        {
            var dp_params = new DynamicParameters();
            var result = _repository.GetAll<List<OpenIPOIssues>>("[dbo].[psp_dsp_ipo_openissue]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }

        public object psp_dsp_ipo_clients_list(psp_dsp_ipo_clients_list dataObj)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("family_token", dataObj.family_token, DbType.String);

            var result = _repository.GetAll<psp_dsp_ipo_clients_list>("[dbo].[psp_dsp_ipo_clients_list]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }
        public object psp_amd_ipo_requests(psp_amd_ipo_requests dataObj)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("applicationno", dataObj.applicationno, DbType.String);
            dp_params.Add("dp_id", dataObj.dp_id, DbType.String);
            dp_params.Add("main_client_id", dataObj.main_client_id, DbType.Int32);
            dp_params.Add("family_token", dataObj.family_token, DbType.String);
            dp_params.Add("ISIN", dataObj.ISIN, DbType.String);
            dp_params.Add("symbol", dataObj.symbol, DbType.String);
            dp_params.Add("bid_id", dataObj.bid_id, DbType.String);
            dp_params.Add("orderno", dataObj.orderno, DbType.String);
            dp_params.Add("bid_qty", dataObj.bid_qty, DbType.Decimal);
            dp_params.Add("bid_price", dataObj.bid_price, DbType.Decimal);
            dp_params.Add("bid_cutoff", dataObj.bid_cutoff, DbType.String);
            dp_params.Add("bid_ttl_amount", dataObj.bid_ttl_amount, DbType.Decimal);
            dp_params.Add("upi_id", dataObj.upi_id, DbType.String);
            dp_params.Add("otp", dataObj.otp, DbType.String);
            dp_params.Add("otp_encrypt", dataObj.otp_encrypt, DbType.String);
            dp_params.Add("login_id", dataObj.login_id, DbType.String);

            var result = _repository.Get<psp_amd_ipo_requests>("[dbo].[psp_amd_ipo_requests]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }

        public object psp_verify_ipo_otp(psp_verify_ipo_otp dataObj)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("dp_id", dataObj.dp_id, DbType.String);
            dp_params.Add("applicationno", dataObj.applicationno, DbType.String);
            dp_params.Add("flag", dataObj.flag, DbType.String);
            

            var result = _repository.Get<psp_verify_ipo_otp>("[dbo].[psp_verify_ipo_otp]", dp_params, commandType: CommandType.StoredProcedure);

            return result;

        }

        
    }
}