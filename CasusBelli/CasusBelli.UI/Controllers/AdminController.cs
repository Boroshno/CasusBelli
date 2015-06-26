using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using CasusBelli.Domain.Abstract;
using CasusBelli.Domain.Concrete;
using CasusBelli.Domain.Entities;
using CasusBelli.UI.Models;
using Microsoft.Ajax.Utilities;
using WebGrease.Css.Extensions;

namespace CasusBelli.UI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private ITypeRepository typeRepository;
        private ISubTypeRepository subTypeRepository;
        private ICountryRepository countryRepository;
        private IProductRepository productRepository;
        private IProductStatusRepository statusRepository;
        private IClientRepository clientRepository;

        public AdminController(ITypeRepository typeRep, ISubTypeRepository subTypeRep, ICountryRepository countryRep, IProductRepository prodRep, IProductStatusRepository prodstatusRep, IClientRepository clientRep)
        {
            typeRepository = typeRep;
            subTypeRepository = subTypeRep;
            countryRepository = countryRep;
            productRepository = prodRep;
            statusRepository = prodstatusRep;
            clientRepository = clientRep;
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

        public ActionResult Products()
        {
            List<AdminProductsListViewModel> productsVM = new List<AdminProductsListViewModel>();
            var types = typeRepository.Types.ToList();
            var subtypes = subTypeRepository.ProductSubTypes.ToList();
            var countries = countryRepository.Countries.ToList();
            var statuses = statusRepository.ProductStatuses.ToList();
            var products = productRepository.Products.ToList();
            foreach (Product product in products)
            {
                productsVM.Add(new AdminProductsListViewModel(product, types, countries, subtypes, statuses));
            }
            
            return View(productsVM);
        }

        [HttpPost]
        public ActionResult SoldProduct(int productid, int price, int count)
        {
            List<AdminProductsListViewModel> productsVM = new List<AdminProductsListViewModel>();
            var types = typeRepository.Types.ToList();
            var subtypes = subTypeRepository.ProductSubTypes.ToList();
            var countries = countryRepository.Countries.ToList();
            var statuses = statusRepository.ProductStatuses.ToList();
            var products = productRepository.Products.ToList();
            foreach (Product product in products)
            {
                productsVM.Add(new AdminProductsListViewModel(product, types, countries, subtypes, statuses));
            }

            if (count == 1)
            {
                AdminProductsListViewModel p = productsVM.First(x => x.ProductId == productid);
                p.StatusId = 3;
                p.SoldPrice = price;
                productRepository.AddOrUpdateProduct(p);
            }
            else
            {
                AdminProductsListViewModel p = productsVM.First(x => x.ProductId == productid);
                List<AdminProductsListViewModel> prodtosold =
                    productsVM.Where(
                        x =>
                            x.Size == p.Size && x.NATOSize == p.NATOSize && x.Price == p.Price &&
                            x.CountryId == p.CountryId && x.AdditionalInfo == p.AdditionalInfo &&
                            x.Condition == p.Condition && x.StatusId == p.StatusId && x.TradePrice == p.TradePrice &&
                            x.SoldPrice == p.SoldPrice && x.SubTypeId == p.SubTypeId &&
                            x.TypeId == p.TypeId).ToList();
                if (prodtosold.Count < count) throw new HttpException("There are is no enought products to sold them.");
                foreach (AdminProductsListViewModel model in prodtosold)
                {
                    model.StatusId = 3;
                    model.SoldPrice = price;
                    productRepository.AddOrUpdateProduct(model);
                    count--;
                    if (count == 0) break;
                }

            }
            return Redirect("/Admin/Products");
        }

        public ActionResult CreateProduct()
        {
            ProductsViewModel newprod = new ProductsViewModel(new EFProductTypeRepository(),
                new EFProductSubTypeRepository(), new EFCountryRepository());
            newprod.Status = "Creating new product...";
            return View(newprod);
        }

        [HttpPost]
        public ActionResult CreateProduct(ProductsViewModel newproduct)
        {
            if (ModelState.IsValid)
            {
                int count = newproduct.Count;
                newproduct.Count = 1;
                while (count > 0)
                {
                    newproduct.ProductId = (productRepository.Products.Max(x => (int?)x.ProductId) ?? 0) + 1;
                    productRepository.AddOrUpdateProduct(newproduct);
                    count--;
                }

                ProductsViewModel copiedproduct = new ProductsViewModel(new EFProductTypeRepository(),
                new EFProductSubTypeRepository(), new EFCountryRepository());
                copiedproduct.AdditionalInfo = newproduct.AdditionalInfo;
                copiedproduct.Condition = newproduct.Condition;
                copiedproduct.Count = newproduct.Count;
                copiedproduct.CountryId = newproduct.CountryId;
                copiedproduct.NATOSize = newproduct.NATOSize;
                copiedproduct.Price = newproduct.Price;
                copiedproduct.Size = newproduct.Size;
                copiedproduct.SubTypeId = newproduct.SubTypeId;
                copiedproduct.TradePrice = newproduct.TradePrice;
                copiedproduct.TypeId = newproduct.TypeId;
                copiedproduct.Status = "New product was successfuly created. Creating other new product...";
                return View(copiedproduct);
            }
            else
            {
                newproduct.Status = "Validation of page failed. Creating new product...";
                return View(newproduct);
            }
        }

        public ActionResult Clients()
        {
            List<ClientsViewModel> clientsVM = new List<ClientsViewModel>();
            var clients = clientRepository.Clients.ToList();
            foreach (Client client in clients)
            {
                clientsVM.Add(new ClientsViewModel(client));
            }

            return View(clientsVM);
        }

        [HttpPost]
        public ActionResult CreateClient(string name, string phone, string email, string city, string NPOffice, string additionalInfo)
        {
            if (ModelState.IsValid)
            {
                ClientsViewModel clientVM = new ClientsViewModel();
                clientVM.ClientId = (clientRepository.Clients.Max(x => (int?) x.ClientId) ?? 0) + 1;
                clientVM.Name = name;
                clientVM.Phone = phone;
                clientVM.Email = email;
                clientVM.City = city;
                clientVM.NPOffice = int.Parse(NPOffice);
                clientVM.AdditionalInfo = additionalInfo;
                clientRepository.AddOrUpdateClient(clientVM);
                return Redirect("/Admin/Clients");
            }
            else
            {
                return Redirect("/Admin/Clients");
            }
        }
    }
}
