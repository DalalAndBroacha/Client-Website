using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Login
{
    [DataContract]
  public class LogOut:dtoBase
    {
        [DataMember]
        public string sql_message { get; set; }
            }
}
