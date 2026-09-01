using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.ClientComms
{
    [DataContract]
    public class psp_dsp_client_coms_dormant_mail_content
    {
        [DataMember]
        public string login_id { get; set; }
        [DataMember]
        public string content_id { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string code { get; set; }
        [DataMember]
        public string client_category { get; set; }
        [DataMember]
        public string mobile { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public string email_body { get; set; }
        [DataMember]
        public string sms_content { get; set; }        
        [DataMember]
        public string from_email { get; set; }
        [DataMember]
        public string email_subject { get; set; }
        [DataMember]
        public string email_display_name { get; set; }
        [DataMember]
        public string replytoList { get; set; }
        [DataMember]
        public string attachments_path { get; set; }
        [DataMember]
        public string has_attachments { get; set; }
        [DataMember]
        public string email_guid { get; set; }
        [DataMember]
        public string sql_msg { get; set; }

    }
}