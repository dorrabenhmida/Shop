using Shop.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.ViewModel
{
   public  class ProductCategoyViewModel
    {
        public Product Product { get; set; }
        public IEnumerable<ProductCategory> ProductCtagories { get; set; }

    }
}
