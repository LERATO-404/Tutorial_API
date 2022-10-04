using System.ComponentModel.DataAnnotations;
using System;
namespace Tutorial_API.Models
{
    public class Tutorial{

        [Key]
        public Guid Id { get; set; }

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