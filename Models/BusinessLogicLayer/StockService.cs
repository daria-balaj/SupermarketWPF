using Supermarket.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Supermarket.Models.EntityLayer;

namespace Supermarket.Models.BusinessLogicLayer
{
    public class StockService
    {
        public StockService() 
        {
            _context = ((MainWindow)Application.Current.MainWindow)._context;
        }

        private DataContext _context;

        public void CreateStock(Stock stock)
        {
            stock.ID = (_context.Stocks.Count() + 1).ToString();
            _context.Stocks.Add(stock);
            _context.SaveChanges();
        }
    }
}
