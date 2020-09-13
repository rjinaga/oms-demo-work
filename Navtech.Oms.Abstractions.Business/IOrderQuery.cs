namespace Navtech.Oms.Abstractions.Business
{
    using System;
    using System.Collections.Generic;
    using IocServiceStack;

    using Navtech.Oms.Dtos;

    [Contract]
    public interface IOrderQuery : IDisposable
    {
        IEnumerable<OrderView> GetOrdersByBuyer(int buyerId);
        IEnumerable<OrderView> GetAllOrders();
    }
}
