using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PieReports.SSRS;

namespace PieReports.Controllers
{
    public class SSRS_API : Controller
    {
        private IConfiguration _config;

        public SSRS_API(IConfiguration config)
        {
            _config = config;
        }

        public async void MISBrokerage()
        {
            var unused = await IndexAsync();
        }

        public async Task<IActionResult> IndexAsync()
        {

            ReportServer rS = new ReportServer(_config);
            byte[] result = await rS.RenderReport("/Daily Equity Brokerage MIS");

            System.IO.File.WriteAllBytes("hello.pdf", result);

            //stream.Write(result, 0, result.Length);
            //stream.Close();
            return File(result, "pdf");
        }
    }
}
