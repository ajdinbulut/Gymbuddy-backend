using Gymbuddy;
using Gymbuddy.Core.Entities;
using GymBuddy.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymBuddy. Repository
{
    public class UserCountryRepository : Repository<UserCountry>, IUserCountryRepository
    {
        private readonly DB _db;
       public UserCountryRepository(DB db) : base(db)
        {
            _db = db;
        }
      
        public void Update(UserCountry obj)
        {
            _db.UserCountries.Update(obj);
        }
    }
}
