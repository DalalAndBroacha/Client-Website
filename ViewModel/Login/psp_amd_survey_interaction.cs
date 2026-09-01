using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Login
{
    [DataContract]
    public class psp_amd_survey_interaction
    {
		[DataMember]
		public string login_id { get; set; }
		[DataMember]
		public string rec_id { get; set; }
		[DataMember]
        public string survey_id { get; set; }
		[DataMember]
		public string family_id { get; set; }
		[DataMember]
		public string survey_date { get; set; }

		[DataMember]
		public string action { get; set; }
		[DataMember]
		public string status { get; set; }
		[DataMember]
		public string interact_date { get; set; }
		[DataMember]
		public string remarks { get; set; }

	}
}
