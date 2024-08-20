using System.ComponentModel.DataAnnotations;

namespace Labb_3___API.Models
{
    public class Link
    {
        public int Id { get; set; }
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Website must be between 3 and 150 characters")]
        public string Website { get; set; }
        public int InterestId { get; set; }
        public Interest Interest { get; set; }
    }
}
