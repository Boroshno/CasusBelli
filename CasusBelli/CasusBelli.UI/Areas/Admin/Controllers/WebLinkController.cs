using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CasusBelli.Domain.Abstract;
using CasusBelli.Domain.Entities;
using CasusBelli.UI.Models;
using Newtonsoft.Json;

namespace CasusBelli.UI.Areas.Admin.Controllers
{
    public class WebLinkController : Controller
    {
        //
        // GET: /Admin/WebLink/

        private IWebLinkRepository webLinkRepository;

        public WebLinkController(IWebLinkRepository webLinkRep)
        {
            webLinkRepository = webLinkRep;
        }

        public ViewResult Index()
        {
            List<WebLinkViewModel> webLinkVM = new List<WebLinkViewModel>();
            var links = webLinkRepository.WebLink.ToList();
            foreach (WebLink l in links)
            {
                webLinkVM.Add(new WebLinkViewModel(l));
            }

            return View(webLinkVM);
        }

        public ActionResult CreateWebLink(string name, string url, string login, string password, string additionalInfo)
        {
            if (ModelState.IsValid)
            {
                WebLinkViewModel linkVM = new WebLinkViewModel();
                linkVM.Id = (webLinkRepository.WebLink.Max(x => (int?)x.Id) ?? 0) + 1;
                linkVM.Name = name;
                linkVM.URL = url;
                linkVM.Login = login;
                linkVM.Password = password;
                linkVM.AdditionalInfo = additionalInfo;
                webLinkRepository.AddOrUpdateWebLink(linkVM);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult DeleteWebLink(int id)
        {
            WebLink weblink = webLinkRepository.WebLink.First(c => c.Id == id);
            webLinkRepository.DeleteWebLink(weblink);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult EditWebLink(WebLinkViewModel weblinkVM)
        {
            if (ModelState.IsValid)
            {
                WebLink weblink = webLinkRepository.WebLink.First(c => c.Id == weblinkVM.Id);
                weblink.AdditionalInfo = weblinkVM.AdditionalInfo;
                weblink.Name = weblinkVM.Name;
                weblink.URL = weblinkVM.URL;
                weblink.Login = weblinkVM.Login;
                weblink.Password = weblinkVM.Password;
                webLinkRepository.AddOrUpdateWebLink(weblink);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public string GetWebLinkById(int id)
        {
            WebLink l = webLinkRepository.WebLink.First(c => c.Id == id);
            string jsonClient = JsonConvert.SerializeObject(l);
            return jsonClient;
        }

    }
}
