using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModel.Login;
using ViewModel.Exports;

namespace PieAPI.Repository.Interface
{
    public interface IExports
    {
        object psp_amd_send_report_client_portal(psp_amd_send_report_client_portal pasrCP);

        object getFamId(FamilyList pasrCP);

        object psp_dsp_client_portal_downloads_details(psp_dsp_client_portal_downloads_details pdcpdd);
        
    }
}
