using System.Collections.Generic;
using System.Linq;
using Example.Repositories.Interfaces;

namespace Example.Services.Tests
{
    public class FakeRepository<T> : IRepository<T> where T : class
    {
        public IList<T> DataSet { get; } = new List<T>();

        public T Get<TKey>(TKey id)
        {
            return DataSet.FirstOrDefault();
        }

        public IQueryable<T> GetAll()
        {
            return DataSet.AsQueryable();
        }
    }
}
