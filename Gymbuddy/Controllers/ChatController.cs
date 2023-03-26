using Gymbuddy.Models;
using Microsoft.AspNetCore.Mvc;

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
            var chat = _db.Chats.Where(x => x.UserSenderId == model.UserSender && x.UserReceiverId == model.UserReceiver).ToList();
            return Ok(chat);
        }
    }
}
