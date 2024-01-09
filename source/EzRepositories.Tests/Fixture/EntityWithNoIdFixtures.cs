using Bogus;
using EzRepositories.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzRepositories.Tests.Fixture
{
    public class EntityWithNoIdFixtures
    {
        public static List<EntityWithNoId> GetTestData()
        {
            var faker = new Faker<EntityWithNoId>()
                .RuleFor(e => e.Name, _ => _.Name.FullName())
                .RuleFor(e => e.Added, _ => _.Date.Between(DateTime.UtcNow.Subtract(TimeSpan.FromDays(14)), DateTime.UtcNow));

            return faker.Generate(20);
        }
    }
}
