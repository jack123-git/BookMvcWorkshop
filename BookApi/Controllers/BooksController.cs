using BookApi.Models;
using BookApi.Repository;
using BookApi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BookApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BooksController : Controller
    {
        private readonly ILogger<BooksController> _logger;

        private readonly IBookRepository _bookRepository;

        public BooksController(ILogger<BooksController> logger, IBookRepository bookRepository)
        {
            this._logger = logger;
            this._bookRepository = bookRepository;
        }

        //GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDataViewModel>>> GetBookDatasAsync([FromQuery] string bookName = "", [FromQuery] string bookClassId = "", [FromQuery] string BorrowerId = "", [FromQuery] string bookStatusCode = "")
        {
            return await _bookRepository.GetQueryBookDataAsync(bookName, bookClassId, BorrowerId, bookStatusCode);
        }

        //GET: api/Books/5
        [HttpGet("{bookId}")]
        public async Task<ActionResult<BookData>> GetBookDataAsync(int bookId)
        {
            try
            {
                var bookData = await _bookRepository.GetBookDataByIdAsync(bookId);
                if (bookData == null)
                    return NotFound();

                return bookData;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // 新增
        // POST: api/Books
        [HttpPost]
        public async Task<ActionResult<CommadResult>> AddBookAsync(BookData book)
        {
            if (book == null)
                return BadRequest();

            var result = await _bookRepository.AddBookDataAsync(book);

            return result;
        }

        //修改
        // PUT: api/Books
        [HttpPut("{bookId:int}")]
        public async Task<ActionResult<CommadResult>> UpdateBookAsync(int bookId, [FromBody] BookData book)
        {
            if (bookId != book.BOOK_ID)
                return BadRequest();

            var bookData = await _bookRepository.GetBookDataByIdAsync(book.BOOK_ID);
            if (bookData == null)
                return NotFound();

            var result = new CommadResult();
            try
            {
                result = await _bookRepository.UpdateBookDataAsync(book);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return result;
        }

        //DELETE: api/Books/5
        [HttpDelete("{bookId}")]
        public async Task<ActionResult<CommadResult>> DeleteBookAsync(int bookId)
        {
            var book = await _bookRepository.GetBookDataByIdAsync(bookId);
            if (book == null)
                return NotFound();

            var result = await _bookRepository.DeleteBookDataAsync(bookId);

            return result;
        }
    }
}
