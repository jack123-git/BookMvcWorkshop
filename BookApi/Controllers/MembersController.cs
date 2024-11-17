using BookApi.Models;
using BookApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BookApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class MembersController : Controller
    {
        private readonly ILogger<BooksController> _logger;

        private readonly IBookRepository _bookRepository;

        public MembersController(ILogger<BooksController> logger, IBookRepository bookRepository)
        {
            this._logger = logger;
            this._bookRepository = bookRepository;
        }

        //GET: api/Members
        [HttpGet]
        public async Task<IEnumerable<Member>> GetAllMemberAsync()
        {
            return await _bookRepository.GetAllMemberAsync();
        }
    }
}
