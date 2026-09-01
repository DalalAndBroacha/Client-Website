using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.MINT
{
	public class InvestwellToken
	{
		public int status { get; set; }
		public string message { get; set; }
		public InvestwellResult result { get; set; }
	}
	public class InvestwellResult
	{
		public string token { get; set; }
	}
}
