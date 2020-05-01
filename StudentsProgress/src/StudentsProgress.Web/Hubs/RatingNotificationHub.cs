using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using StudentsProgress.Web.Models;

namespace StudentsProgress.Web.Hubs
{
    public class RatingNotificationHub : Hub
    {
        public async Task SendNotification(string userId, RatingViewModel rating)
        {
            await Clients.User(userId).SendAsync("ReceiveNotification", rating);
        }
    }
}
