using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
namespace ViewModel.Login
{
    [DataContract]
   public class ChangeUsername:dtoBase
    {
      [DataMember]
        public string new_username { get; set; }
        [DataMember]
        public string sql_message { get; set; }
        [DataMember]
        public string LoginId { get; set; }

        [DataMember]
        public string Flag { get; set; }

    }
}
