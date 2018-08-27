using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using util;
using web.Models.ViewModel;
using web.Repository;
using Web.Models;

namespace web.Bus
{
    public class DisplayTypePriceBus : IDisplayTypePriceBus
    {
        private IRepositoryBase<DisplayTypePrice> _displayRepository;
        public DisplayTypePriceBus(IRepositoryBase<DisplayTypePrice> displayRepository)
        {
            _displayRepository = displayRepository;
        }

        public List<DisplayTypePriceViewModel> Consult(int? height, int? width, TypePrice? typePrice)
        {
            var query = _displayRepository.Query();

            if (height != null && width != null)
            {
                var heightPx = Converter.CmInPixel(height.Value);
                var widthPx = Converter.CmInPixel(width.Value);

                query = query.Where(d => d.Display.DisplaySize.Height == heightPx &&
                                         d.Display.DisplaySize.Width == widthPx);
            }
            if (typePrice != null)
                query = query.Where(d => d.Type == typePrice);

            var display = query.Include(d => d.Display)
                               .Include(d => d.Display.DisplaySize)
                               .ToList();

            return MappToDisplayTypePriceViewModel(display);

        }

        public static List<DisplayTypePriceViewModel> MappToDisplayTypePriceViewModel(List<DisplayTypePrice> displayTypePrice)
        {
            var viewModelList = new List<DisplayTypePriceViewModel>();
            foreach (var display in displayTypePrice)
            {
                viewModelList.Add(new DisplayTypePriceViewModel()
                {
                    DisplaySize = display.Display.DisplaySize,
                    Id = display.DisplayId,
                    Price = display.Price,
                    Type = display.Type
                });
            }

            return viewModelList;
        }
    }
}
