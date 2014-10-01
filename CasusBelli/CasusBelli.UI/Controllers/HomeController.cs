using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CasusBelli.Domain.Abstract;
using CasusBelli.Domain.Entities;

namespace CasusBelli.UI.Controllers
{
    public class HomeController : Controller
    {
        private ITypeRepository typeRepository;
        private ISubTypeRepository subTypeRepository;
        public HomeController(ITypeRepository repository, ISubTypeRepository subType)
        {
            typeRepository = repository;
            subTypeRepository = subType;
        }

        public ActionResult Index()
        {
            IEnumerable<ProductType> s = typeRepository.Types.ToList();      
            return View(s);
        }

        public FileContentResult GetImageSubType(int SubTypeId)
        {
            ProductSubType prod = subTypeRepository.ProductSubTypes.FirstOrDefault(p => p.SubTypeId == SubTypeId);
            if (prod != null)
            {
                return File(prod.ImageData, prod.ImageMimeData);
            }
            else
            {
                return null;
            }
        }

        public FileContentResult GetImageType(int TypeId)
        {
            ProductType prod = typeRepository.Types.FirstOrDefault(p => p.TypeId == TypeId);
            if (prod != null)
            {
                return File(prod.ImageData, prod.ImageMimeData);
            }
            else
            {
                return null;
            }
        }
    }
}
