using GymBuddy.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Gymbuddy.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly DB _db;
        private readonly IHttpContextAccessor _contextAccessor;
        public ChatHub(DB db, IHttpContextAccessor contextAccessor)
        {
            _db = db;
            _contextAccessor = contextAccessor;
        }
        public string GetConnectionId() => Context.ConnectionId;

        public override Task OnConnectedAsync()
        {
            var connectionId = Context.ConnectionId;
            var obj = Context.User.FindFirst("Id").Value;
            var userId = Convert.ToInt32(obj);
            Connection model = new Connection();
            model.ConnectionId = connectionId;
            model.UserId = userId;
            _db.Connection.Add(model);
            _db.SaveChanges();

            return base.OnConnectedAsync();
        }
    }
}

