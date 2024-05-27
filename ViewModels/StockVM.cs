using Supermarket.Commands;
using Supermarket.Data;
using Supermarket.Models.EntityLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Supermarket.ViewModels
{
    public class StockVM
    {
        private string _stockID;
        private string _stockProductID;
        private string _stockMeasurement;
        private DateTime _stockRestockDate;
        private DateTime _stockExpirationDate;
        private float _stockBuyingPrice;
        private float _stockSellingPrice;


        public string StockID
        {
            get => _stockID;
            set { _stockID = value; OnPropertyChanged(); }
        }

        public string StockProductID
        {
            get => _stockProductID;
            set { _stockProductID = value; OnPropertyChanged(); }
        }

        public string StockMeasurement
        {
            get => _stockMeasurement;
            set { _stockMeasurement = value; OnPropertyChanged(); }
        }

        public DateTime StockRestockDate
        {
            get => _stockRestockDate;
            set { _stockRestockDate = value; OnPropertyChanged(); }
        }

        public DateTime StockExpirationDate
        {
            get => _stockExpirationDate;
            set { _stockExpirationDate = value; OnPropertyChanged(); }
        }

        public float StockBuyingPrice
        {
            get => _stockBuyingPrice;
            set { _stockBuyingPrice = value; OnPropertyChanged(); }
        }

        public float StockSellingPrice
        {
            get => _stockSellingPrice;
            set { _stockSellingPrice = value; OnPropertyChanged(); }
        }

        public ICommand AddStockCommand => new RelayCommand(x => AddStock());

        public StockVM()
        {

        }

        private void AddStock()
        {
            // Add the logic to add a new stock item to the database
            var newStock = new Stock
            {
                ID = StockID,
                ProductID = StockProductID,
                Measurement = StockMeasurement,
                RestockDate = StockRestockDate,
                ExpirationDate = StockExpirationDate,
                BuyingPrice = StockBuyingPrice,
                SellingPrice = StockSellingPrice
            };

            // Add newStock to the database using your DataContext
            using (var context = new DataContext())
            {
                context.Stocks.Add(newStock);
                context.SaveChanges();
            }
        }

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
