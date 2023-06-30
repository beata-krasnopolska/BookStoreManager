using System.ComponentModel.DataAnnotations;

namespace BookStoreManager.Models
{
    public class UpdateBookStoreDto
    {
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool HasDelivery { get; set; }
    }
}
