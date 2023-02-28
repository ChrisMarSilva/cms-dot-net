namespace AwesomeDevEvents.API.DTOs
{
    //public class DevEventSpeakerDTO
    //{
    //    public Guid Id { get; set; }
    //    public string Name { get; set; }
    //    public string TalkTitle { get; set; }
    //    public string TalkDescription { get; set; }
    //    public string LinkedInProfile { get; set; }
    //    public Guid DevEventId { get; set; }
    //
    //    public DevEventSpeakerDTO()
    //    {
    //
    //    }
    //
    //    public DevEventSpeakerDTO(
    //        Guid id, 
    //        string name, 
    //        string talkTitle, 
    //        string talkDescription, 
    //        string linkedInProfile, 
    //        Guid devEventId
    //        )
    //    {
    //        this.Id = id;
    //        this.Name = name;
    //        this.TalkTitle = talkTitle;
    //        this.TalkDescription = talkDescription;
    //        this.LinkedInProfile = linkedInProfile;
    //        this.DevEventId = devEventId;
    //    }
    //}

    public record DevEventSpeakerDTO(
        Guid id,
        string name,
        string talkTitle,
        string talkDescription,
        string linkedInProfile,
        Guid devEventId
    );
}
