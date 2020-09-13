namespace Navtech.Oms.DataValidators
{
    using IocServiceStack;
    using FluentValidation;

    using Navtech.Oms.Dtos;
    using Navtech.Oms.Abstractions.DataValidators;

    [Service]
    public class OrderValidator : AbstractValidator<Order>, IOrderValidator
    {
        public OrderValidator()
        {
            RuleFor((order) => order.Buyer).NotNull();
            RuleFor((order) => order.ShippingAddress).NotNull();

            When(order => order.Buyer != null, () =>
            {
                RuleFor((order) => order.Buyer.FirstName).NotEmpty();
                RuleFor((order) => order.Buyer.LastName).NotEmpty();
                RuleFor((order) => order.Buyer.Email).NotEmpty().EmailAddress();
                RuleFor((order) => order.Buyer.Phone).NotEmpty(); //TODO: phone validation
            });

            When(order => order.ShippingAddress != null, () =>
            {
                RuleFor((order) => order.ShippingAddress.AddressLine1).NotEmpty();
                RuleFor((order) => order.ShippingAddress.City).NotEmpty();
                RuleFor((order) => order.ShippingAddress.State).NotEmpty();
                RuleFor((order) => order.ShippingAddress.Zip).NotEmpty(); // TODO: DIGITS validation
            });
        }
    }
}
