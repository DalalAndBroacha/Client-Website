using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Reports
{
    [DataContract]
    public class psp_rpt_cdsl_holding_report_familyhead
    {
        [DataMember]
        public string LoginId { get; set; }
        [DataMember]
        public string LoginHashId { get; set; }
        [DataMember]
        public string FamilyId { get; set; }
        [DataMember]
        public string AccessToken { get; set; }
        [DataMember]
        public string main_client_id { get; set; }
        [DataMember]
        public string as_on_date { get; set; }
        [DataMember]
        public string summary_flag { get; set; }

        [DataMember]
        public string client_id { get; set; }
        [DataMember]
        public string holding_as_on { get; set; }
        [DataMember]
        public string display_order { get; set; }

        [DataMember]
        public string family_name { get; set; }
        [DataMember]
        public string client_name { get; set; }
        [DataMember]
        public string bo_id { get; set; }
        [DataMember]
        public string ISIN { get; set; }
        [DataMember]
        public string ISIN_Name { get; set; }
        [DataMember]
        public string bo_code { get; set; }
        [DataMember]
        public string client_code { get; set; }
        [DataMember]
        public string scrip_code { get; set; }
        [DataMember]
        public float free { get; set; }
        [DataMember]
        public float current { get; set; }
        [DataMember]
        public float demat { get; set; }
        [DataMember]
        public float remat { get; set; }
        [DataMember]
        public float Freeze { get; set; }
        [DataMember]
        public float locked { get; set; }
        [DataMember]
        public float pledge { get; set; }
        [DataMember]
        public float cdsl_holding { get; set; }
        [DataMember]
        public string cdsl_holding_as_on { get; set; }
        [DataMember]
        public string rate_date { get; set; }
        [DataMember]
        public float rate { get; set; }

        [DataMember]
        public float valuation { get; set; }


    }
}












									