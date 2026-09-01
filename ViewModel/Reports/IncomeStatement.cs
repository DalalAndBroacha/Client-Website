using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ViewModel.Reports
{
    [DataContract]
    public class IncomeStatement : dtoBase
    {
        //[DataMember]
        //public int login_id { get; set; }
        [DataMember]
        public int family_id { get; set; }
        [DataMember]
        public int year { get; set; } 
        [DataMember]
        public int rpt_period { get; set; }
        [DataMember]
        public int rpt_period_value { get; set; }
        [DataMember]
        public string login_source { get; set; }
        //[DataMember]
        //public string tokenId { get; set; }
        [DataMember]
        public string account_code { get; set; }
        [DataMember]
        public string client_id { get; set; }
        [DataMember]
        public string main_client_id { get; set; }
        [DataMember]
        public string client_name { get; set; }
        [DataMember]
        public string pan_number { get; set; }
        [DataMember]
        public string category { get; set; }
        [DataMember]
        public string sub_category { get; set; }
        [DataMember]
        public string sub_cat_code { get; set; }
        [DataMember]
        public float int_pnl { get; set; }
        [DataMember]
        public long st_pnl { get; set; }
        [DataMember]
        public long lt_pnl { get; set; }
        [DataMember]
        public long lt_pnl_gf { get; set; }
        [DataMember]
        public long total_pnl { get; set; }
        [DataMember]
        public long total_pnl_gf { get; set; }
        [DataMember]
        public long dividend { get; set; }
    }

}

