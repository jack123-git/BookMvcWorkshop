using BookApi.Models;
using BookApi.ViewModels;
using BookApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BookApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BookLendRecordsController : Controller
    {
        private readonly ILogger<BooksController> _logger;

        private readonly IBookRepository _bookRepository;

        public BookLendRecordsController(ILogger<BooksController> logger, IBookRepository bookRepository)
        {
            this._logger = logger;
            this._bookRepository = bookRepository;
        }

        //GET: api/GetBookLendRecords
        [HttpGet]
        public async Task<IEnumerable<BookLendViewModel>> GetBookLendRecordAsync(int bookId)
        {
            return await _bookRepository.GetBookLendRecordAsync(bookId);
        }

        // 新增
        // POST: api/BookLendRecords
        [HttpPost]
        public async Task<ActionResult<CommadResult>> AddBookLendRecordAsync(BookLend bookLend)
        {
            var result = new CommadResult();
            if (bookLend == null)
                return BadRequest();

            result = await _bookRepository.AddBookLendRecordAsync(bookLend);
            return result;
        }
    }
}
