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

        public User CurrentUser { get; set; }

        public ProductVM ProductVM { get; set; } = new ProductVM();

        private readonly ProductService productService;
        private readonly ReceiptService receiptService;

        public ObservableCollection<Models.EntityLayer.ReceiptItem> Items { get; set; }

        public User Cashier { get; set; } 
        

        public float Total { get; set; }

       
        public void AddToReceipt()
        {
            if (ProductVM.SelectedProduct != null)
            {
                Models.EntityLayer.ReceiptItem item = new Models.EntityLayer.ReceiptItem { ReceiptID = (((MainWindow)Application.Current.MainWindow)._context.Receipts.Count() + 1).ToString(), Product = ProductVM.SelectedProduct, ProductID = ProductVM.SelectedProduct.ID, Quantity = 1 };
                item.Subtotal = item.Quantity * ProductVM.SelectedProduct.Price;
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
