using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CasusBelli.Domain.Abstract;
using CasusBelli.Domain.Concrete;
using CasusBelli.Domain.Entities;
using CasusBelli.UI.Models;

namespace CasusBelli.UI.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        //
        // GET: /Admin/Products/

        private ITypeRepository typeRepository;
        private ISubTypeRepository subTypeRepository;
        private ICountryRepository countryRepository;
        private IProductRepository productRepository;
        private IProductStatusRepository statusRepository;
        private IClientRepository clientRepository;
        private ITransactionRepository transactionRepository;

        public ProductsController(ITypeRepository typeRep, ISubTypeRepository subTypeRep, ICountryRepository countryRep, IProductRepository prodRep, IProductStatusRepository prodstatusRep, IClientRepository clientRep, ITransactionRepository tranRep)
        {
            typeRepository = typeRep;
            subTypeRepository = subTypeRep;
            countryRepository = countryRep;
            productRepository = prodRep;
            statusRepository = prodstatusRep;
            clientRepository = clientRep;
            transactionRepository = tranRep;
        }

        public ViewResult Index()
        {
            List<AdminProductsListViewModel> productsVM = new List<AdminProductsListViewModel>();
            var types = typeRepository.Types.ToList();
            var subtypes = subTypeRepository.ProductSubTypes.ToList();
            var countries = countryRepository.Countries.ToList();
            var statuses = statusRepository.ProductStatuses.ToList();
            var products = productRepository.Products.ToList();
            var clients = clientRepository.Clients.ToList();
            clients.Add(new Client { ClientId = -1, Name = "No client" });
            foreach (Product product in products)
            {
                productsVM.Add(new AdminProductsListViewModel(product, types, countries, subtypes, statuses, clients));
            }

            return View(productsVM);
        }

        [HttpPost]
        public ActionResult SoldProduct(int productid, int price, int count, int clientId)
        {
            int totalcount = count;
            List<AdminProductsListViewModel> productsVM = new List<AdminProductsListViewModel>();
            var types = typeRepository.Types.ToList();
            var subtypes = subTypeRepository.ProductSubTypes.ToList();
            var countries = countryRepository.Countries.ToList();
            var statuses = statusRepository.ProductStatuses.ToList();
            var products = productRepository.Products.ToList();
            var clients = clientRepository.Clients.ToList();
            foreach (Product product in products)
            {
                productsVM.Add(new AdminProductsListViewModel(product, types, countries, subtypes, statuses, clients));
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
            Transaction lasttrans = transactionRepository.transactions.OrderByDescending(t => t.Date).First();
            AdminProductsListViewModel newproduct = productsVM.First(x => x.ProductId == productid);
            Transaction newtran = new Transaction
            {
                BecameMoney = lasttrans.BecameMoney + (price * totalcount),
                ClientId = -1,
                Currency = price * totalcount,
                Date = DateTime.Now,
                WasMoney = lasttrans.BecameMoney,
                Text = "Продано " + totalcount + " " + newproduct.SubTypeName
            };
            transactionRepository.AddTransaction(newtran);
            return Redirect("/Admin/Products");
        }

        public ActionResult DeleteProduct(int id)
        {
            Product product = productRepository.Products.First(p => p.ProductId == id);
            productRepository.DeleteProduct(product);
            return RedirectToAction("Index");
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
                while (count > 0)
                {
                    newproduct.ProductId = (productRepository.Products.Max(x => (int?)x.ProductId) ?? 0) + 1;
                    productRepository.AddOrUpdateProduct(newproduct);
                    count--;
                }

                //Transaction lasttrans = transactionRepository.transactions.OrderByDescending(t => t.Date).First();
                //Transaction newtran = new Transaction
                //{
                //    BecameMoney = lasttrans.BecameMoney + (newproduct.TradePrice * newproduct.Count),                        // Uncomment it it when all products will be in DB
                //    ClientId = -1,
                //    Currency = newproduct.TradePrice * newproduct.Count,
                //    Date = DateTime.Now,
                //    WasMoney = lasttrans.BecameMoney,
                //    Text = "Закупка " + newproduct.subTypeName
                //};
                //transactionRepository.AddTransaction(newtran);

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

    }
}
