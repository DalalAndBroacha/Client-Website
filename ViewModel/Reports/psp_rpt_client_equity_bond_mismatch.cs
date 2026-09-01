using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Reports
{
    [DataContract]
    public class psp_rpt_client_equity_bond_mismatch //Used for both psp_rpt_client_script_mismatch & psp_rpt_client_bond_mismatch
    {
        [DataMember]
        public string LoginId { get; set; }
        [DataMember]
        public string Branch { get; set; }
        [DataMember]
        public string RM { get; set; }
        [DataMember]
        public string family_id { get; set; }
        [DataMember]
        public string client_code { get; set; }
        [DataMember]
        public string client_name { get; set; }
        [DataMember]
        public string family_name { get; set; }
        [DataMember]
        public string rm_name { get; set; }
        [DataMember]
        public string family_branch_name { get; set; }
        [DataMember]
        public string scrip_name { get; set; }
        [DataMember]
        public float? dp_holding_qty { get; set; }
        [DataMember]
        public float? holding_qty { get; set; }
        [DataMember]
        public float? latest_buy { get; set; }
        [DataMember]
        public float? latest_sell { get; set; }
        [DataMember]
        public string Remarks { get; set; }

         
    }
}
