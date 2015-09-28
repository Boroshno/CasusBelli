using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CasusBelli.Domain.Abstract;
using CasusBelli.Domain.Entities;

namespace CasusBelli.UI.Areas.Admin.Controllers
{
    public class ImageController : Controller
    {
        //
        // GET: /Admin/Image/

        private ITypeRepository typeRepository;
        private ISubTypeRepository subTypeRepository;
        public ImageController(ITypeRepository repository, ISubTypeRepository subType)
        {
            typeRepository = repository;
            subTypeRepository = subType;
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
