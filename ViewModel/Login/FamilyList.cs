using System.Runtime.Serialization;

namespace ViewModel.Login
{
    [DataContract]
    public class FamilyList :dtoBase 
    {
        [DataMember]
        public string Family_Id { get; set; }

        [DataMember]
        public string Family_Name { get; set; }
        [DataMember]
        public string Family_Token { get; set; }

    }
}
