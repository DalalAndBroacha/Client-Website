using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ViewModel.Shared
{
    [DataContract]
    public class psp_dsp_mst_index_cp
    {
        [DataMember]
        public string from_date { get; set; }
        [DataMember]
        public string to_date { get; set; }
        [DataMember]
        public string flag { get; set; }
        [DataMember]
        public string rec_id { get; set; }
        [DataMember]
        public string Date { get; set; }
        [DataMember]
        public string master_id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public float Rate { get; set; }
    }
}

