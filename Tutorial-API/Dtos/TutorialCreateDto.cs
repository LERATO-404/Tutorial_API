using System.ComponentModel.DataAnnotations;

namespace Tutorial_API.Dtos
{
    public class TutorialCreateDto{

        [Required]
        public string HowTo { get; set; }

        [Required]
        [MaxLength(100)]
        public string Line { get; set; }

        [Required]
        [MaxLength(100)]
        public string Platform { get; set; }

        
    }
}