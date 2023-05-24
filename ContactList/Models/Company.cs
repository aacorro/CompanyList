using System.ComponentModel.DataAnnotations;

namespace ContactList.Models
{
    public class Company
    {
        public int CompanyId { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }
        [Display(Name = "Street Address")]
        [Required]
        [StringLength(50, MinimumLength = 10)]
        public string Address { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string City { get; set; }
        [StringLength(2, MinimumLength = 2)]
        public string State { get; set; }
        [Display(Name = "Postal Code")]
        [StringLength(5, MinimumLength = 5)]
        public string PostalCode { get; set; }
    }
}
