using System.Net.Mail;
using System;
using System.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using ViewModel.ClientComms;
using PieReports.Models;
using System.Net.Sockets;


namespace PieAPI
{
    public static class Smtp
    {
        public static void SendEmail(SmtpSettings smtpSettings, SmtpSendEmail sseObj)
        {
            try
            {
                SmtpClient smtp = new SmtpClient()
                {
                    Host = smtpSettings.smtpHost,
                    Port = smtpSettings.smtpPort,
                    Credentials = new NetworkCredential(smtpSettings.smtpUid, smtpSettings.smtpPass),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    EnableSsl = true
                };

                //MailAddress to = new MailAddress(sseObj.emailTo);
                MailAddress to = new MailAddress("devang.doshi@dalal-broacha.com");
                MailAddress from = new MailAddress("client.communications@dalal-broacha.com", sseObj.emailDisplayName);

                MailMessage email = new MailMessage(from, to)
                {
                    IsBodyHtml = true
                };

                email.CC.Add("client.communications@dalal-broacha.com");
                email.CC.Add(sseObj.emailFrom);

                email.Subject = sseObj.emailSubject;
                email.Body = sseObj.emailBody;

                email.Headers.Add("X-APIHEADER", String.Format("<{0}>", sseObj.emailGuid));
                email.ReplyToList.Add(sseObj.emailFrom);

                if (sseObj.attachPath != null)
                {

                    foreach (string item in sseObj.attachPath)
                    {
                        if (item != "")
                        {
                            System.Net.Mime.ContentType contentType = new System.Net.Mime.ContentType();
                            contentType.MediaType = System.Net.Mime.MediaTypeNames.Application.Octet;
                            email.Attachments.Add(new Attachment(item, contentType));
                        }
                    }

                }

                try
                {
                    smtp.Send(email);
                }
                catch (Exception ex)
                {
                    WriteLog.WritewebLog("Exception while Mailing");
                    WriteLog.WritewebLog(ex.ToString());

                    throw;
                }
            }
            catch (Exception ex)
            {
                WriteLog.WritewebLog("Exception while Mailing");
                WriteLog.WritewebLog(ex.ToString());

                throw;
            }
        }

    }
}
