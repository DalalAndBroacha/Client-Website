using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ViewModel.Reports
{
    [DataContract]
    public class psp_dsp_performance_holding_bonds_report_cp
    {
        [DataMember]
        public int LoginId { get; set; }
        [DataMember]
        public string Family { get; set; }
        [DataMember]
        public string Client { get; set; }
        [DataMember]
        public int FINYR { get; set; }
        [DataMember]
        public string Subcategory { get; set; }
        [DataMember]
        public string main_client_name { get; set; }
        [DataMember]
        public string scrip_code { get; set; }
        [DataMember]
        public string scrip_name { get; set; }
        [DataMember]
        public string category { get; set; }
        [DataMember]
        public string sub_category { get; set; }
        [DataMember]
        public string dsp_cat { get; set; }
        [DataMember]
        public string display_order { get; set; }
        [DataMember]
        public string security_name { get; set; }
        [DataMember]
        public string security_code { get; set; }
        [DataMember]
        public string rating { get; set; }
        [DataMember]
        public float? coupon { get; set; }
        [DataMember]
        public string interest_payment_date { get; set; }
        [DataMember]
        public string interest_payment_date_remarks { get; set; }
        [DataMember]
        public string call_date { get; set; }
        [DataMember]
        public string put_date { get; set; }
        [DataMember]
        public string maturity_date { get; set; }
        [DataMember]
        public string maturity_remark { get; set; }
        [DataMember]
        public string date_of_purchase { get; set; }
        [DataMember]

        public float? cdsl_quantity { get; set; }
        [DataMember]
        public float? quantity { get; set; }
        [DataMember]
        public float? face_value { get; set; }
        [DataMember]
        public float? total_holding { get; set; }
        [DataMember]
        public float? purchase_price { get; set; }
        [DataMember]
        public float? principal_value { get; set; }
        [DataMember]
        public string accrued_interest { get; set; }
        [DataMember]
        public float? total_considertion { get; set; }
        [DataMember]
        public string interest_recieved { get; set; }
        [DataMember]
        public float? cmp { get; set; }
        [DataMember]
        public float? principle_market_value { get; set; }
        [DataMember]
        public string first_int_date { get; set; }
        [DataMember]
        public string issue_date { get; set; }
        [DataMember]
        public string isin_no { get; set; }
        [DataMember]
        public string final_amount { get; set; }
        [DataMember]
        public string accrued_interest_from_last_ip { get; set; }
        [DataMember]
        public float? total_market_value { get; set; }
        [DataMember]
        public string srt_unreal_profit { get; set; }
        [DataMember]
        public string long_unreal_profit { get; set; }
        [DataMember]
        public string srt_real_profit { get; set; }
        [DataMember]
        public string long_real_profit { get; set; }
        [DataMember]
        public string ttlshort_term { get; set; }
        [DataMember]
        public string ttllong_term { get; set; }
        [DataMember]
        public string return_abs { get; set; }
        [DataMember]
        public string return_xirr { get; set; }
        [DataMember]
        public string bench_absolute { get; set; }
        [DataMember]
        public string bench_xirr { get; set; }
        [DataMember]
        public string price_updated_on { get; set; }
        [DataMember]
        public string net_return_abs { get; set; }
        [DataMember]
        public string net_return_xirr { get; set; }
        [DataMember]
        public string match { get; set; }
    }
}
