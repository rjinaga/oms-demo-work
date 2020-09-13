namespace Navtech.Oms.Abstractions.Communication
{
    using IocServiceStack;

    [Contract]
    public interface IEmail
    {
        void Send(EmailMessage emailMessage);
    }
}
