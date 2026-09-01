using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
namespace ViewModel.Reports
{
    [DataContract]
   public class SIPTerminating:dtoBase
    {
        [DataMember]
        public int loginid { get; set; }
        [DataMember]
        public string Login_Name { get; set; }
        [DataMember]
        public string main_client_name { get; set; }
        [DataMember]
        public string Scheme_Name { get; set; }
        [DataMember]
        public string Folio_no { get; set; }
        [DataMember]
        public DateTime? Start_Date { get; set; }
        [DataMember]
        public DateTime? End_Date { get; set; } 
        [DataMember]
        public double? Amount { get; set; }
        [DataMember]
        public string Frequency { get; set; }
        [DataMember]
        public string Tran_Type { get; set; }
        [DataMember]
        public string Target_scheme { get; set; }
        [DataMember]
        public string Mobile { get; set; }
        [DataMember]
        public string Email_Id { get; set; }
        [DataMember]
        public string SIP_no { get; set; }
        [DataMember]
        public DateTime? SIP_reg_dt { get; set; }
        [DataMember]
        public string Ac_No { get; set; }
        [DataMember]
        public string Bank_Name { get; set; }



    }
}
