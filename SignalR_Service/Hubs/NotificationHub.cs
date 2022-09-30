using System;
using Microsoft.AspNetCore.SignalR;
using SignalR_Common;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SignalR_Service.Hubs
{
  public class NotificationHub : Hub<INotificationClient>
  {
    public Task SendMessage(Message message)
    {
      Debug.WriteLine(Context.ConnectionId);

      if (Context.Items.ContainsKey("user_name"))
        message.Title = $"Message from user: {Context.Items["user_name"]}";

      return Clients.Others.Send(message);
    }

    public Task SetName(string name)
    {
      Context.Items.TryAdd("user_name", name);

      return Task.CompletedTask;
    }

    protected override void Dispose(bool disposing)
    {
      Debug.WriteLine("Hub disposed");
      
      base.Dispose(disposing);
    }

    public override Task OnConnectedAsync()
    {
      var message = new Message()
      {
        Title = $"New client connected {Context.ConnectionId}",
        Body = string.Empty
      };

      Clients.Others.Send(message);  
      return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception exception)
    {
      var message = new Message()
      {
        Title = $"client disconnected {Context.ConnectionId}",
        Body = string.Empty
      };
      
      Clients.Others.Send(message);
      return base.OnDisconnectedAsync(exception);
    }
  }
}
