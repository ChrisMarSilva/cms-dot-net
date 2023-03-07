namespace AwesomeDevEvents.Domain.Models
{
    public class RelationshipsBlog
    {
        public int BlogId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }

        //navigation properties
        public RelationshipsUser User { get; set; } // 1 to 1
        public ICollection<RelationshipsPost> Posts { get; set; } // 1 to many
        public ICollection<RelationshipsImage> Images { get; set; } // many to many
    }
}
