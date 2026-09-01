using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
namespace ViewModel.Reports
{
    [DataContract]
   public class psp_rpt_performance_holding_report:dtoBase
    {
       
        [DataMember]
        public string Family { get; set; }
        [DataMember]
        public string FINYR { get; set; }
        [DataMember]
        public string family_name { get; set; }
        [DataMember]
        public string main_client_id { get; set; }
        [DataMember]
        public string main_client_name { get; set; }
        [DataMember]
        public string main_client_name_header { get; set; }
        [DataMember]
        public string pan_number { get; set; }
        [DataMember]
        public string account_code { get; set; }
        [DataMember]
        public string client_id { get; set; }
        [DataMember]
        public string category { get; set; }
        [DataMember]
        public string sub_category { get; set; }
        [DataMember]
        public string sub_category_display { get; set; }
        [DataMember]
        public string sub_category_display_order { get; set; }
        [DataMember]
        public float contribution { get; set; }
        [DataMember]
        public float hld_cost { get; set; }
        [DataMember]
        public float Market_Value { get; set; }
        [DataMember]
        public float holding_per { get; set; }
        [DataMember]
        public float dividend { get; set; }
        [DataMember]
        public float srt_unreal_profit { get; set; }
        [DataMember]
        public float long_unreal_profit { get; set; }
        [DataMember]
        public float srt_real_profit { get; set; }
        [DataMember]
        public float long_real_profit { get; set; }
        [DataMember]
        public float srt_ttl_gain { get; set; }
        [DataMember]
        public float lng_ttl_gain { get; set; }
        [DataMember]
        public float return_abs { get; set; }
        [DataMember]
        public float return_xirr { get; set; }
        [DataMember]
        public float net_return_abs { get; set; }
        [DataMember]
        public float net_return_xirr { get; set; }
       

    }
}
    