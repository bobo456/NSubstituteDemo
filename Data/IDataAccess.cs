using System;
using System.Collections.Generic;
using Domain;

namespace Data
{
	public interface IDataAccess
	{
		IEnumerable<Drug> GetAllTheDrugs();
		IEnumerable<Drug> GetSpecificDrugs(IEnumerable<int> drugIds);
		event EventHandler<SpecificDrugRetrievedArgs> SpecificDrugRetrievedEvent;

	}
}