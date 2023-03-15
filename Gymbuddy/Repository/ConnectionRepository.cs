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
    public class ConnectionRepository : Repository<Connection>, IConnectionRepository
    {
        private readonly DB _db;
       public ConnectionRepository(DB db) : base(db)
        {
            _db = db;
        }
      
        public void Update(Connection obj)
        {
            _db.Connection.Update(obj);
        }
    }
}
