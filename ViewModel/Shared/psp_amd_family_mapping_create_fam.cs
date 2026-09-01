using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;
using System.Web;

namespace ViewModel.Shared
{
    [DataContract]
    public class psp_amd_family_mapping_create_fam
    {
        [DataMember]
        public string login_id { get; set; }
        [DataMember]
        public string pan { get; set; }
        [DataMember]
        public string pass { get; set; }
        [DataMember]
        public string pass_en { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Please enter Name.")]
        public string family_name { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Please enter Mobile.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Invalid Mobile")]
        public string mobile { get; set; }
        [DataMember]
        [EmailAddress(ErrorMessage = "Invalid Email.")]
        [Required(ErrorMessage = "Please enter Email Id.")]
        public string email_id { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Please enter Branch.")]
        public string branch { get; set; }
        [DataMember]
        [Required(ErrorMessage = "RM list cannot be empty.")]
        public string rm_list { get; set; }
        [DataMember]
        public string sql_msg { get; set; }
        [DataMember]
        public string sql_status { get; set; }
    }
}