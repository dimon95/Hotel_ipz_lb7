using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace Hotel.Web.Filters
{
    public class ExceptionAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException ( ExceptionContext filterContext )
        {
            if ( !filterContext.ExceptionHandled )
            {
                filterContext.Result = new RedirectToRouteResult( 
                    new System.Web.Routing.RouteValueDictionary(
                        new Dictionary<string, object> { { "controller", "Home" },
                                                         { "action", "ErrorHandler" },
                                                         { "exception", filterContext.Exception.Message } } ) );

                filterContext.ExceptionHandled = true;
            }
        }
    }
}