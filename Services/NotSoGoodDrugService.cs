using System.Collections.Generic;
using Data;
using Domain;
using ThirdPartyData;

namespace Services
{
	public class NotSoGoodDrugService
	{
		public IEnumerable<Drug> GetTheDrugsBadly()
		{
			var dataAccess = new DataAccess();
			var drugs = dataAccess.GetAllTheDrugs();

			var thirdPartyDataAccess = new ThirdPartyDataAccess();

			foreach (var drug in drugs)
			{
				drug.ThirdPartyInfo = thirdPartyDataAccess.GetThirdPartyDrugInfo(drug);
			}

			return drugs;
		}
	}
}
