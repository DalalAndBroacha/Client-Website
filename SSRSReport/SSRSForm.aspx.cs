using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Extensions.Configuration;
using Microsoft.Reporting.WebForms;
using Microsoft.Reporting.WebForms.Internal.Soap.ReportingServices2005.Execution;
using PTUtility.Interfaces.Data;
using System.Net;
using System.Security.Principal;

namespace SSRSReport
{
    public partial class SSRSForm : System.Web.UI.Page
    {

        //string userId = "Administrator";
        //string passWord = "DbsBpvtltd@1961";
        //string domain = "MFSERVER";
        string userId = System.Configuration.ConfigurationManager.AppSettings["ReportViewerUser"];
        string passWord = System.Configuration.ConfigurationManager.AppSettings["ReportViewerPassword"];
        string domain = System.Configuration.ConfigurationManager.AppSettings["ReportViewerDomain"];

        protected void Page_Load(object sender, EventArgs e)
        {
            _bindReportData();
        }

        private void _bindReportData()
        { 
              
            try
            {
                string navId = Convert.ToString(146);
                string loginId = Convert.ToString(279359);
               // IQueryProcess result = (new Report()).ssrsReport(navId);
                string url = "/Pie Reports/Client Contact Details";
                ScriptManager.GetCurrent(Page).ScriptMode = ScriptMode.Release;
                Microsoft.Reporting.WebForms.ReportParameter[] param = new Microsoft.Reporting.WebForms.ReportParameter[1];
                param[0] = new Microsoft.Reporting.WebForms.ReportParameter("LoginId", loginId);
                IReportServerCredentials irsc = new CustomReportCredentials2(userId, passWord, domain);
              //  ReportViewer rptViewer = new ReportViewer();
                rptViewer.ProcessingMode = ProcessingMode.Remote;
                rptViewer.ServerReport.ReportServerUrl = new Uri(System.Configuration.ConfigurationManager.AppSettings["ReportServerUrl"]);
                rptViewer.ServerReport.ReportPath = url;
                rptViewer.ShowParameterPrompts = true;
                rptViewer.ShowPromptAreaButton = true;
                rptViewer.ServerReport.SetParameters(param);
                rptViewer.ServerReport.Refresh();
                rptViewer.Visible = true;
                rptViewer.ShowToolBar = true;
            }
            catch
            {
                throw;
            }
        }

    }

    public class CustomReportCredentials2 : Microsoft.Reporting.WebForms.IReportServerCredentials
    {
        private string _userName;
        private string _passWord;
        private string _domainName;

        public CustomReportCredentials2(string userName, string passWord, string domainName)
        {
            _userName = System.Configuration.ConfigurationManager.AppSettings["ReportViewerUser"];
            _passWord = System.Configuration.ConfigurationManager.AppSettings["ReportViewerPassword"];
            _domainName = System.Configuration.ConfigurationManager.AppSettings["ReportViewerDomain"];
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