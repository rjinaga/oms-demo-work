namespace Navtech.Oms.Entities
{

    public class OrderItemEntity
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public string ProductName { get; set; }
        public decimal Weight { get; set; }
        public decimal Height { get; set; }
        public string SKU { get; set; }
        public string Barcode { get; set; }
        public string Image { get; set; }
    }
}
