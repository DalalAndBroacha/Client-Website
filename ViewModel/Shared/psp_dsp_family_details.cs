using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Shared
{
    [DataContract]
    public class psp_dsp_family_details
    {
        [DataMember]
        public string LoginId { get; set; }
        [DataMember]
        public string family_user_id { get; set; }
        [DataMember]
        public string family_code { get; set; }
        [DataMember]
        public string family_name { get; set; }
        [DataMember]
        public string Contact_No { get; set; }
        [DataMember]
        public string Email_Id { get; set; }
        [DataMember]
        public string Branch_Name { get; set; }
    }
}


 				
