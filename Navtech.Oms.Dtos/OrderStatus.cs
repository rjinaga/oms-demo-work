namespace Navtech.Oms.Dtos
{
    public struct OrderStatus
    {
        readonly string _status;
        private OrderStatus(string status)
        {
            _status = status;
        }

        public override string ToString()
        {
            return _status.ToString();
        }

        public static implicit operator string(OrderStatus status) => status._status;

        public static OrderStatus Placed => new OrderStatus("P");
        public static OrderStatus Approved => new OrderStatus("A");
        public static OrderStatus Cancelled => new OrderStatus("N");
        public static OrderStatus Completed => new OrderStatus("C");
        public static OrderStatus InDelivery => new OrderStatus("I");
    }
}
