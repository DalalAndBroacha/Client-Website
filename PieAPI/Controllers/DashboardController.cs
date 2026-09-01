using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PieAPI.Encryption_Decryption;
using PieAPI.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModel.Login;
using ViewModel.Reports;
using ViewModel.Shared;

namespace PieAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboard _locationLookup;

        public DashboardController(IDashboard locationLookup)
        {
            _locationLookup = locationLookup;
        }

        [HttpPost]
        [Route("DeshboardFinYears")]
        public object DeshboardFinYears(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            DashboardFinYears serializeProfile = JsonConvert.DeserializeObject<DashboardFinYears>(data);


            return _locationLookup.DeshboardFinYears(serializeProfile);
        }
        [HttpPost]
        [Route("FamilyDetails")]
        public object FamilyDetails(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            FamilyList serializeProfile = JsonConvert.DeserializeObject<FamilyList>(data);


            return _locationLookup.FamilyDetails(serializeProfile);
        }
        

        [HttpPost]
        [Route("Familymenu")]
        public object Familymenu(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            menu serializeProfile = JsonConvert.DeserializeObject<menu>(data);


            return _locationLookup.Familymenu(serializeProfile);
        }
        [HttpPost]
        [Route("FetchFevDetails")]
        public object FetchFevDetails(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            FetchFevourite serializeProfile = JsonConvert.DeserializeObject<FetchFevourite>(data);
            return _locationLookup.FetchFevDetails(serializeProfile);

        }

        [HttpPost]
        [Route("UpdateFevouriteDetails")]
        public object UpdateFevouriteDetails(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            UpdateFevourite serializeProfile = JsonConvert.DeserializeObject<UpdateFevourite>(data);
            return _locationLookup.UpdateFevouriteDetails(serializeProfile);

        }

        [HttpPost]
        [Route("AssetList")]
        public object AssetList(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_client_portal_assets serializeProfile = JsonConvert.DeserializeObject<psp_dsp_client_portal_assets>(data);
            return _locationLookup.AssetList(serializeProfile);

        }
        [HttpPost]
        [Route("BranchList")]
        public object BranchList(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_client_portal_branch serializeProfile = JsonConvert.DeserializeObject<psp_dsp_client_portal_branch>(data);
            return _locationLookup.BranchList(serializeProfile);

        }
        [HttpPost]
        [Route("RMList")]
        public object RMList(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_rpt_ssrs_template_RM serializeProfile = JsonConvert.DeserializeObject<psp_rpt_ssrs_template_RM>(data);
            return _locationLookup.RMList(serializeProfile);

        }
        [HttpPost]
        [Route("psp_dsp_all_rm_list")]
        public object psp_dsp_all_rm_list(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_all_rm_list serializeProfile = JsonConvert.DeserializeObject<psp_dsp_all_rm_list>(data);
            return _locationLookup.psp_dsp_all_rm_list(serializeProfile);

        }
        [HttpPost]
        [Route("psp_rpt_scripwiseholding_scrip")]
        public object psp_rpt_scripwiseholding_scrip(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_rpt_scripwiseholding_scrip serializeProfile = JsonConvert.DeserializeObject<psp_rpt_scripwiseholding_scrip>(data);
            return _locationLookup.psp_rpt_scripwiseholding_scrip(serializeProfile);

        }
        [HttpPost]
        [Route("psp_dsp_kyc_income_range")]
        public object psp_dsp_kyc_income_range(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_kyc_income_range serializeProfile = JsonConvert.DeserializeObject<psp_dsp_kyc_income_range>(data);
            return _locationLookup.psp_dsp_kyc_income_range(serializeProfile);

        }
        [HttpPost]
        [Route("psp_dsp_kyc_relations")]
        public object psp_dsp_kyc_relations(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_kyc_relations serializeProfile = JsonConvert.DeserializeObject<psp_dsp_kyc_relations>(data);
            return _locationLookup.psp_dsp_kyc_relations(serializeProfile);

        }
        [HttpPost]
        [Route("psp_dsp_branch_list")]
        public object psp_dsp_branch_list(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_branch_list serializeProfile = JsonConvert.DeserializeObject<psp_dsp_branch_list>(data);

            return _locationLookup.psp_dsp_branch_list(serializeProfile);
        }
        [HttpPost]
        [Route("psp_amd_survey_interaction")]
        public object psp_amd_survey_interaction(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_amd_survey_interaction serializeProfile = JsonConvert.DeserializeObject<psp_amd_survey_interaction>(data);

            return _locationLookup.psp_amd_survey_interaction(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_client_portal_dashboard_RM_details")]
        public object psp_dsp_client_portal_dashboard_RM_details(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_client_portal_dashboard_RM_details serializeProfile = JsonConvert.DeserializeObject<psp_dsp_client_portal_dashboard_RM_details>(data);

            return _locationLookup.psp_dsp_client_portal_dashboard_RM_details(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_client_portal_dashboard_client_list")]
        public object psp_dsp_client_portal_dashboard_client_list(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_client_portal_dashboard_client_list serializeProfile = JsonConvert.DeserializeObject<psp_dsp_client_portal_dashboard_client_list>(data);

            return _locationLookup.psp_dsp_client_portal_dashboard_client_list(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_client_portal_dashboard_asset_allocation")]
        public object psp_dsp_client_portal_dashboard_asset_allocation(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_client_portal_dashboard_asset_allocation serializeProfile = JsonConvert.DeserializeObject<psp_dsp_client_portal_dashboard_asset_allocation>(data);

            return _locationLookup.psp_dsp_client_portal_dashboard_asset_allocation(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_client_portal_dashboard_SIP_details")]
        public object psp_dsp_client_portal_dashboard_SIP_details(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_client_portal_dashboard_SIP_details serializeProfile = JsonConvert.DeserializeObject<psp_dsp_client_portal_dashboard_SIP_details>(data);

            return _locationLookup.psp_dsp_client_portal_dashboard_SIP_details(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_client_portal_dashboard_xirr_details")]
        public object psp_dsp_client_portal_dashboard_xirr_details(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_client_portal_dashboard_xirr_details serializeProfile = JsonConvert.DeserializeObject<psp_dsp_client_portal_dashboard_xirr_details>(data);

            return _locationLookup.psp_dsp_client_portal_dashboard_xirr_details(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_client_portal_dashboard_notification")]
        public object psp_dsp_client_portal_dashboard_notification(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_client_portal_dashboard_notification serializeProfile = JsonConvert.DeserializeObject<psp_dsp_client_portal_dashboard_notification>(data);

            return _locationLookup.psp_dsp_client_portal_dashboard_notification(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_current_holding_drill_down")]
        public object psp_dsp_current_holding_drill_down(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_current_holding_drill_down serializeProfile = JsonConvert.DeserializeObject<psp_dsp_current_holding_drill_down>(data);

            return _locationLookup.psp_dsp_current_holding_drill_down(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_searchable_family_list_client_portal")]
        public object psp_dsp_searchable_family_list_client_portal(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_searchable_family_list_client_portal serializeProfile = JsonConvert.DeserializeObject<psp_dsp_searchable_family_list_client_portal>(data);

            return _locationLookup.psp_dsp_searchable_family_list_client_portal(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_research_category")]
        public object psp_dsp_research_category(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_research_category serializeProfile = JsonConvert.DeserializeObject<psp_dsp_research_category>(data);

            return _locationLookup.psp_dsp_research_category(serializeProfile);
        }

        [HttpPost]
        [Route("psp_dsp_research_recommendation")]
        public object psp_dsp_research_recommendation(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            psp_dsp_research_recommendation serializeProfile = JsonConvert.DeserializeObject<psp_dsp_research_recommendation>(data);

            return _locationLookup.psp_dsp_research_recommendation(serializeProfile);
        }
        
    }
}
