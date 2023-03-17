namespace CMS_DotNet_Teste_WebAPI_with_MediatR.Requests
{
    public class WeatherRequest : IHttpRequest
    {
        public string City { get; set; }
        public int Days { get; set; }
    }
}
