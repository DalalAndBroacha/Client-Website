using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Shared
{
    [DataContract]
    public class psp_amd_profile_details
    {
        [DataMember]
        public string Login_Id { get; set; }
        [DataMember]
        public string family_name { get; set; }
        [DataMember]
        public string contact_No { get; set; }
        [DataMember]
        [EmailAddress(ErrorMessage = "Please enter valid E-Mail Address.")]
        public string email_Id { get; set; }
        [DataMember]
        public string nickname { get; set; }
        [DataMember]
        public string passFlag { get; set; }
        [DataMember]
        public string newPassword { get; set; }
        [DataMember]
        public string sql_msg { get; set; }
        [DataMember]
        public string sql_status { get; set; }
    }
}
