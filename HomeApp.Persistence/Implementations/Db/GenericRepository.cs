using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HomeApp.Persistence
{
    public class GenericRepository<T, TKey> : IGenericRepository<T, TKey> where T : class
    {
        internal AppDbContext dbContext;

        internal GenericRepository(AppDbContext context)
        {
            dbContext = context;
        }

        public bool Delete(T obj)
        {
            var ent = dbContext.Find<T>(obj);
            if (ent != null) {
                dbContext.Remove<T>(ent);
                return true;
            }

            return false;
        }

        public bool Delete(TKey id)
        {
            var ent = dbContext.Find<T>(id);
            if (ent != null) {
                dbContext.Remove<T>(ent);
                return true;
            }

            return false;
        }

        public void DeleteRange(IEnumerable<T> objs)
        {
            dbContext.Set<T>().RemoveRange(objs);
        }

        public void DeleteAll()
        {
            DbSet<T> set = dbContext.Set<T>();
            set.RemoveRange(set);
        }

        public List<T> Get<TProperty>(Expression<Func<T, bool>> filter = null, Expression<Func<T, TProperty>> include = null) where TProperty : class
        {
            IQueryable<T> query;
            if (include != null)
                query = dbContext.Set<T>().Include(include).AsQueryable();
            else
                query = dbContext.Set<T>().AsQueryable();
            if (filter != null) query = query.Where(filter);

            return query.ToList();
        }

        public List<T> Get(Expression<Func<T, bool>> filter = null, Func<DbSet<T>, IQueryable<T>> settings = null)
        {
            IQueryable<T> query;
            if (settings != null)
                query = settings(dbContext.Set<T>());
            else
                query = dbContext.Set<T>().AsQueryable();
            if (filter != null) query = query.Where(filter);

            return query.ToList();
        }

        public T First(Expression<Func<T, bool>> filter = null)
        {
            return dbContext.Set<T>().AsQueryable().FirstOrDefault(filter);
        }

        //public T Create(T obj)
        //{
        //    var dbObj = dbContext.Set<T>().Add(obj);

        //    var propInfo = typeof(T).GetProperties();
        //    foreach (var prop in propInfo)
        //    {
        //        if (prop.CanWrite) prop.SetValue(dbObj, prop.GetValue(obj, null), null);
        //    }
        //    dbContext.Set<T>().Add(dbObj);
        //    return dbObj;
        //}

        public TKey Add(T obj)
        {
            dbContext.Set<T>().Attach(obj);
            dbContext.Set<T>().Add(obj);

            return (TKey) obj.GetType().GetProperty("Id")?.GetValue(obj);
        }

        //public T Attach(T obj)
        //{
        //    var dbProxy = dbContext.Set<T>().Create();

        //    var propInfo = typeof(T).GetProperties();
        //    foreach (var prop in propInfo)
        //    {
        //        if (prop.CanWrite) prop.SetValue(dbProxy, prop.GetValue(obj, null), null);
        //    }

        //    dbContext.Entry<T>(dbProxy).State = EntityState.Modified;

        //    return dbProxy;
        //}

        public T Detach(T obj)
        {
            dbContext.Entry<T>(obj).State = EntityState.Detached;
            return obj;
        }

        public T GetByKey(TKey id)
        {
            return dbContext.Set<T>().Find(id);
        }

        public int Count(Expression<Func<T, bool>> filter = null)
        {
            return dbContext.Set<T>().Count(filter);
        }
    }
}
