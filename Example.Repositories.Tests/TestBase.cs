using System.Collections.Generic;
using System.Linq;
using Example.Models;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Example.Repositories.Tests
{
    public class TestBase
    {
        protected Repository<Thing> Repository;
        protected Mock<DbSet<Thing>> MockSet { get; }
        protected Mock<ExampleContext> MockContext { get; }

        public TestBase()
        {
            var things = new List<Thing>().AsQueryable();
            var options = new DbContextOptions<ExampleContext>();

            MockSet = new Mock<DbSet<Thing>>();
            MockSet.As<IQueryable<Thing>>().Setup(x => x.Provider).Returns(things.Provider);
            MockContext = new Mock<ExampleContext>(options);
            MockContext.Setup(c => c.Set<Thing>()).Returns(MockSet.Object);

            Repository = new Repository<Thing>(MockContext.Object);
        }

        public class Thing : DbSet<Thing>
        {
        }
    }
}
