using Shop.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.ViewModel
{
    public class ProductListViewModel
    {
        public List<Product> Products { get; set; }
        public List<ProductCategory> ProductsCategory { get; set; }
    }
}
