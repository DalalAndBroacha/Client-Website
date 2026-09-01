using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DalalReportingService.ReportExecution;
using DalalReportingService.ReportService;
using System.Web;
using System.Data;
using System.IO;

namespace ReportingServiceAPI
{
    public class SSRS
    {
        public static string Inprocess;

        public static string CurrentStage;
        public static int data_count { get; private set; }
        public static int report_count { get; private set; }
        public static string report_server_user { get; private set; }
        public static string report_server_passwod { get; private set; }
        public static string smtp_email { get; private set; }
        public static string smtp_email_password { get; private set; }
        public static string smtp_host { get; private set; }
        public static int smtp_port { get; private set; }
        public static int data_fetch_retry_timer { get; private set; }
        public static int smtp_retry_timer { get; private set; }
        public static string email_display_name { get; private set; }
        public static string exportpath { get; private set; }
        public static string downloadedFilepath { get; private set; }
        public static string logpath { get; private set; }

        public static string Errorlogpath { get; private set; }

        //reportvalue 
        public static string report_trn_id { get; private set; }
        public static string report_name { get; private set; }
        public static string ssrs_report_path { get; private set; }
        public static string export_file_name { get; private set; }
        public static string report_to { get; private set; }
        public static string report_cc { get; private set; }
        public static string report_export_format { get; private set; }
        public static string report_subject { get; private set; }
        public static string report_body { get; private set; }
        public static string message_type { get; private set; }
        public static string profile_id { get; private set; }

        public static string Req_Id { get; private set; }
        public static string Trans_Id { get; private set; }

        public static string connectionSql { get; private set; }
        public static void CreateFile(string fileName)
        {
            if (string.IsNullOrEmpty(report_name) && string.IsNullOrEmpty(ssrs_report_path) && string.IsNullOrEmpty(export_file_name))
            {
                fileName = "";
            }
            else
            {
                CurrentStage = "Creating file";
                ReportingService2005 rs;
                ReportExecutionService rsExec;
                rs = new ReportingService2005();
                rsExec = new ReportExecutionService();
                //rs.Credentials = new NetworkCredential(report_server_user, report_server_passwod);
                //rsExec.Credentials = new NetworkCredential(report_server_user, report_server_passwod);
                //rs.Url = "http://192.168.0.121/reportserver/reportservice2005.asmx

                byte[] result = null;

                string reportPath = ssrs_report_path;
                string format = report_export_format;
                string historyID = null;
                string devInfo = @"<DeviceInfo><Toolbar>False</Toolbar></DeviceInfo>";

                //Add reports parameters
                DataTable ReportParameterData = GetReportParameter();


                DalalReportingService.ReportExecution.ParameterValue[] parameters = new DalalReportingService.ReportExecution.ParameterValue[ReportParameterData.Rows.Count];

                for (int i = 0; i < ReportParameterData.Rows.Count; i++)
                {
                    parameters[i] = new DalalReportingService.ReportExecution.ParameterValue();
                    parameters[i].Name = ReportParameterData.Rows[i]["parameter_name"].ToString();
                    parameters[i].Value = ReportParameterData.Rows[i]["parameter_value"].ToString();
                }
                //WriteToFile("Parameter Values Set");

                DalalReportingService.ReportExecution.DataSourceCredentials[] credentials = null;
                string showHideToggle = null;
                string encoding;
                string mimeType;
                string extension;
                DalalReportingService.ReportExecution.Warning[] warnings = null;
                DalalReportingService.ReportExecution.ParameterValue[] reportHistoryParameters = null;
                string[] streamIDs = null;

                ExecutionHeader execHeader = new ExecutionHeader();
                rsExec.ExecutionHeaderValue = execHeader;
                ExecutionInfo execInfo;
                try
                {
                    var x = rs.ListChildren("/", false);
                    //WriteToFile(x.ToString());
                    execInfo = rsExec.LoadReport(x[0].Path + reportPath, historyID);
                   //WriteToFile(x[0].Path);
                    //WriteToFile(reportPath);
                    //WriteToFile(historyID);
                    //WriteToFile("Connection established");
                }
                catch (Exception e)
                {
                    //SendStatus("E", "Connection failed" + e);
                    //WriteToErrorFile("Connection failed. " + e);
                    Inprocess = "N";
                }

                try
                {
                    rsExec.SetExecutionParameters(parameters, "en-us");
                    //WriteToFile("Parameter set");
                }
                catch (Exception e)
                {
                    //SendStatus("E", "Failed to set parameter" + e);
                    //WriteToErrorFile("Failed to set parameter" + e);
                    Inprocess = "N";
                }
                String SessionId = rsExec.ExecutionHeaderValue.ExecutionID;

                try
                {
                    result = rsExec.Render(format, devInfo, out extension, out encoding, out mimeType, out warnings, out streamIDs);
                    execInfo = rsExec.GetExecutionInfo();
                    //WriteToFile("Report rendered ");
                }
                catch (Exception e)
                {
                    //WriteToErrorFile("Fail to render File " + e);
                    //SendStatus("E", "Fail to render File " + e);
                    Inprocess = "N";
                }

                try
                {
                    if (File.Exists(fileName) && message_type == "E")
                    {
                        DeleteFile(fileName);
                    }

                    FileStream stream = File.Create(fileName, result.Length);
                    stream.Write(result, 0, result.Length);
                    //WriteToFile("fileName " + fileName);
                    //WriteToFile("File created");
                    stream.Close();
                }
                catch (Exception e)
                {
                    //SendStatus("E", "Failed to create file " + e);
                    //WriteToErrorFile("Failed to create file. Report Id is " + report_trn_id + e);
                    Inprocess = "N";
                }

            }
        }

        private static void DeleteFile(string fileName)
        {
            File.Delete(filename);
        }
    }
}
