using Gymbuddy.Entities;
using Gymbuddy.Models;
using Gymbuddy.Utilities;
using GymBuddy.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.CodeDom.Compiler;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Gymbuddy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class UserController : Controller
    {
        private readonly DB _db;
        private readonly IConfiguration _config;
        private readonly IUnitOfWork _unitOfWork;
        public UserController(DB db, IConfiguration config,IUnitOfWork unitOfWork) { 
            _db= db;
            _config= config;
            _unitOfWork= unitOfWork;
        }
        [HttpPost("register-user")]
        public IActionResult User([FromForm] RegisterUser mod)
        {
            User model = new User();
            UserRole userRole = new UserRole();
            model.UserName = mod.username;
            var salt = Cryptography.Salt.Create();
            var hash = Cryptography.Hash.Create(mod.password, salt);
            model.PasswordSalt = salt;
            model.PasswordHash = hash;
            model.ImageUrl = "/images/profilePhotos/profile.jpg";
            model.Age = mod.age;
            model.Email = mod.email;
            model.FirstName = mod.firstname;
            model.LastName = mod.lastname;
            _unitOfWork.User.Add(model);
            _unitOfWork.Save();
            userRole.RoleId = 1;
            userRole.UserId = model.Id;
            _unitOfWork.UserRole.Add(userRole);
            _unitOfWork.Save();
            return Ok();
               
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromForm] LoginPost postLogin)
        {
            var userAuth = Authenticate(postLogin);
            if(userAuth != null)
            {
                
                var token = Generate(userAuth);
                return Ok(token);
            }
            return Unauthorized();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {
            var user = _unitOfWork.User.GetFirstOrDefault(x => x.Id == id);
            if (user != null)
            {
                _unitOfWork.User.Remove(user);
                _unitOfWork.Save();
                return Ok();
            }
            return NotFound();
        }
        [HttpPut("Edit")]
        public IActionResult Edit([FromForm]EditUser editAcc)
        {
            var user = _db.Users.Find(editAcc.Id);
            user.FirstName = editAcc.Firstname;
            user.LastName = editAcc.Lastname;
            user.UserName = editAcc.Username;
            user.Email = editAcc.Email;
            user.Age = editAcc.Age;
            _db.Users.Update(user);
            _db.SaveChanges();
            return Ok(user);
        }

        private object Generate(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var roles = _unitOfWork.UserRole.GetAll(includeProperties:"Role").Where(x => x.UserId == user.Id);
            var claims = new List<Claim>();
                claims.Add(new Claim("Email", user.Email));
                claims.Add(new Claim("Lastname", user.LastName));
                claims.Add(new Claim("Username", user.UserName));
                claims.Add(new Claim("FirstName", user.FirstName));
                foreach(var item in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, item.Role.Name));
            };
                claims.Add(new Claim("Id", user.Id.ToString()));
                claims.Add(new Claim("ProfilePhoto", user.ImageUrl));

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Audience"],
              claims,
              expires: DateTime.Now.AddMinutes(240),
              signingCredentials: credentials) ;

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private User Authenticate(LoginPost model)
        {
            var user = _db.Users.FirstOrDefault(x=>x.UserName == model.username );
            if(user != null && Cryptography.Hash.Validate(model.password, user.PasswordSalt, user.PasswordHash))
            {
                return user;
            }
            return null;
        }

    }
}
