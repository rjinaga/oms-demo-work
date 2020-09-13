namespace Navtech.Oms.Entities
{

    public class ProductEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Weight { get; set; }
        public decimal Height { get; set; }
        public string Image { get; set; }
        public string SKU { get; set; }
        public string Barcode { get; set; }
        public decimal AvailableQuantity { get; set; }
        public bool IsActive { get; set; }
    }
}
