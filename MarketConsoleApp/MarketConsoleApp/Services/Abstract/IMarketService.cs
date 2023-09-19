using MarketConsoleApp.Data.Enums;
using MarketConsoleApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketConsoleApp.Services.Abstract
{
    public interface IMarketService
    {
        public int AddProduct(string name, decimal price, Department department, int quantity);
        public int UpdateProduct(int id, string name, decimal price, Department department, int quantity);
        public int DeleteProduct(int id);
        public List<Product> GetProducts();
        public List<Product> GetProductsByCategory(Department department);
        public List<Product> GetProductsByPriceRange(int minPrice, int maxPrice);
        public List<Product> GetProductsByGivenName(string name);

        
        public int AddSale(List<SaleItem>saleItems,DateTime dateTime);
        public int DeleteSale(int id);
        public List<Sale> GetSales();




        //Satisin silinmesi - satisin ID-sine esasen silinmesi


        //public List<Sale> AddSales(decimal amount, DateTime date, int saleItem, int productId);
        //public List<Product> GetSales(int id, decimal amount,DateOnly date,SaleItem saleItem);
        //public int ReturnProductOnSale(SaleItem saleItem,Product product,int quantity);
        //public List<Product> GetSales(int id, decimal amount, DateOnly date,int quantity);
        //public List<Product> GetSalesByDateRange(int id, decimal amount, int quantity, DateOnly date);
        //public List<Product> GetSalesByPriceRange(int id, decimal amount, DateOnly date, int quantity);
        //public List<Product> GetSalesByGivenDate(DateOnly date);
        //public int GetSalesByGivenId(int id);














    }
}
