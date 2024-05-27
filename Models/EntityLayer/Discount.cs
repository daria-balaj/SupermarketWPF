using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Models.EntityLayer
{
    public enum Reasons
    {
        AboutToExpire,
        Clearance,
        Promotional,
        Seasonal

    }
    public class Discount
    {
        [Key]
        public int ID { get; set; }
        public int ProductID { get; set; }
        public Reasons Reason { get; set; }
        public int Percentage { get; set; }
        public DateOnly Start { get; set; }
        public DateOnly End { get; set; }

        public Discount() { }
        public Discount(int productID, Reasons reason, int percentage, DateOnly start, DateOnly end)
        {
            ProductID = productID;
            Reason = reason;
            Percentage = percentage;
            Start = start;
            End = end;
        }
    }

}
