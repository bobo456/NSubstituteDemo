using System;
using System.Threading;
using Domain;

namespace ThirdPartyData
{
    public class ThirdPartyDataAccess : IThirdPartyDataAccess
	{
		public string GetThirdPartyDrugInfo(Drug drug)
		{
			// Simulate Third party Data Access time
			Thread.Sleep(2000);

			return "Extra Info";
		}

		public event EventHandler<DrugsRetrievedArgs> OnDrugsRetrievedEvent;
	}

	public class DrugsRetrievedArgs
	{
		public bool HaveDrugsBeenRetrieved { get; set; }
	}
}
