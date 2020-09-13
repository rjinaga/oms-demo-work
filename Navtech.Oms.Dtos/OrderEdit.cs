namespace Navtech.Oms.Dtos
{
    using System.Collections.Generic;

    public class OrderEdit
    {
        public int OrderId { get; set; }

        // We can use BuyerId in same class, 
        // since we use same class for Placing Order, structure doesn't make sense to that 
        // or we can create new class with BuyerId property for this purpose by inheriting Buyer.
        // Same for ShippingAddress too

        public Buyer Buyer { get; set; }
        public ShippingAddress ShippingAddress { get; set; }

        public IEnumerable<OrderItemWithState> OrderItems { get; set; }
    }
}
