namespace BookStoreManager.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

        public int BookstoreId { get; set; }
        public virtual BookStore Bookstore { get; set; }
    }
}
