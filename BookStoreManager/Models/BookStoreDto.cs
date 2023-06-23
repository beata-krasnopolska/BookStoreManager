using BookStoreManager.Entities;
using System.Collections.Generic;

namespace BookStoreManager.Models
{
    public class BookStoreDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public bool HasDelivery { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }

        public List<Book> Books { get; set; }
    }
}
