using System.Collections.Generic;
using web.Models.ViewModel;
using Web.Models;

namespace web.Bus
{
    public interface IDisplayTypePriceBus
    {
        List<DisplayTypePriceViewModel> Consult(int? height, int? width, TypePrice? typePrice);
    }
}
