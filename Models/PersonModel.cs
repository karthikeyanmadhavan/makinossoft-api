using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class PersonModel
    {
        [Required]
        [MinLength(2)]
        public string FirstName { get; set; }
        [Required]
        [MinLength(2)]
        public string LastName { get; set; }
    }
}
