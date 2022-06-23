using HomeApp.Library.Entities;
using Microsoft.EntityFrameworkCore;

namespace HomeApp.Persistence
{
    public class RepositoryDatabaseService : IRepositoryService, IDisposable
    {
        public readonly AppDbContext context;

        private IGenericRepository<Room, string>? roomRepository;
        private IGenericRepository<Temperature, DateTime>? temperatureRepository;
        private IGenericRepository<Co2, DateTime>? co2Repository;
        private IGenericRepository<Humidity, DateTime>? humidityRepository;
        private IGenericRepository<Battery, DateTime>? batteryRepository;

        public RepositoryDatabaseService(AppDbContext context)
        {
            this.context = context;
            this.context.Database.Migrate();
        }

        public IGenericRepository<Room, string> Rooms
        {
            get
            {
                if (roomRepository == null)
                    roomRepository = new GenericRepository<Room, string>(context);
                return roomRepository;
            }
        }
        public IGenericRepository<Temperature, DateTime> TemperatureLevels
        {
            get
            {
                if (temperatureRepository == null)
                    temperatureRepository = new GenericRepository<Temperature, DateTime>(context);
                return temperatureRepository;
            }
        }

        public IGenericRepository<Co2, DateTime> Co2Levels
        {
            get
            {
                if (co2Repository == null)
                    co2Repository = new GenericRepository<Co2, DateTime>(context);
                return co2Repository;
            }
        }

        public IGenericRepository<Humidity, DateTime> HumidityLevels
        {
            get
            {
                if (humidityRepository == null)
                    humidityRepository = new GenericRepository<Humidity, DateTime>(context);
                return humidityRepository;
            }
        }



        public IGenericRepository<Battery, DateTime> BatteryLevels
        {
            get
            {
                if (batteryRepository == null)
                    batteryRepository = new GenericRepository<Battery, DateTime>(context);
                return batteryRepository;
            }
        }

        public void Dispose()
        {
            Save();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
