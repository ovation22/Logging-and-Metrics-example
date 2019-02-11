using System.Linq;

namespace Example.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        T Get<TKey>(TKey id);
        IQueryable<T> GetAll();
    }
}
