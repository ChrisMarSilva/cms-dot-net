using AutoMapper;
using AwesomeDevEvents.Domain.Dtos;
using AwesomeDevEvents.Domain.Models;

namespace AwesomeDevEvents.API.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                //DevEvent
                config.CreateMap<DevEventInputDto, DevEvent>();
                config.CreateMap<DevEvent, DevEventOutputDto>();

                //DevEventSpeaker
                config.CreateMap<DevEventSpeakerInputDto, DevEventSpeaker>();
                config.CreateMap<DevEventSpeaker, DevEventSpeakerOutputDto>();

                // CreateMap<Paciente, PacienteDetalhesDto>().ReverseMap();
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
