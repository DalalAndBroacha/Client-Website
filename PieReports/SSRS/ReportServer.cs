using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using PieReports.Encryption_Decryption;
using ReportingServiceReference;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.IO;

namespace PieReports.SSRS
{
    //internal class ReportingServicesEndpointBehavior : IEndpointBehavior
    //{
    //    public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters) { }

    //    public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
    //    {
    //        clientRuntime.ClientMessageInspectors.Add(new ReportingServicesExecutionInspector());
    //    }

    //    public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher) { }

    //    public void Validate(ServiceEndpoint endpoint) { }
    //}

    //internal class ReportingServicesExecutionInspector : IClientMessageInspector
    //{
    //    private MessageHeaders headers;

    //    public void AfterReceiveReply(ref Message reply, object correlationState)
    //    {
    //        var index = reply.Headers.FindHeader("ExecutionHeader", "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices");
    //        if (index >= 0 && headers == null)
    //        {
    //            headers = new MessageHeaders(MessageVersion.Soap11);
    //            headers.CopyHeaderFrom(reply, reply.Headers.FindHeader("ExecutionHeader", "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices"));
    //        }
    //    }

    //    public object BeforeSendRequest(ref Message request, IClientChannel channel)
    //    {
    //        if (headers != null)
    //            request.Headers.CopyHeadersFrom(headers);

    //        return Guid.NewGuid(); //https://msdn.microsoft.com/en-us/library/system.servicemodel.dispatcher.iclientmessageinspector.beforesendrequest(v=vs.110).aspx#Anchor_0
    //    }
    //}
    public class ReportServer
    {
        private IConfiguration _config;
        string reportingServicesUserName;
        string reportingServicesPassword;
        string reportingServicesDomain;
        string reportingServicesUrl;

        public ReportServer(IConfiguration config)
        {
            _config = config;
            reportingServicesUserName = EncryptionDecryption.Decrypt(_config.GetValue<string>("reportingServicesUserName"));
            reportingServicesPassword = EncryptionDecryption.Decrypt(_config.GetValue<string>("reportingServicesPassword"));
            reportingServicesDomain = EncryptionDecryption.Decrypt(_config.GetValue<string>("reportingServicesDomain"));
            reportingServicesUrl = EncryptionDecryption.Decrypt(_config.GetValue<string>("reportingServicesUrl"));
        }

        public async Task<byte[]> RenderReport(string report, string exportFormat = null)
        {
            //My binding setup, since ASP.NET Core apps don't use a web.config file
            var binding = new BasicHttpBinding();

            binding.AllowCookies = true;
            binding.Security.Mode = System.ServiceModel.BasicHttpSecurityMode.TransportCredentialOnly;
            binding.TransferMode = System.ServiceModel.TransferMode.Buffered;
            binding.MaxReceivedMessageSize = int.MaxValue;
            binding.MaxBufferSize = int.MaxValue;
            binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Windows;
            //binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;
            //binding.UseDefaultWebProxy = true;

            //Create the execution service SOAP Client
            var rsExec = new ReportExecutionServiceSoapClient(binding, new EndpointAddress(reportingServicesUrl));

            //Setup access credentials. I use windows credentials, yours may differ
            var clientCredentials = new NetworkCredential(reportingServicesUserName, reportingServicesPassword, reportingServicesDomain);
            rsExec.ClientCredentials.Windows.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Impersonation;
            //rsExec.ClientCredentials.Windows.ClientCredential = System.Net.CredentialCache.DefaultNetworkCredentials;
            rsExec.ClientCredentials.Windows.ClientCredential = clientCredentials;

            //binding.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;
            //binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Ntlm;

            //try
            //{
            //    rsExec.Endpoint.EndpointBehaviors.Add(new ReportingServicesEndpointBehavior());
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
            //This handles the problem of "Missing session identifier"


            //Load the report
            try
            {
                var taskLoadReport = await rsExec.LoadReportAsync(null, report, null);
            }
            catch (Exception e)
            {

                throw;
            }
            


            ReportingServiceReference.ParameterValue[] Parameters = new ReportingServiceReference.ParameterValue[2];

            Parameters[0] = new ReportingServiceReference.ParameterValue();
            Parameters[0].Name = "LoginId";
            Parameters[0].Value = "10";

            Parameters[1] = new ReportingServiceReference.ParameterValue();
            Parameters[1].Name = "as_on_date";
            Parameters[1].Value = "2023-08-15";

            //Set the parameteres asked for by the report

            await rsExec.SetExecutionParametersAsync(null, null, Parameters, "en-us");

            //run the report
            
            const string deviceInfo = @"<DeviceInfo><Toolbar>False</Toolbar></DeviceInfo>";
            var response = await rsExec.RenderAsync(new RenderRequest(null, null, exportFormat ?? "PDF", deviceInfo));
            
            
            

            //spit out the result
            return response.Result;
        }
    }



    
}
