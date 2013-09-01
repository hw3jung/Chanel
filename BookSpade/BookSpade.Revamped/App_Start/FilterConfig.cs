using System.Web;
using System.Web.Mvc;

namespace BookSpade.Revamped
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}