using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Domain;

namespace Data
{
    public class DataAccess : IDataAccess
	{
	    public IEnumerable<Drug> GetAllTheDrugs()
	    {
			// Simulate DB Access Times
			Thread.Sleep(2000);

			return new List<Drug>
			{
				Drug.GetFakeDrug(),
				Drug.GetFakeDrug(),
				Drug.GetFakeDrug()
			};
	    }

		public IEnumerable<Drug> GetSpecificDrugs(IEnumerable<int> drugIds)
		{
			if (drugIds.Any(di => di > 100))
				throw new ArgumentException(nameof(drugIds));

			var drugs = new List<Drug>
			{
				Drug.GetFakeDrug(),
				Drug.GetFakeDrug(),
				Drug.GetFakeDrug()
			};

			SpecificDrugRetrievedEvent?.Invoke(this, new SpecificDrugRetrievedArgs {DrugIds = drugIds});

			return drugs.Where(d => drugIds.Any(drugId => drugId == d.Id));
		}

		public event EventHandler<SpecificDrugRetrievedArgs> SpecificDrugRetrievedEvent;
	}

	public class SpecificDrugRetrievedArgs
	{
		public IEnumerable<int> DrugIds { get; set; }
	}
}
