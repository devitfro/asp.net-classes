using app_hw.Data;
using app_hw.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace app_hw.Features.Queries.GetBooks
{
    public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, List<Book>>
    {
        private readonly ApplicationContext _db;
        public GetBooksQueryHandler(ApplicationContext db) => _db = db;

        public async Task<List<Book>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            var q = _db.Books.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Title))
                q = q.Where(b => b.Title.Contains(request.Title));
            if (!string.IsNullOrWhiteSpace(request.Author))
                q = q.Where(b => b.Author != null && b.Author.Contains(request.Author));
            if (request.Year.HasValue)
                q = q.Where(b => b.Year == request.Year.Value);

            return await q.ToListAsync(cancellationToken);
        }
    }
}
