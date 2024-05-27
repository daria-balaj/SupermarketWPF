using Supermarket.Data;
using Supermarket.Models.EntityLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ReceiptItem = Supermarket.Models.EntityLayer.ReceiptItem;


namespace Supermarket.Models.BusinessLogicLayer
{
    public class ReceiptService
    {
        public ReceiptService() 
        {
            Context = ((MainWindow)Application.Current.MainWindow)._context;
        }

        public DataContext Context { get; set; }

        public void CreateReceipt(ObservableCollection<ReceiptItem> items, float total)
        {
            var r = new Receipt() { Cashier = Context.CurrentUser, Items = items.ToList(), Emitted = DateTime.Now, Total = total };
            Context.Receipts.Add(r);
            Context.SaveChanges();
        }
    }
}
