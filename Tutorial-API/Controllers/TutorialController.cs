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
        public ActionResult <Tutorial> GetTutorialById(Guid id){
            var tutorialItem = _tutorialRepository.GetTutorialById(id);
            if(tutorialItem != null){
                return Ok(tutorialItem);
            }
            return NotFound();
            
        }


        // POST - /api/tutorials/dto
        [HttpPost]
        public ActionResult<Tutorial> CreateTutorial(Tutorial tutorialCreate){
            var tutorModel = _mapper.Map<Tutorial>(tutorialCreate);
            _tutorialRepository.CreateTutorial(tutorModel);

            _tutorialRepository.SaveChanges();
            var tutorialRead = _mapper.Map<Tutorial>(tutorModel);
            return CreatedAtRoute(nameof(GetAllTutorials), new {Id = tutorialCreate.Id }, tutorialRead);
        }

    }
}