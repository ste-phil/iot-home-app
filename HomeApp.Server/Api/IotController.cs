using HomeApp.Library.Entities;
using HomeApp.Persistence;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace HomeApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IotController : ControllerBase
    {
        public class ReceiveData 
        {
            public string RoomName { get; set; }
            public float? Battery { get; set; }
            public float? Temperature { get; set; }
            public float? Humidity { get; set; }
            public int? Co2 { get; set; }
        }

        private readonly IRepositoryService repo;

        public IotController(IRepositoryService repo)
        {
            this.repo = repo;
        }

        [HttpPost]
        public void Publish([FromBody] ReceiveData data)
        {
            var now = DateTime.UtcNow;
            now = now.AddTicks(-(now.Ticks % TimeSpan.TicksPerMinute)); //remove ms and s from time

            var room = repo.Rooms.First(x => x.Name == data.RoomName);
            if (room == null) {
                room = new Room {
                    Name = data.RoomName
                };

                repo.Rooms.Add(room);
                repo.Save();
            }
        
            if (data.Temperature.HasValue) {
                repo.Datapoints.Add(new DataPoint 
                { 
                    Id = now,
                    Type = DataPointType.Temperature,
                    RoomId = room.Id,
                    Value = data.Temperature.Value
                });
            }
            
            if (data.Co2.HasValue) {
                repo.Datapoints.Add(new DataPoint 
                { 
                    Id = now,
                    Type = DataPointType.Co2,
                    RoomId = room.Id,
                    Value = data.Co2.Value
                });
            }

            if (data.Humidity.HasValue) {
                repo.Datapoints.Add(new DataPoint 
                { 
                    Id = now,
                    Type = DataPointType.Humidity,
                    RoomId = room.Id,
                    Value = data.Humidity.Value
                });
            }

             if (data.Battery.HasValue) {
                repo.Datapoints.Add(new DataPoint
                { 
                    Id = now,
                    Type = DataPointType.Battery,
                    RoomId = room.Id,
                    Value = data.Battery.Value
                });
            }
        }

    }
}