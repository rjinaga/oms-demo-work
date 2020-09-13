namespace Navtech.Oms.Abstractions.Business.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class OrderNotFoundException : Exception, IBusinessException
    {
        public OrderNotFoundException(): base("OrderNotFound")
        {
        }

        public OrderNotFoundException(string message) : base(message)
        {
        }

        public OrderNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected OrderNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
