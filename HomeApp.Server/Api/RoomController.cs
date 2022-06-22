using HomeApp.Library.Entities;
using HomeApp.Persistence;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HomeApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IRepositoryService repo;

        public RoomController(IRepositoryService repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public IEnumerable<RoomDto> Get()
        {
            return repo.Rooms.Get().Select(x => x.ToDto());
        }

        [HttpPost]
        public void Create([FromForm]RoomDto dto)
        {
            repo.Rooms.Add(new Room {
                Name = dto.Name,
            });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTodoItem(string id)
        {
            if (repo.Rooms.Delete(id))
                return Ok();
            return NotFound();
        }


//         [HttpGet]
//         public IEnumerable<string> Get()
//         {
//             return new string[] { "value1", "value2" };
//         }

//         // GET api/<ValuesController>/5
//         [HttpGet("{id}")]
//         public string Get(int id)
//         {
//             return "value";
//         }

//         // POST api/<ValuesController>
//         [HttpPost]
//         public void Post([FromBody] string value)
//         {
//         }

//         // PUT api/<ValuesController>/5
//         [HttpPut("{id}")]
//         public void Put(int id, [FromBody] string value)
//         {
//         }

//         // DELETE api/<ValuesController>/5
//         [HttpDelete("{id}")]
//         public void Delete(int id)
//         {
//         }

    }
}