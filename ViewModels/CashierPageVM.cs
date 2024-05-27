using Supermarket.Models.EntityLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Supermarket.Commands;
//using static Supermarket.ViewModels.CashierPageVM;
using Supermarket.Models.BusinessLogicLayer;
using ReceiptItem = Supermarket.Models.EntityLayer.ReceiptItem;
using System.Windows;
using System.ComponentModel;
//using System.Windows.Forms;

namespace Supermarket.ViewModels
{
    public class CashierPageVM
    {
        //public class ReceiptItem : Product
        //{
        //    public int Quantity { get; set; }
        //    public float Subtotal => Quantity * Price;
        //}

        public User CurrentUser { get; set; }

        private readonly ProductService productService;
        private readonly ReceiptService receiptService;
        private ObservableCollection<Product> _searchResults;
        public ObservableCollection<Product> SearchResults
        {
            get { return _searchResults; }
            set
            {
                _searchResults = value;
                OnPropertyChanged(nameof(SearchResults));
            }
        }
        public List<Category> Categories { get; set; }
        public List<Producer> Producers { get; set; }
        public ObservableCollection<Models.EntityLayer.ReceiptItem> Items { get; set; }

        public User Cashier { get; set; } 
        public string SearchID { get; set; }
        public string SearchName { get; set; }
        public string SearchBarcode { get; set; }
        public Category SearchCategory { get; set; }
        public string SearchProducer { get; set; }
        public DateOnly? SearchExpDate { get; set; }

        public Product SelectedProduct { get; set; }

        public float Total { get; set; }

        public ICommand SearchByIDCommand => new RelayCommand(parameter => 
        { 
            SearchResults.Clear(); 
            var p = productService.GetProductByID((string)parameter);
            if (p != null) 
                SearchResults.Add(p); 
        });
        public ICommand SearchByNameCommand => new RelayCommand(parameter => { SearchResults.Clear(); var p = productService.GetProductByName((string)parameter); if (p != null) SearchResults.Add(p); });
        public ICommand SearchByBarcodeCommand => new RelayCommand(parameter => { SearchResults.Clear(); var p = productService.GetProductByBarcode((string)parameter); if (p != null) SearchResults.Add(p); });
        public ICommand SearchByProducerCommand => new RelayCommand(parameter => { SearchResults.Clear(); var p = productService.GetProductsByProducer((string)parameter); if (p != null) foreach (var result in p) SearchResults.Add(result); });
        public ICommand SearchByCategoryCommand => new RelayCommand(parameter => { SearchResults.Clear(); var p = productService.GetProductsByCategory((Category)parameter); if (p != null) foreach (var result in p) SearchResults.Add(result); });
        public ICommand SearchByExpDateCommand => new RelayCommand(parameter => { SearchResults.Clear(); var p = productService.GetProductsByExpDate((DateOnly)parameter); if (p != null) foreach (var result in p) SearchResults.Add(result); });

        public void AddToReceipt()
        {
            if (SelectedProduct != null)
            {
                Models.EntityLayer.ReceiptItem item = new Models.EntityLayer.ReceiptItem { ReceiptID = (((MainWindow)Application.Current.MainWindow)._context.Receipts.Count() + 1).ToString(), Product = SelectedProduct, ProductID = SelectedProduct.ID, Quantity = 1 };
                item.Subtotal = item.Quantity * SelectedProduct.Price;
                Items.Add(item);
            }
        }

        public void SaveReceipt()
        {
            receiptService.CreateReceipt(Items, Total);
        }

        public ICommand AddToReceiptCommand => new RelayCommand(p => AddToReceipt());
        public ICommand SaveReceiptCommand => new RelayCommand(s => SaveReceipt());
        public CashierPageVM() 
        {
            productService = new ProductService();
            receiptService = new ReceiptService();
            SearchResults = new ObservableCollection<Product>();
            Categories = ((MainWindow)Application.Current.MainWindow)._context.Categories.ToList();
            Producers = ((MainWindow)Application.Current.MainWindow)._context.Producers.ToList();
            Items = new ObservableCollection<Models.EntityLayer.ReceiptItem>();
            Cashier = ((MainWindow)Application.Current.MainWindow)._context.CurrentUser;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
