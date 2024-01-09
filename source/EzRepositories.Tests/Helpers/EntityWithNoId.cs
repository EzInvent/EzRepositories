using EzRepositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzRepositories.Tests.Helpers
{
    public class EntityWithNoId : IEntity
    {
        public string Name { get; set; }
        public DateTime Added { get; set; }
    }
}
