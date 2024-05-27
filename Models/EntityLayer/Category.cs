using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Models.EntityLayer
{
    public class Category
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public Category() { }
    }

}
