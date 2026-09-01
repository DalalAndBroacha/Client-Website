using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.ClientComms
{
    public class smsApiContent
    {
        public string app_code { get; set; }
        public string mobile { get; set; }
        public string text { get; set; }
        public string token { get; set; }
        public string XAPIHeader { get; set; }

    }
}
