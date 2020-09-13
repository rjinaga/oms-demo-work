namespace Navtech.Oms.Dtos
{
    using System;
    
    public class OrderView
    {
        public int OrderId { get; set; }
        public Buyer Buyer { get; set; }
        public ShippingAddress ShippingAddress { get; set; }
        public int ItemsCount { get; set; }
        public DateTime OrderDateTime { get; set; }
    }
}
