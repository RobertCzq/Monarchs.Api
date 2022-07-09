using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Monarchs.Api.Controllers;
using Monarchs.Api.UnitTests.Utils;
using Monarchs.Common.Models;
using Monarchs.Common.ViewModels;

namespace Monarchs.Api.UnitTests
{
    public class MonarchsControllerUnitTests
    {
        [Fact]
        public async void GetNumberOfMonarchs_Returns_The_Same_Nr_As_Instantiated_Fakes()
        {
            //Arrange
            var cache = Setup.GetMockMonarchsCache();
            var logger = Setup.GetMockMonarchsLogger();
            var nrOfMonarchs = 5;
            var fakeMonarchs = A.CollectionOfDummy<Monarch>(nrOfMonarchs);

            A.CallTo(() => cache.GetAll()).Returns(fakeMonarchs);
            
            var controller = new MonarchsController(cache, logger);

            //Act
            var actionResult = await controller.GetNumberOfMonarchs();

            //Assert
            var result = actionResult as OkObjectResult;
            var returnedNrOfMonarchs = result?.Value;
            Assert.Equal(nrOfMonarchs, returnedNrOfMonarchs);

        }

        [Fact]
        public async void GetNumberOfMonarchs_Returns_NotFound_Because_ListOfMonarchs_Not_Instantiated()
        {
            //Arrange
            var cache = Setup.GetMockMonarchsCache();
            var logger = Setup.GetMockMonarchsLogger();
            var fakeMonarchs = A.CollectionOfDummy<Monarch>(0);
            fakeMonarchs = null;
            A.CallTo(() => cache.GetAll()).Returns(fakeMonarchs);
            var controller = new MonarchsController(cache, logger);

            //Act
            var actionResult = await controller.GetNumberOfMonarchs();

            //Assert
            Assert.IsType<NotFoundObjectResult>(actionResult);

        }

        [Fact]
        public async void GetLongestRulingMonarch_Returns_The_Right_One_Instantiated()
        {
            //Arrange
            var cache = Setup.GetMockMonarchsCache();
            var logger = Setup.GetMockMonarchsLogger();
            var firstFakeMonarch = A.Fake<Monarch>(x => x.WithArgumentsForConstructor
            (() => new Monarch(1, "Dummy1", "Dummy1 the Great", "DummyCountry", "DummyHouse", 50)));
            var secondFakeMonarch = A.Fake<Monarch>(x => x.WithArgumentsForConstructor
            (() => new Monarch(2, "Dummy2", "Dummy2 the Great", "DummyCountry", "DummyHouse", 40)));

            var fakeMonarchs = new List<Monarch>()
            {
                firstFakeMonarch,
                secondFakeMonarch,
            };   

            A.CallTo(() => cache.GetAll()).Returns(fakeMonarchs);

            var controller = new MonarchsController(cache, logger);

            //Act
            var actionResult = await controller.GetLongestRulingMonarch();

            //Assert
            var result = actionResult as OkObjectResult;
            var returnedMonarch = result?.Value as MonarchViewModel;
            Assert.Equal(firstFakeMonarch.NrOfYearsRuled, returnedMonarch?.NrOfYearsRuled);
            Assert.Equal(firstFakeMonarch.FullName, returnedMonarch?.FullName);
        }

        [Fact]
        public async void GetLongestRulingMonarch_Returns_NotFound_Because_ListOfMonarchs_Not_Instantiated()
        {
            //Arrange
            var cache = Setup.GetMockMonarchsCache();
            var logger = Setup.GetMockMonarchsLogger();

            var controller = new MonarchsController(cache, logger);

            //Act
            var actionResult = await controller.GetLongestRulingMonarch();

            //Assert
            Assert.IsType<NotFoundObjectResult>(actionResult);
        }

        [Fact]
        public async void GetLongestRulingHouse_Returns_The_Right_One_Instantiated()
        {
            //Arrange
            var cache = Setup.GetMockMonarchsCache();
            var logger = Setup.GetMockMonarchsLogger();
            var rigthHouse = "DummyHouse1";
            var rightNrOfYears = 90;
            var firstFakeMonarch = A.Fake<Monarch>(x => x.WithArgumentsForConstructor
            (() => new Monarch(1, "Dummy1", "Dummy1 the Great", "DummyCountry", rigthHouse, 50)));
            var secondFakeMonarch = A.Fake<Monarch>(x => x.WithArgumentsForConstructor
            (() => new Monarch(2, "Dummy2", "Dummy2 the Great", "DummyCountry", rigthHouse, 40)));
            var thirdFakeMonarch = A.Fake<Monarch>(x => x.WithArgumentsForConstructor
            (() => new Monarch(3, "Dummy3", "Dummy3 the Great", "DummyCountry", "DummyHouse2", 10)));
            var forthFakeMonarch = A.Fake<Monarch>(x => x.WithArgumentsForConstructor
            (() => new Monarch(4, "Dummy4", "Dummy4 the Great", "DummyCountry", "DummyHouse2", 40)));

            var fakeMonarchs = new List<Monarch>()
            { 
                firstFakeMonarch,
                secondFakeMonarch,
                thirdFakeMonarch,
                forthFakeMonarch,
            };

            A.CallTo(() => cache.GetAll()).Returns(fakeMonarchs);

            var controller = new MonarchsController(cache, logger);

            //Act
            var actionResult = await controller.GetLongestRulingHouse();

            //Assert
            var result = actionResult as OkObjectResult;
            var returnedHouse = result?.Value as HouseViewModel;
            Assert.Equal(rightNrOfYears, returnedHouse?.NrOfYearsRuled);
            Assert.Equal(rigthHouse, returnedHouse?.House);
        }

        [Fact]
        public async void GetLongestRulingHouse_Returns_NotFound_Because_ListOfMonarchs_Not_Instantiated()
        {
            //Arrange
            var cache = Setup.GetMockMonarchsCache();
            var logger = Setup.GetMockMonarchsLogger();

            var controller = new MonarchsController(cache, logger);

            //Act
            var actionResult = await controller.GetLongestRulingHouse();

            //Assert
            Assert.IsType<NotFoundObjectResult>(actionResult);
        }

        [Fact]
        public async void GetMostCommonFirstName_Returns_The_Right_One_Instantiated()
        {
            //Arrange
            var cache = Setup.GetMockMonarchsCache();
            var logger = Setup.GetMockMonarchsLogger();
            var mostCommonName = "Dummy1";
            var firstFakeMonarch = A.Fake<Monarch>(x => x.WithArgumentsForConstructor
            (() => new Monarch(1, mostCommonName, "Dummy1 the Great", "DummyCountry", "DummyHouse1", 50)));
            var secondFakeMonarch = A.Fake<Monarch>(x => x.WithArgumentsForConstructor
            (() => new Monarch(2, mostCommonName, "Dummy2 the Great", "DummyCountry", "DummyHouse1", 40)));
            var thirdFakeMonarch = A.Fake<Monarch>(x => x.WithArgumentsForConstructor
            (() => new Monarch(3, mostCommonName, "Dummy3 the Great", "DummyCountry", "DummyHouse2", 10)));
            var forthFakeMonarch = A.Fake<Monarch>(x => x.WithArgumentsForConstructor
            (() => new Monarch(4, "Dummy4", "Dummy4 the Great", "DummyCountry", "DummyHouse2", 40)));

            var fakeMonarchs = new List<Monarch>()
            {
                firstFakeMonarch,
                secondFakeMonarch,
                thirdFakeMonarch,
                forthFakeMonarch,
            };

            A.CallTo(() => cache.GetAll()).Returns(fakeMonarchs);

            var controller = new MonarchsController(cache, logger);

            //Act
            var actionResult = await controller.GetMostCommonFirstName();

            //Assert
            var result = actionResult as OkObjectResult;
            var returnedCommonName = result?.Value;
            Assert.Equal(mostCommonName, returnedCommonName);
        }

        [Fact]
        public async void GetMostCommonFirstName_Returns_NotFound_Because_ListOfMonarchs_Not_Instantiated()
        {
            //Arrange
            var cache = Setup.GetMockMonarchsCache();
            var logger = Setup.GetMockMonarchsLogger();

            var controller = new MonarchsController(cache, logger);

            //Act
            var actionResult = await controller.GetMostCommonFirstName();

            //Assert
            Assert.IsType<NotFoundObjectResult>(actionResult);
        }

    }
}