using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Login
{
    [DataContract]
    public  class menu : dtoBase
    {
        [DataMember]
        public string id { get; set; }

        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string source_path { get; set; }

        [DataMember]
        public string parent_id { get; set; }
        [DataMember]
        public string order_number  { get; set; }

        [DataMember]
        public string Icon_path { get; set; }
        [DataMember]
        public string sp_name { get; set; }
        [DataMember]
        public string target { get; set; }

    }
}
