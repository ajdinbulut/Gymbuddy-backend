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
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        private readonly DB _db;
        public RoleRepository(DB db) : base(db)
        {
            _db = db;

        }
      
        public void Update(Role obj)
        {
            _db.Roles.Update(obj);
        }
    }
}
