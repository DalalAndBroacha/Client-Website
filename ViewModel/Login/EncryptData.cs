using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Login
{
    [DataContract]
    public  class EncryptData
    {
        [DataMember]
        public string EncryptObject { get; set; }
        [DataMember]
        public string DecryptObject { get; set; }
    }
}
