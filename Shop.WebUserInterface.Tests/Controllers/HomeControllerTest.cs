using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shop.Core.Logique;
using Shop.Core.Model;
using Shop.Core.ViewModel;
using Shop.WebUserInterface;
using Shop.WebUserInterface.Controllers;
using Shop.WebUserInterface.Tests.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Shop.WebUserInterface.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        [TestCategory("HomeController")]
        public void Index_DoesReturn_Product()
        {
            IRepository<Product> context = new MockContext<Product>();
            IRepository<ProductCategory> categoryContext = new MockContext<ProductCategory>();
            HomeController controller = new HomeController(context, categoryContext);
            context.Insert(new Product());

            var result = controller.Index() as ViewResult;
            var viewModel = (ProductListViewModel)result.ViewData.Model;

            Assert.AreEqual(1, viewModel.Products.Count()); 
        }
        
    }
}
