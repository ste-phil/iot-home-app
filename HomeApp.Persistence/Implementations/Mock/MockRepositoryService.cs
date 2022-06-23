using HomeApp.Library.Entities;
using Microsoft.EntityFrameworkCore;

namespace HomeApp.Persistence
{
    public class MockRepositoryService : IRepositoryService, IDisposable
    {
        public IGenericRepository<Room, string> Rooms => new MockRepository<Room, string>(
            new Room { Id = "#1", Name = "Roof"}
        );

        public IGenericRepository<Temperature, DateTime> TemperatureLevels => new MockRepository<Temperature, DateTime>(
            new Temperature { Id = new DateTime(2022, 06, 01), RoomId = "#1", Value = 10.5f},
            new Temperature { Id = new DateTime(2022, 06, 02), RoomId = "#1", Value = 11.5f},
            new Temperature { Id = new DateTime(2022, 06, 03), RoomId = "#1", Value = 14.5f}
        );

        public IGenericRepository<Co2, DateTime> Co2Levels => new MockRepository<Co2, DateTime>(
            new Co2 { Id = new DateTime(2022, 06, 01), RoomId = "#1", Value = 10.5f}
        );

        public IGenericRepository<Humidity, DateTime> HumidityLevels => new MockRepository<Humidity, DateTime>(
            new Humidity { Id = new DateTime(2022, 06, 01), RoomId = "#1", Value = 0.3f}
        );

        public IGenericRepository<Battery, DateTime> BatteryLevels => new MockRepository<Battery, DateTime>(
            new Battery { Id = new DateTime(2022, 06, 01), RoomId = "#1", Value = 1.0f}
        );


        public void Dispose()
        {
            Save();
        }

        public void Save()
        {
        }
    }
}
