using CMS_DotNet_Teste_WebAPI_with_MediatR.Services;

namespace CMS_DotNet_Teste_WebAPI_with_MediatR.Requests;

public class ExempleRequest : IHttpRequest
{
    public int Age { get; set; }
    public string Name { get; set; }
    public GuidService GuidService { get; set; }
}
