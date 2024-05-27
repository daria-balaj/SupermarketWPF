using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Models.EntityLayer
{
    public class ReceiptItem
    {
        //[Key]
        //public string ID { get; set; }
        public int ReceiptID { get; set; }
        public Receipt Receipt { get; set; }
        public Product Product { get; set; }
        public int ProductID { get; set; }
        public float Quantity { get; set; }
        public float Subtotal { get; set; }
        public ReceiptItem() { }

    }
}
