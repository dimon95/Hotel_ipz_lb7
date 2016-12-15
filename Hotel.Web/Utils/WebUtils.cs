using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hotel.Repository;

namespace Hotel.Web.Utils
{
    public static class WebUtils
    {
        private static object GetSessionValue ( HttpSessionStateBase session, string name )
        {
            object res;

            try
            {
                res = session [ name ];
            }
            catch
            {
                res = null;
            }

            return res;
        }

        public static bool IsAuthenticated ( HttpSessionStateBase session )
        {
            object tmp = GetSessionValue( session, "IsAuthenticated" );

            if ( tmp == null )
                return false;

            if ( tmp.ToString() == true.ToString() )
                return true;

           return false;
        }

        public static bool IsAuthorizedInRole ( HttpSessionStateBase session, IList<Dto.Role> roles )
        {
            object tmp = GetSessionValue( session, "Role");

            if ( tmp == null )
                return false;

            bool res = false;

            foreach ( Dto.Role r in roles )
            {
                if ( r.ToString() == tmp.ToString() )
                    res = true;
            }

            return res;
        }
    }
}