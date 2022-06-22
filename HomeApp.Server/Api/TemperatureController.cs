using HomeApp.Library.Entities;
using HomeApp.Persistence;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HomeApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TemperatureController : ControllerBase
    {
        private readonly IRepositoryService repo;

        public TemperatureController(IRepositoryService repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public IEnumerable<DataPointDto> Get()
        {
            return repo.TemperatureLevels.Get().Select(x => x.ToDto());
        }

        [HttpPost]
        public void Create([FromForm]DataPointDto dto)
        {
            var now = DateTime.Now;
            now = now.AddTicks(-(now.Ticks % TimeSpan.TicksPerSecond)); //remove ms and s from time

            repo.TemperatureLevels.Add(new Temperature {
                Id = now,
                RoomId = dto.RoomId,
                Value = dto.Value
            });
        }

        [HttpDelete("{roomId}/{timestamp}")]
        public IActionResult Delete(string roomId, DateTime timestamp)
        {
            var temp = new Temperature { RoomId = roomId, Id = timestamp };
            if (repo.TemperatureLevels.Delete(temp))
                return Ok();
            return NotFound();
        }
    }
}