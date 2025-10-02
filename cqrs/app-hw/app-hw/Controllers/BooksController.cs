using app_hw.Features.Commands.AddBook;
using app_hw.Features.Commands.DeleteBook;
using app_hw.Features.Commands.UpdateBook;
using app_hw.Features.Queries.GetBookById;
using app_hw.Features.Queries.GetBooks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace app_hw.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BooksController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? title, [FromQuery] string? author, [FromQuery] int? year)
        {
            var books = await _mediator.Send(new GetBooksQuery(title, author, year));
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await _mediator.Send(new GetBookByIdQuery(id));
            if (book == null) return NotFound();
            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddBookCommand command)
        {
            var book = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateBookCommand dto)
        {
            if (id != dto.Id) return BadRequest("Id mismatch");

            var updated = await _mediator.Send(dto);
            if (updated == null)
            {
                // либо не найдена, либо заблокирована
                var maybe = await _mediator.Send(new GetBookByIdQuery(id));
                if (maybe == null) return NotFound();
                if (maybe.IsLocked) return StatusCode(423, "Book is locked for editing");
                return StatusCode(500, "Update failed");
            }
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _mediator.Send(new DeleteBookCommand(id));
            if (!ok)
            {
                var maybe = await _mediator.Send(new GetBookByIdQuery(id));
                if (maybe == null) return NotFound();
                if (maybe.IsLocked) return StatusCode(423, "Book is locked");
                return StatusCode(500, "Delete failed");
            }
            return NoContent();
        }

        [HttpGet("{id}/history")]
        public async Task<IActionResult> History(int id, [FromServices] app_hw.Data.ApplicationContext db)
        {
            var hist = db.BookHistories.Where(h => h.BookId == id).OrderByDescending(h => h.ChangedAt).ToList();
            return Ok(hist);
        }
    }
}
