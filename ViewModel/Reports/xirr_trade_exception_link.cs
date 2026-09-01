using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Reports
{
    [DataContract]
    public class xirr_trade_exception_link: dtoBase
    {
        [DataMember]
        public string account_code { get; set; }
        [DataMember]
        public string client_code { get; set; }
        [DataMember]
        public string remarks { get; set; }
    }
}
