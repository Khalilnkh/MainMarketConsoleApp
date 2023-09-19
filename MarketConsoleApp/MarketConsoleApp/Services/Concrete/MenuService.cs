using ConsoleTables;
using MarketConsoleApp.Data.Enums;
using MarketConsoleApp.Data.Models;
using MarketConsoleApp.Services.Abstract;
using System;
using System.Collections.Generic;
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
                Department department = (Department)Enum.Parse(typeof(Department), Console.ReadLine()!);

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
                var table = new ConsoleTable(" Sale ID", "Product Id", "Quantity", "Date");
                //(ID, meblegi, mehsul sayi, tarixi)

                foreach (var sale in marketService.GetSales())
                {
                    foreach (var product in marketService.GetSales())
                    {
                        foreach (var item in sale.SaleItems)
                        {
                            table.AddRow(sale.Id,item.Id,item.Quantity,sale.Date);


                        }
                    }
                }

                table.Write();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error {ex.Message}");
            }
        }

        public static void AddSale()
        {
            try
            {

                Console.WriteLine("Enter count sale of items");
                int itemCount = int.Parse(Console.ReadLine()!);

                var saleItems = new List<SaleItem>();
                for (int i = 1; i <= itemCount; i++)
                {
                    Console.WriteLine($"Enter product Id of product {i}");
                    int id = int.Parse(Console.ReadLine()!);

                    Console.WriteLine($"Enter Product quantity of product {id}");
                    int quantity = int.Parse(Console.ReadLine()!);

                    Console.WriteLine($"Enter Product quantity of product {id}");
                    int amount = int.Parse(Console.ReadLine()!);


                    saleItems.Add(new SaleItem()
                    {
                        ProductId = id,
                        Quantity = quantity,
                        
                       
                    });

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





    }
}
