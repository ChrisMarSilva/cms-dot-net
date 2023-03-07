namespace AwesomeDevEvents.Domain.Models
{
    public class RelationshipsUser
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public int Age { get; set; }
        public string AboutMe { get; set; }
        public DateTime CreatedDate { get; set; }

        //navigation properties
        public RelationshipsBlog Blog { get; set; } // 1 to 1
    }
}
