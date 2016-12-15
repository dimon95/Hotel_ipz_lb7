using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Hotel.Web.Filters;

namespace Hotel.Web.Controllers
{
    [Filters.Exception]
    public class AdminController : Controller
    {
        [Authentication]
        [Filters.Authorize(Dto.Role.Admin)]
        public ActionResult Index()
        {
            return View();
        }
    }
}