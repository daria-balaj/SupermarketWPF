using Supermarket.Views;
using NavigationService = Supermarket.Services.NavigationService;
using Supermarket.Data;
using Supermarket.Models.EntityLayer;
using Type = Supermarket.Models.EntityLayer.Type;
using System.Windows;


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
                    new User() { ID="1", Username = "admin", Password = "7890", Role = Type.Administrator },
                    new User() { ID="2", Username = "cashier", Password = "1234", Role = Type.Cashier }
                );
                context.SaveChanges();
            }

            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                    new Category() { ID = "1", Name = "Dairy" },
                    new Category() { ID = "2", Name = "MeatAndFish" },
                    new Category() { ID = "3", Name = "Clothing" },
                    new Category() { ID = "4", Name = "House" },
                    new Category() { ID = "5", Name = "FoodGeneral" },
                    new Category() { ID = "6", Name = "Bakery" },
                    new Category() { ID = "7", Name = "HealthAndBeauty" }
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
                context.Products.AddRange(
                    new Product() { ID = "100", Name = "Milk", Price = 9.50f, Barcode = "6012958", Category = context.Categories.Find("1"), Producer = context.Producers.Find("Napolact")!, ExpirationDate = DateOnly.FromDateTime(DateTime.Now.AddMonths(1)) },
                    new Product() { ID= "101", Name = "Ice cream", Price = 14.90f, Barcode = "4576856", Category = context.Categories.Find("5"), Producer = context.Producers.Find("Transalpina")!, ExpirationDate = DateOnly.FromDateTime(DateTime.Now.AddDays(40)) }

                );
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
            NavigationService = new NavigationService(Frame, _context);
            Frame.Content = new StartPage();
        }
    }
}