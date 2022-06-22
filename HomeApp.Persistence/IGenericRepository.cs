using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HomeApp.Persistence
{
    public interface IGenericRepository<T, TKey> where T : class
    {
        TKey Add(T obj);
        int Count(Expression<Func<T, bool>> filter = null);
        bool Delete(T obj);
        bool Delete(TKey id);
        void DeleteAll();
        void DeleteRange(IEnumerable<T> objs);
        T Detach(T obj);
        T First(Expression<Func<T, bool>> filter = null);
        List<T> Get(Expression<Func<T, bool>> filter = null, Func<DbSet<T>, IQueryable<T>> settings = null);
        List<T> Get<TProperty>(Expression<Func<T, bool>> filter = null, Expression<Func<T, TProperty>> include = null) where TProperty : class;
        T GetByKey(TKey id);
    }
}