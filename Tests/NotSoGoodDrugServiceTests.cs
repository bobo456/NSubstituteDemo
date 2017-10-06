using System.Linq;
using Services;
using Xunit;
using Shouldly;

namespace Tests
{
	public class NotSoGoodDrugServiceTests
	{
		[Fact]
		public void GetTheDrugsBadly_WhenCalled_ReturnsDrugs()
		{
			// Arrange
			var notSoGoodDrugService = new NotSoGoodDrugService();

			// Act
			var actualDrugs = notSoGoodDrugService.GetTheDrugsBadly();

			// Assert
			actualDrugs.Count().ShouldBe(3);
		}
	}
}
