namespace Navtech.Oms.Abstractions.Business.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class ProductNotInStockException : Exception, IBusinessException
    {
        public ProductNotInStockException() :  base("ProductNotInStock")
        {
        }

        public ProductNotInStockException(string message) : base(message)
        {
        }

        public ProductNotInStockException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ProductNotInStockException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
