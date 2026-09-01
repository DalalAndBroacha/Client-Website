using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.Shared
{
	public class mintCreds
	{
        public string authName { get; set; }
		public string password { get; set; }
	}

	public class mintToken
	{
		public int status { get; set; }
		public string message { get; set; }
		public mintAuthResult result { get; set; }
	}
	public class mintAuthResult
	{
		public string token { get; set; }
	}
}
