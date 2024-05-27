using Supermarket.Models.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Supermarket.ViewModels
{
    public class AdminPageVM
    {
        public AdminPageVM() 
        {
            StockList = (((MainWindow)Application.Current.MainWindow)._context.Stocks).ToList();
            //ViewStocksListBox.ItemSource = StockList;
        }

        public StockVM StockVM = new StockVM();

        public List<Stock> StockList { get; set; }


    }
}
