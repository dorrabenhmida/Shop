using Shop.Core.Logique;
using Shop.Core.Model;
using Shop.Core.ViewModel;
using Shop.DataAccess.Sql;
using Shop.DataAccessInMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.WebUserInterface.Controllers
{
    public class ProductManagerController : Controller
    {
        IRepository<Product> context;
        IRepository<ProductCategory> contextCategory; 

        public ProductManagerController ()
        {
            context = new SqlRepository<Product>(new MyContext());
            contextCategory = new SqlRepository<ProductCategory>(new MyContext()); 
        }



        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList(); 
            return View(products);
        }

        public ActionResult Create ()
        {
            Product p = new Product();
            ProductCategoyViewModel ViewModel = new ProductCategoyViewModel();
            ViewModel.Product = new Product();
            ViewModel.ProductCtagories = contextCategory.Collection();

            return View(ViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {

           
            if (!ModelState.IsValid)
            {
                return View(product); 
            }
            else
            {
                context.Insert(product);
                context.SaveChanges();
                return RedirectToAction("Index"); 
            }

        }

        public ActionResult Edit (int id)
        {
            try
            {
                Product p = context.FindById(id);
                if (p == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    ProductCategoyViewModel ViewModel = new ProductCategoyViewModel();
                    ViewModel.Product = p; 
                    ViewModel.ProductCtagories = contextCategory.Collection();
                    return View(ViewModel);
                }
            }
            catch (Exception)
            {

                return HttpNotFound();
            }     
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product ,  int id)
        {
            try
              
            {
                Product prodToEdit = context.FindById(id);
                if (prodToEdit == null)
                {
                    return HttpNotFound(); 
                }
                else
                {
                    if (!ModelState.IsValid)
                    {
                        return View(product);
                    }
                    else
                    {
                        // context.UpDate(product); // ce n'es pas un context entity framwork
                        prodToEdit.Name = product.Name;
                        prodToEdit.Description = product.Description;
                        prodToEdit.Category = product.Category;
                        prodToEdit.Price = product.Price;
                        prodToEdit.Image = product.Image;
                        context.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                
            }
            catch (Exception)
            {

                return HttpNotFound();
            }
           
        }

        public ActionResult Delete( int id )
        {
            try
            {
                Product p = context.FindById(id);
                if (p == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    return View(p);
                }
            }
            catch (Exception)
            {

                return HttpNotFound();
            }                    
        }


        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            try
            {
                Product prodToDelete = context.FindById(id);
                if (prodToDelete == null)
                {
                    return HttpNotFound();

                }
                else
                {
                    context.Delete(id);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {

                return HttpNotFound();
            }
           
        }
    }
}