using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ViewModel.Reports
{
    [DataContract]
    public class Equitydealertracksheet:dtoBase
    {
      
        [DataMember]
        public string yearID { get; set; }
        [DataMember]
        public int Fin_yr { get; set; }
        [DataMember]
        public float show_value { get; set; }
        [DataMember]
        public float greater_than { get; set; }
        [DataMember]
        public float less_than { get; set; }
        [DataMember]
        public string account_code { get; set; }
        [DataMember]
        public string client_name { get; set; }
        [DataMember]
        public float op_valuation_amt { get; set; }
        [DataMember]
        public float inflow_amt { get; set; }
        [DataMember]
        public float outflow_amt { get; set; }
        [DataMember]
        public float cl_ledger { get; set; }
        [DataMember]
        public float cl_valuation_amt { get; set; }
        [DataMember]
        public float Liquid_bees_amt { get; set; }
        [DataMember]
        public float xirr_ret_yr     { get; set; }
        [DataMember]
        public float nifty_ret_yr { get; set; }
        [DataMember]
        public float abs_ret { get; set; }
        [DataMember]
        public float xirr_ret { get; set; }
        [DataMember]
        public float nifty_ret { get; set; }
        [DataMember]
        public float stk_percent { get; set; }
        [DataMember]
        public float focused_amt { get; set; }
        [DataMember]
        public float focused_percent { get; set; }
        [DataMember]
        public float cy_brkg { get; set; }
        [DataMember]
        public float pfy_brkg_1 { get; set; }
        [DataMember]
        public float pfy_brkg_2 { get; set; }
        [DataMember]
        public float ledstocktotal { get; set; }
    }
}
