
namespace Navtech.Oms.Abstractions.Business.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class OrderItemIdNotProvidedException : Exception, IBusinessException
    {
        public OrderItemIdNotProvidedException() : base("OrderItemIdNotProvided")
        {
        }

        public OrderItemIdNotProvidedException(string message) : base(message)
        {
        }

        public OrderItemIdNotProvidedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected OrderItemIdNotProvidedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
