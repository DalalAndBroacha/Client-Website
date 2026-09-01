using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Shared
{
    [DataContract]
    public class psp_amd_equity_research_reco
    {   
        [DataMember]
        public string Login_Id { get; set; }
        [DataMember]
        public int? rec_id { get; set; }
        [DataMember]
        public string repo_title { get; set; }
        [DataMember]
        public string category { get; set; }
        [DataMember]
        public string scrip_code { get; set; }
        [DataMember]
        public string recommendation { get; set; }
        [DataMember]
        public float? target_price { get; set; }
        [DataMember]
        public string url { get; set; }
        [DataMember]
        public string sql_msg { get; set; }
        [DataMember]
        public string flag { get; set; }
        [DataMember]
        public string dsp_web { get; set; }

        
    }

    public class research_category
    {
        public string category_id { get; set; }
        
        public string category_name { get; set; }
        
        public string stock_related_flag { get; set; }
    }
}
