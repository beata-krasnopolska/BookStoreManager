using System.ComponentModel.DataAnnotations;

namespace BookStoreManager.Models
{
    public class CreateBookDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

        public int RestaurantId { get; set; }
    }
}
