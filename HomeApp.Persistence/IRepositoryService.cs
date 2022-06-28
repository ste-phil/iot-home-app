using HomeApp.Library.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace HomeApp.Persistence
{
    public interface IRepositoryService
    {
        IGenericRepository<Room, string> Rooms { get; }
        // IGenericRepository<Temperature, DateTime> TemperatureLevels { get; }
        // IGenericRepository<Co2, DateTime> Co2Levels { get; }
        // IGenericRepository<Humidity, DateTime> HumidityLevels { get; }
        // IGenericRepository<Battery, DateTime> BatteryLevels { get; }
        IDatapointRepository Datapoints { get; }

        void Save();
    }

}