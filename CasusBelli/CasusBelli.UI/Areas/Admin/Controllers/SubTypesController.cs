using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CasusBelli.Domain.Abstract;
using CasusBelli.Domain.Concrete;
using CasusBelli.Domain.Entities;
using CasusBelli.UI.Models;

namespace CasusBelli.UI.Areas.Admin.Controllers
{
    public class SubTypesController : Controller
    {
        //
        // GET: /Admin/SubTypes/

        private ISubTypeRepository subTypeRepository;
        private ITypeRepository typeRepository;
        private ICountryRepository countryRepository;
        private IProductRepository productRepository;

        public SubTypesController(ISubTypeRepository subTypeRep, ITypeRepository typeRep, ICountryRepository countryRep, IProductRepository prodRep)
        {
            subTypeRepository = subTypeRep;
            typeRepository = typeRep;
            countryRepository = countryRep;
            productRepository = prodRep;
        }

        public ViewResult Index()
        {
            List<SubTypesViewModel> subtypes = new List<SubTypesViewModel>();
            var s = subTypeRepository.ProductSubTypes.ToList();
            foreach (ProductSubType ps in s)
            {
                var watch = Stopwatch.StartNew();
                // the code that you want to measure comes here (Time measuring)

                List<Country> countries = countryRepository.Countries.ToList();
                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;

                subtypes.Add(new SubTypesViewModel(ps, countries));
            }
            return View(subtypes);
        }

        public ViewResult EditSubType(int id)
        {
            SubTypesViewModel model;
            try
            {
                 model =
                    new SubTypesViewModel(subTypeRepository.ProductSubTypes.First(p => p.SubTypeId == id),
                        typeRepository.Types.ToList(), countryRepository.Countries.ToList(),
                        productRepository.Products.Where(p => p.SubTypeId == id).ToList());
            }
            catch(Exception ex)
            {
                throw new HttpException(404, "Such subtype doesnt exist");
            }
            return View(model);

        }

        [HttpPost]
        public ActionResult EditSubType(SubTypesViewModel subTypesViewModel, HttpPostedFileBase image)
        {
            try
            {
                if (ModelState.IsValid &&
                    subTypeRepository.ProductSubTypes.First(p => p.SubTypeId == subTypesViewModel.SubTypeId) != null)
                {
                    if (image != null)
                    {
                        subTypesViewModel.ImageMimeData = image.ContentType;
                        byte[] bt = new byte[image.ContentLength];
                        image.InputStream.Read(bt, 0, image.ContentLength);
                        subTypesViewModel.ImageData = bt; //NullableHelper.ConvertArray(bt);
                    }
                    subTypeRepository.AddOrUpdateSubType(subTypesViewModel);
                    TempData["message"] = string.Format("Зміни підтипу до {0} було збережено",
                        subTypesViewModel.SubTypeName);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(subTypesViewModel);
                }
            }
            catch (Exception ex)
            {
                throw new HttpException(404, "Such subtype doesnt exist");
            }
        }

        public ActionResult DeleteSubType(int id)
        {
            ProductSubType subtype = subTypeRepository.ProductSubTypes.FirstOrDefault(p => p.SubTypeId == id);
            subTypeRepository.DeleteSubType(subtype);
            TempData["message"] = string.Format("Підтип {0} було видалено", subtype.TypeId);
            return RedirectToAction("Index");
        }

        public ActionResult CreateSubType()
        {
            return View(new SubTypesViewModel(new EFProductTypeRepository(), new EFCountryRepository(), new EFProductRepository()));
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
                productSubType.SubTypeId = subTypeRepository.ProductSubTypes.Max(p => p.SubTypeId) + 1;
                subTypeRepository.AddOrUpdateSubType(productSubType);
                return RedirectToAction("Index");
            }
            else
            {
                productSubType.AvailableTypes = typeRepository.Types.ToList();
                return View(productSubType);
            }
        }
    }
}
