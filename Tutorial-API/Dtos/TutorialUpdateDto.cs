using System.ComponentModel.DataAnnotations;

namespace Tutorial_API.Dtos{
    public class TutorialUpdateDto{
        [Required]
        public string HowTo { get; set; }

        [Required]
        [MaxLength(100)]
        public string Line { get; set; }

        [Required]
        [MaxLength(100)]
        public string Platform { get; set; }

        public TutorialUpdateDto(){
            
        }

        public TutorialUpdateDto(string _howTo, string _line, string _platform){
            
            HowTo = _howTo;
            Line = _line;
            Platform = _platform;
        }
    }
}