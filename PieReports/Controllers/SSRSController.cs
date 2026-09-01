using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Reporting.WebForms;
using Microsoft.Reporting.WebForms.Internal.Soap.ReportingServices2005.Execution;
using PTUtility.Interfaces.Data;
using System;
using System.Net;
using System.Security.Principal;

namespace PieReports.Controllers
{
    public class SSRSController : Controller
    {

        private IConfiguration _config;

        public SSRSController(IConfiguration config)
        {
            _config = config;
        }
        public IActionResult SSRSReport()
        {
            SSRSReportsr();
            return View();
        }
        public void SSRSReportsr()
        {
            string userId = "Administrator";
            string passWord = "DbsBpvtltd@1961";
            string domain = "MFSERVER";
            try
            {
                string navId = Convert.ToString(183);
                string loginId = Convert.ToString(272970);
               //  IQueryProcess result = (new PI.Business.Report()).ssrsDashBoardReport(0);
                 string url = "psp_dsp_dw_mst_asset_wise_script_search";
                //ScriptManager.GetCurrent(Page).ScriptMode = ScriptMode.Release;
                Microsoft.Reporting.WebForms.ReportParameter[] param = new Microsoft.Reporting.WebForms.ReportParameter[1];
                param[0] = new Microsoft.Reporting.WebForms.ReportParameter("LoginId", loginId);
                IReportServerCredentials irsc = new CustomReportCredentials2(userId, passWord, domain);
                ReportViewer rptViewerDashboard = new ReportViewer();
                rptViewerDashboard.ProcessingMode = ProcessingMode.Remote;
               
                rptViewerDashboard.ServerReport.ReportServerUrl = new Uri("http://192.168.0.250/ReportServer");
                rptViewerDashboard.ServerReport.ReportPath =url ;
                rptViewerDashboard.ShowParameterPrompts = true;
                rptViewerDashboard.ShowPromptAreaButton = true;
                rptViewerDashboard.ServerReport.SetParameters(param);
                rptViewerDashboard.ServerReport.Refresh();
                //rptViewerDashboard.Visible = true;
                rptViewerDashboard.ShowToolBar = true;
            }
            catch
            {
                throw;
            }
            
        }

        //public void GetPDF()
        //{
        //    ReportingService2005 rs;
        //    ReportExecutionService rsExec;
        //    rs = new ReportingService2005();
        //    rsExec = new ReportExecutionService();
        //    rs.Credentials = new NetworkCredential(report_server_user, report_server_passwod);
        //    rsExec.Credentials = new NetworkCredential(report_server_user, report_server_passwod);
        //}




public class CustomReportCredentials2 : IReportServerCredentials
        {
            private string _userName;
            private string _passWord;
            private string _domainName;

            public CustomReportCredentials2(string userName, string passWord, string domainName)
            {
                _userName = "Administrator";
                _passWord = "DbsBpvtltd@1961";
                _domainName = "MFSERVER";
            }

            public WindowsIdentity ImpersonationUser
            {
                get
                {
                    // Use default identity.
                    return null;
                }
            }

            public ICredentials NetworkCredentials
            {
                get
                {
                    // Use default identity.
                    return new NetworkCredential(_userName, _passWord, _domainName);
                }
            }

            public bool GetFormsCredentials(out Cookie authCookie, out string user, out string password, out string authority)
            {
                authCookie = null;
                user = password = authority = null;
                return false;
            }
        }
    }
}
