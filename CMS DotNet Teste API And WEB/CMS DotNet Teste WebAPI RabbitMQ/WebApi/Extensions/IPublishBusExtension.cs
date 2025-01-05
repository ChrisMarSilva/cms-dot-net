using MassTransit;

namespace RabbitMQ.Api.Extensions;

//internal interface IPublishBusExtension
//{
//    Task SendAsync<T>(string queue, T message, CancellationToken cancellationToken = default) where T : class;
//}

public static class Extensions// internal class PublishBus : IPublishBusExtension
{
    //private readonly ISendEndpointProvider _sendEndpointProvider;

    //public PublishBus(ISendEndpointProvider sendEndpointProvider)
    //{
    //    _sendEndpointProvider = sendEndpointProvider;
    //}

    public static async Task SendAsync<T>(this ISendEndpointProvider sendEndpointProvider, string queue, T message, CancellationToken cancellationToken = default) where T : class
    {
        var endpoint = await sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{queue}")).ConfigureAwait(continueOnCapturedContext: false);

        //var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{queueName}"));
        //await endpoint.Send(new Message { Text = message.Text });

        //var timeout = TimeSpan.FromSeconds(30);
        //using var source = new CancellationTokenSource(timeout);
        //await endpoint.Send(new SubmitOrder { OrderId = "123" }, source.Token);

        //var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{queueName}"));
        //await endpoint.Send(message);

        //await endpoint.Send(message, delegate (SendContext<T> c)
        //        {
        //            c.Durable = true;
        //            //c.TimeToLive = TimeSpan.FromMinutes(5);
        //            //c.Headers.Set("Idempotency-Key", idempotencyKey);
        //            //c.MessageId = commandId;
        //        }, 
        //        cancellationToken)
        //    .ConfigureAwait(continueOnCapturedContext: false);

        await endpoint
            .Send(message, cancellationToken)
            .ConfigureAwait(continueOnCapturedContext: false);
    }
}