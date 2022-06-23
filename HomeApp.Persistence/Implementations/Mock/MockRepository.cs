using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HomeApp.Persistence
{
    public class MockRepository<T, TKey> : IGenericRepository<T, TKey> where T : class
    {
        private List<T> data;

        public MockRepository(List<T> data)
        {
            this.data = data;
        }

        public MockRepository(params T[] data)
        {
            this.data = data.ToList();
        }

        public TKey Add(T obj)
        {
            throw new NotImplementedException();
        }

        public int Count(Expression<Func<T, bool>> filter = null)
        {
            return data.Count;
        }

        public bool Delete(T obj)
        {
            return data.Remove(obj);
        }

        public bool Delete(TKey id)
        {
            throw new NotImplementedException();
        }

        public void DeleteAll()
        {
            data.Clear();
        }

        public void DeleteRange(IEnumerable<T> objs)
        {
            for(int i = 0; i < objs.Count(); i++) {
                data.Remove(objs.ElementAt(i));
            }
        }

        public T Detach(T obj)
        {
            throw new NotImplementedException();
        }

        public T First(Expression<Func<T, bool>> filter = null)
        {
            return data.First();
        }

        public List<T> Get(Expression<Func<T, bool>> filter = null, Func<DbSet<T>, IQueryable<T>> settings = null)
        {
            if (filter == null) return data;
            return data.Where(filter.Compile()).ToList();
        }

        public List<T> Get<TProperty>(Expression<Func<T, bool>> filter = null, Expression<Func<T, TProperty>> include = null) where TProperty : class
        {
            throw new NotImplementedException();
        }

        public T GetByKey(TKey id)
        {
            return data.First(x => typeof(T).GetProperty("Id").GetValue(x).Equals(id));
        }
    }
}
