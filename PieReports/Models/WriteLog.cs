using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PieReports.Models
{
    public static class WriteLog
    {
      //  private static IConfiguration _config;

        public static void WritewebLog(string strLog)
        {
            //StreamWriter log;
            //FileStream fileStream = null;
            //DirectoryInfo logDirInfo = null;
            //FileInfo logFileInfo;

            //string logFilePath = @"D:\ClientPortal\errorlog\";
            //logFilePath = logFilePath + "Log-" + System.DateTime.Today.ToString("MM-dd-yyyy") + "." + "txt";
            //logFileInfo = new FileInfo(logFilePath);
            //logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
            //if (!logDirInfo.Exists) logDirInfo.Create();
            //if (!logFileInfo.Exists)
            //{
            //    fileStream = logFileInfo.Create();
            //}
            //else
            //{
            //    fileStream = new FileStream(logFilePath, FileMode.Append);
            //}
            //log = new StreamWriter(fileStream);
            //log.WriteLine(DateTime.Now.ToString() + strLog);
            //log.Close();
        }
    }
}
