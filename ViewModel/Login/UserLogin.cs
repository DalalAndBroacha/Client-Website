using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ViewModel.Login
{

    [DataContract]
    public class UserLogin : dtoBase
    {
        [DataMember]
        public int? UserId { get; set; }
        [DataMember]
        public string Username { get; set; }
        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public string Login_name { get; set; }
        [DataMember]
        public string ConfirmPassword { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public System.DateTime? CreatedDate { get; set; }
        [DataMember]
        public string msg { get; set; }
        [DataMember]
        public int login_status { get; set; }
        [DataMember]
        public string type { get; set; }
        [DataMember]
        public string login_source { get; set; }
        [DataMember]
        public string geo_location { get; set; }
        [DataMember]
        public string browser_name { get; set; }
        [DataMember]
        public string browser_version { get; set; }
        [DataMember]
        public string opertation_system { get; set; }
        [DataMember]
        public string device_info { get; set; }
        [DataMember]
        public string otp { get; set; }
        [DataMember]
        public string otp_token { get; set; }
        [Required]
        [StringLength(4)]
        public string CaptchaCode { get; set; }
        [DataMember]
        public bool LoginModalDisplay { get; set; }
        [DataMember]
        public string LoginModalMessage { get; set; }
    }
}
