using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.ClientComms
{
    public class psp_amd_equity_settlement_upload
    {
        public string login_id { get; set; }
        public string jsonString { get; set; }
        public string file_name { get; set; }
        public string file_ogName { get; set; }
        public string file_date { get; set; }

        public string file_guid { get; set; }
        public string sms_content { get; set; }
        public string mobile { get; set; }
        public string comms_guid { get; set; }
    }
}