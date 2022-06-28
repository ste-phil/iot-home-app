using HomeApp.Library.Entities;
using Microsoft.EntityFrameworkCore;

namespace HomeApp.Persistence
{
    public class MockRepositoryService : IRepositoryService, IDisposable
    {
        public IGenericRepository<Room, string> Rooms => new MockRepository<Room, string>(
            new Room { Id = "#1", Name = "Roof"}
        );

        public IDatapointRepository Datapoints => new MockDatapointRepository(
            new DataPoint { Id = new DateTime(2022, 06, 01, 13, 10, 0), RoomId = "#1", Type = DataPointType.Temperature, Value = 29.0f},
            new DataPoint { Id = new DateTime(2022, 06, 01, 13, 11, 0), RoomId = "#1", Type = DataPointType.Temperature, Value = 30.2f},
            new DataPoint { Id = new DateTime(2022, 06, 01, 13, 12, 0), RoomId = "#1", Type = DataPointType.Temperature, Value = 30.3f},
            new DataPoint { Id = new DateTime(2022, 06, 01, 13, 09, 0), RoomId = "#1", Type = DataPointType.Temperature, Value = 30.6f},
            new DataPoint { Id = new DateTime(2022, 06, 01, 15, 19, 0), RoomId = "#1", Type = DataPointType.Temperature, Value = 32.6f}
        );

        public void Dispose()
        {
        }

        public void Save()
        {
        }
    }
}
