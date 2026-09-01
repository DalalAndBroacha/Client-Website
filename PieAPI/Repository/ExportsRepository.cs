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
using ViewModel.Exports;
using ViewModel.Login;
using ViewModel.UtilityClasses;

namespace PieAPI.Repository
{
    public class ExportsRepository : IExports
    {
        private readonly IDapperRepository _repository;
        public ExportsRepository(IDapperRepository repository)
        {
            _repository = repository;
        }

        public object psp_amd_send_report_client_portal(psp_amd_send_report_client_portal pasrCP)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("login_id", pasrCP.loginID, DbType.Int32);
            dp_params.Add("family_id", pasrCP.family_id, DbType.Int32);
            dp_params.Add("report_id", pasrCP.report_id, DbType.Int32);
            dp_params.Add("parameters", pasrCP.parameters, DbType.String);
            dp_params.Add("description", pasrCP.description.HtmlEncode(), DbType.String);
            dp_params.Add("export_type", pasrCP.export_type, DbType.String); //E for email, X for Download, S for SMS
            dp_params.Add("export_format", pasrCP.export_format, DbType.String); // PDF, EXCEL
            dp_params.Add("mail_to", "R", DbType.String);
            
            var result = _repository.GetAll<List<psp_amd_send_report_client_portal>>("[dbo].[psp_amd_send_report_client_portal]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }

        public object getFamId(FamilyList pasrCP)
        {
            var dp_params = new DynamicParameters();

            dp_params.Add("Family_token", pasrCP.Family_Token, DbType.String);

            var result = _repository.Get<FamilyList>("[dbo].[psp_dsp_get_family_id_client_portal]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }

        public object psp_dsp_client_portal_downloads_details(psp_dsp_client_portal_downloads_details pdcpdd)
        {
            var dp_params = new DynamicParameters();
            
            dp_params.Add("requester_id", pdcpdd.requester_id, DbType.Int32);

            var result = _repository.GetAll<psp_dsp_client_portal_downloads_details>("[dbo].[psp_dsp_client_portal_downloads_details]", dp_params, commandType: CommandType.StoredProcedure);

            return result;
        }
    }
}
