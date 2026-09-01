using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ViewModel.Reports
{
    [DataContract]
    public class Dividend:dtoBase
    {
        [DataMember]
        public int loginid { get; set; }
        [DataMember]
        public int family_id { get; set; }
        [DataMember]
        public int finyr { get; set; }
        [DataMember]
        public string tokenId { get; set; }
        [DataMember]
        public string login_source { get; set; }
        [DataMember]
        public string family_name { get; set; }
        [DataMember]
        public string branch_name { get; set; }
        [DataMember]
        public string rm_id { get; set; }
        [DataMember]
        public string rm_name { get; set; }
        [DataMember]
        public string main_client_id { get; set; }
        [DataMember]
        public string account_code { get; set; }
        [DataMember]
        public string client_name { get; set; }
        [DataMember]
        public string category { get; set; }
        [DataMember]
        public string sub_category { get; set; }
        [DataMember]
        public string sub_cat_code { get; set; }
        [DataMember]
        public string scrip_name { get; set; }
        [DataMember]
        public DateTime dividend_date { get; set; }
        [DataMember]
        public float value { get; set; }
        [DataMember]
        public string ISIN { get; set; }


    }
}
