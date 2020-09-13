namespace Navtech.Oms.Abstractions.Business
{
    using System;
    using IocServiceStack;

    using Navtech.Oms.Dtos;

    [Contract]
    public interface IOrder : IDisposable
    {
        void PlaceOrder(Order order);
    }
}
