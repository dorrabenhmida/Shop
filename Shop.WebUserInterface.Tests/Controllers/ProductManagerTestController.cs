using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shop.Core.Logique;
using Shop.Core.Model;
using Shop.WebUserInterface.Controllers;
using Shop.WebUserInterface.Tests.Mock;
using System;
using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;
using Shop.Core.ViewModel;
using System.IO;

namespace Shop.WebUserInterface.Tests.Controllers
{
    [TestClass]
    public class ProductManagerTestController
    {
        IRepository<Product> context;
        IRepository<ProductCategory> categoryContext;

        [TestInitialize]
        public void SetupUp()
        {
            context = new MockContext<Product>();
            categoryContext = new MockContext<ProductCategory>();
        }

        [TestMethod]
        [TestCategory("Product Manager Controller ")]
        public void IndexAction_DoesReturn_ListOfProduct()
        {
            context.Insert(new Product());
            context.Insert(new Product());
            ProductManagerController controllerProduct = new ProductManagerController(context, categoryContext);
            var result = controllerProduct.Index() as ViewResult;
            var viewModel = (List<Product>)result.ViewData.Model;
            Assert.AreEqual(2, viewModel.Count);
        }

        [TestMethod]
        [TestCategory("Product Manager Controller ")]
        public void Create_DoesReturn_ProductListOfCategory ()
        {
            context.Insert(new Product());
            categoryContext.Insert(new ProductCategory());
            categoryContext.Insert(new ProductCategory());
            ProductManagerController controllerProduct = new ProductManagerController(context, categoryContext);
            var result = controllerProduct.Create() as ViewResult;
            var viewModel = (ProductCategoyViewModel)result.ViewData.Model;

            Assert.IsNotNull(viewModel.Product);
            Assert.AreEqual(2, viewModel.ProductCtagories.Count()); 
        }

        [TestMethod]
        [TestCategory("Product Manager Controller ")]
        public void CreateWithHttpPostedFile_DoesInsertProductAndImage()
        {

            // arrange 
            string filePath = Path.GetFullPath(@"c:\tmp\images2.jpg");
            FileStream fileStream = new FileStream(filePath, FileMode.Open);
            MyFileBase fileImage = new MyFileBase(fileStream, "image/jpg", "image.jpg");

            ProductManagerController controllerProduct = new ProductManagerController(context, categoryContext);
            var result = controllerProduct.Create( new Product { Id = 1 }, fileImage) as ViewResult;

            Assert.IsNotNull(context.FindById(1)); 

        }
    }

   
}
