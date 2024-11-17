using BookApi.Models;
using BookApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BookApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BookStatusController : Controller
    {
        private readonly ILogger<BooksController> _logger;

        private readonly IBookRepository _bookRepository;

        public BookStatusController(ILogger<BooksController> logger, IBookRepository bookRepository)
        {
            this._logger = logger;
            this._bookRepository = bookRepository;
        }

        //GET: api/BookStatus
        [HttpGet]
        public async Task<IEnumerable<BookStatus>> GetAllBookStatusAsync()
        {
            return await _bookRepository.GetAllBookStatusAsync();
        }
    }
}
