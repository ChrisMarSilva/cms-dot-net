namespace CMS_DotNet_Teste_Object_Mapping.Models;

public class SpotifyAlbum
{
    public string AlbumType { get; set; } = string.Empty;
    public Artist[] Artists { get; set; }
    public string[] AvailableMarkets { get; set; }
    public Copyright[] Copyrights { get; set; }
    public ExternalIds ExternalIds { get; set; }
    public ExternalUrls ExternalUrls { get; set; }
    public object[] Genres { get; set; }
    public string Href { get; set; } = string.Empty;
    public string Id { get; set; } = string.Empty;
    public Image[] Images { get; set; }
    public string Name { get; set; } = string.Empty;
    public long Popularity { get; set; }
    public string ReleaseDate { get; set; } = string.Empty;
    public string ReleaseDatePrecision { get; set; } = string.Empty;
    public Tracks Tracks { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Uri { get; set; } = string.Empty;
}

public class Tracks
{
    public string Href { get; set; } = string.Empty;
    public Item[] Items { get; set; }
    public long Limit { get; set; }
    public object Next { get; set; }
    public long Offset { get; set; }
    public object Previous { get; set; }
    public long Total { get; set; }
}

public class Item
{
    public Artist[] Artists { get; set; }
    public string[] AvailableMarkets { get; set; }
    public long DiscNumber { get; set; }
    public long DurationMs { get; set; }
    public bool Explicit { get; set; }
    public ExternalUrls ExternalUrls { get; set; }
    public string Href { get; set; } = string.Empty;
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string PreviewUrl { get; set; } = string.Empty;
    public long TrackNumber { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Uri { get; set; } = string.Empty;
}

public class Image
{
    public long Height { get; set; }
    public string Url { get; set; } = string.Empty;
    public long Width { get; set; }
}

public class ExternalIds
{
    public string Upc { get; set; } = string.Empty;
}

public class Copyright
{
    public string Text { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
}

public class Artist
{
    public ExternalUrls ExternalUrls { get; set; }
    public string Href { get; set; } = string.Empty;
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Uri { get; set; } = string.Empty;
}

public class ExternalUrls
{
    public string Spotify { get; set; } = string.Empty;
}
