using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace PieReports.Models
{
    public static class WriteLog
    {
      //  private static IConfiguration _config;

        public static void WritewebLog(string strLog)
        {
            StreamWriter log;
            FileStream fileStream = null;
            DirectoryInfo logDirInfo = null;
            FileInfo logFileInfo;

            string logFilePath = @"D:\cpapi\errorlog\";
            logFilePath = logFilePath + "Log-" + System.DateTime.Today.ToString("MM-dd-yyyy") + "." + "txt";
            logFileInfo = new FileInfo(logFilePath);
            logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
            if (!logDirInfo.Exists) logDirInfo.Create();
            if (!logFileInfo.Exists)
            {
                fileStream = logFileInfo.Create();
            }
            else
            {
                fileStream = new FileStream(logFilePath, FileMode.Append);
            }
            log = new StreamWriter(fileStream);
            log.WriteLine( DateTime.Now.ToString() + " - " + strLog);
            log.Close();
        }
    }

public class HeaderReader
    {
        public static async Task ReadHeadersExample()
        {
            using HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync("https://www.example.com");

            // Reading a specific header from HttpResponseMessage.Headers
            if (response.Headers.TryGetValues("Server", out IEnumerable<string> serverHeaderValues))
            {
                string serverName = serverHeaderValues.First();
                Console.WriteLine($"Server: {serverName}");
            }
            else
            {
                Console.WriteLine("Server header not found in response headers.");
            }

            // Reading a specific header from HttpResponseMessage.Content.Headers
            if (response.Content.Headers.TryGetValues("Content-Type", out IEnumerable<string> contentTypeValues))
            {
                string contentType = contentTypeValues.First();
                Console.WriteLine($"Content-Type: {contentType}");
            }
            else
            {
                Console.WriteLine("Content-Type header not found in content headers.");
            }

            // Combining and iterating through all headers
            Console.WriteLine("\nAll Headers:");
            var allHeaders = response.Headers.Concat(response.Content.Headers);
            foreach (var header in allHeaders)
            {
                Console.WriteLine($"{header.Key}: {string.Join(", ", header.Value)}");
            }
        }
    }


}
