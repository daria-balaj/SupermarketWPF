using Supermarket.Models.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Models.EntityLayer
{
    //unitate de masura, data aprovizionarii, data expirarii,
    //pret achizitie, pret de vanzare
    public class Stock
    {
        public string ID { get; set; }
        public string ProductID { get; set; }
        public string Measurement { get; set; }
        public DateTime RestockDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public float BuyingPrice { get; set; }
        public float SellingPrice { get; set; }

        public Stock() { }

    }
}
