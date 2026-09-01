using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ViewModel.Shared
{
    [DataContract]
    public class psp_amd_index_currency_master_cp
    {
        [DataMember]
        public string login_id { get; set; }
        [DataMember]
        public string rec_id { get; set; }
        [DataMember]
        public string mod_flag { get; set; }
        [DataMember]
        public string table_id { get; set; }
        [DataMember]
        public string rate { get; set; }
        [DataMember]
        public string tr_date { get; set; }
        [DataMember]
        public string master_id { get; set; }
        [DataMember]
        public string sql_msg { get; set; }
        [DataMember]
        public string sql_response { get; set; }
    }
}
