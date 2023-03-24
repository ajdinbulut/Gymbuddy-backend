using GymBuddy.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Gymbuddy.Hubs
{
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
        public JwtPayload GetPayload()
        {
            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJFbWFpbCI6ImFqZGluYnVsdXRAZ21haWwuY29tIiwiTGFzdG5hbWUiOiJCdWx1dCIsIlVzZXJuYW1lIjoiYWRtaW4iLCJGaXJzdE5hbWUiOiJBamRpbiIsIlJvbGVzIjoiU3lzdGVtLkNvbGxlY3Rpb25zLkdlbmVyaWMuTGlzdGAxW0d5bWJ1ZGR5LkVudGl0aWVzLlVzZXJSb2xlXSIsIklkIjoiMSIsIlByb2ZpbGVQaG90byI6Imh0dHBzOi8vbG9jYWxob3N0OjcwMTAvL2ltYWdlcy9wcm9maWxlUGhvdG9zL3Byb2ZpbGVwaG90by5qcGciLCJleHAiOjE2Nzk0NTIxNjEsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjQ0MzExLyIsImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0OjQ0MzExLyJ9.2Qmo24PopEoCm1GUsfK7EBRUOaa-eJBtJqdeCVbNsto";
            var handler = new JwtSecurityTokenHandler();
            var tokenData = handler.ReadJwtToken(token);
            return tokenData.Payload;
        }
        public override Task OnConnectedAsync()
        {

            var connectionId = Context.ConnectionId;
            var mod = _contextAccessor.HttpContext.Items;
            var token = GetPayload();
            var obj = token.FirstOrDefault(x => x.Key == "Id").Value;
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

