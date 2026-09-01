using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Login
{
    [DataContract]
   public class DashboardFinYears :dtoBase
    {
        [DataMember]
        public string ID { get; set; }

        [DataMember]
        public string year { get; set; }
       
    }
}
