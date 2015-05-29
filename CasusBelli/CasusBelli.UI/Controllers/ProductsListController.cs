using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using CasusBelli.Domain.Abstract;
using CasusBelli.Domain.Entities;
using CasusBelli.UI.Models;

namespace CasusBelli.UI.Controllers
{
    public class ProductsListController : Controller
    {
        private ISubTypeRepository subtypeRepository;
        private IProductRepository productRepository;
        public ProductsListController(ISubTypeRepository repository, IProductRepository prodrep)
        {
            subtypeRepository = repository;
            productRepository = prodrep;
        }
        public ActionResult Index(int TypeId = 0, int CountryId = 0)
        {
            IEnumerable<ProductSubType> s = new List<ProductSubType>();
            if (TypeId == 0 && CountryId == 0) { s =  subtypeRepository.ProductSubTypes.ToList(); }
            else if (TypeId != 0) { s = subtypeRepository.ProductSubTypes.ToList().Where(p => p.TypeId == Convert.ToInt32(TypeId)).ToList(); }
            else if (CountryId != 0) { s = subtypeRepository.ProductSubTypes.ToList().Where(p => p.CountryId == Convert.ToInt32(CountryId)).ToList(); }
            List<Product> products = productRepository.Products.ToList();
            List<ProductListViewModel> prlistvm = new List<ProductListViewModel>();
            //foreach (ProductSubType v in s)
            //{
            //    prlistvm.Add(new ProductListViewModel(v, products.Where(p=>p.SubTypeId == v.SubTypeId).ToList()));
            //}
            return View(prlistvm);
        }

    }
}
