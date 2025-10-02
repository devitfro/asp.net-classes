using app_hw.Models;
using MediatR;

namespace app_hw.Features.Commands.AddBook
{
    public record AddBookCommand(string Title, string? Author, int? Year) : IRequest<Book>;
}
