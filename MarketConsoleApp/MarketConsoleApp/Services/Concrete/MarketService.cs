using MarketConsoleApp.Data.Enums;
using MarketConsoleApp.Data.Models;
using MarketConsoleApp.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MarketConsoleApp.Services.Concrete
{
    public class MarketService : IMarketService
    {
        private List<Product> _products = new();
        private List<Sale> _sales = new();
        private List<SaleItem> _saleItems = new();
        public int AddProduct(string name, decimal price, Department department, int quantity)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("Name can't be empty!");
            if (price < 0)
                throw new Exception("Price per product can't be less than 0!");
            if (string.IsNullOrWhiteSpace(department.ToString()))
                throw new Exception("Department can't be empty!");
            if (quantity < 0)
                throw new Exception("Quantity per product can't be less than 0!}");

            var product = new Product
            {
                Name = name,
                Price = price,
                Department = department,
                Quantity = quantity,

            };

            _products.Add(product);
            return product.Id;
        }

        public int DeleteProduct(int id)
        {
            if (id < 0)
                throw new Exception("Id cant be less than 0");
            var product = _products.FirstOrDefault(x => x.Id == id);
            if (product == null)
                throw new Exception($"Product with Id {id} can not found");
            _products.Remove(product);

            return id;
        }

        public int UpdateProduct(int id, string name, decimal price, Department department, int quantity)
        {


            if (id < 0)
                throw new Exception("Id cant be less than 0");
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("Name can't be empty!");
            if (price < 0)
                throw new Exception("price cant be less than 0");
            //if (string.IsNullOrEmpty(department.ToString())) ;
            //throw new Exception("department can't be empty!");
            if (quantity < 0)
                throw new Exception("quantity cant be less than 0");
            var product = _products.Find(x => x.Id == id);
            if (product == null)
                throw new Exception($"Product with Id {id} can not found");

            product.Name = name;
            product.Price = price;
            product.Department = department;
            product.Quantity = quantity;



            return product.Id;


        }
        public List<Product> GetProducts()
        {
            return _products;
        }

        public List<Product> GetProductsByCategory(Department department)
        {
            var product = _products.Where(x => x.Department == department).ToList();
            if (product == null)
                throw new Exception("Product can not found");
            return product;
        }

        public List<Product> GetProductsByPriceRange(int minPrice, int maxPrice)
        {
            if (minPrice < 0)
                throw new Exception("Minimum price cant be less than 0");
            if (maxPrice < 0)
                throw new Exception("Maximum price cant be less than 0");
            if (minPrice > maxPrice)
                throw new Exception("Min price can't be larger than max price!");

            var product = _products.Where(x => x.Price >= minPrice && x.Price <= maxPrice).ToList();
            return product;
        }

        public List<Product> GetProductsByGivenName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("Name can't be empty!");
            var product = _products.Where(x => x.Name == name).ToList();
            return product;
        }
        public int AddSale(List<SaleItem> saleItems, DateTime dateTime)
        {

            if (saleItems == null || !saleItems.Any())
                throw new Exception("There are no sale items");

            var sale = new Sale()
            {
                Date = dateTime,
                SaleItems = new List<SaleItem>()
            };

            decimal totalPrice = 0;
            foreach (var item in saleItems)
            {

                if (item.Quantity <= 0)
                    throw new Exception("Quantity can't be less than 0!");

                var product = _products.FirstOrDefault(x => x.Id == item.ProductId);
                if (product is null)
                    throw new Exception($"Product with ID {item.ProductId} not found.");

                if (product.Quantity < item.Quantity)
                    throw new Exception("Not enough quantity available for sale");

                item.TotalPrice = product.Price * item.Quantity;
                //totalPrice += item.TotalPrice;

                var saleItem = new SaleItem()
                {
                    SaleId = sale.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    TotalPrice = item.TotalPrice
                };
                sale.SaleItems.Add(saleItem);
                product.Quantity -= saleItem.Quantity;

            }

            _sales.Add(sale);

            return sale.SaleItems.Count;
        }



        public int DeleteSale(int id)
        {
            if (id < 0)
                throw new Exception("Id cant be less than 0");
            var sale = _sales.FirstOrDefault(x => x.Id == id);
            if (sale == null)
                throw new Exception($"Product with Id {id} can not found");
            _sales.Remove(sale);
            return sale.Id;

        }

        public List<Sale> GetSales()
        {
            return _sales;
        }

        public List<Sale> GetSalesByDateRange(DateTime minDate, DateTime maxDate)
        {
            if (minDate > maxDate)
                throw new Exception("Min price can't be larger than max price!");
            var sale = _sales.Where(x => x.Date >= minDate && x.Date <= maxDate).ToList();
            if (sale == null)
                throw new Exception("Sale can not found");
            return sale;
           
        }





        //public int AddSales(decimal amount, DateTime date, int saleItemId, int productId)
        //{

        //    if (amount < 0)
        //        throw new Exception("Amount can't be 0 or less than 0!");
        //    if (date < DateTime.Now)
        //        throw new Exception("Sales can't be created in past!");
        //    var saleItem = _saleItems.FirstOrDefault(x => x.Id == saleItemId);
        //    if (saleItem is null)
        //        throw new Exception($"SalesItem with ID ${saleItem.Id}: was not found!");
        //    var products = _products.Where(x => x.Id == productId).ToList();
        //    if (products is null)
        //        throw new Exception($"SalesItem with ID ${}: was not found!");

        //    var sale = new Sale
        //    {
        //        Amount = amount,
        //        Date = date,
        //        Product =products.First(),



        //    };

        //    _sales.Add(sale);


        //    return sale.Id;


        //}


    }
}
