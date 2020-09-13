namespace Navtech.Oms.Dtos
{
    using System.ComponentModel.DataAnnotations;

    public class ShippingAddress
    {
        [StringLength(100)]
        public string AddressLine1 { get; set; }
        [StringLength(100)]
        public string AddressLine2 { get; set; }
        [StringLength(50)]
        public string City { get; set; }
        [StringLength(50)]
        public string State { get; set; }
        [StringLength(9)]
        public string Zip { get; set; }
    }
}
