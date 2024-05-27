using Supermarket.Data;
using Supermarket.Models.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Supermarket.Models.BusinessLogicLayer
{
    public class ProductService
    {
        private readonly DataContext _context;
        public ProductService() 
        {
            _context = ((MainWindow)Application.Current.MainWindow)._context;
        }

        public Product? GetProductByID(string id)
        {
            return _context.Products.FirstOrDefault(p => p.ID == id);
        }
        public Product? GetProductByName(string name)
        {
            return _context.Products.FirstOrDefault(p => p.Name == name);
        }

        public Product? GetProductByBarcode(string barcode)
        {
            return _context.Products.FirstOrDefault(p => p.Barcode == barcode);
        }

        public IEnumerable<Product> GetProductsByCategory(Category category)
        {
            return _context.Products.Where(p => p.Category.Name == category.Name).ToList();
        }

        public IEnumerable<Product> GetProductsByProducer(string producerName)
        {
            return _context.Products.Where(p => p.Producer.Name == producerName).ToList();
        }

        public IEnumerable<Product> GetProductsByExpDate(DateOnly expirationDate)
        {
            return _context.Products.Where(p => p.ExpirationDate == expirationDate).ToList();
        }
    }
}
