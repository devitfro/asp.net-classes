using app_hw.Data;
using app_hw.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace app_hw.Features.Queries.GetBookById
{
    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, Book?>
    {
        private readonly ApplicationContext _db;
        public GetBookByIdQueryHandler(ApplicationContext db) => _db = db;

        public async Task<Book?> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            return await _db.Books.FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);
        }
    }
}
