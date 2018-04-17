using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace FiiPrezent.Infrastructure.Hubs
{
    public class ParticipantsHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            string eventId = Context.GetHttpContext().Request.Query["eventId"];

            await Groups.AddAsync(Context.ConnectionId, eventId);

            await base.OnConnectedAsync();
        }
    }
}