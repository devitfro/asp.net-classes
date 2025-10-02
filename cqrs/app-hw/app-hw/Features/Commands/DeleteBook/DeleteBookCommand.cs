using MediatR;

namespace app_hw.Features.Commands.DeleteBook
{
    public record DeleteBookCommand(int Id) : IRequest<bool>;
}
