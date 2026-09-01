using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ViewModel.Login
{
    [DataContract]
    public class UpdateFevourite:dtoBase
    {
        [DataMember]
        public string userid { get; set; }
        [DataMember]
        public string module_id { get; set; }

        [DataMember]
        public int moduleid { get; set; }


        [DataMember]
        public string flag { get; set; }
        [DataMember]
        public string sql_message { get; set; }
        [DataMember]
        public string sql_status { get; set; }

    }
}
