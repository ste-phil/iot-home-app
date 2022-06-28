using HomeApp.Library.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HomeApp.Persistence
{
    public interface IDatapointRepository
    {
        void Add(DataPoint datapoint);
        List<DataPoint> GetRange(DateTime from, DateTime to, string? roomId = null, DataPointType? type = null);
        List<DataPoint> GetRangeFromLast(TimeSpan span, string? roomId = null, DataPointType? type = null);
        void DeleteRange(DateTime from, DateTime to, string? roomId = null, DataPointType? type = null);
        List<DataPoint> Get(Expression<Func<DataPoint, bool>> filter = null);
    }
}