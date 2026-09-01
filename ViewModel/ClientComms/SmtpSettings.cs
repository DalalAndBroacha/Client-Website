using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.ClientComms
{
    public class SmtpSettings
    {
        public string smtpHost { get; set; }
        public string smtpUid { get; set; }
        public string smtpPass { get; set; }
        public int smtpPort { get; set; }

    }
}
