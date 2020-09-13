namespace OrderManagementSystem
{
    using System.Web.Http.Filters;
    using System.Web.Mvc;
    using Navtech.Oms.WebApi.Filters;

    public class FilterConfig
    {
        public static void RegisterGlobalFilters(HttpFilterCollection httpFilterCollection)
        {
            httpFilterCollection.Add(new ExceptionHandlerAttribute());
        }
    }
}
