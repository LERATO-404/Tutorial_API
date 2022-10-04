using System;
using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Tutorial_API.Interfaces;
using Tutorial_API.Controllers;
using Tutorial_API.Models;
using Tutorial_API.Dtos;
using AutoMapper;
using FluentAssertions;
using System.Collections;

namespace Tutorial_UnitTests
{
    public class TutorialControllerTests
    {

        private readonly Mock<ITutorial> repositoryStub = new();
        private readonly Mock<ILogger<TutorialController>> loggerStub = new();
        private readonly Mock<IMapper> mapperStub = new();
        private Guid identifier = Guid.NewGuid();
        private Guid identifier2 = Guid.NewGuid();
        private Guid identifier3 = Guid.NewGuid();
        


        private Tutorial CreateVariableTutorial(){
            return new(){
                Id =identifier,
                HowTo ="How to string",
                Line = "Line string",
                Platform = "Platform string"
            };
        }

        // test name convention - unitOfWork_stateUnderTest_ExpectedBehaviour
        [Fact] 
        public void GetTutorialById_WithNonExistingTutorial_ReturnNotFound()
        {   
            //AAA
            // Arrange -set everything (e.g mocks, input, var)
            /* stub - fake instance of a class for testing
            stub method  actually methods used for testing methods of a particular class. 
            It is used by inputting some values for the local variables in
            your actual development methods and check if the output is correct. */
            
            repositoryStub.Setup(repo => repo.GetTutorialById(It.IsAny<Guid>()))
            .Returns((Tutorial)null);

            var controllers = new TutorialController(repositoryStub.Object, mapperStub.Object, loggerStub.Object);
            
            //Act - execute the test
            var result = controllers.GetTutorialById(Guid.NewGuid());

            //Asset - verify the execution
            //Assert.IsType<NotFoundResult>(result.Result);
            // FluentAssertions
            result.Result.Should().BeOfType<NotFoundResult>();
        
        }

        [Fact] 
        public void GetTutorialById_WithExistingTutorial_ReturnExpectedTutorial()
        { 
            //Arrange
            // line in the controller item
            var expectedTutorial = CreateVariableTutorial();
            
            //repositoryStub.Setup(repo => repo.GetTutorialById(It.IsAny<Guid>())).Returns(expectedTutorial);
            repositoryStub.Setup(x => x.GetTutorialById(It.IsAny<Guid>())).Returns(expectedTutorial);

            var controller = new TutorialController(repositoryStub.Object, mapperStub.Object, loggerStub.Object);
            
            //Act - execute the test
            var result = controller.GetTutorialById(identifier);

            //Asset - verify the execution
            //Assert.IsType<Tutorial>(result.Result);

            //result.Result.Should().BeEquivalentTo(expectedTutorial,options => options.ComparingByMembers<Tutorial>());

            //Assert.IsType<Tutorial>(result.Value);
            //Assert.IsType<Tutorial>(result.Value);S

            //check if the result match the dto
            Assert.IsType<OkObjectResult>(result.Result);
            // dto = (result as ActionResult<Tutorial>).Value;
            //Assert.Equal(expectedTutorial.Id, dto.Id);
            //Assert.Equal(expectedTutorial.Line, dto.Line);
            //Assert.Equal(expectedTutorial.HowTo, dto.HowTo);
            //Assert.Equal(expectedTutorial.Platform, dto.Platform);
            
             // Or Use dotnet add package FluentAssertions --version 6.7.0
            //compare the values of the properties of expectedItem and result top

            //Now we need to check the value of the result for the ok object result.
            var item = result.Result as OkObjectResult;

            //We Expect to return a single book
            Assert.IsType<Tutorial>(item.Value);
            var tutorialItem = item.Value as Tutorial;
            Assert.Equal(identifier, tutorialItem.Id);
            //Assert.Equal("How to", tutorialItem.HowTo); out faile expecte How to string 
            Assert.Equal("How to string", tutorialItem.HowTo);
            
        }



        [Fact] 
        public void GetAllTutorials_WithExistingTutorial_ReturnAllTutorials()
        { 
            //Arrange
            var expectedTutorials = new[] {CreateVariableTutorial()};
            repositoryStub.Setup(repo => repo.GetAllTutorials()).Returns(expectedTutorials);

            var controller = new TutorialController(repositoryStub.Object, mapperStub.Object, loggerStub.Object);
            //Act
            var result = controller.GetAllTutorials();
            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
            //var list = result.Result as OkObjectResult;
            //Assert.IsType<IEnumerable<Tutorial>>(list.Value);
        }
    }
}
