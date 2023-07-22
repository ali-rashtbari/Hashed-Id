using System.ComponentModel.DataAnnotations;

namespace Hashed_Id.Models.Dtos
{
    public class UpdatePersonDto
    {
        [Required]
        public string Id { get; set; }
        [Required(ErrorMessage = "First Name is Reqired for the Person.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is Reqired for the Person.")]
        public string LastName { get; set; }
    }
}
