using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json.Linq;

namespace ViewModel.Reports
{
    public class psp_dsp_client_communication_log
    {
        [DataMember]
        public string login_id { get; set; }
        [DataMember]
        public string from_date { get; set; }
        [DataMember]
        public string to_date { get; set; }
        [DataMember]
        public DateTime sent_date { get; set; }
        [DataMember]
        public string dsp_sent_date { get; set; }

        [DataMember]
        public string client_name { get; set; }
        [DataMember]
        public string account_code { get; set; }
        [DataMember]
        public string sender { get; set; }
        [DataMember]
        public string content_name { get; set; }
        [DataMember]
        public string status { get; set; }
        [DataMember]
        public string comm_type { get; set; }
        [DataMember]
        public string SortDate
        {
            get
            {
                if (sent_date == null)
                {
                    return "NA";
                }
                else
                {
                    return sent_date.ToString("yyyyMMddHHmmss");
                }
            }
        }
    }
}


//long.Parse(date.ToString("yyyyMMddHHmmss"));