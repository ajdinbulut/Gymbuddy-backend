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
    public class ChatRepository : Repository<Chat>, IChatRepository
    {
        private readonly DB _db;
       public ChatRepository(DB db) : base(db)
        {
            _db = db;
        }
      
        public void Update(Chat obj)
        {
            _db.Chats.Update(obj);
        }
    }
}
