using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ViewModel.Shared
{
    public class psp_amd_equity_kyc_capture_update_request
    {
        [DataMember]
        public string OTP1_Token { get; set; }
        [DataMember]
        public string email_update { get; set; }
		[DataMember]
		public string email_address { get; set; }
		[DataMember]
		public string email_relation { get; set; }
		[DataMember]
		public string mobile_update { get; set; }
		[DataMember]
		public string mobile_number { get; set; }
		[DataMember]
		public string mobile_relation { get; set; }
		[DataMember]
		public string income_update { get; set; }
		[DataMember]
		public string income_range { get; set; }
		[DataMember]
		public string networth { get; set; }
		[DataMember]
		public string networth_date { get; set; }
		[DataMember]
		public string token_no { get; set; }
		[DataMember]
		public string sql_message { get; set; }
		[DataMember]
		public string sql_status { get; set; }
		


	}
}
