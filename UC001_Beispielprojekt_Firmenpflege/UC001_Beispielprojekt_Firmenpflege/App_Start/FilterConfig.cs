using System.Web;
using System.Web.Mvc;

namespace UC001_Beispielprojekt_Firmenpflege
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
