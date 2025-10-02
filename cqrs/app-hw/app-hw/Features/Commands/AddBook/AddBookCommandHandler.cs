using app_hw.Data;
using app_hw.Models;
using MediatR;

namespace app_hw.Features.Commands.AddBook
{
    public class AddBookCommandHandler : IRequestHandler<AddBookCommand, Book>
    {
        private readonly ApplicationContext _db;
        public AddBookCommandHandler(ApplicationContext db) => _db = db;

        public async Task<Book> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            var book = new Book { Title = request.Title, Author = request.Author, Year = request.Year };
            _db.Books.Add(book);
            await _db.SaveChangesAsync(cancellationToken);
            // не забываем историю (создание)
            _db.BookHistories.Add(new BookHistory
            {
                BookId = book.Id,
                ChangedAt = DateTime.UtcNow,
                ChangedBy = "user",
                Summary = $"Created: {book.Title}"
            });
            await _db.SaveChangesAsync(cancellationToken);
            return book;
        }
    }
}
