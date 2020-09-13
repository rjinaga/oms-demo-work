namespace Navtech.Oms.Dtos
{
    using System.Collections.Generic;

    public class Order
    {
        public Buyer Buyer { get; set; }
        public ShippingAddress ShippingAddress { get; set; }
        public IEnumerable<OrderItem> OrderItems {get;set;}
    }
}
