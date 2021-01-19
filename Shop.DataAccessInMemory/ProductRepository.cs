using Shop.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataAccessInMemory
{
  
    public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;

        List<Product> products; 

        public ProductRepository()
        {
            products = cache["products"] as List<Product>;  
            if (products == null)
            {
                products = new List<Product>(); 
            }
        }


        public void SaveChanges()
        {
            cache["products"] = products; 
        }

        public void Insert(Product p)
        {
            products.Add(p); 
        }

        public void UpDate (Product p)
        {
            Product prodToUpdate = products.Find(prod => prod.Id == p.Id); 
            if (prodToUpdate != null)
            {
                prodToUpdate = p; 
            }
            else
            {
                throw new Exception("Product not found"); 
            }
        }

        public Product FinfById( int id)
        {
            Product p = products.Find(prod => prod.Id == id); 

            if (p != null)
            {
                return p; 
            }
            else
            {
                throw new Exception("Product not found");
            }
          
        }

        public IQueryable<Product> Collection() // accepte des requette linq sur la liste 
        {
            return products.AsQueryable(); 
        }

        public void Delete(int id)
        {
            Product prodToDelete = products.Find(prod => prod.Id == id);
            if (prodToDelete != null)
            {
                products.Remove(prodToDelete); 
            }
            else
            {
                throw new Exception("Product not found");
            }
        }
    }
}
