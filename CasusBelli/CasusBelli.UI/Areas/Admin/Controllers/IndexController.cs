using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CasusBelli.UI.Areas.Admin.Controllers
{
    [Authorize]
    public class IndexController : Controller
    {
        //
        // GET: /Admin/Index/

        public ActionResult Index()
        {
            return View();
        }

    }
}
