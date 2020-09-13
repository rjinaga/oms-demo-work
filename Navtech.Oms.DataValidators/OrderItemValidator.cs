namespace Navtech.Oms.DataValidators
{
    using System.Linq;
    using System.Collections.Generic;
    using IocServiceStack;
    using FluentValidation;
    using FluentValidation.Results;

    using Navtech.Oms.Dtos;
    using Navtech.Oms.Abstractions.Data;
    using Navtech.Oms.Abstractions.DataValidators;
    using Navtech.Oms.Entities;

    [Service]
    public class OrderItemCollectionValidator : AbstractValidator<IEnumerable<OrderItem>>, IOrderItemCollectionValidator
    {
        public void ValidateItemsAndThrow(IEnumerable<OrderItem> orderItems, AbstractOmsDbContext abstractOmsDbContext)
        {
            RuleForEach(items => items)
                .ChildRules(item =>
                {
                    item.RuleFor(x => x.Quantity).GreaterThan(0);
                    item.RuleFor(x => x.ProductId).GreaterThan(0);
                    item.RuleFor(x => x).Custom((x, context) =>
                    {
                        ProductEntity productEntity = abstractOmsDbContext.Products.SingleOrDefault(product => product.Id == x.ProductId);
                        if (productEntity == null)
                        {
                            context.AddFailure($"Specified product number {x.ProductId} does not exist.");
                        }
                        else if (productEntity.AvailableQuantity < x.Quantity)
                        {
                            context.AddFailure($"Product {productEntity.Name} is not in stock");
                        }
                    });
                });
            this.ValidateAndThrow(orderItems);
        }

    }

}
