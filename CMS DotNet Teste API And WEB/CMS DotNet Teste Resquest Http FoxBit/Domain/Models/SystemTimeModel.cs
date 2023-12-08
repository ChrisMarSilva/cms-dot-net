namespace CMS_DotNet_Teste_Resquest_Http_FoxBit.Domain.Models;

public class SystemTimeModel
{
    public SystemTimeModel()
    {

    }

    public SystemTimeModel(string type, DateTime iso, long timestamp) : this()
    {
        this.type = type;
        this.iso = iso;
        this.timestamp = timestamp;
    }

    public string type { get; set; } = string.Empty;
    public DateTime iso { get; set; }
    public long timestamp { get; set; }

    //public override string ToString()
    //{
    //    return "";
    //}
}
