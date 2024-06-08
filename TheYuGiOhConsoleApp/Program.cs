using Newtonsoft.Json;
using ServiceLayer;
using System.Text.Json;
using BusinessLayer;
using DataLayer;
namespace TheYuGiOhConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MainController main = new MainController();
            main.Start();
            Console.ReadKey();
        }
    }
}
