using System;
using System.Collections.Generic;
using Data;
using Moq;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Rhino.Mocks;
using Services;
using Shouldly;
using ThirdPartyData;
using Xunit;
using Arg = NSubstitute.Arg;
using MockRepository = Rhino.Mocks.MockRepository;

namespace Tests
{
	public class ExceptionTests
	{
		[Fact]
		public void Rhino_Exception()
		{
			// Arrange
			var mockDataAccess = MockRepository.GenerateMock<IDataAccess>();
			mockDataAccess.Stub(da => da.GetSpecificDrugs(Arg<List<int>>.Is.Anything)).Throw(new ArgumentException());

			var mockThirdPartyAccess = MockRepository.GenerateMock<IThirdPartyDataAccess>();

			var drugService = new DrugService(mockDataAccess, mockThirdPartyAccess);

			// Act
			Action getSpecificDrugs = () => drugService.GetSpecificDrugs(Arg<List<int>>.Is.Anything);

			// Assert
			getSpecificDrugs.ShouldThrow<ArgumentException>();
		}

		[Fact]
		public void Moq_Exception()
		{
			// Arrange
			var mockDataAccess = new Mock<IDataAccess>();
			mockDataAccess.Setup(da => da.GetSpecificDrugs(It.IsAny<IEnumerable<int>>())).Throws<ArgumentException>();

			var mockThirdPartyAccess = new Mock<IThirdPartyDataAccess>(MockBehavior.Strict);

			var drugService = new DrugService(mockDataAccess.Object, mockThirdPartyAccess.Object);

			// Act
			Action getSpecificDrugs = () => drugService.GetSpecificDrugs(It.IsAny<IEnumerable<int>>());

			// Assert
			getSpecificDrugs.ShouldThrow<ArgumentException>();
		}

		[Fact]
		public void NSubstitute_Exception()
		{
			// Arrange
			var mockDataAccess = Substitute.For<IDataAccess>();
			mockDataAccess.GetSpecificDrugs(Arg.Any<List<int>>()).Throws<ArgumentException>();

			var mockThirdPartyAccess = Substitute.For<IThirdPartyDataAccess>();

			var drugService = new DrugService(mockDataAccess, mockThirdPartyAccess);

			// Act
			Action getSpecificDrugs = () => drugService.GetSpecificDrugs(Arg.Any<List<int>>());

			// Assert
			getSpecificDrugs.ShouldThrow<ArgumentException>();
		}
	}
}
