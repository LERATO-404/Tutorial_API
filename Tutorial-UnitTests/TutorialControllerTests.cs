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
        
        private Tutorial CreateNullTutorial(){
            return null;
        }

        private Tutorial CreateVariableTutorial(){
            return new(){
                Id =identifier,
                HowTo ="How to string",
                Line = "Line string",
                Platform = "Platform string"
            };
        }

        private Tutorial CreateVariableWithNullPropertiesTutorial(){
            return new(){
                Id =identifier,
                //HowTo ="How to string",
                //Line = "Line string",
                //Platform = "Platform string"
            };
        }

        // test name convention - unitOfWork_stateUnderTest_ExpectedBehaviour
        [Fact]
        public void CreateTutorial_CreateATutorial_ReturnCreatedTutorial(){

            //Assign
            var tutorialToCreate = CreateVariableTutorial();
            repositoryStub.Setup(repo => repo.CreateTutorial(tutorialToCreate));
            var controller = new TutorialController(repositoryStub.Object, mapperStub.Object, loggerStub.Object);

            //Act
            var result = controller.CreateTutorial(tutorialToCreate);

            //Assert
            Assert.NotNull(result.Result);
            Assert.Equal("How to string", tutorialToCreate.HowTo);
            Assert.Equal("Line string", tutorialToCreate.Line);
            Assert.Equal("Platform string", tutorialToCreate.Platform);
            //Assert.IsType<CreatedAtRouteResult>(result.Result);

        }

        [Fact]
        public void CreateTutorial_CreateNullTutorialAttribute_ReturnCreatedTutorial(){

            //Assign
            var tutorialToCreate = CreateVariableWithNullPropertiesTutorial();

            repositoryStub.Setup(repo => repo.CreateTutorial(tutorialToCreate));
            var controller = new TutorialController(repositoryStub.Object, mapperStub.Object, loggerStub.Object);


            //Act
            var result = controller.CreateTutorial(tutorialToCreate);

            //Assert
            Assert.NotNull(result.Result);
            tutorialToCreate.HowTo.Should().BeNull();
            tutorialToCreate.Line.Should().BeNull();
            tutorialToCreate.Platform.Should().BeNull();

        }

         [Fact] 
        public void GetTutorialById_WithExistingTutorial_ReturnExpectedTutorial()
        { 
            //Arrange
            var expectedTutorial = CreateVariableTutorial();
            repositoryStub.Setup(repo => repo.GetTutorialById(It.IsAny<Guid>())).Returns(expectedTutorial);
            var controller = new TutorialController(repositoryStub.Object, mapperStub.Object, loggerStub.Object);
            
            //Act
            var result = controller.GetTutorialById(identifier);

            //Asset - verify the execution
            Assert.IsType<OkObjectResult>(result.Result);
            
            //Now we need to check the value of the result for the ok object result.
            var item = result.Result as OkObjectResult;

            //We Expect to return a single tutorial
            Assert.IsType<Tutorial>(item.Value);
            var tutorialItem = item.Value as Tutorial;
            Assert.Equal(identifier, tutorialItem.Id);
            Assert.Equal("How to string", tutorialItem.HowTo);
            
        }
    
        [Fact] 
        public void GetTutorialById_WithNonExistingTutorial_ReturnNotFound()
        {   
            //AAA
            // Arrange 
            repositoryStub.Setup(repo => repo.GetTutorialById(It.IsAny<Guid>())).Returns((Tutorial)null);
            var controllers = new TutorialController(repositoryStub.Object, mapperStub.Object, loggerStub.Object);
            
            //Act 
            var result = controllers.GetTutorialById(identifier);

            //Asset 
            //using  FluentAssertions
            result.Result.Should().BeOfType<NotFoundResult>();
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
            expectedTutorials.Should().NotBeEmpty();
            Assert.IsType<OkObjectResult>(result.Result);
        }
        

    }
}
