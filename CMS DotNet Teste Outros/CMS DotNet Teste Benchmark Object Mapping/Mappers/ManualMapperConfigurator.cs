using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CMS_DotNet_Teste_Object_Mapping.Dtos;
using CMS_DotNet_Teste_Object_Mapping.Models;

namespace CMS_DotNet_Teste_Object_Mapping.Mappers;

public static class ManualMapperConfigurator
{
    public static List<PersonDto> ToDto(this IEnumerable<Person> models)
    {
        return models
            .Select(ToDto)
            .ToList();
    }

    public static PersonDto ToDto(this Person model)
    {
        return new()
        {
            Id = model.Id,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Birthday = model.Birthday
        };
    }


    //    public static List<Customer> MapTo(this IEnumerable<CustomerDto> dto)
    //    {
    //        return dto.Select(MapTo).ToList();
    //    }

    //    public static Customer MapTo(this CustomerDto dto)
    //    {
    //        return new()
    //        {
    //            CustomerID = dto.CustomerID,
    //            CompanyName = dto.CompanyName,
    //            ContactName = dto.ContactName,
    //            ContactTitle = dto.ContactTitle,
    //            Address = dto.Address,
    //            City = dto.City,
    //            Region = dto.Region,
    //            PostalCode = dto.PostalCode,
    //            Country = dto.Country,
    //            Phone = dto.Phone,
    //            Fax = dto.Fax,
    //            Orders = dto.Orders.Select(MapTo).ToList(),
    //            CustomerDemographics = dto.CustomerDemographics.Select(MapTo).ToArray()
    //        };
    //    }

    //    private static Order MapTo(this OrderDto dto)
    //    {
    //        return new()
    //        {
    //            OrderID = dto.OrderID,
    //            CustomerID = dto.CustomerID,
    //            EmployeeID = dto.EmployeeID,
    //            OrderDate = dto.OrderDate,
    //            RequiredDate = dto.RequiredDate,
    //            ShippedDate = dto.ShippedDate,
    //            ShipVia = dto.ShipVia,
    //            Freight = dto.Freight,
    //            ShipName = dto.ShipName,
    //            ShipAddress = dto.ShipAddress,
    //            ShipCity = dto.ShipCity,
    //            ShipRegion = dto.ShipRegion,
    //            ShipPostalCode = dto.ShipPostalCode,
    //            ShipCountry = dto.ShipCountry,
    //            Employee = MapTo(dto.Employee),
    //            OrderDetails = dto.OrderDetails.Select(MapTo).ToList(),
    //            Shipper = MapTo(dto.Shipper),
    //        };
    //    }

    //    private static OrderDetail MapTo(this OrderDetailDto dto)
    //    {
    //        return new()
    //        {
    //            OrderID = dto.OrderID,
    //            ProductID = dto.ProductID,
    //            UnitPrice = dto.UnitPrice,
    //            Quantity = dto.Quantity,
    //            Discount = dto.Discount,
    //            Product = MapTo(dto.Product)
    //        };
    //    }

    //    private static Product MapTo(this ProductDto dto)
    //    {
    //        return new()
    //        {
    //            ProductID = dto.ProductID,
    //            ProductName = dto.ProductName,
    //            SupplierID = dto.SupplierID,
    //            CategoryID = dto.CategoryID,
    //            QuantityPerUnit = dto.QuantityPerUnit,
    //            UnitPrice = dto.UnitPrice,
    //            UnitsInStock = dto.UnitsInStock,
    //            UnitsOnOrder = dto.UnitsOnOrder,
    //            ReorderLevel = dto.ReorderLevel,
    //            Discontinued = dto.Discontinued,
    //            Category = MapTo(dto.Category),
    //            Supplier = MapTo(dto.Supplier)
    //        };
    //    }

    //    private static Employee MapTo(this EmployeeDto dto)
    //    {
    //        return new()
    //        {
    //            EmployeeID = dto.EmployeeID,
    //            LastName = dto.LastName,
    //            FirstName = dto.FirstName,
    //            Title = dto.Title,
    //            TitleOfCourtesy = dto.TitleOfCourtesy,
    //            BirthDate = dto.BirthDate,
    //            HireDate = dto.HireDate,
    //            Address = dto.Address,
    //            City = dto.City,
    //            Region = dto.Region,
    //            PostalCode = dto.PostalCode,
    //            Country = dto.Country,
    //            HomePhone = dto.HomePhone,
    //            Extension = dto.Extension,
    //            Photo = dto.Photo,
    //            Notes = dto.Notes,
    //            ReportsTo = dto.ReportsTo,
    //            PhotoPath = dto.PhotoPath,
    //            Territories = dto.Territories.Select(MapTo).ToArray()
    //        };
    //    }

    //    private static CustomerDemographic MapTo(this CustomerDemographicDto dto)
    //    {
    //        return new()
    //        {
    //            CustomerTypeID = dto.CustomerTypeID,
    //            CustomerDesc = dto.CustomerDesc
    //        };
    //    }

    //    private static Territory MapTo(this TerritoryDto dto)
    //    {
    //        return new()
    //        {
    //            TerritoryID = dto.TerritoryID,
    //            TerritoryDescription = dto.TerritoryDescription,
    //            RegionID = dto.RegionID,
    //            Region = MapTo(dto.Region)
    //        };
    //    }

    //    private static Region MapTo(this RegionDto dto)
    //    {
    //        return new()
    //        {
    //            RegionID = dto.RegionID,
    //            RegionDescription = dto.RegionDescription
    //        };
    //    }

    //    private static Shipper MapTo(this ShipperDto dto)
    //    {
    //        return new()
    //        {
    //            ShipperID = dto.ShipperID,
    //            CompanyName = dto.CompanyName,
    //            Phone = dto.Phone
    //        };
    //    }

    //    private static Category MapTo(this CategoryDto dto)
    //    {
    //        return new()
    //        {
    //            CategoryID = dto.CategoryID,
    //            CategoryName = dto.CategoryName,
    //            Description = dto.Description,
    //            Picture = dto.Picture.ToArray()
    //        };
    //    }

    //    private static Supplier MapTo(this SupplierDto dto)
    //    {
    //        return new()
    //        {
    //            SupplierID = dto.SupplierID,
    //            CompanyName = dto.CompanyName,
    //            ContactName = dto.ContactName,
    //            ContactTitle = dto.ContactTitle,
    //            Address = dto.Address,
    //            City = dto.City,
    //            Region = dto.Region,
    //            PostalCode = dto.PostalCode,
    //            Country = dto.Country,
    //            Phone = dto.Phone,
    //            Fax = dto.Fax,
    //            HomePage = dto.HomePage,
    //        };
    //    }

    //    public static List<City> MapTo(this IEnumerable<CityDto> dto)
    //    {
    //        return dto.Select(MapTo).ToList();
    //    }

    //    public static City MapTo(this CityDto dto)
    //    {
    //        return new()
    //        {
    //            CityID = dto.CityID,
    //            Name = dto.Name,
    //            Region = dto.Region,
    //            Country = dto.Country
    //        };
    //    }

    //    public static SpotifyAlbum Map(this SpotifyAlbumDto spotifyAlbumDto)
    //    {
    //        return new SpotifyAlbum()
    //        {
    //            AlbumType = spotifyAlbumDto.AlbumType,
    //            Artists = spotifyAlbumDto.Artists.Select(spotifyAlbumDtoArtist => new Artist()
    //            {
    //                ExternalUrls = new ExternalUrls()
    //                {
    //                    Spotify = spotifyAlbumDtoArtist.ExternalUrls.Spotify
    //                },
    //                Href = spotifyAlbumDtoArtist.Href,
    //                Id = spotifyAlbumDtoArtist.Id,
    //                Name = spotifyAlbumDtoArtist.Name,
    //                Type = spotifyAlbumDtoArtist.Type,
    //                Uri = spotifyAlbumDtoArtist.Uri
    //            }).ToArray(),
    //            AvailableMarkets = spotifyAlbumDto.AvailableMarkets,
    //            Copyrights = spotifyAlbumDto.Copyrights.Select(spotifyAlbumDtoCopyright => new Copyright()
    //            {
    //                Text = spotifyAlbumDtoCopyright.Text,
    //                Type = spotifyAlbumDtoCopyright.Type
    //            }).ToArray(),
    //            ExternalIds = new ExternalIds()
    //            {
    //                Upc = spotifyAlbumDto.ExternalIds.Upc
    //            },
    //            ExternalUrls = new ExternalUrls()
    //            {
    //                Spotify = spotifyAlbumDto.ExternalUrls.Spotify
    //            },
    //            Genres = spotifyAlbumDto.Genres,
    //            Href = spotifyAlbumDto.Href,
    //            Id = spotifyAlbumDto.Id,
    //            Images = spotifyAlbumDto.Images.Select(spotifyAlbumDtoImage => new Image()
    //            {
    //                Height = spotifyAlbumDtoImage.Height,
    //                Url = spotifyAlbumDtoImage.Url,
    //                Width = spotifyAlbumDtoImage.Width
    //            }).ToArray(),
    //            Name = spotifyAlbumDto.Name,
    //            Popularity = spotifyAlbumDto.Popularity,
    //            ReleaseDate = spotifyAlbumDto.ReleaseDate,
    //            ReleaseDatePrecision = spotifyAlbumDto.ReleaseDatePrecision,
    //            Tracks = new Tracks()
    //            {
    //                Href = spotifyAlbumDto.Tracks.Href,
    //                Items = spotifyAlbumDto.Tracks.Items.Select(spotifyAlbumDtoTracksItem => new Item()
    //                {
    //                    Artists = spotifyAlbumDtoTracksItem.Artists.Select(spotifyAlbumDtoTracksItemArtist => new Artist()
    //                    {
    //                        ExternalUrls = new ExternalUrls()
    //                        {
    //                            Spotify = spotifyAlbumDtoTracksItemArtist.ExternalUrls.Spotify
    //                        },
    //                        Href = spotifyAlbumDtoTracksItemArtist.Href,
    //                        Id = spotifyAlbumDtoTracksItemArtist.Id,
    //                        Name = spotifyAlbumDtoTracksItemArtist.Name,
    //                        Type = spotifyAlbumDtoTracksItemArtist.Type,
    //                        Uri = spotifyAlbumDtoTracksItemArtist.Uri
    //                    }).ToArray(),
    //                    AvailableMarkets = spotifyAlbumDtoTracksItem.AvailableMarkets,
    //                    DiscNumber = spotifyAlbumDtoTracksItem.DiscNumber,
    //                    DurationMs = spotifyAlbumDtoTracksItem.DurationMs,
    //                    Explicit = spotifyAlbumDtoTracksItem.Explicit,
    //                    ExternalUrls = new ExternalUrls()
    //                    {
    //                        Spotify = spotifyAlbumDtoTracksItem.ExternalUrls.Spotify
    //                    },
    //                    Href = spotifyAlbumDtoTracksItem.Href,
    //                    Id = spotifyAlbumDtoTracksItem.Id,
    //                    Name = spotifyAlbumDtoTracksItem.Name,
    //                    PreviewUrl = spotifyAlbumDtoTracksItem.PreviewUrl,
    //                    TrackNumber = spotifyAlbumDtoTracksItem.TrackNumber,
    //                    Type = spotifyAlbumDtoTracksItem.Type,
    //                    Uri = spotifyAlbumDtoTracksItem.Uri
    //                }).ToArray(),
    //                Limit = spotifyAlbumDto.Tracks.Limit,
    //                Next = spotifyAlbumDto.Tracks.Next,
    //                Offset = spotifyAlbumDto.Tracks.Offset,
    //                Previous = spotifyAlbumDto.Tracks.Previous,
    //                Total = spotifyAlbumDto.Tracks.Total
    //            },
    //            Type = spotifyAlbumDto.Type,
    //            Uri = spotifyAlbumDto.Uri
    //        };
    //    }
}

