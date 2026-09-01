using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel
{
    [DataContract]
    public  class dtoBase
    {
        
        [DataMember]
        public string Login_id { get; set; }

        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        public string Login_client_name { get; set; }
        [DataMember]
        public string Profile_Image { get; set; }
        [DataMember]
        public string Role_Name { get; set; }
        [DataMember]
        public string access_token { get; set; }
        [DataMember]
        public string password_type { get; set; }

        [DataMember]
        public string type { get; set; } 
        [DataMember]
        public string token_no { get; set; }

        [DataMember]
        public string web_session_id { get; set; }
        [DataMember]
        public string Username { get; set; }
        [DataMember]
        public string row_guid { get; set; }
    } 
}
