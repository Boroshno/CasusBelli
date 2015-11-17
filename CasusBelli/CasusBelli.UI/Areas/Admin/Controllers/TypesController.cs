using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CasusBelli.Domain.Abstract;
using CasusBelli.Domain.Entities;

namespace CasusBelli.UI.Areas.Admin.Controllers
{
    [Authorize]
    public class TypesController : Controller
    {
        //
        // GET: /Admin/Types/
        private ITypeRepository typeRepository;

        public TypesController(ITypeRepository typeRep)
        {
            typeRepository = typeRep;
        }

        #region TypesActions
        public ViewResult Index()
        {
            return View(typeRepository.Types);
        }

        public ViewResult EditType(int id)
        {
            ProductType prod = typeRepository.Types.FirstOrDefault(p => p.TypeId == id);
            if (prod != null)
                return View(prod);
            else
            {
                throw new HttpException(404, "Such type doesnt exist");
            }
        }

        [HttpPost]
        public ActionResult EditType(ProductType productType, HttpPostedFileBase image)
        {
            if (ModelState.IsValid && productType.TypeId > 0)
            {
                //if (image != null)
                //{
                //    productType.ImageMimeData = image.ContentType;
                //    byte[] bt = new byte[image.ContentLength];
                //    image.InputStream.Read(bt, 0, image.ContentLength);
                //    productType.ImageData = bt;//NullableHelper.ConvertArray(bt);
                //}
                //else
                //{
                //    ProductType oldtype = typeRepository.Types.FirstOrDefault(p => p.TypeId == productType.TypeId);
                //    productType.ImageData = oldtype.ImageData;
                //}
                typeRepository.AddOrUpdateType(productType);
                TempData["message"] = string.Format("Зміни типу до {0} було збережено", productType.TypeName);
                return RedirectToAction("Index");
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
            return RedirectToAction("Index");
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
                //if (image != null)
                //{
                //    productType.ImageMimeData = image.ContentType;
                //    byte[] bt = new byte[image.ContentLength];
                //    image.InputStream.Read(bt, 0, image.ContentLength);
                //    productType.ImageData = bt; //NullableHelper.ConvertArray(bt);
                //}
                productType.TypeId = typeRepository.Types.Max(p => p.TypeId) + 1;
                typeRepository.AddOrUpdateType(productType);
                return RedirectToAction("Index");
            }
            else
            {
                return View(productType);
            }
        }
        #endregion

    }
}
