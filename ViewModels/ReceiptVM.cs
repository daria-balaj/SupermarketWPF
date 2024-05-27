using Supermarket.Models.EntityLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.ViewModels
{
    //public class ReceiptItem : Product
    //{
    //    public int Quantity { get; set; }
    //    public float Subtotal => Quantity * Price;
    //}
    public class ReceiptVM
    {
        public User CurrentUser { get; set; }
        public ObservableCollection<ReceiptItem> items { get; set; }
        public ReceiptVM() { }
    }
}
