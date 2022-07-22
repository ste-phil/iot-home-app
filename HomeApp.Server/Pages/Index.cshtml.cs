using System.Text.Json;
using HomeApp.Library.Entities;
using HomeApp.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeApp.Server.Pages;

public class LinearGaugeModel 
{
    public string Title { get; set; }
    public string ImagePath { get; set; }
    public float Min { get; set; }
    public float Max { get; set; }
    public float Value { get; set; }
    public Dictionary<float, Color> ColorMap { get; set; }
}


public class IndexModel : PageModel
{
    public class DataPointRange 
    {
        public float Min { get; set; }
        public float Max { get; set; }
        public Dictionary<float, Color> ColorMap { get; set; }
    }

    
    private readonly ILogger<IndexModel> logger;
    public readonly IRepositoryService repo;

    public Dictionary<DataPointType, DataPointRange> DataPointRanges = new Dictionary<DataPointType, DataPointRange> {
        { 
            DataPointType.Co2, 
            new DataPointRange 
            { 
                Min = 0, 
                Max = 5000, 
                ColorMap = new Dictionary<float, Color> 
                { 
                    { 400.0f,  "00bdae" },
                    { 1000.0f, "feae73" },
                    { 2000.0f, "d64e52" },
                    { 5000.0f, "2b383e" },
                } 
            } 
        },
    };

    public IndexModel(ILogger<IndexModel> logger, IRepositoryService repo)
    {
        this.logger = logger;
        this.repo = repo;
    }

    public LinearGaugeModel? GetLinearGaugeModel(Room r, DataPointType type) {
        var latest = repo.Datapoints
            .Get(x => x.RoomId == r.Id && x.Type == type && x.Value != 0)
            .OrderByDescending(x => x.Id)
            .FirstOrDefault();
        if (latest != null) 
        {
            var range = DataPointRanges[type];
            return new LinearGaugeModel
            {
                ImagePath = type.ToString() + ".png",
                ColorMap = range.ColorMap,
                Max = range.Max,
                Min = range.Min,
                Value = latest.Value
            };
        }
            
        return null;
    }

    public string GetLatestDatapointOfType(Room r, DataPointType type, int decimals = 1) {
        var latest = repo.Datapoints
            .Get(x => x.RoomId == r.Id && x.Type == type && x.Value != 0)
            .OrderByDescending(x => x.Id)
            .FirstOrDefault();
        if (latest != null) return Math.Round(latest.Value, decimals).ToString();
        return "";
    }

    public string GetRoomDataJson(Room r) 
    {
        return JsonSerializer.Serialize(new {
            Temperature = repo
                .Datapoints
                .GetRangeFromLast(new TimeSpan(3, 0, 0), r.Id, DataPointType.Temperature)
                .Select(x => new { x = x.LocalTimestamp.ToString("yyyy-MM-dd HH:mm:ss"), y = x.Value}),
            Co2 = repo
                .Datapoints
                .GetRangeFromLast(new TimeSpan(3, 0, 0), r.Id, DataPointType.Co2)
                .Select(x => new { x = x.LocalTimestamp.ToString("yyyy-MM-dd HH:mm:ss"), y = x.Value}),
            Humidity = repo
                .Datapoints
                .GetRangeFromLast(new TimeSpan(3, 0, 0), r.Id, DataPointType.Humidity)
                .Select(x => new { x = x.LocalTimestamp.ToString("yyyy-MM-dd HH:mm:ss"), y = x.Value}),
            Battery = repo
                .Datapoints
                .GetRangeFromLast(new TimeSpan(3, 0, 0), r.Id, DataPointType.Battery)
                .Select(x => new { x = x.LocalTimestamp.ToString("yyyy-MM-dd HH:mm:ss"), y = x.Value}),
        });
    }
}
