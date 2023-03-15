using Gymbuddy;
using GymBuddy.Core.Entities;
using GymBuddy.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymBuddy.Repository
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        private readonly DB _db;
       public CommentRepository(DB db) : base(db)
        {
            _db = db;
        }
      
        public void Update(Comment obj)
        {
            _db.Comments.Update(obj);
        }
    }
}
