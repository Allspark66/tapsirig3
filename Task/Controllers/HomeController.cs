using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Diagnostics;
using Task.Models;

namespace Task.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            List<Product> products = new List<Product>();

            using (var connection = new SqliteConnection("Data Source=products.db"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT title, price, image FROM products";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new Product
                        {
                            Title = reader.GetString(0),
                            Price = reader.GetDouble(1),
                            Image = reader.IsDBNull(2) ? "default.jpg" : reader.GetString(2)
                        });
                    }
                }
            }
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}