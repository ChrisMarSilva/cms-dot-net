using AwesomeDevEvents.API.Entities;

namespace AwesomeDevEvents.API.DTOs
{
    //public class DevEventDTO
    //{
    //    public Guid Id { get; set; }
    //    public string Title { get; set; }
    //    public string Description { get; set; }
    //    public DateTime StartDate { get; set; }
    //    public DateTime EndDate { get; set; }
    //    public List<DevEventSpeaker> Speakers { get; set; }
    //    public bool IsDeleted { get; set; }
    //
    //    public DevEventDTO()
    //    {
    //
    //    }
    //
    //    public DevEventDTO(Guid id, string title, string description, DateTime startDate, DateTime endDate, List<DevEventSpeaker> speakers, bool isDeleted)
    //    {
    //        this.Id = id;
    //        this.Title = title;
    //        this.Description = description;
    //        this.StartDate = startDate;
    //        this.EndDate = endDate;
    //        //this.Speakers = new List<DevEventSpeaker>();
    //        this.Speakers = speakers;
    //        this.IsDeleted = isDeleted;
    //    }
    //}

    public record DevEventDTO(
        Guid id, 
        string title, 
        string description, 
        DateTime startDate, 
        DateTime endDate, 
        List<DevEventSpeaker> speakers, 
        bool isDeleted
    );
}
