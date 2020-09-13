namespace Navtech.Oms.Abstractions.Business
{
    using System;
    using IocServiceStack;

    using Navtech.Oms.Dtos;

    [Contract]
    public interface IOrderEdit : IDisposable
    {
        void Update(OrderEdit order);
        void Delete(int orderId);
    }
}
