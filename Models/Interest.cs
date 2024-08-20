using System.ComponentModel.DataAnnotations;

namespace Labb_3___API.Models
{
    public class Interest
    {
        public int Id { get; set; }

        [StringLength(100, MinimumLength = 1, ErrorMessage = "Title must be between 3 and 100 characters")]
        public string Title { get; set; }
        [StringLength(250, MinimumLength = 1, ErrorMessage = "Description must be between 3 and 250 characters")]
        public string Description { get; set; }
        public ICollection<Link> Links { get; set; } = [];
        public ICollection<PeopleWithInterest> PeopleWithInterests { get; set; } = [];
    }
}
