using Microsoft.AspNetCore.SignalR;

namespace WebApplicationSignalR;

public class NotificationHub : Hub
{
    public async Task SendMessage(string user, string message)
        => await Clients.All.SendAsync("ReceiveMessage", user, message);

    //public async Task BroadcastMessage(string message)
    //{
    //    await Clients.All.ReceiveMessage(message);
    //}

    //public override async Task OnConnectedAsync()
    //{
    //    await base.OnConnectedAsync();
    //}

    //public override async Task OnDisconnectedAsync(Exception? exception)
    //{
    //    await base.OnDisconnectedAsync(exception);
    //}
}
