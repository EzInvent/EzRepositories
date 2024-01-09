using EzRepositories.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzRepositories.Tests.Fixture
{
    public class UsersFixtures
    {
        public static List<User> GetTestData()
        {
            return new List<User>()
            {
                new User { Id = 1, Name = "John Doe" },
                new User { Id = 2, Name = "Jane Smith" },
                new User { Id = 3, Name = "Bob Johnson" },
                new User { Id = 4, Name = "Alice Williams" },
                new User { Id = 5, Name = "Charlie Brown" }
            };
        }
    }
}
