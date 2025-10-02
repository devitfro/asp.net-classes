using app_hw.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace app_hw.Features.Commands.DeleteBook
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, bool>
    {
        private readonly ApplicationContext _db;
        public DeleteBookCommandHandler(ApplicationContext db) => _db = db;

        public async Task<bool> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _db.Books.FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);
            if (book == null) return false;

            if (book.IsLocked) return false;

            _db.Books.Remove(book);
            _db.BookHistories.Add(new Models.BookHistory
            {
                BookId = book.Id,
                ChangedAt = DateTime.UtcNow,
                ChangedBy = "user",
                Summary = $"Deleted: {book.Title}"
            });
            await _db.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
