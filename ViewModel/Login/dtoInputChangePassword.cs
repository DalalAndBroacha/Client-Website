using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Login
{
    [DataContract]
    public class dtoInputChangePassword :dtoBase
    {
        [DataMember]
        public string OldPassword { get; set; } 
        [DataMember]
        public string NewPassword { get; set; }
        [DataMember]
        public string ConfirmPassword { get; set; }
        [DataMember]
        public string msg { get; set; }

        [DataMember]
        public string sql_message { get; set; }

        [DataMember]
        public string Flag { get; set; }
    }
}
