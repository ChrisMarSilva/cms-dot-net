namespace AwesomeDevEvents.Domain.Models
{
    public class RelationshipsPost
    {
        public int PostId { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }

        public RelationshipsBlog Blog { get; set; } // 1 to many
    }
}
