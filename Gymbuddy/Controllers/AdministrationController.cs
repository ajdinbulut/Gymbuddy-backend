using Gymbuddy.Models;
using GymBuddy.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;

namespace Gymbuddy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministrationController : Controller
    {
        private readonly DB _db;
        private readonly IUnitOfWork _unitOfWork;
        public AdministrationController(DB db,IUnitOfWork unitOfWork)
        {
            _db = db;
            _unitOfWork = unitOfWork;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("getUsers")]
        public IActionResult getUsers()
        {
            var users = _unitOfWork.User.GetAll(includeProperties: "UserRoles.Role");
            return Ok(users);
        }
    }
}
