using Supermarket.Commands;
using Supermarket.Models.EntityLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;

namespace Supermarket.ViewModels
{
    public class AdminPageVM
    {
        public AdminPageVM() 
        {
            StockList = (((MainWindow)Application.Current.MainWindow)._context.Stocks).ToList();
            StockQuantity = 1;
            //ViewStocksListBox.ItemSource = StockList;
        }

        public StockVM StockVM = new StockVM();
        public ProductVM ProductVM = new ProductVM();
        private float Markup = 40;
        public float StockQuantity { get; set; }
        public float StockSellingPrice { get; set; }
        public float _stockBuyingPrice
        {
            get
            {
                return _stockBuyingPrice;
            }
            set
            {
                _stockBuyingPrice = value;
                StockSellingPrice = _stockBuyingPrice + ((_stockBuyingPrice * Markup) / 100);
                OnPropertyChanged(nameof(_stockBuyingPrice));
            }
        }

        public ICommand AddStockCommand => new RelayCommand(x => AddStock());

        public void AddStock()
        {
            var stock = new Stock() { ProductID = ProductVM.SelectedProduct.ID };
        }
        public List<Stock> StockList { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
