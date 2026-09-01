using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Linq;

namespace ViewModel.Shared
{
    [DataContract]
    public class psp_dsp_equity_research_reports
    {
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public string Report_Title { get; set; }
        [DataMember]
        public string Category { get; set; }
        [DataMember]
        public string Sector { get; set; }
        [DataMember]
        public string Display_on_web { get; set; }
        [DataMember]
        public string BSE_Code { get; set; }
        [DataMember]
        public string NSE_symbol { get; set; }
        [DataMember]
        public string Scrip_code { get; set; }
        [DataMember]
        public string Recommendation { get; set; }
        [DataMember]
        public string CMP { get; set; }
        [DataMember]
        public string Reco_price { get; set; }
        [DataMember]
        public string Price_target { get; set; }
        [DataMember]
        public DateTime Latest_reco_date { get; set; }
        [DataMember]
        public string URL { get; set; }
        [DataMember]
        public string MobNotifyBtn { get; set; }

        [DataMember]
        public string amd_date { get; set; }
        [DataMember]
        public string amd_user { get; set; }

    }
}
