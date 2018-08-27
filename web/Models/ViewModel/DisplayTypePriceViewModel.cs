using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace web.Models.ViewModel
{
    public class DisplayTypePriceViewModel
    {
        public int Id { get; set; }
        public TypePrice Type { get; set; }
        public decimal Price { get; set; }
        public virtual Size DisplaySize { get; set; }
    }
}
