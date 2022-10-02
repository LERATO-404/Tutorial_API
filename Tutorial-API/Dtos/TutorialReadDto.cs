using System.ComponentModel.DataAnnotations;
namespace Tutorial_API.Dtos
{
    public class TutorialReadDto{

        //[Key]
        public int Id { get; set; }

        //[Required]
        public string HowTo { get; set; }

        //[Required]
        //[MaxLength(100)]
        public string Line { get; set; }

        //Removed Platform 
        /*Why?
            Without DTOs, wed have to expose the entire entities to the remote interface.
            This causes a strong coupling between an API and persisitence model. 

            By using a DTO to transfer just the required information, we loosen the coupling 
            between the API and our model, allowing us to more easily maintain and scale the service
        
        */
        public TutorialReadDto(){
            
        }

        public TutorialReadDto(int _id, string _howTo, string _line){
            Id = _id;
            HowTo = _howTo;
            Line = _line;
            
        }


        
    }
}