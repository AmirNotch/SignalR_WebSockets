using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SignalR_Common;
using SignalR_Service.Hubs;

namespace SignalR_Service.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class NotificationsController : ControllerBase
  {

    public NotificationsController()
    {
      
    }

    [HttpPost]
    public async Task<IActionResult> Push([FromBody] Message message,
      [FromServices] IHubContext<NotificationHub, INotificationClient> hubContext)
    {
      await hubContext.Clients.All.Send(message);

      return Ok();
    }
  }
}
