using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PieAPI.Encryption_Decryption;
using PieAPI.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModel;
using ViewModel.Login;
using ViewModel.Exports;
using ViewModel.Shared;

namespace PieAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExportsController : Controller
    {
        private readonly IExports _locationLookup;
        public ExportsController(IExports locationLookup)
        {
            _locationLookup = locationLookup;
        }

        [HttpPost]
        [Route("EmailReport")]
        public object EmailReport(EncryptData endData)
        {
            string data = EncryptionDecryption.Decrypt(endData.EncryptObject);
            psp_amd_send_report_client_portal serializeProfile = JsonConvert.DeserializeObject<psp_amd_send_report_client_portal>(data);


            return _locationLookup.psp_amd_send_report_client_portal (serializeProfile);
        }
        [HttpPost]
        [Route("GetFamilyID")]
        public object GetFamilyID(EncryptData endData)
        {
            string data = EncryptionDecryption.Decrypt(endData.EncryptObject);
            FamilyList serializeProfile = JsonConvert.DeserializeObject<FamilyList>(data);


            return _locationLookup.getFamId(serializeProfile);
        }
        [HttpPost]
        [Route("psp_dsp_client_portal_downloads_details")]
        public object psp_dsp_client_portal_downloads_details(EncryptData endData)
        {
            string data = EncryptionDecryption.Decrypt(endData.EncryptObject);
            psp_dsp_client_portal_downloads_details serializeProfile = JsonConvert.DeserializeObject<psp_dsp_client_portal_downloads_details>(data);


            return _locationLookup.psp_dsp_client_portal_downloads_details(serializeProfile);
        }
    
    }
}
