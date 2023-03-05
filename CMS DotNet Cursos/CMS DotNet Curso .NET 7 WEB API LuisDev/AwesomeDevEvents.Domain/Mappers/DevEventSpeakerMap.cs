using AwesomeDevEvents.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AwesomeDevEvents.Domain.Mappers
{
    public class DevEventSpeakerMap : BaseEntityMap<DevEventSpeaker>
    {
        public DevEventSpeakerMap() : base("DevEventSpeakers") { }

        public override void Configure(EntityTypeBuilder<DevEventSpeaker> builder)
        {
            base.Configure(builder);
        }
    }
}
