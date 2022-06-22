using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeApp.Library.Entities
{
    public class Room
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Name { get; set; }

        public virtual IList<Temperature> TemperatureLevels { get; set; }
        public virtual IList<Co2> Co2Levels { get; set; }
        public virtual IList<Humidity> HumidityLevels { get; set; }
        public virtual IList<Battery> BatteryLevels { get; set; }
    }

    public class RoomDto
    {
        public string? Id { get; set; }
        public string Name { get; set; }
    }

    public static partial class Extensions 
    {
        public static RoomDto ToDto(this Room x) 
        {
            return new RoomDto
            {
                Id = x.Id,
                Name = x.Name
            };
        }
    }
    
}