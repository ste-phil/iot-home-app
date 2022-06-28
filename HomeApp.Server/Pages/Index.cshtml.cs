using System.Text.Json;
using HomeApp.Library.Entities;
using HomeApp.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeApp.Server.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> logger;
    public readonly IRepositoryService repo;


    public IndexModel(ILogger<IndexModel> logger, IRepositoryService repo)
    {
        this.logger = logger;
        this.repo = repo;
    }

    public string GetRoomDataJson(Room r) {
        

        return JsonSerializer.Serialize(new {
            Temperature = repo
                .Datapoints
                .GetRangeFromLast(new TimeSpan(3, 0, 0), r.Id, DataPointType.Temperature)
                .Select(x => new { x = x.Timestamp.ToString("yyyy-MM-dd HH:mm:ss"), y = x.Value}),
            Co2 = repo
                .Datapoints
                .GetRangeFromLast(new TimeSpan(3, 0, 0), r.Id, DataPointType.Co2)
                .Select(x => new { x = x.Timestamp.ToString("yyyy-MM-dd HH:mm:ss"), y = x.Value}),
            Humidity = repo
                .Datapoints
                .GetRangeFromLast(new TimeSpan(3, 0, 0), r.Id, DataPointType.Humidity)
                .Select(x => new { x = x.Timestamp.ToString("yyyy-MM-dd HH:mm:ss"), y = x.Value}),
            Battery = repo
                .Datapoints
                .GetRangeFromLast(new TimeSpan(3, 0, 0), r.Id, DataPointType.Battery)
                .Select(x => new { x = x.Timestamp.ToString("yyyy-MM-dd HH:mm:ss"), y = x.Value}),
        });
    }
}
