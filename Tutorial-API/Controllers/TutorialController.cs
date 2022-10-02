using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Tutorial_API.Interfaces;
using Tutorial_API.Dtos;
using Tutorial_API.Models;


namespace Tutorial_API.Controllers{

    // api/commands
    [Route("api/tutorials")]
    [ApiController]
    public class TutorialController : ControllerBase 
    {
        //no need
        //private readonly TutorRepoImp _tutorialRepository = new TutorRepoImp();
        private readonly ITutorial _tutorialRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<TutorialController> _logger;

        //create a constructor for DJ and the register in Startup.cs ConfigureServices
        public TutorialController(ITutorial repository, IMapper mapper,ILogger<TutorialController> logger ){
            _tutorialRepository = repository;
            _mapper = mapper;
            _logger = logger;
        }



        
        //Get - read all resources & Success = 200 (OK) & Failure = 400 (Bad)/404 (Not found)
        //[Route("api/tutorials")]
        [HttpGet]
        public ActionResult<IEnumerable<Tutorial>> GetAllTutorials(){
            
            var tutorialItems = _tutorialRepository.GetAllTutorials();
            return Ok(tutorialItems);
        }
        
        //Get - read a single resource & Success = 200 & Failure = 400 (Bad)/404 (Not found)
        //[Route("api/tutorials/{id}")]
        [HttpGet("{id}")]
        public ActionResult <Tutorial> GetTutorialById(int id){
            var tutorialItem = _tutorialRepository.GetTutorialById(id);
            if(tutorialItem != null){
                return Ok(tutorialItem);
            }
            return NotFound();
            
        }

        //Dto implementation using AutoMapper
        //Get /api/tutorials/dto
        [HttpGet("dto")]
        public ActionResult<IEnumerable<TutorialReadDto>> GetAllTutorialsDto(){
            
            var tutorialItems = _tutorialRepository.GetAllTutorials();
            return Ok(_mapper.Map<IEnumerable<TutorialReadDto>>(tutorialItems));
        }


        //Get /api/tutorials/dto/{id}
        [HttpGet("dto/{id}", Name ="GetTutorialByIdDto")]
        public ActionResult <TutorialReadDto> GetTutorialByIdDto(int id){
            var tutorialItem = _tutorialRepository.GetTutorialById(id);
            if(tutorialItem != null){
                return Ok(_mapper.Map<TutorialReadDto>(tutorialItem));
            }
            return NotFound();
            
        }

        //Post - Create a new resource & Success = 201 (Created) & Failure = 400 (Bad)/404 (Not found)
        // POST - /api/tutorials/dto
        [HttpPost("dto")]
        public ActionResult <TutorialCreateDto> CreateTutorialDto(TutorialCreateDto tutorialCreateDto){
            //use mapper to create model in the profiles

            var tutorModel = _mapper.Map<Tutorial>(tutorialCreateDto);
            _tutorialRepository.CreateTutorial(tutorModel);

            //without SaveChange the db is not updated
            _tutorialRepository.SaveChanges();

            //return read dto without platform
            var tutorialReadDto = _mapper.Map<TutorialReadDto>(tutorModel);

            //return the body with platform 
            //return Ok(tutorModel);

            //return the body without platform 
            //return Ok(tutorialReadDto);

            //to return the correct status code and the URL Location Header in PostMan
            // CreatedAtRoute(string routeName, string routeValue, string content)
            return CreatedAtRoute(nameof(GetTutorialByIdDto), new {Id = tutorialReadDto.Id }, tutorialReadDto);

        }

        //Put - Update an existing & Success = 204 (No content) & Failure = 400 (Bad)/404 (Not found)
        //Update - need to supply the entire object, inefficent (error prone)
        //PUT -"api/tutorials/dto/{id}"
        [HttpPut("dto/{id}")]
        public ActionResult UpdateTutorialDto(int id, TutorialUpdateDto tutorialUpdateDto){
            //check if resource exist
            var tutorialModelFromRepo = _tutorialRepository.GetTutorialById(id);

            if(tutorialModelFromRepo == null){
                return NotFound();
            }
            //otherwise update using an auto mapper create a map from your profiles
            //updates the model - tutorialModelFromRepo and tracked in the dbContext
            _mapper.Map(tutorialUpdateDto, tutorialModelFromRepo);
            _tutorialRepository.UpdateTutorial(tutorialModelFromRepo);
            //save to the db in line  - _mapper.Map(tutorialUpdateDto, tutorialModelFromRepo);
            _tutorialRepository.SaveChanges();

            return NoContent();

        }

        
        //Patch - update partial resource & Success = 204 & Failure = 400 (Bad)/404 (Not found)
        // more favourable to PUT as you can only update what u want
        // no separate dto like PUT
        //JSON Patch standard - Add, Remove, Replace, Copy, Move, Test
        // install packages: (1) Microsoft.AspNetCore.JsonPatch, (2) Se/Deserialize json - Microsoft.AspNetCore.Mvc.NewtonsoftJso
        // PATCH - "api/tutorials/dto/{id}"
        [HttpPatch("dto/{id}")]
        public ActionResult PartialTutorialUpdate(int id, JsonPatchDocument<TutorialUpdateDto> patchDoc){
            //check if resource exist
            var tutorialModelFromRepo = _tutorialRepository.GetTutorialById(id);
            if(tutorialModelFromRepo == null){
                return NotFound();
            }

            // a new update Dto with data from the repo from profiles - CreateMap<Tutor, TutorialUpdateDto>();
            var tutorialToPatch = _mapper.Map<TutorialUpdateDto>(tutorialModelFromRepo);

            //make use of the patch doc, ModelState - ensure validations from package 2 installed newton...
            patchDoc.ApplyTo(tutorialToPatch, ModelState);
            if(!TryValidateModel(tutorialToPatch)){
                return ValidationProblem(ModelState);
            }
            _mapper.Map(tutorialToPatch, tutorialModelFromRepo);
            _tutorialRepository.SaveChanges();
            return NoContent();
        }


        
        //Delete - delete a sigle resource & Success = 200/204 & Failure = 400 (Bad)/404 (Not found)
        // no need for a dto
        //Delete  - "api/tutorials/{id}"
        [HttpDelete("{id}")]
        public ActionResult DeleteTutorial(int id){
             //check if resource exist
            var tutorialModelFromRepo = _tutorialRepository.GetTutorialById(id);
            if(tutorialModelFromRepo == null){
                return NotFound();
            }
            _tutorialRepository.DeleteTutorial(tutorialModelFromRepo);
            _tutorialRepository.SaveChanges();
            return NoContent();
        }

        /*
        //Delete - delete a coolection of resources & Success = 200/204 & Failure = 400 (Bad)/404 (Not found)
        [HttpDelete]
        [Route("api/tutorials/{id}")]
        */
    }
}