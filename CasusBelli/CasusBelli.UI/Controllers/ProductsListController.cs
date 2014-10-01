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
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ByType(string TypeId)
        {
            IEnumerable<ProductSubType> s = subtypeRepository.ProductSubTypes.ToList().Where(p => p.TypeId == Convert.ToInt32(TypeId)).ToList();
            return View(s);
        }

    }
}
