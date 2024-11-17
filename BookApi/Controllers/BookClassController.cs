using BookApi.Models;
using BookApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BookApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BookClassController : Controller
    {

        private readonly ILogger<BooksController> _logger;

        private readonly IBookRepository _bookRepository;

        public BookClassController(ILogger<BooksController> logger, IBookRepository bookRepository)
        {
            this._logger = logger;
            this._bookRepository = bookRepository;
        }

        //GET: api/BookClass
        [HttpGet]
        public async Task<IEnumerable<BookClass>> GetAllBookClassAsync()
        {
            return await _bookRepository.GetAllBookClassAsync();
        }
    }
}
