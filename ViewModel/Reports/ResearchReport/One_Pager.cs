using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Reports.ResearchReport
{
    [DataContract]
    public class One_Pager
    {
        [DataMember]
        public string Login_id { get; set; }
        [DataMember]
        public string Repo_name { get; set; }
        [DataMember]
        public string Repo_title { get; set; }
        [DataMember]
        public string Repo_subtitle { get; set; }
        [DataMember]
        public string Reco_price { get; set; }
        [DataMember]
        public string target_price { get; set; }
        [DataMember]
        public string Analyst    { get; set; }

        [DataMember]
        public string jsonObjSec { get; set; }
        [DataMember]
        public string jsonObjSubSec { get; set; }

    }
}
