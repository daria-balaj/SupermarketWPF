using Microsoft.EntityFrameworkCore.Migrations.Operations;
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

        public void CreateProduct(Product p)
        {
            if (p != null)
                _context.Products.Add(p);
            _context.SaveChanges();
        }

        public void UpdateProduct(Product p)
        {
            if (p != null)
            {
                _context.Products.Update(p);
                _context.SaveChanges();
            }

        }
        public Product? GetProductByID(int id)
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

        public IEnumerable<Product> GetProductsByExpDate(DateTime expirationDate)
        {
            return _context.Products.Where(p => p.ExpirationDate.ToString() == expirationDate.Date.ToShortDateString()).ToList();
        }
    }
}
