using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using TestWebApi.Converters;

namespace TestWebApi.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        [Column(TypeName = "TEXT")]
        public SystemType SystemType { get; set; }
        public string OrderNumber { get; set; }
        public string SourceOrder { get; set; }
        public string ConvertedOrder { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public enum SystemType
    {
        [EnumMember(Value = "zomato")]
        Zomato,
        [EnumMember(Value = "uber")]
        Uber,
        [EnumMember(Value = "talabat")]
        Talabat
    }
}