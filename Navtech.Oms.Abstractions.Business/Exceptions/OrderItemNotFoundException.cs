
namespace Navtech.Oms.Abstractions.Business.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class OrderItemNotFoundException : Exception, IBusinessException
    {
        public OrderItemNotFoundException() : base("OrderItemNotFound")
        {
        }

        public OrderItemNotFoundException(string message) : base(message)
        {
        }

        public OrderItemNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected OrderItemNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
