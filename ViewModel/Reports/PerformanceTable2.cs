using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
namespace ViewModel.Reports
{
    [DataContract]
    public class PerformanceTable2 : dtoBase
    {

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
        public string sub_category { get; set; }
        [DataMember]
        public string contribution { get; set; }
        [DataMember]
        public string hld_cost { get; set; }
        [DataMember]
        public string Market_Value { get; set; }
        [DataMember]
        public string dividend { get; set; }
        [DataMember]
        public string srt_unreal_profit { get; set; }
        [DataMember]
        public string long_unreal_profit { get; set; }
        [DataMember]
        public string srt_real_profit { get; set; }
        [DataMember]
        public string long_real_profit { get; set; }
        [DataMember]
        public string srt_ttl_gain { get; set; }
        [DataMember]
        public string lng_ttl_gain { get; set; }
        [DataMember]
        public string return_abs { get; set; }
        [DataMember]
        public string return_xirr { get; set; }
        [DataMember]
        public string net_return_abs { get; set; }
        [DataMember]
        public string net_return_xirr { get; set; }


    }
}
