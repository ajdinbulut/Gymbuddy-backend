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
    public class PostLikesRepository : Repository<PostLikes>, IPostLikesRepository
    {
        private readonly DB _db;
       public PostLikesRepository(DB db) : base(db)
        {
            _db = db;
        }
      
        public void Update(PostLikes obj)
        {
            _db.PostLikes.Update(obj);
        }
    }
}
