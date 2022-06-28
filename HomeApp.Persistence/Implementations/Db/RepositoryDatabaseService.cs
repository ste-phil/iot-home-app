using HomeApp.Library.Entities;
using Microsoft.EntityFrameworkCore;

namespace HomeApp.Persistence
{
    public class RepositoryDatabaseService : IRepositoryService, IDisposable
    {
        public readonly AppDbContext context;

        private IGenericRepository<Room, string>? roomRepository;
        private IDatapointRepository? datapointRepository;

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


        public IDatapointRepository Datapoints
        {
            get
            {
                if (datapointRepository == null)
                    datapointRepository = new DatapointRepository(context);
                return datapointRepository;
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
