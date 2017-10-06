using System.Collections.Generic;
using System.Linq;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Data;
using Domain;
using Moq;
using NSubstitute;
using Rhino.Mocks;
using Rhino.Mocks.Constraints;
using Services;
using Shouldly;
using ThirdPartyData;
using Xunit;
using MockRepository = Rhino.Mocks.MockRepository;
using Arg = NSubstitute.Arg;

namespace Tests
{
	public class ListenToEvents
	{
		private readonly List<int> _drugIds;
		private readonly DataAccess _dataAccess;

		public ListenToEvents()
		{
			_dataAccess = new DataAccess();
			_drugIds = new List<int> { 1, 5, 7 };
		}

		[Fact]
		public void Rhino_onSpecificDrugsRetrieved_WhenEventRaised_DrugServiceListens()
		{
			// Arrange
			var mockDrugService = MockRepository.GenerateMock<IDrugService>();
			_dataAccess.SpecificDrugRetrievedEvent += mockDrugService.OnSpecificDrugsRetrieved;

			// Act
			_dataAccess.GetSpecificDrugs(_drugIds);

			// Assert
			mockDrugService.AssertWasCalled(ds => ds.OnSpecificDrugsRetrieved(Arg<object>.Is.Equal(_dataAccess), Arg<SpecificDrugRetrievedArgs>.Matches(a => Equals(a.DrugIds, _drugIds))));
		}

		[Fact]
		public void Moq_onSpecificDrugsRetrieved_WhenEventRaised_DrugServiceListens()
		{
			// Arrange
			var mockDrugService = new Mock<IDrugService>();
			_dataAccess.SpecificDrugRetrievedEvent += mockDrugService.Object.OnSpecificDrugsRetrieved;

			// Act
			_dataAccess.GetSpecificDrugs(_drugIds);

			// Assert
			mockDrugService.Verify(ds => ds.OnSpecificDrugsRetrieved(_dataAccess, It.Is<SpecificDrugRetrievedArgs>(a => Equals(a.DrugIds, _drugIds))));
		}

		[Fact]
		public void NSubstitute_onSpecificDrugsRetrieved_WhenEventRaised_DrugServiceListens()
		{
			// Arrange
			var mockDrugService = Substitute.For<IDrugService>();
			_dataAccess.SpecificDrugRetrievedEvent += mockDrugService.OnSpecificDrugsRetrieved;

			// Act
			_dataAccess.GetSpecificDrugs(_drugIds);

			// Assert
			mockDrugService.Received().OnSpecificDrugsRetrieved(_dataAccess, Arg.Is<SpecificDrugRetrievedArgs>(a => Equals(a.DrugIds, _drugIds)));
		}
	}
}
