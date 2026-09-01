using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Login
{
    [DataContract]
    public class Dashboard : dtoBase
    {
        [DataMember]
        public List<DashboardFinYears> dashboardFinYears { get; set; }

        [DataMember]
        public List<FamilyList> familyLists { get; set; }

        [DataMember]
        public List<LogOut> logout { get; set; }

        [DataMember]
        public List<menu> menus { get; set; }

        [DataMember]
        public List<FetchFevourite> Fetchfev { get; set; }

        //[DataMember]
        //public UpdateFevourite Updatefev { get; set; }

        [DataMember]
        public string user { get; set; }
        [DataMember]
        public bool LoginModalDisplay { get; set; }
        [DataMember]
        public string LoginModalMessage { get; set; }

    }
}
