using app_hw.Models;
using System.Numerics;

namespace app_hw.Services
{
    public class BookService
    {
        public List<Book> books = new();

        public BookService()
        {
            books = new List<Book>()
            {
                new Book { Id = 1, Title = "To Kill a Mockingbird", Author = "Harper Lee", Genre = "Fiction", Year = 1960 },
                new Book { Id = 2, Title = "1984", Author = "George Orwell", Genre = "Dystopian", Year = 1949 },
                new Book { Id = 3, Title = "Pride and Prejudice", Author = "Jane Austen", Genre = "Romance", Year = 1813 },
                new Book { Id = 4, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", Genre = "Fiction", Year = 1925 },
                new Book { Id = 5, Title = "Moby-Dick", Author = "Herman Melville", Genre = "Adventure", Year = 1851 }
            };
        }

        public IEnumerable<Book> GetBooks() => books;

        public void AddBook(Book book)
        {
            books.Add(book);
        }
    } 
}
