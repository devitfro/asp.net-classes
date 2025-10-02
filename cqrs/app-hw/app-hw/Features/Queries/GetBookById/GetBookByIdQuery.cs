using app_hw.Models;
using MediatR;

namespace app_hw.Features.Queries.GetBookById
{
    public record GetBookByIdQuery(int Id) : IRequest<Book?>;
}
