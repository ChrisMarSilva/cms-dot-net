using CMS_DotNet_Teste_WebAPI_with_MediatR.Requests;
using MediatR;

namespace CMS_DotNet_Teste_WebAPI_with_MediatR.Extensions;

public static class MinimalatrExtensions
{
    public static WebApplication MediateGet<TResquest>(this WebApplication app, string template) where TResquest : IHttpRequest
    {
        app.MapGet(template, async (IMediator mediator, [AsParameters] TResquest request) => await mediator.Send(request));
        return app;
    }

    public static WebApplication MediatePost<TResquest>(this WebApplication app, string template) where TResquest : IHttpRequest
    {
        app.MapPost(template, async (IMediator mediator, [AsParameters] TResquest request) => await mediator.Send(request));
        return app;
    }

    public static WebApplication MediatePut<TResquest>(this WebApplication app, string template) where TResquest : IHttpRequest
    {
        app.MapPut(template, async (IMediator mediator, [AsParameters] TResquest request) => await mediator.Send(request));
        return app;
    }

    public static WebApplication MediatePatch<TResquest>(this WebApplication app, string template) where TResquest : IHttpRequest
    {
        app.MapPatch(template, async (IMediator mediator, [AsParameters] TResquest request) => await mediator.Send(request));
        return app;
    }

    public static WebApplication MediateDelete<TResquest>(this WebApplication app, string template) where TResquest : IHttpRequest
    {
        app.MapDelete(template, async (IMediator mediator, [AsParameters] TResquest request) => await mediator.Send(request));
        return app;
    }
}
