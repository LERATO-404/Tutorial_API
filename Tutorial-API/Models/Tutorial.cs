using System.ComponentModel.DataAnnotations;

namespace Tutorial_API.Models
{
    public class Tutorial{

        [Key]
        public int Id { get; set; }

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