using System.Collections.Generic;
using System.Linq;
using Data;
using Domain;
using Moq;
using NSubstitute;
using Rhino.Mocks;
using Services;
using Shouldly;
using ThirdPartyData;
using Xunit;
using Arg = NSubstitute.Arg;
using MockRepository = Rhino.Mocks.MockRepository;

namespace Tests
{
    public class CommonInteractionTests
    {
	    private readonly Drug _fakeDrug1;
	    private readonly Drug _fakeDrug2;
	    private readonly Drug _fakeDrug3;
	    private readonly List<Drug> _fakeDrugList;

	    private const string FakeThirdPartyInfo1 = "Best Drug";
	    private const string FakeThirdPartyInfo2 = "Worst Drug";
	    private const string FakeThirdPartyInfo3 = "Just OK Drug";

		public CommonInteractionTests()
	    {
		    _fakeDrug1 = Drug.GetFakeDrug();
		    _fakeDrug2 = Drug.GetFakeDrug();
		    _fakeDrug3 = Drug.GetFakeDrug();
		    _fakeDrugList = new List<Drug> { _fakeDrug1, _fakeDrug2, _fakeDrug3 };
		}

	    [Fact]
		public void Rhino_GetTheDrugs()
	    {
			// Arrange
			var mockDataAccess = MockRepository.GenerateMock<IDataAccess>();
			mockDataAccess.Expect(da => da.GetAllTheDrugs()).Return(_fakeDrugList);

			var mockThirdPartyAccess = MockRepository.GenerateMock<IThirdPartyDataAccess>();
			mockThirdPartyAccess.Expect(tpa => tpa.GetThirdPartyDrugInfo(_fakeDrug1)).Return(FakeThirdPartyInfo1);
			mockThirdPartyAccess.Expect(tpa => tpa.GetThirdPartyDrugInfo(_fakeDrug2)).Return(FakeThirdPartyInfo2);
			mockThirdPartyAccess.Expect(tpa => tpa.GetThirdPartyDrugInfo(_fakeDrug3)).Return(FakeThirdPartyInfo3);

			var drugService = new DrugService(mockDataAccess, mockThirdPartyAccess);

			// Act
			var actualDrugs = drugService.GetTheDrugs();

			// Assert
			actualDrugs.Count().ShouldBe(3);

			mockDataAccess.AssertWasCalled(da => da.GetAllTheDrugs(), options => options.Repeat.Times(1));
			mockThirdPartyAccess.AssertWasCalled(da => da.GetThirdPartyDrugInfo(Arg<Drug>.Is.Anything), options => options.Repeat.Times(3));
		}

	    [Fact]
		public void Moq_GetTheDrugs()
	    {
			// Arrange
		    var mockDataAccess = new Mock<IDataAccess>();
			mockDataAccess.Setup(da => da.GetAllTheDrugs()).Returns(_fakeDrugList);

		    var mockThirdPartyAccess = new Mock<IThirdPartyDataAccess>(MockBehavior.Strict);
		    mockThirdPartyAccess.SetupSequence(tpa => tpa.GetThirdPartyDrugInfo(It.IsAny<Drug>()))
									.Returns(FakeThirdPartyInfo1)
									.Returns(FakeThirdPartyInfo2)
									.Returns(FakeThirdPartyInfo3);
			
			var drugService = new DrugService(mockDataAccess.Object, mockThirdPartyAccess.Object);

			// Act
			var actualDrugs = drugService.GetTheDrugs();

			// Assert
			actualDrugs.Count().ShouldBe(3);

			mockDataAccess.Verify(da => da.GetAllTheDrugs(), Times.Once);
			mockThirdPartyAccess.Verify(da => da.GetThirdPartyDrugInfo(It.IsAny<Drug>()), Times.Exactly(3));
		}

	    [Fact]
		public void NSubstitute_GetTheDrugs()
	    {
			// Arrange
		    var mockDataAccess = Substitute.For<IDataAccess>();
			mockDataAccess.GetAllTheDrugs().Returns(_fakeDrugList);

		    var mockThirdPartyAccess = Substitute.For<IThirdPartyDataAccess>();
		    mockThirdPartyAccess.GetThirdPartyDrugInfo(Arg.Any<Drug>())
									.Returns(FakeThirdPartyInfo1, FakeThirdPartyInfo2, FakeThirdPartyInfo3);
			
			var drugService = new DrugService(mockDataAccess, mockThirdPartyAccess);

			// Act
			var actualDrugs = drugService.GetTheDrugs();

			// Assert
			actualDrugs.Count().ShouldBe(3);

		    mockDataAccess.Received(1).GetAllTheDrugs();
			mockThirdPartyAccess.Received(3).GetThirdPartyDrugInfo(Arg.Any<Drug>());
		}
	}
}
