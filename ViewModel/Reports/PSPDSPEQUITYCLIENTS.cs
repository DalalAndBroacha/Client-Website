using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ViewModel.Reports
{
    [DataContract]
   public class PSPDSPEQUITYCLIENTS
    {
        [DataMember]
        public string LoginId { get; set; }
        [DataMember]
        public string clientcode { get; set; }
       
        [DataMember]
        public string account_code { get; set; }
        [DataMember]
        public string client_name { get; set; }

    }
}
