using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Domain;
using ThirdPartyData;

namespace Services
{
	public class DrugService : IDrugService
	{
		private readonly IDataAccess _dataAccess;
		private readonly IThirdPartyDataAccess _thirdPartyDataAccess;

		public DrugService(IDataAccess dataAccess, IThirdPartyDataAccess thirdPartyDataAccess)
		{
			_dataAccess = dataAccess;
			_dataAccess.SpecificDrugRetrievedEvent += OnSpecificDrugsRetrieved;
			_thirdPartyDataAccess = thirdPartyDataAccess;
			_thirdPartyDataAccess.OnDrugsRetrievedEvent += onDrugsRetrieved;
		}

		public IEnumerable<int> DrugIds { get; set; }
		public bool HaveDrugsBeenReceived { get; set; }

		public IEnumerable<Drug> GetTheDrugs()
		{
			var allTheDrugs = _dataAccess.GetAllTheDrugs().ToList();

			foreach (var drug in allTheDrugs)
			{
				drug.ThirdPartyInfo = _thirdPartyDataAccess.GetThirdPartyDrugInfo(drug);
			}

			return allTheDrugs;
		}

		public IEnumerable<Drug> GetSpecificDrugs(IEnumerable<int> drugIds)
		{
			var allTheDrugs = _dataAccess.GetSpecificDrugs(drugIds);

			return allTheDrugs;
		}

		private void onDrugsRetrieved(object sender, DrugsRetrievedArgs args)
		{
			HaveDrugsBeenReceived = args.HaveDrugsBeenRetrieved;
		}

		public void OnSpecificDrugsRetrieved(object sender, SpecificDrugRetrievedArgs args)
		{
			DrugIds = args.DrugIds;
		}
	}
}
