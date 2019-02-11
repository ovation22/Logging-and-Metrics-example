using Example.Models;
using Example.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Example.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext Context;
        protected DbSet<T> DbSet;

        public Repository(ExampleContext context)
        {
            Context = context;
            DbSet = context.Set<T>();
        }
        
        public T Get<TKey>(TKey id)
        {
            return DbSet.Find(id);
        }

        public IQueryable<T> GetAll()
        {
            return DbSet;
        }
    }
}
