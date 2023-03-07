namespace AwesomeDevEvents.Domain.Models
{
    public class RelationshipsImage
    {
        public int ImageId { get; set; }
        public string Url { get; set; }

        //navigation properties
        public ICollection<RelationshipsBlog> Blogs { get; set; } // many to many
    }
}
