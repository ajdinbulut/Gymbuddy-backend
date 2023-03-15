using Gymbuddy.Core.Entities;
using Gymbuddy.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymBuddy.Repository.IRepository
{
    public interface IUserRoleRepository : IRepository<UserRole>
    {
        public void Update(UserRole obj);
    }
}
