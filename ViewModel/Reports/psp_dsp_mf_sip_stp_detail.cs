using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ViewModel.Reports
{
    [DataContract]
    public class psp_dsp_mf_sip_stp_detail : dtoBase
    {
        //[DataMember]
        //public int loginid { get; set; }
        
        [DataMember]
        public string FamilyID { get; set; }
        [DataMember]
        public string MainClientID { get; set; }
        [DataMember]
        public string FINYRData { get; set; }
        [DataMember]
        public string Subcategory { get; set; }
        [DataMember]
        public int family_id { get; set; }
        [DataMember]
        public int main_client_id { get; set; }
        [DataMember]
        public string tran_type { get; set; }
        [DataMember]
        public string scheme_category { get; set; }
        [DataMember]
        public int fin_year { get; set; }
        [DataMember]
        public string Source_Scheme_Name { get; set; }
        [DataMember]
        public string Folio_no { get; set; }
        [DataMember]
        public DateTime Start_Date { get; set; }
        [DataMember]
        public DateTime End_Date { get; set; }
        [DataMember]
        public float Amount { get; set; }
        [DataMember]
        public string Frequency { get; set; }
        [DataMember]
        public string Target_Scheme_Name { get; set; }
        
        [DataMember]
        public int Total_Installments { get; set; }
        
        [DataMember]
        public int Pending_Installments { get; set; }
        
        [DataMember]
        public float Pending_Amount { get; set; }
        [DataMember]
        public string Terminating { get; set; }
        [DataMember]
        public string display_order { get; set; }
    }
}
