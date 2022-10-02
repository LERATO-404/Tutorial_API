using System;
using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Tutorial_API.Interfaces;
using Tutorial_API.Controllers;
using Tutorial_API.Models;
using AutoMapper;

namespace Tutorial_UnitTests
{
    public class TutorialControllerTests
    {
        // test name convention - unitOfWork_stateUnderTest_ExpectedBehaviour
        [Fact] 
        public void GetTutorialById_WithExistingTutorial_ReturnNotFound()
        {   
            //AAA
            // Arrange -set everything (e.g mocks, input, var)
            /* stub - fake instance of a class for testing
            stub method  actually methods used for testing methods of a particular class. 
            It is used by inputting some values for the local variables in
            your actual development methods and check if the output is correct. */
            var repositoryStub = new Mock<ITutorial>();
            repositoryStub.Setup(repo => repo.GetTutorialById(It.IsAny<int>()))
            .Returns((Tutorial)null);

            var loggerStub = new Mock<ILogger<TutorialController>>();
            var mapperStub = new Mock<IMapper>();
            var controllers = new TutorialController(repositoryStub.Object, mapperStub.Object, loggerStub.Object);
            
            //Act - execute the test
            var result = controllers.GetTutorialById(1000);

            //Asset - verify the execution
            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}
