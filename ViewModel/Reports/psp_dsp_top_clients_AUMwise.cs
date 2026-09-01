using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Reports
{
    public class psp_dsp_top_clients_AUMwise
    {
        [DataMember]
        public string from_rec { get; set; }
        [DataMember]
        public string to_rec { get; set; }
        [DataMember]
        public string login_id { get; set; }
        [DataMember]
        public string rm_id { get; set; }
        [DataMember]
        public string client_name { get; set; }
        [DataMember]
        public string account_code { get; set; }
        [DataMember]
        public decimal holding_value { get; set; }
        [DataMember]
        public DateTime last_trade_date { get; set; }
        [DataMember]
        public string dsp_last_trade_date
        {
            get
            {
                DateTime naValue;
                DateTime.TryParseExact("1900-01-01", "yyyy-MM-dd", new CultureInfo("en-US"),
                       DateTimeStyles.None, out naValue);

                if (naValue == this.last_trade_date)
                {
                    return "NA";
                }
                else
                {
                    return this.last_trade_date.ToString("dd-MM-yyyy");
                }
            }
        }
        [DataMember]
        public string branch { get; set; }
        [DataMember]
        public string RM { get; set; }
    }
}

