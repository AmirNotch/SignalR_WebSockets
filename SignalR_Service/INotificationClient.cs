using SignalR_Common;
using System.Threading.Tasks;

namespace SignalR_Service
{
  public interface INotificationClient
  {
    Task Send(Message message);
  }
}
