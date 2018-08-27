using System;
namespace Web.Models
{
    public class DisplayTypePrice
    {
        public int Id { get; set; }
        public TypePrice Type { get; set; }
        public decimal Price { get; set; }
        public int DisplayId { get; set; }
        public virtual Display Display { get; set; }
    }
}
