using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using web.Bus;
using web.Models.ViewModel;
using Web.Models;

namespace web.Controllers
{
    [Route("[controller]")]
    public class DisplayController : Controller
    {
        private readonly IDisplayTypePriceBus _displayTypePriceBus;
        public DisplayController(IDisplayTypePriceBus displayTypePriceBus)
        {
            _displayTypePriceBus = displayTypePriceBus;
        }

        [HttpPost("Consult/")]
        public List<DisplayTypePriceViewModel> Consult(int? height, int? width, TypePrice? typePrice)
        {
            return _displayTypePriceBus.Consult(height, width, typePrice);
        }

       
    }
}

