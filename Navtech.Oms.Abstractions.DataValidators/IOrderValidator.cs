namespace Navtech.Oms.Abstractions.DataValidators
{
    using IocServiceStack;
    using FluentValidation;

    using Navtech.Oms.Dtos;
    
    
    [Contract]
    public interface IOrderValidator : IValidator<Order>
    {

    }
}
