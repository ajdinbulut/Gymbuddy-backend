using Gymbuddy;
using Gymbuddy.Core.Entities;
using Gymbuddy.Entities;
using GymBuddy.Core.Entities;
using GymBuddy.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymBuddy.Repository
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        private readonly DB _db;
       public PostRepository(DB db) : base(db)
        {
            _db = db;
        }
      
        public void Update(Post obj)
        {
            _db.Posts.Update(obj);
        }
    }
}
