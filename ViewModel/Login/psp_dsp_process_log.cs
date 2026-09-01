using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ViewModel.Login
{
    [DataContract]
    public class psp_dsp_process_log
    {
        [DataMember]
        public string log { get; set; }
        [DataMember]
        public string portfolio { get; set; }
        [DataMember]
        public DateTime portfolio_date { get; set; }

        public string strPortfolio_date { get { return this.portfolio_date.ToString("dd-MM-yyyy HH:mm:ss:fff"); } }
    }
}
