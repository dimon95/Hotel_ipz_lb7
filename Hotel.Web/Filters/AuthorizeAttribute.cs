using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace Hotel.Web.Filters
{
    public class AuthorizeAttribute : FilterAttribute, IAuthorizationFilter
    {
        private IList<Dto.Role> _allowedRoles;

        /*public AuthorizeAttribute ( List<Dto.Role> roles )
        {
            _allowedRoles = roles;
        }*/

        public AuthorizeAttribute ( params Dto.Role [] roles)
        {
            if ( roles.Length == 0 )
                throw new ArgumentException();

            _allowedRoles = new List<Dto.Role>();

            foreach ( Dto.Role r in roles )
            {
                _allowedRoles.Add( r );
            }
        }

        /*public AuthorizeAttribute ( Dto.Role role )
        {
            _allowedRoles = new List<Dto.Role>();

            _allowedRoles.Add( role );
        }*/

        public void OnAuthorization ( AuthorizationContext filterContext )
        {
            if ( !Utils.WebUtils.IsAuthorizedInRole(filterContext.HttpContext.Session, _allowedRoles) )
                filterContext.Result = new RedirectToRouteResult( 
                    new System.Web.Routing.RouteValueDictionary( 
                        new Dictionary<string, object> { { "controller", "Account" },
                                                         { "action", "AccessViolation" } } ) );
        }
    }
}