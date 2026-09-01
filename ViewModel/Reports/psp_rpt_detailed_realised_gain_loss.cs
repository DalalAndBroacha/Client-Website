using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ViewModel.Reports
{
    [DataContract]
    public class psp_rpt_detailed_realised_gain_loss
    {
        [DataMember]
        public string FINYR { get; set; }
        [DataMember]
        public string Client { get; set; }
        [DataMember]
        public string sub_category { get; set; }
        [DataMember]
        public string source { get; set; }

        [DataMember]
        public string client_code { get; set; }

        [DataMember]
        public string scrip_code { get; set; }
        [DataMember]
        public string client_name { get; set; }
        [DataMember]
        public string pan_no { get; set; }
        [DataMember]
        public string family_name { get; set; }
        [DataMember]
        public string Script_Name { get; set; }
        [DataMember]
        public string buy_trn_date { get; set; }
        [DataMember]
        public string sell_trn_date { get; set; }
        [DataMember]
        public float? buy_trn_qty { get; set; }
        [DataMember]
        public float? buy_trn_rate { get; set; }
        [DataMember]
        public float? sell_trn_rate { get; set; }
        [DataMember]
        public float? st_pnl { get; set; }
        [DataMember]
        public float? lt_pnl { get; set; }
        [DataMember]
        public float? int_pnl { get; set; }
        [DataMember]
        public string dsp_order { get; set; }
        [DataMember]
        public float? buy_amt { get; set; }
        [DataMember]
        public float? sell_amt { get; set; }
        [DataMember]
        public float? total_amt { get; set; }


    }

}
