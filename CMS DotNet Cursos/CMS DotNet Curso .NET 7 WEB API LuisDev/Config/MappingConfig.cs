using AutoMapper;
using AwesomeDevEvents.API.Models;
using AwesomeDevEvents.API.ViewModels;

namespace AwesomeDevEvents.API.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                //DevEvent
                config.CreateMap<DevEventInput, DevEvent>();
                config.CreateMap<DevEvent, DevEventOutput>();

                //DevEventSpeaker
                config.CreateMap<DevEventSpeakerInput, DevEventSpeaker>();
                config.CreateMap<DevEventSpeaker, DevEventSpeakerOutput>();

                // CreateMap<ProjectDTO, Project>().ReverseMap();
            });
            return mappingConfig;
        }
    }

    //public class DevEventProfile: Profile
    //{
    //    public DevEventProfile()
    //    {
    //        CreateMap<DevEventInput, DevEvent>();
    //        CreateMap<DevEvent, DevEventOutput>();
    //    }
    //}
}
