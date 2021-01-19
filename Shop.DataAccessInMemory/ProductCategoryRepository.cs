using Shop.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataAccessInMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;

        List<ProductCategory> productsCategory;

        public ProductCategoryRepository()
        {
            productsCategory = cache["productsCategory"] as List<ProductCategory>;
            if (productsCategory == null)
            {
                productsCategory = new List<ProductCategory>();
            }
        }


        public void SaveChanges()
        {
            cache["productsCategory"] = productsCategory;
        }

        public void Insert(ProductCategory p)
        {
            productsCategory.Add(p);
        }

        public void UpDate(ProductCategory p)
        {
            ProductCategory prodCategoryToUpdate = productsCategory.Find(prod => prod.Id == p.Id);
            if (prodCategoryToUpdate != null)
            {
                prodCategoryToUpdate = p;
            }
            else
            {
                throw new Exception("Product category not found");
            }
        }

        public ProductCategory FinfById(int id)
        {
            ProductCategory p = productsCategory.Find(prod => prod.Id == id);

            if (p != null)
            {
                return p;
            }
            else
            {
                throw new Exception("Product category not found");
            }

        }

        public IQueryable<ProductCategory> Collection() // accepte des requette linq sur la liste 
        {
            return productsCategory.AsQueryable();
        }

        public void Delete(int id)
        {
            ProductCategory prodCategoryToDelete = productsCategory.Find(prod => prod.Id == id);
            if (prodCategoryToDelete != null)
            {
                productsCategory.Remove(prodCategoryToDelete);
            }
            else
            {
                throw new Exception("Product category not found");
            }
        }
    }
}
