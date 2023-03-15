using Gymbuddy;
using Gymbuddy.Core.Entities;
using GymBuddy.Core.Entities;
using GymBuddy.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymBuddy.Repository
{
    public class FollowRepository : Repository<Follow>, IFollowRepository
    {
        private readonly DB _db;
        public FollowRepository(DB db) : base(db)
        {
            _db = db;

        }

        public void Update(Follow obj)
        {
            _db.Follows.Update(obj);
        }
    }
}
