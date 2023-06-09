﻿using Gymbuddy.Core.Entities;
using Gymbuddy.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymBuddy.Repository.IRepository
{
    public interface IRoleRepository : IRepository<Role>
    {
        void Update(Role obj);
    }
}
