using MarketConsoleApp.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketConsoleApp.Data.Models
{
    public class SaleItem : BaseModel
    {
        private static int id = 0;
        public SaleItem()
        {
            Id = id;
            id++;
        }
        public int ProductId { get; set; } 
        public int SaleId { get; set; } 
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
