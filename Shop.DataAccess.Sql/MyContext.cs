using Shop.Core.Model;
using System;
using System.Data.Entity;
using System.Linq;

namespace Shop.DataAccess.Sql
{
    public class MyContext : DbContext
    {
       
        public MyContext()
            : base("name=MyContext")
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductsCategories { get; set; }


    }

  
}