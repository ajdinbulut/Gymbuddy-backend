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
    public class UserRoleRepository : Repository<UserRole>, IUserRoleRepository
    {
        private readonly DB _db;
        public UserRoleRepository(DB db) : base(db)
        {
            _db = db;
        }
      
        public void Update(UserRole obj)
        {
            _db.UserRoles.Update(obj);
        }
    }
}
