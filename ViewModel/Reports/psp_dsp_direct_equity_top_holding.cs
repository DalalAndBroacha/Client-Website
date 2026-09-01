using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Reports
{
    public class psp_dsp_direct_equity_top_holding
    {
        [DataMember]
        public string login_id { get; set; }
        [DataMember]
        public string top_count { get; set; }
        [DataMember]
        public string scrip_industry { get; set; }
        [DataMember]
        public string Branch_Name { get; set; }
        [DataMember]
        public string scrip_name { get; set; }
        [DataMember]
        public string scrip_code { get; set; }
        [DataMember]
        public string Top_Picks { get; set; }
        [DataMember]
        public int Total_Client { get; set; }
        [DataMember]
        public int client_count { get; set; }
        [DataMember]
        public decimal holding_qty { get; set; }
        [DataMember]
        public decimal holding_value { get; set; }
        [DataMember]
        public decimal minimum_return_abs { get; set; }
        [DataMember]
        public decimal maximum_return_abs { get; set; }
        [DataMember]
        public decimal avg_return_abs { get; set; }
    }
}




 									
