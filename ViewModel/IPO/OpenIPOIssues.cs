using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.IPO
{
    [DataContract]
    public class OpenIPOIssues
    {        
        [DataMember]
        public string symbol { get; set; }
        [DataMember]
        public string jsonString { get; set; }
        [DataMember]
        public string response { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string isin { get; set; }
        [DataMember]
        public string category { get; set; }
        [DataMember]
        public string issuetype { get; set; }
        [DataMember]
        public DateTime opendatetime { get; set; }
        [DataMember]
        public DateTime closedatetime { get; set; }
        [DataMember]
        public decimal floorprice { get; set; }

        public string strFloorprice { get { return this.floorprice.ToString("#,##0.00"); } }

        [DataMember]
        public float ceilingprice { get; set; }

        public string strCeilingprice { get { return this.ceilingprice.ToString("#,##0.00"); } }

        [DataMember]
        public float cuttoff { get; set; }
        [DataMember]
        public float tickprice { get; set; }
        [DataMember]
        public float minbidqty { get; set; }

        public string strMinbidqty { get { return this.minbidqty.ToString("#,##0.00"); } }

        [DataMember]
        public string maxbidqty { get; set; }
        [DataMember]
        public string tradinglot { get; set; }
        [DataMember]
        public float minvalue { get; set; }
        public string strMinvalue { get { return this.minvalue.ToString("#,##0.00"); } }

        [DataMember]
        public float maxvalue { get; set; }

        public string strMaxvalue { get { return this.maxvalue.ToString("#,##0.00"); } }

        [DataMember]
        public string discounttype { get; set; }
        [DataMember]
        public string discountvalue { get; set; }
        [DataMember]
        public string asbanonasba { get; set; }
        [DataMember]
        public string tplusmodificationfrom { get; set; }
        [DataMember]
        public string tplusmodificationto { get; set; }
        [DataMember]
        public string errorcode { get; set; }
        [DataMember]
        public string message { get; set; }
        [DataMember]
        public string issuesize { get; set; }

        public string strOpenDateTime { get { return this.opendatetime.ToString("dd-MMM-yyyy HH:mm tt"); } }

        public string strCloseDateTime { get { return this.closedatetime.ToString("dd-MMM-yyyy HH:mm tt"); } }
    }


}
