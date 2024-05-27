using Supermarket.Commands;
using Supermarket.Models.BusinessLogicLayer;
using Supermarket.Models.EntityLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.DirectoryServices;
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
            StockVM = new StockVM();
            ProductVM = new ProductVM();
            ProducerVM = new ProducerVM();
            StockList = (((MainWindow)Application.Current.MainWindow)._context.Stocks).ToList();
            ProductsByProducer = new ObservableCollection<Product>();
            StockQuantity = 1;
            //ViewStocksListBox.ItemSource = StockList;
        }

        public StockVM StockVM { get; set; }
        public ProductVM ProductVM { get; set; }
        public ProducerVM ProducerVM { get; set; }

        private float Markup = 40;
        public float StockQuantity { get; set; }
        public float StockSellingPrice { get; set; }
        public DateOnly StockRestockDate { get; set; }
        public DateOnly StockExpirationDate { get; set; }
        public float _stockBuyingPrice { get; set; }
        public float StockBuyingPrice
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

        public ObservableCollection<Product> productsByProducer;
        public ObservableCollection<Product> ProductsByProducer 
        {
            get { return productsByProducer; }
            set { productsByProducer = value; OnPropertyChanged(nameof(productsByProducer)); }
        }

        public void ViewProductsFromProducer()
        {
            ProductsByProducer.Clear();
            if (ProducerVM.SelectedProducer != null)
            {
                var results = ProductVM.productService.GetProductsByProducer(ProducerVM.SelectedProducer.Name);
                if (results != null)
                {
                    results.AsQueryable().GroupBy(c => c.Category.Name);
                    foreach (var r in results)
                        ProductsByProducer.Add(r);
                }
            }
        }

        public ICommand AddStockCommand => new RelayCommand(x => AddStock());
        public ICommand AddProducerCommand => new RelayCommand(x =>
        {
            if (ProducerVM != null && ProducerVM.ProducerName != null && ProducerVM.ProducerOrigin != null)
            {
                var producer = new Producer() { Name = ProducerVM.ProducerName, Country = ProducerVM.ProducerOrigin };
                if (producer != null)
                {
                    ProducerVM.producerService.AddProducer(producer);
                    //Producers.Add(producer);
                }
            }
        });

        public ICommand ModifyProducerCommand => new RelayCommand(x => ProducerVM.producerService.UpdateProducer(ProducerVM.SelectedProducer.ID, ProducerVM.SelectedProducer.Name, ProducerVM.SelectedProducer.Country));
        public void AddStock()
        {
            var stock = new Stock() { Quantity = StockVM.StockQuantity, ExpirationDate = StockVM.StockExpirationDate, ProductID = ProductVM.SelectedProduct.ID };
            StockService.CreateStock(stock);
        }

        public ICommand DeleteProducerCommand => new RelayCommand(x =>
        {
            if (ProducerVM.SelectedProducer != null)
            {
                var name = ProducerVM.SelectedProducer.Name;
                if (!string.IsNullOrEmpty(name))
                {
                    ProducerVM.producerService.DeleteProducer(name);
                    //var producerToDelete = SearchResults.FirstOrDefault(p => p.Name == name);
                    //if (producerToDelete != null)
                    //{
                    //    SearchResults.Remove(producerToDelete);
                    //}
                }
            }
        });

        public List<Stock> StockList { get; set; }
        public StockService StockService { get; set; }
        //public ProductService ProductService { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
