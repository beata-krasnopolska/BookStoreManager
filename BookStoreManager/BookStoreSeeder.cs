using BookStoreManager.Entities;
using System.Collections.Generic;
using System.Linq;

namespace BookStoreManager
{
    public class BookStoreSeeder
    {
        private readonly BookStoreDbContext _dbContext;
        public BookStoreSeeder(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.BookStores.Any())
                {
                    var bookStores = GetBookStores();
                    _dbContext.BookStores.AddRange(bookStores);
                    _dbContext.SaveChanges();
                };
            };
        }

        private IEnumerable<BookStore> GetBookStores()
        {
            var bookStores = new List<BookStore>()
            {
                new BookStore()
                {
                    Name = "Empik",
                    Category = "Books and stuff",
                    Description = "books, stuff and other stuff",
                    ContactEmail = "empik@empik.com",
                    HasDelivery = true,
                    Books = new List<Book>
                    {
                        new Book()
                        {
                            Name = "Pride and Prejudice and Zombies",
                            Price = 5.56,
                        },
                         new Book()
                        {
                            Name = "The curious incident of the Dog in the Night",
                            Price = 25.56,
                        }

                    },
                    Address = new Address()
                    {
                        City = "Wrocław",
                        PostalCode = "12-123",
                        Street = "OldTownStreet"
                    }
                },
                 new BookStore()
                {
                    Name = "NewOneEmpik",
                    Category = "Stuff and products",
                    Description = "music, films, games",
                    ContactEmail = "newOneEempik@emp.com",
                    HasDelivery = true,
                    Books = new List<Book>
                    {
                        new Book()
                        {
                            Name = "Perfect book",
                            Price = 33.2,
                        },
                         new Book()
                        {
                            Name = "CSharp for beginners",
                            Price = 25.56,
                        }

                    },
                    Address = new Address()
                    {
                        City = "Kraków",
                        PostalCode = "35-123",
                        Street = "VeryOldTownStreet"
                    }
                }
            };
            return bookStores;
        }
    }
}
