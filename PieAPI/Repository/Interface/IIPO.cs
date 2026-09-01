using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModel.IPO;

namespace PieAPI.Repository.Interface
{
    public interface IIPO
    {
        object PopulateIPOData(string serializeProfile);
        object psp_amd_ipo_requests(psp_amd_ipo_requests serializeProfile);
        object psp_dsp_ipo_clients_list(psp_dsp_ipo_clients_list serializeProfile);
        object psp_dsp_ipo_openissue(string data);
        object psp_verify_ipo_otp(psp_verify_ipo_otp serializeProfile);
    }
}
