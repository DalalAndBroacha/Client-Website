using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Reports
{
    [DataContract]
    public class psp_dsp_client_portfolio_interaction_history
    {
        [DataMember]
        public string login_id { get; set; }
        [DataMember]
        public string main_client_id { get; set; }
        [DataMember]
        public string Login_Name { get; set; }
        [DataMember]
        public string remarks { get; set; }
        [DataMember]
        public string review_date { get; set; }
    }
}


		
