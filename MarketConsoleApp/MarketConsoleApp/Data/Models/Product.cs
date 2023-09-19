using MarketConsoleApp.Data.Common;
using MarketConsoleApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketConsoleApp.Data.Models
{
    public class Product:BaseModel
    {
        private static int id = 0;
        public Product()
        {
             Id=id;
             id++;
        }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public Department Department { get; set; }
        public int Quantity { get; set; }



    }
}
