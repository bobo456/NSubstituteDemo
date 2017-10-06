using System.Collections.Generic;
using Data;
using Domain;

namespace Services
{
	public interface IDrugService
	{
		IEnumerable<Drug> GetTheDrugs();
		void OnSpecificDrugsRetrieved(object sender, SpecificDrugRetrievedArgs args);
		IEnumerable<int> DrugIds { get; set; }
	}
}