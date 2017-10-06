using System;
using Domain;

namespace ThirdPartyData
{
	public interface IThirdPartyDataAccess
	{
		string GetThirdPartyDrugInfo(Drug drug);
		event EventHandler<DrugsRetrievedArgs> OnDrugsRetrievedEvent;
	}
}