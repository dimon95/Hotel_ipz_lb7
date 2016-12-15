using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Hotel.Repository;
using Hotel.Services;

using Microsoft.Practices.Unity;

namespace Hotel.Web.Controllers
{
    [Filters.Exception]
    public class AccountController : Controller
    {
        private readonly IAccountService _accService;
        private readonly Hotel.Utils.IHashProvider<string> _hashProvider;

        private void SetSession ( Dto.AccountDto acc )
        {
            Session [ "IsAuthenticated" ] = true.ToString();
            Session [ "Role" ] = acc.Role.ToString();
        }

        private void ResetSession ()
        {
            Session [ "IsAuthenticated" ] = false.ToString();
            Session [ "Role" ] = null;
        }

        public AccountController (IAccountService accService, Hotel.Utils.IHashProvider<string> hashProvider)
        {
            _accService = accService;
            _hashProvider = hashProvider;
        }

        // GET: Account
        public ActionResult Index ()
        {
            return View();
        }

        public ActionResult Login ( Models.LoginViewModel m )
        {
            if ( ModelState.IsValid )
            {
                var a = _accService.Indentify( _hashProvider.GetHashCode(m.Password), m.Email );

                if ( a != null )
                {
                    SetSession( a );

                    switch ( a.Role )
                    {
                        case Dto.Role.Admin: return Redirect( Url.Action( "Index", "Admin" ) );
                        case Dto.Role.Client: return Redirect( Url.Action( "Index", "Home" ) );
                        default: throw new NotImplementedException();
                    }
                }
            }

            return Redirect( Url.Action( "Index", "Home" ) );
        }

        public ActionResult Logout ()
        {
            ResetSession();

            return Redirect( Url.Action( "Index", "Home" ) );
        }

        public PartialViewResult LoginLogoutPartial ()
        {
            return PartialView( new Models.LoginViewModel() );
        }

        public ActionResult ForceLogin ()
        {
            return View();
        }

        public ActionResult AccessViolation ()
        {
            return View();
        }
    }
}