using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Models.EntityLayer
{

    public class Product
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Barcode { get; set; }
        public Category Category { get; set; }
        public Producer Producer { get; set; }
        public DateTime ExpirationDate { get; set; }
        public Discount? Discount { get; set; }
        public Product() { }
    }
}
