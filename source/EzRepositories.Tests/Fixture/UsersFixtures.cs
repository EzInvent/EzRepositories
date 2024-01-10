using Bogus;
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
            var userId = 1;
            var faker = new Faker<User>()
                .RuleFor(u => u.Id, _ => userId++)
                .RuleFor(u => u.Name, _ => $"{_.Name.FirstName()} {_.Name.LastName()}");
            return faker.Generate(5);
        }
    }
}