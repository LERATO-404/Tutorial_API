using System;
using System.Linq;
using System.Collections.Generic;
using Tutorial_API.Models;
using Tutorial_API.Data;
using Tutorial_API.Interfaces;

// sql implementation of the interface - ITutorialials using DBContext
namespace Tutorial_API.Repository{
    public class TutorialRepository : ITutorial{
        
        public readonly TutorialContext _context;
        
        public TutorialRepository(TutorialContext ctx){
            _context = ctx;
        }

        //implementation of get all Tutorialial using the DB Context
        public IEnumerable<Tutorial> GetAllTutorials(){
            //return all table -Tutorials command to a list
            return _context.Tutorials.ToList();
        }

         //implementation of get Tutorialial by id using the DB Context
        public Tutorial GetTutorialById(Guid id){
            return _context.Tutorials.FirstOrDefault(p => p.Id == id);
        }

        public void CreateTutorial(Tutorial createdTutorial){
            if(createdTutorial == null){
                throw new ArgumentNullException(nameof(createdTutorial));
            }
            _context.Tutorials.Add(createdTutorial);
        }

        //save changes in the db
        public bool SaveChanges(){
            return (_context.SaveChanges() >= 0);
        }

        //update using PUT
        //no need to put the implementaion for Update
        public void UpdateTutorial(Tutorial updateTutorial){
            //nothing
        }


        //delete a resource
        public void DeleteTutorial(Tutorial deleteTutorial){
            if(deleteTutorial == null){
                throw new ArgumentNullException(nameof(deleteTutorial));
            }
            _context.Tutorials.Remove(deleteTutorial);
        }  
    }
}