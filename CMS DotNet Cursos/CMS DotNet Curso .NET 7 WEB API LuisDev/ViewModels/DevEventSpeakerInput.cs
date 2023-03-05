using AwesomeDevEvents.API.Models;

namespace AwesomeDevEvents.API.ViewModels
{
    // public record DevEventSpeakerInput(
    //    string name,
    //    string talkTitle,
    //    string talkDescription,
    //    string linkedInProfile,
    //    Guid devEventId
    //);

    public class DevEventSpeakerInput
    {
        public string Name { get; set; }
        public string TalkTitle { get; set; }
        public string TalkDescription { get; set; }
        public string LinkedInProfile { get; set; }
        public Guid DevEventId { get; set; }
    }
}
