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
        private readonly ILogger logger;

        public IotController(ILogger logger, IRepositoryService repo)
        {
            this.repo = repo;
            this.logger = logger;
        }

        [HttpPost]
        public void Create([FromBody] ReceiveData data)
        {
            var now = DateTime.Now;
            now = now.AddTicks(-(now.Ticks % TimeSpan.TicksPerSecond)); //remove ms and s from time

            var room = repo.Rooms.First(x => x.Name == data.RoomName);
            if (room == null) {
                logger.Log(LogLevel.Information, $"Received data for Room {data.RoomName} which does not exist!");
                return;
            }
        
            if (data.Temperature.HasValue) {
                repo.TemperatureLevels.Add(new Temperature 
                { 
                    RoomId = room.Id,
                    Value = data.Temperature.Value
                });
            }
            
            if (data.Co2.HasValue) {
                repo.Co2Levels.Add(new Co2 
                { 
                    RoomId = room.Id,
                    Value = data.Co2.Value
                });
            }

            if (data.Humidity.HasValue) {
                repo.HumidityLevels.Add(new Humidity 
                { 
                    RoomId = room.Id,
                    Value = data.Humidity.Value
                });
            }

             if (data.Battery.HasValue) {
                repo.BatteryLevels.Add(new Battery
                { 
                    RoomId = room.Id,
                    Value = data.Battery.Value
                });
            }
        }

    }
}