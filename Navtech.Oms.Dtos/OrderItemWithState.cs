namespace Navtech.Oms.Dtos
{
    public class OrderItemWithState
    {
        // this requires only when the state of the object is modify or delete
        public int? OrderItemId { get; set; } 
        public OrderItem OrderItem { get; set; }
        public ObjectState State { get; set; }
    }

}
