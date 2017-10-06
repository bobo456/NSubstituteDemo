using System;

namespace Domain
{
	public class Drug
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string NDC { get; set; }
		public string Form { get; set; }
		public string Route { get; set; }
		public int Quantity { get; set; }
		public string UnitType { get; set; }
		public string ThirdPartyInfo { get; set; }

		public static Drug GetFakeDrug()
		{
			var rando = new Random(72).Next();
			return new Drug
			{
				Id = rando,
				NDC = new Random().Next(11111111, 99999999).ToString(),
				Name = "Drug" + rando,
				Form = "Tablet" + rando,
				Route = "Oral",
				Quantity = rando,
				UnitType = "ML"
			};
		}
	}
}
