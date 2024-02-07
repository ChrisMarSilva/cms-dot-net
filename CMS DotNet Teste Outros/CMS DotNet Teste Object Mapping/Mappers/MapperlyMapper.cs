using CMS_DotNet_Teste_Object_Mapping.Dtos;
using CMS_DotNet_Teste_Object_Mapping.Models;
using Riok.Mapperly.Abstractions;

namespace CMS_DotNet_Teste_Object_Mapping.Mappers;

[Mapper]
public partial class MapperlyMapper
{
    public partial SpotifyAlbum Map(SpotifyAlbumDto spotifyAlbumDto);
    public partial PersonDto Map(Person person);
    public partial Person Map(PersonDto personDto);
    //public partial CarDto ToDto(Car car);
}