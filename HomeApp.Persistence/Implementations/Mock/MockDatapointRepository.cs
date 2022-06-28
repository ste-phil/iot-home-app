using System.Linq.Expressions;
using HomeApp.Library.Entities;

namespace HomeApp.Persistence
{
    public class MockDatapointRepository : IDatapointRepository
    {
        List<DataPoint> elems;

        public MockDatapointRepository(params DataPoint[] elems)
        {
            this.elems = elems.ToList();    
        }

        public void Add(DataPoint datapoint)
        {
            datapoint.Id = DateTime.UtcNow;
            this.elems.Add(datapoint);
        }

        public void DeleteRange(DateTime from, DateTime to, string? roomId = null, DataPointType? type = null)
        {
            elems.RemoveAll(x => x.Timestamp >= from && x.Timestamp <= to 
                    && (!type.HasValue || type == x.Type) 
                    && (roomId != null || roomId == x.RoomId));
        }

        public List<DataPoint> Get(Expression<Func<DataPoint, bool>> filter = null)
        {
            return elems
                .OrderBy(x => x.Timestamp)
                .Where(filter.Compile())
                .ToList();
        }

        public List<DataPoint> GetRange(DateTime from, DateTime to, string? roomId = null, DataPointType? type = null)
        {
            return elems
                .OrderBy(x => x.Timestamp)
                .Where(x => x.Timestamp >= from && x.Timestamp <= to 
                    && (!type.HasValue || type == x.Type) 
                    && (roomId != null || roomId == x.RoomId))
                .ToList();
        }

        public List<DataPoint> GetRangeFromLast(TimeSpan span, string? roomId = null, DataPointType? type = null) 
        {
            var latest = elems.OrderByDescending(x => x.Timestamp).First().Timestamp;

            return elems
                .OrderBy(x => x.Timestamp)
                .Where(x => x.Timestamp >= latest.Add(-span) && x.Timestamp <= latest 
                    && (!type.HasValue || type == x.Type) 
                    && (roomId != null || roomId == x.RoomId))
                .ToList();
        }
    }
}