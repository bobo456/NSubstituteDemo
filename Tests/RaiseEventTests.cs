using System;
using Data;
using Moq;
using NSubstitute;
using Services;
using Shouldly;
using ThirdPartyData;
using Xunit;
using Rhino.Mocks;
using MockRepository = Rhino.Mocks.MockRepository;

namespace Tests
{
	public class RaiseEventTests
	{
		[Fact]
		public void Rhino_onDrugRetrieved_WhenEventRaised_HaveDrugsBeenReceivedIsSet()
		{
			// Arrange
			var mockDataAccess = MockRepository.GenerateMock<IDataAccess>();
			var mockThirdPartyAccess = MockRepository.GenerateMock<IThirdPartyDataAccess>();

			var drugService = new DrugService(mockDataAccess, mockThirdPartyAccess);
			drugService.HaveDrugsBeenReceived.ShouldBeFalse();

			// Act
			// Assert
			mockThirdPartyAccess.Raise(tpa => tpa.OnDrugsRetrievedEvent += null, mockThirdPartyAccess, new DrugsRetrievedArgs { HaveDrugsBeenRetrieved = true });
			drugService.HaveDrugsBeenReceived.ShouldBeTrue();
		}

		[Fact]
		public void Moq_onDrugRetrieved_WhenEventRaised_HaveDrugsBeenReceivedIsSet()
		{
			// Arrange
			var mockDataAccess = new Mock<IDataAccess>();
			var mockThirdPartyAccess = new Mock<IThirdPartyDataAccess>(MockBehavior.Strict);

			var drugService = new DrugService(mockDataAccess.Object, mockThirdPartyAccess.Object);
			drugService.HaveDrugsBeenReceived.ShouldBeFalse();

			// Act
			// Assert
			mockThirdPartyAccess.Raise(tpa => tpa.OnDrugsRetrievedEvent += null, mockThirdPartyAccess.Object, new DrugsRetrievedArgs { HaveDrugsBeenRetrieved = true });
			drugService.HaveDrugsBeenReceived.ShouldBeTrue();
		}

		[Fact]
		public void NSubstitute_onDrugRetrieved_WhenEventRaised_HaveDrugsBeenReceivedIsSet()
		{
			// Arrange
			var mockDataAccess = Substitute.For<IDataAccess>();
			var mockThirdPartyAccess = Substitute.For<IThirdPartyDataAccess>();

			var drugService = new DrugService(mockDataAccess, mockThirdPartyAccess);
			drugService.HaveDrugsBeenReceived.ShouldBeFalse();

			// Act
			// Assert
			mockThirdPartyAccess.OnDrugsRetrievedEvent += Raise.Event<EventHandler<DrugsRetrievedArgs>>(mockThirdPartyAccess, new DrugsRetrievedArgs { HaveDrugsBeenRetrieved = true });
			drugService.HaveDrugsBeenReceived.ShouldBeTrue();
		}
	}
}
