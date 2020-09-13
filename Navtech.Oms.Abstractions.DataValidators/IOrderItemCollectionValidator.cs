namespace Navtech.Oms.Abstractions.DataValidators
{
    using System.Collections.Generic;
    using FluentValidation;
    using IocServiceStack;
    
    using Navtech.Oms.Dtos;
    using Navtech.Oms.Abstractions.Data;

    [Contract]
    public interface IOrderItemCollectionValidator : IValidator<IEnumerable<OrderItem>>
    {
        void ValidateItemsAndThrow(IEnumerable<OrderItem> orderItems, AbstractOmsDbContext abstractOmsDbContext);
    }
}
