using app_hw.Data;
using app_hw.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace app_hw.Features.Commands.UpdateBook
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, Book?>
    {
        private readonly ApplicationContext _db;
        public UpdateBookCommandHandler(ApplicationContext db) => _db = db;

        public async Task<Book?> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _db.Books.FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);
            if (book == null) return null;

            // проверяем блокировку
            if (book.IsLocked)
            {
                // отклоняем команду, книга заблокирована
                return null; // контроллер будет возвращать 423 Locked
            }

            // ставим блокировку и сохраняем
            book.IsLocked = true;
            await _db.SaveChangesAsync(cancellationToken);

            try
            {
                // сохраняем старое состояние для истории
                var old = new { book.Title, book.Author, book.Year };

                // применяем изменения
                if (request.Title != null) book.Title = request.Title;
                if (request.Author != null) book.Author = request.Author;
                if (request.Year.HasValue) book.Year = request.Year;

                await _db.SaveChangesAsync(cancellationToken);

                // записываем историю изменений
                _db.BookHistories.Add(new BookHistory
                {
                    BookId = book.Id,
                    ChangedAt = DateTime.UtcNow,
                    ChangedBy = "user",
                    Summary = $"Updated from Title='{old.Title}', Author='{old.Author}', Year='{old.Year}' to Title='{book.Title}', Author='{book.Author}', Year='{book.Year}'"
                });
                await _db.SaveChangesAsync(cancellationToken);

                return book;
            }
            finally
            {
                // снимаем блокировку в любом случае
                book.IsLocked = false;
                await _db.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
