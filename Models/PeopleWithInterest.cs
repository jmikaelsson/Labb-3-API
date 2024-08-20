namespace Labb_3___API.Models
{
    public class PeopleWithInterest
    {
        public int Id { get; set; }
        public int PeopleId { get; set; }
        public People People { get; set; }

        public int InterestId { get; set; }
        public Interest Interest { get; set; }
    }
}
