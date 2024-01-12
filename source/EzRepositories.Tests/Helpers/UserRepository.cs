using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzRepositories.Tests.Helpers
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(TestDbContext db) : base(db)
        {
        }
    }
}
