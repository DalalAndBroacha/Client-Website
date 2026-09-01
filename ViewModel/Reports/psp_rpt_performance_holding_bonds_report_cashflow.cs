using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
namespace ViewModel.Reports
{
    [DataContract]
  public class psp_rpt_performance_holding_bonds_report_cashflow: dtoBase
    {
        [DataMember]
        public int LoginId { get; set; }
        [DataMember]
        public string FamilyID { get; set; }
        [DataMember]
        public string ClientID { get; set; }
        [DataMember]
        public string FINYRData { get; set; }
        [DataMember]
        public string Subcategory { get; set; }
        [DataMember]
        public string family_id { get; set; }
        [DataMember]
        public string family_name   { get; set; }
        [DataMember]
        public string client_id { get; set; }
        [DataMember]
        public string client_name { get; set; }
        [DataMember]
        public string main_client_id { get; set; }
        [DataMember]
        public string main_client_name { get; set; }
        [DataMember]
        public string account_code { get; set; }
        [DataMember]
        public string asset_class { get; set; }
        [DataMember]
        public string asset_name { get; set; }
        [DataMember]
        public string pan_number { get; set; }
        [DataMember]
        public string scrip_code { get; set; }
        [DataMember]
        public string scrip_name { get; set; }
        [DataMember]
        public string category { get; set; }
        [DataMember]
        public string sub_category { get; set; }
       
        [DataMember]
        public float coupon { get; set; }
        [DataMember]
        public string interest_payment_date { get; set; }
        [DataMember]
        public string interest_payment_date_remarks { get; set; }
      
        [DataMember]
        public string maturity_date { get; set; }
        
        [DataMember]
        public string isin_no { get; set; }

        [DataMember]
        public string dividend_month { get; set; }

        [DataMember]
        public float value { get; set; }

    }
}
