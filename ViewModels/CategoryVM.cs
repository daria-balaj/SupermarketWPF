using Supermarket.Models.BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.ViewModels
{
    public class CategoryVM
    {
        public CategoryVM() 
        {
            categoryService = new CategoryService();    
        }

        public CategoryService categoryService { get; set; }
        public string NewCategoryName { get; set; }

    }
}
