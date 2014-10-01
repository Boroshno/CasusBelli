using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CasusBelli.Domain.Abstract;
using CasusBelli.Domain.Concrete;
using CasusBelli.Domain.Entities;
using CasusBelli.UI.Models;

namespace CasusBelli.UI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private ITypeRepository typeRepository;
        private ISubTypeRepository subTypeRepository;
        private ICountryRepository countryRepository;

        public AdminController(ITypeRepository typeRep, ISubTypeRepository subTypeRep, ICountryRepository countryRep)
        {
            typeRepository = typeRep;
            subTypeRepository = subTypeRep;
            countryRepository = countryRep;
        }

        public ActionResult Index()
        {
            return View();
        }

        #region TypesActions
        public ActionResult Types()
        {
            return View(typeRepository.Types);
        }

        public ActionResult EditType(int id)
        {
            return View(typeRepository.Types.FirstOrDefault(p => p.TypeId == id));
        }

        [HttpPost]
        public ActionResult EditType(ProductType productType, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    productType.ImageMimeData = image.ContentType;
                    byte[] bt = new byte[image.ContentLength];
                    image.InputStream.Read(bt, 0, image.ContentLength);
                    productType.ImageData = bt;//NullableHelper.ConvertArray(bt);
                }
                typeRepository.AddOrUpdateType(productType);
                TempData["message"] = string.Format("Зміни типу до {0} було збережено", productType.TypeName);
                return RedirectToAction("Types");
            }
            else
            {
                return View(productType);
            }
        }

        public ActionResult DeleteType(int id)
        {
            ProductType type = typeRepository.Types.FirstOrDefault(p => p.TypeId == id);
            typeRepository.DeleteType(type);
            TempData["message"] = string.Format("Тип {0} було видалено", type.TypeId);
            return RedirectToAction("Types");
        }

        public ActionResult CreateType()
        {
            return View(new ProductType());
        }

        [HttpPost]
        public ActionResult CreateType(ProductType productType, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    productType.ImageMimeData = image.ContentType;
                    byte[] bt = new byte[image.ContentLength];
                    image.InputStream.Read(bt, 0, image.ContentLength);
                    productType.ImageData = bt;//NullableHelper.ConvertArray(bt);
                }
                productType.TypeId = typeRepository.Types.Max(p => p.TypeId) + 1;
                typeRepository.AddOrUpdateType(productType);
                return RedirectToAction("Types");
            }
            else
            {
                return View(productType);
            }
        }
        #endregion

        public ActionResult SubTypes()
        {
            List<SubTypesViewModel> subtypes = new List<SubTypesViewModel>();
            var s = subTypeRepository.ProductSubTypes.ToList();
            foreach (ProductSubType ps in subTypeRepository.ProductSubTypes)
            {
                subtypes.Add(new SubTypesViewModel(ps, typeRepository.Types.ToList(), countryRepository.Countries.ToList()));
            }
            return View(subtypes);
        }

        public ActionResult EditSubType(int id)
        {
            SubTypesViewModel model =
                new SubTypesViewModel(subTypeRepository.ProductSubTypes.First(p => p.SubTypeId == id),
                    typeRepository.Types.ToList(), countryRepository.Countries.ToList());
            return View(model);
        
        }

        [HttpPost]
        public ActionResult EditSubType(SubTypesViewModel subTypesViewModel, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    subTypesViewModel.ImageMimeData = image.ContentType;
                    byte[] bt = new byte[image.ContentLength];
                    image.InputStream.Read(bt, 0, image.ContentLength);
                    subTypesViewModel.ImageData = bt;//NullableHelper.ConvertArray(bt);
                }
                subTypeRepository.AddOrUpdateSubType(subTypesViewModel);
                TempData["message"] = string.Format("Зміни підтипу до {0} було збережено", subTypesViewModel.SubTypeName);
                return RedirectToAction("SubTypes");
            }
            else
            {
                return View(subTypesViewModel);
            }
        }

        public ActionResult DeleteSubType(int id)
        {
            ProductSubType subtype = subTypeRepository.ProductSubTypes.FirstOrDefault(p => p.SubTypeId == id);
            subTypeRepository.DeleteSubType(subtype);
            TempData["message"] = string.Format("Підтип {0} було видалено", subtype.TypeId);
            return RedirectToAction("SubTypes");
        }

        public ActionResult CreateSubType()
        {
            return View(new SubTypesViewModel(new EFProductTypeRepository(), new EFCountryRepository()));
        }

        [HttpPost]
        public ActionResult CreateSubType(SubTypesViewModel productSubType, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    productSubType.ImageMimeData = image.ContentType;
                    byte[] bt = new byte[image.ContentLength];
                    image.InputStream.Read(bt, 0, image.ContentLength);
                    productSubType.ImageData = bt;//NullableHelper.ConvertArray(bt);
                }
                productSubType.SubTypeId = subTypeRepository.ProductSubTypes.Max(p=>p.SubTypeId)+ 1;
                subTypeRepository.AddOrUpdateSubType(productSubType);
                return RedirectToAction("SubTypes");
            }
            else
            {
                productSubType.AvailableTypes = typeRepository.Types.ToList();
                return View(productSubType);
            }
        }

    }
}
