using AllProject.Model;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AllProject
{
    public partial class App : Application
    {
        public static SignUpWindow signUpWindow { get; set; }
        public static DB db = null;
        [STAThread] public static void Main()
        {
            db = new DB();

            db.Categories.Add(new Category
            {
                Name = "Диваны",
            });
            db.SaveChanges();
            db.Products.Add(new Product
            {
                Name = "одеяло",
                Price = 10,
                Category = db.Categories.FirstOrDefault(x => x.Name == "Одеяла"),
                Duration = 3
            });
           db.Products.Add(new Product
            {
                Name = "ковёр",
                Price = 15,
                Category = db.Categories.FirstOrDefault(x => x.Name == "Ковры"),
                Duration = 5
            });

            db.Products.Add(new Product
            {
                Name = "диван",
                Price = 20,
                Category = db.Categories.FirstOrDefault(x => x.Name == "Диваны"),
                Duration = 7
            });

            db.Clients.Add(new Client
            {
                FullName = "Петров",
                Address = "1",
                Phone = "435465"
            });
            db.Clients.Add(new Client
            {
                FullName = "Иванов",
                Address = "1",
                Phone = "435465"
            });
            db.Clients.Add(new Client
            {
                FullName = "Сидоров",
                Address = "1",
                Phone = "435465"
            });
            App.db.SaveChanges();
            db.Services.Add(new Service
            {
                ServiceCoefficient = 1.1,
                ServiceName = "выведение пятна",
            });
            db.Services.Add(new Service
            {
                ServiceCoefficient = 2,
                ServiceName = "стирка",
            });
            db.Services.Add(new Service
            {
                ServiceCoefficient = 1.3,
                ServiceName = "сушка",
            });
            db.Services.Add(new Service
            {
                ServiceCoefficient = 1.2,
                ServiceName = "глажка",
            });
            App.db.SaveChanges();
            db.Orders.Add(new Order
            {
                ID = 1,
                CompilationDate = new DateTime(2024, 01, 20),
                Client = db.Clients.FirstOrDefault(x => x.FullName == "Петров"),
                OrderPrice = 20,
                TotalDuration = 3,
                Issue = true,
                Readiness = true,
            });
            db.SaveChanges();
            db.OrderCompositions.Add(new OrderComposition
            {
                Order = db.Orders.FirstOrDefault(x => x.ID == 1),
                Product = db.Products.FirstOrDefault(x => x.Name == "одеяло"),
                Service = db.Services.FirstOrDefault(x => x.ServiceName == "стирка"),
                Quantity = 1
            });
            App.db.SaveChanges();

            db.Orders.Add(new Order
            {
                ID = 2,
                CompilationDate = new DateTime(2024, 01, 01),
                Client = db.Clients.FirstOrDefault(x => x.FullName == "Иванов"),
                OrderPrice = 52,
                TotalDuration = 7,
                Issue = true,
                Readiness = true,
            });
            App.db.SaveChanges();
            db.OrderCompositions.Add(new OrderComposition
            {
                Order = db.Orders.FirstOrDefault(x => x.ID == 2),
                Product = db.Products.FirstOrDefault(x => x.Name == "ковёр"),
                Service = db.Services.FirstOrDefault(x => x.ServiceName == "стирка"),
                Quantity = 1
            });

            db.OrderCompositions.Add(new OrderComposition
            {
                Order = db.Orders.FirstOrDefault(x => x.ID == 2),
                Product = db.Products.FirstOrDefault(x => x.Name == "диван"),
                Service = db.Services.FirstOrDefault(x => x.ServiceName == "выведение пятна"),
                Quantity = 1
            });
            App.db.SaveChanges();
            signUpWindow = new SignUpWindow();
            new App().Run((signUpWindow));
 
        }
    }
}
