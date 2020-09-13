namespace OrderManagementSystem.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;

    using Navtech.Oms.Dtos;
    using Navtech.Oms.Abstractions.Business;
    
    using static IocServiceStack.ServiceManager;

    //TODO: Filters need to be implemented for authorization (administrator role)
    public class OrdersManagerController : ApiController
    {
        [HttpGet]
        public IEnumerable<OrderView> GetOrders()
        {
            using (var orderQuery = GetService<IOrderQuery>())
            {
                return orderQuery.GetAllOrders();
            }
        }
    }
}
