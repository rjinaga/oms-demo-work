namespace Navtech.Oms.Entities
{
    using System;

    public class OrderEntity
    {
        public int Id { get; set; }
        public int BuyerId { get; set; }
        public int ShippingAddressId { get; set; }
        public string OrderStatusId { get; set; }
        public DateTime OrderCreatedTimestamp { get; set; }
        public DateTime? LastUpdatedTimestamp { get; set; }
    }
}
