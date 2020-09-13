namespace Navtech.Oms.Abstractions.Business.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class ProductNotFoundException : Exception, IBusinessException
    {
        public ProductNotFoundException() : base("ProductNotFound")
        {
        }

        public ProductNotFoundException(string message) : base(message)
        {
        }

        public ProductNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ProductNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
