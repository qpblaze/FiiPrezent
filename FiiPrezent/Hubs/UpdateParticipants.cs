using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace FiiPrezent.Hubs
{
    public class UpdateParticipants : Hub
    {
        public override async Task OnConnectedAsync()
        {
            string eventId = Context.Connection.GetHttpContext().Request.Query["eventId"];

            await Groups.AddAsync(Context.ConnectionId, eventId);

            await base.OnConnectedAsync();
        }
    }
}