using GymBuddy.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gymbuddy.Controllers
{
      [Route("api/[controller]")]
      [ApiController]
    public class ProfileController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DB _db;
        public ProfileController(IUnitOfWork unitOfWork,DB db)
        {
            _unitOfWork = unitOfWork;
            _db = db;
        }
        [HttpGet("GetPostsById")]
        public IActionResult ById(int Id)
        {
            var usersPosts = _db.Posts.Where(x=>x.UserId == Id).ToList();
            return Ok(usersPosts);
        }
    }
}
