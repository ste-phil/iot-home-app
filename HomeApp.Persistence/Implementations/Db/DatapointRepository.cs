using System.Linq.Expressions;
using HomeApp.Library.Entities;
using Microsoft.EntityFrameworkCore;

namespace HomeApp.Persistence 
{
    public class DatapointRepository : IDatapointRepository
    {
        private DbContext context;

        public DatapointRepository(DbContext context)
        {
            this.context = context;
        }

        public void Add(DataPoint datapoint) 
        {
            context.Set<DataPoint>().Add(datapoint);
        }

        public List<DataPoint> GetRange(DateTime from, DateTime to, string? roomId = null, DataPointType? type = null) 
        {
            return context
                .Set<DataPoint>()
                .OrderBy(x => x.Timestamp)
                .Where(x => x.Timestamp >= from && x.Timestamp <= to 
                    && (!type.HasValue || type == x.Type) 
                    && (roomId != null || roomId == x.RoomId))
                .ToList();
        }

        public List<DataPoint> GetRangeFromLast(TimeSpan span, string? roomId = null, DataPointType? type = null) 
        {
            var latest = context.Set<DataPoint>().OrderByDescending(x => x.Timestamp).First().Timestamp;

            return context
                .Set<DataPoint>()
                .OrderBy(x => x.Timestamp)
                .Where(x => x.Timestamp >= latest.Add(-span) && x.Timestamp <= latest 
                    && (!type.HasValue || type == x.Type) 
                    && (roomId != null || roomId == x.RoomId))
                .ToList();
        }

        public void DeleteRange(DateTime from, DateTime to, string? roomId = null, DataPointType? type = null) 
        {
            var elems = GetRange(from, to, roomId, type);
            context.Set<DataPoint>().RemoveRange(elems);
        }

        public List<DataPoint> Get(Expression<Func<DataPoint, bool>> filter = null)
        {
            var query = context.Set<DataPoint>().AsQueryable();
            if (filter != null) query = query.Where(filter);

            return query.ToList();
        }
    }
}