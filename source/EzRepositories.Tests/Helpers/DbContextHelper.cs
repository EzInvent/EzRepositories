using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzRepositories.Tests.Helpers
{
    public class DbContextHelper
    {

        public static TestDbContext CreateInMemoryDbContext()
        {
            var options = new Microsoft.EntityFrameworkCore.DbContextOptionsBuilder<TestDbContext>()
                .UseInMemoryDatabase(databaseName: $"TestDatabase-{Guid.NewGuid()}")
                .Options;

            return new TestDbContext(options);
        }
    }
}
