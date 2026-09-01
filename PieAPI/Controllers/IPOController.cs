using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PieAPI.Encryption_Decryption;
using PieAPI.Repository.Interface;
using ViewModel.IPO;
using ViewModel.Login;

namespace PieAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IPOController : ControllerBase
    {
        private readonly IIPO _locationLookup;

        public IPOController(IIPO locationLookup)
        {
            _locationLookup = locationLookup;
        }

        [HttpPost]
        [Route("PopulateIPOData")]
        public object PopulateIPOData(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            //OpenIPOIssues serializeProfile = JsonConvert.DeserializeObject<OpenIPOIssues>(data);

            return _locationLookup.PopulateIPOData(data);
        }

        [HttpPost]
        [Route("psp_dsp_ipo_openissue")]
        public object psp_dsp_ipo_openissue(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);
            //OpenIPOIssues serializeProfile = JsonConvert.DeserializeObject<OpenIPOIssues>(data);

            return _locationLookup.psp_dsp_ipo_openissue(data);
        }
        [HttpPost]
        [Route("psp_dsp_ipo_clients_list")]
        public object psp_dsp_ipo_clients_list(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);

            psp_dsp_ipo_clients_list serializeProfile = JsonConvert.DeserializeObject<psp_dsp_ipo_clients_list>(data);

            return _locationLookup.psp_dsp_ipo_clients_list(serializeProfile);
        }
        [HttpPost]
        [Route("psp_amd_ipo_requests")]
        public object psp_amd_ipo_requests(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);

            psp_amd_ipo_requests serializeProfile = JsonConvert.DeserializeObject<psp_amd_ipo_requests>(data);

            return _locationLookup.psp_amd_ipo_requests(serializeProfile);
        }
        [HttpPost]
        [Route("psp_verify_ipo_otp")]
        public object psp_verify_ipo_otp(EncryptData endlogin)
        {
            string data = EncryptionDecryption.Decrypt(endlogin.EncryptObject);

            psp_verify_ipo_otp serializeProfile = JsonConvert.DeserializeObject<psp_verify_ipo_otp>(data);

            return _locationLookup.psp_verify_ipo_otp(serializeProfile);
        }
    }
}
