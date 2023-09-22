using ConsoleTables;
using MarketConsoleApp.Data.Enums;
using MarketConsoleApp.Data.Models;
using MarketConsoleApp.Helpers;
using MarketConsoleApp.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketConsoleApp.Services.Concrete
{
    public class MenuService
    {
        private static IMarketService marketService = new MarketService();

        public static void AddProduct()
        {
            try
            {
                Console.WriteLine("Enter product's name:");
                string name = Console.ReadLine()!;
                ValidateMyString(name);
                static void ValidateMyString(string s)
                {
                    if (s.All(char.IsDigit))
                    {
                        Console.WriteLine("The product can not be consist of only numbers or digits");
                        throw new FormatException();
                    }
                }

                Console.WriteLine("Enter product's price:");
                decimal price = decimal.Parse(Console.ReadLine()!);

                Console.WriteLine("Enter products's department:");
                Department department = (Department)Enum.Parse(typeof(Department), Console.ReadLine().ToLower()!);

                Console.WriteLine("Enter products's quantity :");
                int quantity = int.Parse(Console.ReadLine()!);

                int id = marketService.AddProduct(name, price, department, quantity);

                Console.WriteLine($"Product with ID:{id} was created!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }


        public static void UpdateProduct()
        {
            try
            {
                Console.WriteLine("Please enter new id of product");
                int id = int.Parse(Console.ReadLine()!);
                Console.WriteLine("Enter product's new name:");
                string name = Console.ReadLine()!;
                ValidateMyString(name);
                static void ValidateMyString(string s)

                {
                    if (s.All(char.IsDigit))
                    {
                        Console.WriteLine("The product can not be consist of only numbers or digits");
                        throw new FormatException();

                    }
                }
                Console.WriteLine("Enter product's new price:");
                decimal price = decimal.Parse(Console.ReadLine()!);
                Console.WriteLine("Enter products's new department:");
                Department department = (Department)Enum.Parse(typeof(Department), Console.ReadLine()!);
                Console.WriteLine("Enter products's new quantity :");
                int quantity = int.Parse(Console.ReadLine()!);
                marketService.UpdateProduct(id, name, price, department, quantity);

                Console.WriteLine($"Product with ID {id} was updated");

            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error {ex.Message}");
            }
        }

        public static void DeleteProduct()
        {
            try
            {
                Console.WriteLine("Please enter Id of product");
                int id = int.Parse(Console.ReadLine()!);

                marketService.DeleteProduct(id);
                Console.WriteLine($"Product with ID:{id} was deleted!");

            }
            catch (Exception ex)
            {

                Console.WriteLine($"Eror {ex.Message}");
            }

        }

        public static void ShowProducts()
        {
            try
            {
                var table = new ConsoleTable("ID", "Name", "Price", "Department", "Quantity");

                foreach (var product in marketService.GetProducts())
                {
                    table.AddRow(product.Id, product.Name, product.Price, product.Department, product.Quantity);
                }

                table.Write();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error {ex.Message}");
            }
        }

        public static void ShowCategories()
        {
            try
            {
                var table = new ConsoleTable("Id", "Department");

                foreach (var product in marketService.GetProducts())
                {
                    table.AddRow(product.Id, product.Department);
                }

                table.Write();
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error {ex.Message}");

            }
        }

        public static void GetProductsByCategory()
        {
            try
            {
                Console.WriteLine("List of available products:");
                ShowCategories();
                Console.WriteLine("Please enter category of  product");
                Department department = (Department)Enum.Parse(typeof(Department), Console.ReadLine()!);

                var table = new ConsoleTable("ID", "Name", "Price", "Department", "Quantity");

                foreach (var product in marketService.GetProductsByCategory(department))
                {
                    table.AddRow(product.Id, product.Name, product.Price, product.Department, product.Quantity);
                }
                table.Write();
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error {ex.Message}");
            }




        }

        public static void GetProductsByPriceRange()
        {
            try
            {
                Console.WriteLine("Please Add minimum price of product");
                int minPrice = int.Parse(Console.ReadLine()!);

                Console.WriteLine("Please Add maximum price of product");
                int maxPrice = int.Parse(Console.ReadLine()!);

                var table = new ConsoleTable("ID", "Name", "Price", "Department", "Quantity");

                foreach (var product in marketService.GetProductsByPriceRange(minPrice, maxPrice))
                {
                    table.AddRow(product.Id, product.Name, product.Price, product.Department, product.Quantity);
                }
                table.Write();

            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error {ex.Message}");
            }


        }

        public static void GetProductsByName()
        {
            try
            {
                Console.WriteLine("Please Add product name ");
                string name = Console.ReadLine()!;
                ValidateMyString(name);
                static void ValidateMyString(string s)
                {
                    if (s.All(char.IsDigit))
                    {
                        Console.WriteLine("The product can not be consist of only numbers or digits");
                        throw new FormatException();

                    }
                }
                var table = new ConsoleTable("ID", "Name", "Price", "Department", "Quantity");

                foreach (var product in marketService.GetProductsByGivenName(name))
                {
                    table.AddRow(product.Id, product.Name, product.Price, product.Department, product.Quantity);
                }
                table.Write();

            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error {ex.Message}");
            }


        }

        public static void DeleteSale()
        {


            try
            {
                Console.WriteLine("Please enter Id of sale");
                int id = int.Parse(Console.ReadLine()!);

                marketService.DeleteSale(id);
                Console.WriteLine($"Product with ID:{id} was deleted!");

            }
            catch (Exception ex)
            {

                Console.WriteLine($"Eror {ex.Message}");
            }


        }

        public static void ShowSales()
        {
            try
            {
                var saleList = marketService.GetSales();
                var tableSale = new ConsoleTable("Sale Id", "Price", "DateTime","Amount");
                foreach (var sale in saleList)
                {
                    
                    sale.Amount = 0;
                    foreach (var item in sale.SaleItems)
                    {
                        sale.Amount += item.TotalPrice;
                    }
                    tableSale.AddRow(sale.Id, sale.Amount, sale.Date,sale.Amount);
                }
                tableSale.Write();
                Console.WriteLine("---------------------------------------------------------------------------");
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public static void AddSale()
        {
            try
            {

                Console.WriteLine("Enter count of sale items");
                int itemCount = int.Parse(Console.ReadLine()!);

                var saleItems = new List<SaleItem>();
                for (int i = 1; i <= itemCount; i++)
                {
                    Console.WriteLine($"Enter product Id for item {i}");
                    int id = int.Parse(Console.ReadLine()!);

                    Console.WriteLine($"Enter Product quantity for product {id}");
                    int quantity = int.Parse(Console.ReadLine()!);

                    saleItems.Add(new SaleItem()
                    {
                        ProductId = id,
                        Quantity = quantity
                    }); ;
                    Console.WriteLine("Would you like to add another product for sale (yes/no)");
                    string answer = Console.ReadLine()!;
                    if (answer != "yes")
                    {
                        break;
                    }
                }

                Console.WriteLine("Enter datetime");
                var dateTime = DateTime.ParseExact(Console.ReadLine()!, "dd.MM.yyyy HH:mm:ss", null);

                var saleCount = marketService.AddSale(saleItems, dateTime);
                Console.WriteLine($"Sale count: {saleCount}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public static void GetSalesByDateRange()
        {
            try
            {
                //Console.WriteLine("List of available sales:");
                //ShowSales();
                Console.WriteLine("Please Enter minimum  datetime");
                var minDate = DateTime.ParseExact(Console.ReadLine()!, "dd.MM.yyyy HH:mm:ss", null);


                Console.WriteLine("Please Enter maximum  datetime");
                var maxDate = DateTime.ParseExact(Console.ReadLine()!, "dd.MM.yyyy HH:mm:ss", null);



                var table = new ConsoleTable("ID", "Amount", "Quantity", "Date");

                foreach (var sale in marketService.GetSalesByDateRange(minDate, maxDate))
                {


                    foreach (var item in sale.SaleItems)
                    {
                        table.AddRow(sale.Id, sale.Amount, item.Quantity, sale.Date);


                    }
                    table.Write();


                }

            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error {ex.Message}");

            }
        }

        public static void GetSalesByPriceRange()
        {
            try
            {
                Console.WriteLine("Please Add minimum price of product");
                int minPrice = int.Parse(Console.ReadLine()!);

                Console.WriteLine("Please Add maximum price of product");
                int maxPrice = int.Parse(Console.ReadLine()!);

                var table = new ConsoleTable("ID", "Amount", "Quantity", "Date");
                foreach (var sale in marketService.GetSalesByPriceRange(minPrice, maxPrice))
                {


                    foreach (var item in sale.SaleItems)
                    {
                        table.AddRow(sale.Id, sale.Amount, item.Quantity, sale.Date);


                    }
                    table.Write();


                }

            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error {ex.Message}");
            }

        }


        public static void GetSalesByGivenDate()
        {
            Console.WriteLine("Enter datetime:");
            var date = DateTime.ParseExact(Console.ReadLine()!, "dd.MM.yyyy HH:mm:ss", null);

            var saleByGivenDate = marketService.GetSalesByGivenDate(date);

            var table = new ConsoleTable("Sale Id", "Price", "SaleItems Count", "DateTime");
            foreach (var sale in saleByGivenDate)
            {
                table.AddRow(sale.Id, sale.Amount, sale.SaleItems.Count, sale.Date);
            }

            table.Write();
        }

        public static void GetSalesBySaleId()
        {
            try
            {

                Console.WriteLine("Please Enter sale Id");
                int saleId = int.Parse(Console.ReadLine()!);

                var saleOfSaleID = marketService.GetSalesBySaleId(saleId);
                var products = marketService.GetProducts();


                var saleItemsOfTable = new ConsoleTable("Sale Id", "Product name", "Products Price", "Quantity", "Total Price");

                
                
                    foreach (var item in saleOfSaleID.SaleItems)
                    {
                        var product = products.FirstOrDefault(x => x.Id == item.ProductId);
                        if (product == null)
                            throw new Exception("Product coudnt found");


                          saleItemsOfTable.AddRow(item.SaleId, product.Name, product.Price, item.Quantity, item.TotalPrice);
                    }

                
                saleItemsOfTable.Write();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error {ex.Message}");

            }
        }

        public static void RefundProductFromSale()
        {
            try
            {
                Console.WriteLine("Enter Sale Id: ");
                int saleId = int.Parse(Console.ReadLine()!);

                Console.WriteLine("Enter Product Id: ");
                int productId = int.Parse(Console.ReadLine()!);

                Console.WriteLine("Enter Quantity of Product: ");
                int count = int.Parse(Console.ReadLine()!);

                var refundProdcut = marketService.RefundProductFromSale(saleId, productId, count);
                Console.WriteLine($"Withdraw the Product with ProductId={refundProdcut}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }








    }
}
