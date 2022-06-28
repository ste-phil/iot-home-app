using HomeApp.Library.Entities;
using HomeApp.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace HomeApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DatapointController : ControllerBase
    {
        private readonly IRepositoryService repo;

        public DatapointController(IRepositoryService repo)
        {
            this.repo = repo;
        }

        [HttpGet("{type}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get(string type)
        {
            if (!Enum.TryParse<DataPointType>(type, out var typeEnum)) return NotFound();

            return Ok(repo.Datapoints.Get(x => x.Type == typeEnum).Select(x => x.ToDto()));
        }

        [HttpPost]
        public void Create([FromForm]DataPointDto dto)
        {
            var now = DateTime.UtcNow;
            now = now.AddTicks(-(now.Ticks % TimeSpan.TicksPerSecond)); //remove ms and s from time

            repo.Datapoints.Add(new DataPoint {
                Id = now,
                Type = dto.Type,
                RoomId = dto.RoomId,
                Value = dto.Value
            });
        }

        // [HttpDelete("{roomId}/{type}/{timestamp}")]
        // public IActionResult Delete(string roomId, DateTime timestamp)
        // {
        //     var temp = new DataPoint { RoomId = roomId, Id = timestamp, Type = DataPointType.Temperature };
        //     if (repo.Datapoints.Delete(temp))
        //         return Ok();
        //     return NotFound();
        // }

        [HttpDelete("delete/{roomId}/{type}")]
        public IActionResult DeleteRange(string roomId, DataPointType type, DateTime from, DateTime to) {
            repo.Datapoints.DeleteRange(from, to, roomId, type);

            return Ok();
        }

        [HttpDelete("delete/{type}")]
        public IActionResult DeleteRange(DataPointType type, DateTime from, DateTime to) {
            repo.Datapoints.DeleteRange(from, to, type: type);

            return Ok();
        }
    }
}