using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.ClientComms
{
    [DataContract]
    public class psp_dsp_client_coms_dormant_account
    {
        [DataMember]
        public string loginid { get; set; }
        [DataMember]
        public string months { get; set; }
        [DataMember]
        public string trading_name { get; set; }
        [DataMember]
        public string trading_code { get; set; }
        [DataMember]
        public string pan_number { get; set; }
        [DataMember]
        public string mobile { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public string last_communication { get; set; }
        [DataMember]
        public DateTime last_traded_date { get; set; }
        [DataMember]
        public DateTime last_ReKYC_date { get; set; }
        [DataMember]
        public DateTime last_agreement_date { get; set; }

        [DataMember]
        public string dsp_last_traded_date {
            get
            {
                DateTime naValue;
                DateTime.TryParseExact("1900-01-01", "yyyy-MM-dd", new CultureInfo("en-US"), 
                       DateTimeStyles.None, out naValue);
                
                if (naValue == this.last_traded_date)
                { 
                    return "NA";
                }
                else
                {
                    return this.last_traded_date.ToString("dd-MM-yyyy");
                }
            }
        }
        [DataMember]
        public string dsp_last_ReKYC_date
        {
            get
            {
                DateTime naValue;
                DateTime.TryParseExact("1900-01-01", "yyyy-MM-dd", new CultureInfo("en-US"),
                       DateTimeStyles.None, out naValue);

                if (naValue == this.last_ReKYC_date)
                {
                    return "NA";
                }
                else
                {
                    return this.last_ReKYC_date.ToString("dd-MM-yyyy");
                }
            }
        }
        [DataMember]
        public string dsp_last_agreement_date
        {
            get
            {
                DateTime naValue;
                DateTime.TryParseExact("1900-01-01", "yyyy-MM-dd", new CultureInfo("en-US"),
                       DateTimeStyles.None, out naValue);

                if (naValue == this.last_agreement_date)
                {
                    return "NA";
                }
                else
                {
                    return this.last_agreement_date.ToString("dd-MM-yyyy");
                }
            }
        }
        [DataMember]
        public string bse_status { get; set; }
        [DataMember]
        public string client_category { get; set; }
        [DataMember]
        public string nse_status { get; set; }
        [DataMember]
        public string status { get; set; }
    }
}
