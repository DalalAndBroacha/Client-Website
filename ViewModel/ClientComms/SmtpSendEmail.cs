using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.ClientComms
{
    public class SmtpSendEmail
    {
        public string emailTo { get; set; }
        public string emailFrom { get; set; }
        public string emailDisplayName { get; set; }
        public string replyToList { get; set; }

        public string emailBody { get; set; }
        public string emailSubject { get; set; }
        public string hasAttach { get; set; }

        public List<string> attachPath { get; set; }

        public string emailGuid { get; set; }
        public string smsTID { get; set; }
        public string smsREQID { get; set; }
        public string sql_msg { get; set; }

        
    }
}
