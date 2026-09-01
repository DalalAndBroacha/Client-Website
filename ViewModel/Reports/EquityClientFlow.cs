using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
namespace ViewModel.Reports
{
    [DataContract]
    //This Model is used for 2 Internal XIRR Reports
    public class EquityClientFlow:dtoBase
    {
       
        [DataMember]
        public string account_code { get; set; }
        [DataMember]
        public string flow_type { get; set; }
        
        [DataMember]
        public int summary { get; set; }
        [DataMember]
        public float inflow_amount { get; set; }
        [DataMember]
        public float outflow_amount { get; set; }
        [DataMember]
        public float amount { get; set; }
        [DataMember]
        public float current_value { get; set; }
        [DataMember]
        public float opening_stock { get; set; }
        [DataMember]
        public float opening_ledger { get; set; }
        [DataMember]
        public float closing_ledger { get; set; }
        [DataMember]
        public string client_name { get; set; }
        [DataMember]
        public string Login_Name  { get; set; }
        [DataMember]
        public string ac_open_date { get; set; }
        [DataMember]
        public DateTime trans_date { get; set; }
        [DataMember]
        public string remarks { get; set; }
    }
}
