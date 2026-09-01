using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.ClientComms
{
    [DataContract]
    public class psp_dsp_client_coms_history
    {
        [DataMember]
        public string client_code { get; set; }
        [DataMember]
        public string content_id { get; set; }
        [DataMember]
        public string prev_comunicated_on { get; set; }
        [DataMember]
        public string email_status { get; set; }
        [DataMember]
        public string sms_status { get; set; }
        [DataMember]
        public string Login_Name { get; set; }
    }
}
