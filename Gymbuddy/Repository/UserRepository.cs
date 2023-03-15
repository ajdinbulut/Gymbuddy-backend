using Gymbuddy;
using Gymbuddy.Core.Entities;
using Gymbuddy.Entities;
using GymBuddy.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymBuddy.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly DB _db;
        public UserRepository(DB db) : base(db)
        {
            _db = db;
        }

     
        public void Update(User obj)
        {
            _db.Users.Update(obj);
        }
    }
}
