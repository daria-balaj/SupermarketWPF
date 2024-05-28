using Supermarket.Views;
using NavigationService = Supermarket.Services.NavigationService;
using Supermarket.Data;
using Supermarket.Models.EntityLayer;
using Type = Supermarket.Models.EntityLayer.Type;
using System.Windows;
using Microsoft.VisualBasic.ApplicationServices;
using User = Supermarket.Models.EntityLayer.User;


namespace Supermarket
{
    public partial class MainWindow : Window
    {
        public static void Seed(DataContext context)
        {
            //if (!context.Database.EnsureCreated())
            //{
            //    throw new Exception("database not created/found");
            //}

            // Seed Users
            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new User() { Username = "admin", Password = "7890", Role = Type.Administrator },
                    new User() { Username = "cashier", Password = "1234", Role = Type.Cashier }
                );
                context.SaveChanges();
            }

            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                    new Category() { Name = "Dairy" },
                    new Category() { Name = "MeatAndFish" },
                    new Category() { Name = "Clothing" },
                    new Category() { Name = "House" },
                    new Category() { Name = "FoodGeneral" },
                    new Category() { Name = "Bakery" },
                    new Category() { Name = "HealthAndBeauty" }
                    //new Category { ID = "8", Name = "Dairy" },
                    //new Category { ID = "9", Name = "Dairy" },
                    //new Category { ID = "10", Name = "Dairy" }
                    );
                context.SaveChanges();
            }

            if (!context.Producers.Any())
            {
                context.Producers.AddRange(
                    new Producer() { Name = "Napolact", Country = "Romania" },
                    new Producer() { Name = "Harmopan", Country = "Romania" },
                    new Producer() { Name = "Transalpina", Country = "Romania" }
                    //new Producer { Name = "Napolact", Country = "Romania" }
                );
                context.SaveChanges();
            }
            // Seed Products
            if (!context.Products.Any())
            {
                //context.Products.AddRange(
                //    new Product() { Name = "Milk", Price = 9.50f, Barcode = "6012958", Category = context.Categories.Find(1), Producer = context.Producers.Find(1)!, ExpirationDate = DateOnly.FromDateTime(DateTime.Now.AddMonths(1)) },
                //    new Product() { Name = "Ice cream", Price = 14.90f, Barcode = "4576856", Category = context.Categories.Find(5), Producer = context.Producers.Find(3)!, ExpirationDate = DateOnly.FromDateTime(DateTime.Now.AddDays(40)) }

                //);
                context.SaveChanges();
            }
        }
        public NavigationService NavigationService { get; }
        public DataContext _context;

        public MainWindow()
        {
            InitializeComponent();
            _context = new DataContext();
            Seed(_context);
            var user = _context.Users.SingleOrDefault(u => u.Username == "admin");
            if (user != null)
            {
                // Update the role to Admin
                user.Role = Type.Administrator;

                // Save the changes to the database
                _context.SaveChanges();
            }
            NavigationService = new NavigationService(Frame, _context);
            Frame.Content = new StartPage();
        }
    }
}