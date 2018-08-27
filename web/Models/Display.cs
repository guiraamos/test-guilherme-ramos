using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Web.Models
{
    public class Display
    {
        public int Id { get; set; }
        public virtual Size DisplaySize { get; set; }
        public List<DisplayTypePrice> Prices { get; set; }
    }
}