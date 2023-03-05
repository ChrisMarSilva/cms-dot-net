using System;

namespace AwesomeDevEvents.Domain.Dtos
{
    // public record DevEventSpeakerInputDto(
    //    string name,
    //    string talkTitle,
    //    string talkDescription,
    //    string linkedInProfile,
    //    Guid devEventId
    //);

    public class DevEventSpeakerInputDto
    {
        public string Name { get; set; }
        public string TalkTitle { get; set; }
        public string TalkDescription { get; set; }
        public string LinkedInProfile { get; set; }
        public Guid DevEventId { get; set; }
    }
}
