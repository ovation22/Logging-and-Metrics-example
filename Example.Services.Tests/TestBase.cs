using Example.Models;

namespace Example.Services.Tests
{
    public class TestBase
    {
        protected HorseService HorseService;
        protected FakeRepository<Horse> Repository;

        public TestBase()
        {
            Repository = new FakeRepository<Horse>();
            HorseService = new HorseService(Repository);

            Repository.DataSet.Add(new Horse());
        }
    }
}
