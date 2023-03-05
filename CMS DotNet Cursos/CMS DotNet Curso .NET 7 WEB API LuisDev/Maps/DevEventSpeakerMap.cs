using AwesomeDevEvents.API.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AwesomeDevEvents.API.Maps
{
    public class DevEventSpeakerMap : BaseMap<DevEventSpeaker>
    {
        public DevEventSpeakerMap() : base("DevEventSpeakers") { }

        public override void Configure(EntityTypeBuilder<DevEventSpeaker> builder)
        {
            base.Configure(builder);
        }
    }
}
