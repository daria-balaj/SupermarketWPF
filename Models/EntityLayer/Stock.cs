using Supermarket.Models.EntityLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Models.EntityLayer
{
    //unitate de masura, data aprovizionarii, data expirarii,
    //pret achizitie, pret de vanzare
    public class Stock
    {
        [Key]
        public int ID { get; set; }
        public int ProductID { get; set; }
        //public string Measurement { get; set; }
        public string Quantity { get; set; }

        public DateTime RestockDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public float BuyingPrice { get; set; }
        public float SellingPrice { get; set; }

        public Stock() { }

    }
}
