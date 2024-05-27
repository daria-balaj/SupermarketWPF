using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Supermarket.Models.EntityLayer
{
    
    public class Receipt
    {
        public string ID { get; set; }
        public int ReceiptId { get; set; }
        public DateTime Emitted { get; set; }
        public User Cashier { get; set; }
        public List<ReceiptItem> Items { get; set; } = new List<ReceiptItem>();

        public float Total { get; set; }
        public Receipt() 
        {
            //Emitted = DateTime.Now;
            //Total = _items.Sum(item => item.Subtotal);
        }

        public void AddItem(ReceiptItem item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            Items.Add(item);
        }
    }
}
