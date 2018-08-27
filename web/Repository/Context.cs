using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace Web.Repository
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options)
        { }


        public DbSet<Display> Displays { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<DisplayTypePrice> DisplaysPrices { get; set; }

        public async Task SeedData()
        {
            var displaySizeTypeOne = new Size()
            {
                Width = 1920,
                Height = 1080
            };

            var displaySizeTypeTwo = new Size()
            {
                Width = 1080,
                Height = 1920
            };


            //Displays of type one
            var display = new Display
            {
                DisplaySize = displaySizeTypeOne,
                Prices = new List<DisplayTypePrice>()
            };
            display.Prices.Add(new DisplayTypePrice()
            {
                Price = 1,
                Type = TypePrice.Cheap
            });
            display.Prices.Add(new DisplayTypePrice()
            {
                Price = 2,
                Type = TypePrice.Normal
            });

            var display2 = new Display
            {
                DisplaySize = displaySizeTypeOne,
                Prices = new List<DisplayTypePrice>(),
            };
            display2.Prices.Add(new DisplayTypePrice()
            {
                Price = 2,
                Type = TypePrice.Cheap
            });
            display2.Prices.Add(new DisplayTypePrice()
            {
                Price = 4,
                Type = TypePrice.Normal
            });

            var display3 = new Display
            {
                DisplaySize = displaySizeTypeOne,
                Prices = new List<DisplayTypePrice>()
            };
            display3.Prices.Add(new DisplayTypePrice()
            {
                Price = 4,
                Type = TypePrice.Cheap
            });
            display3.Prices.Add(new DisplayTypePrice()
            {
                Price = 8,
                Type = TypePrice.Normal
            });
           

            //Displays of type two
            var display4 = new Display
            {
                DisplaySize = displaySizeTypeTwo,
                Prices = new List<DisplayTypePrice>()
            };
            display4.Prices.Add(new DisplayTypePrice()
            {
                Price = 8,
                Type = TypePrice.Cheap
            });
            display4.Prices.Add(new DisplayTypePrice()
            {
                Price = 10,
                Type = TypePrice.Normal
            });

            var display5 = new Display
            {
                DisplaySize = displaySizeTypeTwo,
                Prices = new List<DisplayTypePrice>()
            };
            display5.Prices.Add(new DisplayTypePrice()
            {
                Price = 10,
                Type = TypePrice.Cheap
            });
            display5.Prices.Add(new DisplayTypePrice()
            {
                Price = 12,
                Type = TypePrice.Normal
            });


            Displays.Add(display);
            Displays.Add(display2);
            Displays.Add(display3);
            Displays.Add(display4);
            Displays.Add(display5);

            await SaveChangesAsync();
        }
    }

   
}
