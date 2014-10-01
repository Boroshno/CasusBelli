using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CasusBelli.Domain.Abstract;
using CasusBelli.Domain.Entities;

namespace CasusBelli.UI.Controllers
{
    public class OrderController : Controller
    {
        private IOrderProcessor orderProcessor;
        private ISubTypeRepository subTypeRepository;

        public OrderController(IOrderProcessor orderProcessorparam, ISubTypeRepository subType)
        {
            orderProcessor = orderProcessorparam;
            subTypeRepository = subType;
        }
        public ActionResult Index(string subTypeId)
        {
            OrderDetails orderDetails = new OrderDetails();
            int stId = Convert.ToInt32(subTypeId);
            orderDetails.ProductSubType =
                subTypeRepository.ProductSubTypes.Where(s => s.SubTypeId == stId).FirstOrDefault();
            return View(orderDetails);
        }

        [HttpPost]
        public ActionResult Index(OrderDetails orderDetails)
        {
            if (ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(orderDetails);
                return View("Completed");
            }
            else return View(orderDetails);
        }

    }
}
