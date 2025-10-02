using app_hw.Models;
using MediatR;

namespace app_hw.Features.Queries.GetBooks
{
    public record GetBooksQuery(string? Title, string? Author, int? Year) : IRequest<List<Book>>;
}
