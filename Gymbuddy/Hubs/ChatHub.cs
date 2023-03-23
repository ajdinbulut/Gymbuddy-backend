using GymBuddy.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace Gymbuddy.Hubs
{
    public class ChatHub : Hub
    {
        private readonly DB _db;

        public ChatHub(DB db)
        {
            _db = db;
        }
        public string GetConnectionId() => Context.ConnectionId;

        public Task OnConnectedAsync(int Id)
        {

            var connectionId = GetConnectionId();
            var user = _db.Users.Find(Id);
            Connection model = new Connection();
            model.ConnectionId = connectionId;
            model.UserId = user.Id;
            _db.Connection.Add(model);
            _db.SaveChanges();

            return base.OnConnectedAsync();
        }
    }
}

