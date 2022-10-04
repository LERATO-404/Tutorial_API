using System.Collections.Generic;
using Tutorial_API.Models;
using System;

namespace Tutorial_API.Interfaces{

    //Define all the operation no implementation
    public interface ITutorial
    {
        bool SaveChanges();

        //get all resources
        IEnumerable<Tutorial> GetAllTutorials();
        
        //get resource by Id
        Tutorial GetTutorialById(Guid id);

        //create a Tutorialial
        void CreateTutorial(Tutorial tutorial);

        // update using PUT
        void UpdateTutorial(Tutorial tutorial);

        //delete a resource
        void DeleteTutorial(Tutorial tutorial);

    }
}