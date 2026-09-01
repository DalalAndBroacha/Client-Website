using System.Runtime.Serialization;

namespace ViewModel.Login
{
    [DataContract] 
    public class FetchFevourite :dtoBase
    {
        //[DataMember]
        //public string userid { get; set; }
        //[DataMember]
        //public string module_id { get; set; }
        //[DataMember]
        //public string flag { get; set; }
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string source_path { get; set; }

    }
}
