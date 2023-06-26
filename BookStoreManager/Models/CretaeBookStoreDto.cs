using System.ComponentModel.DataAnnotations;

namespace BookStoreManager.Models
{
    public class CretaeBookStoreDto
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public bool HasDelivery { get; set; }
        public string ContactEmail { get; set; }
        public string ContactNumber { get; set; }
        [Required]
        [MaxLength(15)]
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }

    }
}
