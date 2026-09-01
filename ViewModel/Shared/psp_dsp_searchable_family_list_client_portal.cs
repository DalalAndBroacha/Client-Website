using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ViewModel.Shared
{
    [DataContract]
    public class psp_dsp_searchable_family_list_client_portal
    {
        [DataMember]
        public string loginId { get; set; }
        [DataMember]
        public string display_flag { get; set; }
        [DataMember]
        public string search_term { get; set; }
        [DataMember]
        public string Family_Id { get; set; }
        [DataMember]
        public string Family_Name { get; set; }
        [DataMember]
        public string Family_Token { get; set; }

    }
}
