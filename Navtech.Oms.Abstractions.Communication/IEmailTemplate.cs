namespace Navtech.Oms.Abstractions.Communication
{
    using IocServiceStack;
    using Navtech.Oms.Dtos;
    
    [Contract]
    public interface IEmailTemplate
    {
        TemplateResult GetInvoiceEmailMessage(Order order, int orderId);
    }
}
