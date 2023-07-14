namespace BookStoreManager.Models
{
    public class BookStoreQuery
    {
        public string SearchParam { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
