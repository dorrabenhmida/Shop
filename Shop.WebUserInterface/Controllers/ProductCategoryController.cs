using Shop.Core.Logique;
using Shop.Core.Model;
using Shop.DataAccess.Sql;
using Shop.DataAccessInMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.WebUserInterface.Controllers
{
    public class ProductCategoryController : Controller
    {

        IRepository<ProductCategory> context;

        public ProductCategoryController()
        {
            context = new SqlRepository<ProductCategory>(new MyContext());
        }

        public ProductCategoryController(IRepository<ProductCategory> context)
        {
            this.context = context;
        }


        // GET: ProductCategory
        public ActionResult Index()
        {
            List<ProductCategory> productsCat = context.Collection().ToList();
            return View(productsCat);
           
        }

        public ActionResult Create()
        {
            ProductCategory prodCat = new ProductCategory();

            return View(prodCat);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductCategory productCat)
        {
            ProductCategory prodCat = new ProductCategory();
            if (!ModelState.IsValid)
            {
                return View(productCat);
            }
            else
            {
                context.Insert(productCat);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

        }


        public ActionResult Edit(int id)
        {
            try
            {
                ProductCategory pprodCat = context.FindById(id);
                if (pprodCat == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    return View(pprodCat);
                }
            }
            catch (Exception)
            {

                return HttpNotFound();
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductCategory productCat, int id)
        {
            try

            {
                ProductCategory prodCatToEdit = context.FindById(id);
                if (prodCatToEdit == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    if (!ModelState.IsValid)
                    {
                        return View(productCat);
                    }
                    else
                    {
                        // context.UpDate(product); // ce n'es pas un context entity framwork
                        prodCatToEdit.Category = productCat.Category;
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

        public ActionResult Delete(int id)
        {
            try
            {
                ProductCategory prodCat = context.FindById(id);
                if (prodCat == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    return View(prodCat);
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
                ProductCategory prodCatToDelete = context.FindById(id);
                if (prodCatToDelete == null)
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