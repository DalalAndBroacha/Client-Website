using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
namespace ViewModel.Reports
{
    [DataContract]
    public class psp_dsp_mf_category_allocation: dtoBase
    {
        //[DataMember]
        //public int login_id { get; set; }
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
        public string fund_type { get; set; }
        [DataMember]
        public int fin_year { get; set; }
        [DataMember]
        public string Category_Name { get; set; }
        [DataMember]
        public float purchase_cost { get; set; }
        [DataMember]
        public float current_value { get; set; }
        [DataMember]
        public float current_allocation_percent { get; set; }
        [DataMember]
        public float ABS_percent { get; set; }
        [DataMember]
        public float XIRR_percent { get; set; }
        [DataMember]
        public float future_inflow { get; set; }
        [DataMember]
        public float future_outflow { get; set; }
        [DataMember]
        public float future_allocation_percent { get; set; }
        [DataMember]
        public string Order_Id { get; set; }
    }
}
