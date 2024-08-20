using System.ComponentModel.DataAnnotations;

namespace Labb_3___API.Models
{
    public class People
    {
        public int Id { get; set; }
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters")]
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Phone]
        [RegularExpression(@"^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Enter a phone number with 10 digits")]
        public string? PhoneNumber { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public int Age { get; set; }

        public ICollection<PeopleWithInterest> PeopleWithInterests { get; set; } = [];
    }
}
