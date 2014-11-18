using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using CasusBelli.Domain.Abstract;
using CasusBelli.Domain.Entities;

namespace CasusBelli.UI.Controllers
{
    public class ProductsListController : Controller
    {
        private ISubTypeRepository subtypeRepository;
        public ProductsListController(ISubTypeRepository repository)
        {
            subtypeRepository = repository;
        }
        public ActionResult Index(int TypeId = 0, int CountryId = 0)
        {
            IEnumerable<ProductSubType> s = new List<ProductSubType>();
            if (TypeId == 0 && CountryId == 0) { s = subtypeRepository.ProductSubTypes.ToList(); }
            else if (TypeId != 0) { s = subtypeRepository.ProductSubTypes.ToList().Where(p => p.TypeId == Convert.ToInt32(TypeId)).ToList(); }
            else if (CountryId != 0) { s = subtypeRepository.ProductSubTypes.ToList().Where(p => p.CountryId == Convert.ToInt32(CountryId)).ToList(); }
            return View(s);
        }

    }
}
