namespace OrderManagementSystem.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using System.Collections.Generic;

    using Navtech.Oms.Dtos;
    using Navtech.Oms.Abstractions.Business;
    using Navtech.Oms.WebApi.Exceptions;
    using static IocServiceStack.ServiceManager;

    public class OrderController : ApiController
    {

        [HttpPost]
        public void Create([FromBody]Order order)
        {
            if (!ModelState.IsValid)
            {
                throw new ModelStateException(ModelState.Values.SelectMany(v => v.Errors));
            }

            // Exceptions are handled by Golbal Filter, Please check the ExceptionHandler class
            using (var service = GetService<IOrder>())
            {
                service.PlaceOrder(order);
            }
        }

        [HttpPost]
        public void Update([FromBody]OrderEdit order)
        {
            if (!ModelState.IsValid)
            {
                throw new ModelStateException(ModelState.Values.SelectMany(v => v.Errors));
            }

            // Exceptions are handled by Golbal Filter, Please check the ExceptionHandler class
            using (var service = GetService<IOrderEdit>())
            {
                service.Update(order);
            }

        }

        [HttpGet]
        public void Delete([FromUri]int orderId)
        {
            //TODO: accept orderId as base64 (url friendly string), convert back to integer to make unclear to user for not to tamper
            // Exceptions are handled by Golbal Filter, Please check the ExceptionHandler class

            using (var service = GetService<IOrderEdit>())
            {
                service.Delete(orderId);
            }

        }

        // GET api/values
        [HttpGet]
        public IEnumerable<OrderView> GetYourOrders()
        {
            // Get orders based on current logged in user id
            // We can get it from current user object; 
            // TODO: implment JWT or some other mechanism for authentication

            // Assume, BuyerId = 1

            using (var orderQuery = GetService<IOrderQuery>())
            {
                return orderQuery.GetOrdersByBuyer(1);
            }
        }
    }
}
