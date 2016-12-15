using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace Hotel.Web.Filters
{
    public class AuthenticationAttribute : FilterAttribute, IAuthenticationFilter
    {
       /* private bool Check ( ControllerContext filterContext )
        {
            string str;

            try
            {
                str = filterContext.HttpContext.Session [ "IsAuthenticated" ].ToString();
            }
            catch
            {
                str = false.ToString();
            }

            if ( str == false.ToString() )
            {
                return false;
            }

            return true;
        }*/

        public void OnAuthentication ( AuthenticationContext filterContext )
        {
            if ( !Utils.WebUtils.IsAuthenticated(filterContext.HttpContext.Session) )
                filterContext.Result = new HttpUnauthorizedResult();
        }

        public void OnAuthenticationChallenge ( AuthenticationChallengeContext filterContext )
        {
            if ( !Utils.WebUtils.IsAuthenticated( filterContext.HttpContext.Session ) )
                filterContext.Result =
                    new RedirectToRouteResult(
                        new System.Web.Routing.RouteValueDictionary(
                            new Dictionary<string, object>{ { "controller", "Account" },
                                                            { "action", "ForceLogin" } } ) );
        }
    }
}