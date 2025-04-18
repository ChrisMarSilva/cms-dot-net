using CMS_DotNet_Teste_Object_Mapping.Dtos;
using CMS_DotNet_Teste_Object_Mapping.Models;
using Riok.Mapperly.Abstractions;

namespace CMS_DotNet_Teste_Object_Mapping.Mappers;

[Mapper] //[Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName)]
public partial class MapperlyMapperConfigurator
{
    public partial PersonDto Map(PersonModel person); // ToDto // MapPersonToDto
    public partial ICollection<PersonDto>? Map(ICollection<PersonModel>? person); // ToDtos // MapPersonToDtos

    //[MapProperty(nameof(Car.Manufacturer), nameof(CarDto.Producer))] // Map property with a different name in the target type
    //public partial Person Map(PersonDto personDto);
    //public partial SpotifyAlbum Map(SpotifyAlbumDto spotifyAlbumDto);
    //public partial CarDto ToDto(Car car);
}