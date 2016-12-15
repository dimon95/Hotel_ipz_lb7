using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Hotel.Services;

namespace Hotel.Web.Controllers
{
    [Filters.Exception]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ErrorHandler ( string exception )
        {
            return View( exception );
        }

        public ActionResult Error404 ()
        {
            return View();
        }
    }
}