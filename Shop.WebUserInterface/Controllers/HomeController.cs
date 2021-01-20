using Shop.Core.Logique;
using Shop.Core.Model;
using Shop.Core.ViewModel;
using Shop.DataAccess.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.WebUserInterface.Controllers
{
    public class HomeController : Controller
    {
        IRepository<Product> context;
        IRepository<ProductCategory> Categorycontext;

        public HomeController ()
        {
            Categorycontext = new SqlRepository<ProductCategory>(new MyContext());
            context = new SqlRepository<Product>(new MyContext());

        }
        public ActionResult Index( string Category = null)
        {
            List<Product> products;
            List<ProductCategory> Categories = Categorycontext.Collection().ToList(); 
            if (Category == null)
            {
                products = context.Collection().ToList();

            }
            else
            {
                products = context.Collection().Where(p => p.Category == Category).ToList();
            }

            ProductListViewModel viewModel = new ProductListViewModel();
            viewModel.Products = products;
            viewModel.ProductsCategory = Categories;
            return View(viewModel);
        }


        public ActionResult Details(int id )
        {
            Product p = context.FindById(id); 
            if(p == null)
            {
                return HttpNotFound(); 
            }
            else
            {
                return View(p); 
            }           
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}