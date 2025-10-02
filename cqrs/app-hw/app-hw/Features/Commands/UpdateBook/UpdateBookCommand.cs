using app_hw.Models;
using MediatR;

namespace app_hw.Features.Commands.UpdateBook
{
    public record UpdateBookCommand(int Id, string? Title, string? Author, int? Year) : IRequest<Book?>;
}
