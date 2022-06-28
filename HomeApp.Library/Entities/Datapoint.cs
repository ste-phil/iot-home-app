using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeApp.Library.Entities
{
    public enum DataPointType {
        Battery, Co2, Humidity, Temperature
    } 
    
    public class DataPoint
    {
        [Key]
        public DateTime Id { get; set; }
        public string RoomId { get; set; }
        public DataPointType Type { get; set; }

        public virtual Room Room { get; set; }
        public float Value { get; set; }

        [NotMapped]
        public DateTime Timestamp => Id;
    }

    public class DataPointDto
    {
        public DateTime Id { get; set; }
        public string RoomId { get; set; }
        public DataPointType Type { get; set; }

        public float Value { get; set; }
    }

    public static partial class Extensions 
    {
        public static DataPointDto ToDto(this DataPoint x) 
        {
            return new DataPointDto {
                Id = x.Id,
                RoomId = x.RoomId,
                Type = x.Type,
                Value = x.Value
            };
        }
    }
}
