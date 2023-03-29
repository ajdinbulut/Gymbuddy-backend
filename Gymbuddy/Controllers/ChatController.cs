using Gymbuddy.Models;
using GymBuddy.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gymbuddy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : Controller
    {
        private readonly DB _db;
        public ChatController(DB db)
        {
            _db = db;
        }
        [HttpGet("getChat")]
        public IActionResult GetChat([FromQuery]ChatModel model)
        {
            var chat = _db.Chats.Include(x => x.UserReceiver).Include(x => x.UserSender).Where(x => x.UserSenderId == model.UserSender && x.UserReceiverId == model.UserReceiver);
            return Ok(chat);
        }
        [HttpPost("add")]
        public IActionResult Add(MessageModel model)
        {
            Chat obj = new Chat();
            obj.UserSenderId = model.UserSender;
            obj.UserReceiverId = model.UserReceiver;
            obj.Message = model.Message;
            obj.IsSeen = false;
            obj.SentAt = DateTime.Now;
            _db.Chats.Add(obj);
            _db.SaveChanges();
            var rtrn = _db.Chats.Include(x => x.UserSender).FirstOrDefault(x => x.SentAt == obj.SentAt);
            return Ok(rtrn);
        }
    }
}
