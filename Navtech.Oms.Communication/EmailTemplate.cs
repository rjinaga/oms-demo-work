namespace Navtech.Oms.Communication
{
    using IocServiceStack;

    using Navtech.Oms.Dtos;
    using Navtech.Oms.Abstractions.Communication;

    [Service]
    public class EmailTemplate : IEmailTemplate
    {
        public TemplateResult GetInvoiceEmailMessage(Order order, int orderId)
        {
            // TODO: we can create dynamic binding to the template (handlbar based or someother way)
            return new TemplateResult
            {
                 Subject = $"Your Order # {orderId}",
                 // TODO: create invoice email content
                 Body = $"Dear {order.Buyer.FirstName} {order.Buyer.LastName}, Your order has been successfully placed. ....." 
            };
        }
    }
}
