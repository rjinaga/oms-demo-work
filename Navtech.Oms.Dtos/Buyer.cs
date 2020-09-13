namespace Navtech.Oms.Dtos
{
    using System.ComponentModel.DataAnnotations;
    public class Buyer
    {
        [StringLength(45)]
        public string FirstName { get; set; }
        [StringLength(45)]
        public string LastName { get; set; }
        [StringLength(255)]
        public string Email { get; set; }
        [StringLength(10)]
        public string Phone { get; set; }
    }
}
