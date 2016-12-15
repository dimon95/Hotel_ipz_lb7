using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Model
{
    public enum SearchCriteria : byte { Freedge = 0x01, TV = 0x02, WiFi = 0x04, Vault = 0x08 };

    public static class Utils
    {
        public static IList<string> GetAllCriterias ()
        {
            IList<string> res = new List<string>();

            foreach ( var s in Enum.GetValues( typeof( SearchCriteria ) ) )
            {
                res.Add( s.ToString() );
            }

            return res;
        }

        public static int GetSearchCriteriaBitMask ( SearchCriteria sc )
        {
            return ( 1 << ( int ) sc );
        }

        public static IDictionary<string, bool> SearchCriteriaToDictionary ( int sCriterias )
        {
            IDictionary<string, bool> res = new Dictionary<string, bool>();

            foreach ( SearchCriteria s in Enum.GetValues( typeof( SearchCriteria ) ) )
            {
                res.Add( s.ToString(), HasCriteria( sCriterias, s ) );
            }

            return res;
        }

        public static bool HasCriteria ( int sCriterias, SearchCriteria sc )
        {
            return ( GetSearchCriteriaBitMask( sc ) & sCriterias ) != 0;
        }

        public static void SetCriteria ( ref int sCriterias, SearchCriteria criteria )
        {

            sCriterias |= Utils.GetSearchCriteriaBitMask( criteria );
        }

        public static void ResetCriteria ( ref int sCriterias, SearchCriteria criteria )
        {

            sCriterias &= ~Utils.GetSearchCriteriaBitMask( criteria );
        }


        public static int ToInt ( IDictionary<string, bool> criterias )
        {
            int res = 0;

            Array ar = Enum.GetValues(typeof(Model.SearchCriteria));

            for ( int i = 0; i < criterias.Count; i++ )
            {
                if ( criterias.Values.ToList() [ i ] )
                {
                    SetCriteria( ref res, ( Model.SearchCriteria ) ar.GetValue( i ) );
                }
            }

            return res;
        }

        public static int ToInt ( params bool [ ] criterias )
        {
            int res = 0;

            int i = 0;
            Array ar = Enum.GetValues(typeof(SearchCriteria));

            foreach ( bool b in criterias )
            {
                if ( b ) SetCriteria( ref res, (SearchCriteria)ar.GetValue( i ) );
            }

            return res;
        }
    }
}
