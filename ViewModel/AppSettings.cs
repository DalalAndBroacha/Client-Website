using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel
{
    [DataContract]
   public class MintSettings
	{
        [DataMember]
        public string APIKey { get; set; }
		[DataMember]
		public string mintApiBaseUrl { get; set; }
		[DataMember]
		public string mintApiUser { get; set; }
		[DataMember]

		public string mintApiPassword { get; set; }
	}
}
