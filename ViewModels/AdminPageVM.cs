using Supermarket.Commands;
using Supermarket.Models.BusinessLogicLayer;
using Supermarket.Models.EntityLayer;
using Supermarket.Services;
using Supermarket.ViewModels;

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
            CategoryVM = new CategoryVM();
            StockService = new StockService();
            InitializeLists();
            StockQuantity = 1;
            //ViewStocksListBox.ItemSource = StockList;
        }

        public void InitializeLists()
        {
            //Stocks
            StockList = new ObservableCollection<Stock>();
            var list = (((MainWindow)Application.Current.MainWindow)._context.Stocks).ToList();
            if (list != null)
                foreach (var item in list)
                    StockList.Add(item);

            //Categories
            CategoryList = new ObservableCollection<Category>();
            var c = (((MainWindow)Application.Current.MainWindow)._context.Categories).ToList();
            if (c != null)
                foreach (var item in c)
                    CategoryList.Add(item);
            ProductsByProducer = new ObservableCollection<Product>();

            ProductList = new ObservableCollection<Product>();
            var p = (((MainWindow)Application.Current.MainWindow)._context.Products).ToList();
            if (p != null)
                foreach (var item in p)
                    ProductList.Add(item);

        }

        public StockVM StockVM { get; set; }
        public ProductVM ProductVM { get; set; }
        public ProducerVM ProducerVM { get; set; }
        public CategoryVM CategoryVM { get; set; }

        private float Markup = 40;
        public float StockQuantity { get; set; }
        public float StockSellingPrice { get; set; }
        public DateTime StockRestockDate { get; set; }
        public DateTime StockExpirationDate { get; set; }

       
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
        public ICommand AddProductCommand => new RelayCommand(x => {
            var prod = new Product() 
            { 
                Name = ProductVM.Name, 
                Barcode = ProductVM.Barcode, 
                Price = ProductVM.Price, 
                ExpirationDate = ProductVM.ExpirationDate, 
                Producer = ProducerVM.producerService.GetProducerByName(ProductVM.Producer.Name), 
                Category = ProductVM.Category }; 
            if (prod != null)
            {
                ProductVM.productService.CreateProduct(prod);
            }
        });
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

        public ICommand AddCategoryCommand => new RelayCommand(x => { if (x != null) { var cat = new Category() { Name = CategoryVM.NewCategoryName }; CategoryVM.categoryService.CreateCategory((Category)cat); } });

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

        public ObservableCollection<Stock> StockList { get; set; }
        public ObservableCollection<Category> CategoryList { get; set; }
        private ObservableCollection<Product> productList;
        public ObservableCollection<Product> ProductList
        {
            get { return productList; }
            set
            {
                productList = value;
                OnPropertyChanged(nameof(productList));
            }
        }
        public StockService StockService { get; set; }
        //public ProductService ProductService { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
