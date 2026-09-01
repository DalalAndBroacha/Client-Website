using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Reports
{
    [DataContract]
    public class Period :dtoBase
    {
        [DataMember]
        public int period_typeValue { get; set; }
        [DataMember]
        public int Year_typevalue { get; set; }

        [DataMember]
        public string period_type { get; set; }
        [DataMember]
        public string Year_value_data { get; set; }

        [DataMember]
        public string year_value { get; set; }
        [DataMember]
        public string YEAR { get; set; }
        [DataMember]
        public string period_value_display { get; set; }
        [DataMember]
        public string period_value { get; set; }


    }
}
