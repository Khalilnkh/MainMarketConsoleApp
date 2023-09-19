using MarketConsoleApp.Helpers;

namespace MarketConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int selectedOption;

            Console.WriteLine("Welcome to Market Console App!");

            do
            {
                Console.WriteLine("1. Operations on Products");
                Console.WriteLine("2. Operations on Sales");
                Console.WriteLine("3. Exit");

                Console.WriteLine("----------------------------");
                Console.WriteLine("Please, select an option:");

                while (!int.TryParse(Console.ReadLine(), out selectedOption))
                {
                    Console.WriteLine("Please enter valid option:");
                }

                switch (selectedOption)
                {
                    case 1:
                        SubMenuHelper.DisplayOperationsOnProducts();
                        break;
                    case 2:
                        SubMenuHelper.DisplayOperationsOnSale();
                        break;
                    case 3:
                        Console.WriteLine("Bye!");
                        break;                  
                    default:
                        Console.WriteLine("No such option!");
                        break;
                }
            } while (selectedOption != 0);
        }
    }
}