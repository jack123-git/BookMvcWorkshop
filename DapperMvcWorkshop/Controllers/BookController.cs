using DapperMvcWorkshop.Extension;
using DapperMvcWorkshop.Repository;
using DapperMvcWorkshop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using static System.Reflection.Metadata.BlobBuilder;
using DapperMvcWorkshop.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DapperMvcWorkshop.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IActionResult> Index(int pg = 1, string SearchBookName = "", string SearchBookClassId = "", string SearchUserId = "", string SearchBookStatusId = "")
        {
            ViewBag.BookClassList = await _bookRepository.GetAllBookClassAsync();
            ViewBag.BookStatusList = await _bookRepository.GetAllBookStatusAsync();
            ViewBag.MemberList = await _bookRepository.GetAllMemberAsync();

            //List<BookDataViewModel> queryDataList = new List<BookDataViewModel>()
            //{
            //    new BookDataViewModel() { BOOK_ID=1, BOOK_CLASS_NAME="0001", BOOK_NAME="Asp.Net CORE MVC", BOOK_STATUS_NAME="可以借出",BOOK_BOUGHT_DATE=Convert.ToDateTime("2023/01/01"), BOOK_KEEPER_NAME ="Andy"},
            //    new BookDataViewModel() { BOOK_ID=2, BOOK_CLASS_NAME="0002", BOOK_NAME="JQuery", BOOK_STATUS_NAME="已借出",BOOK_BOUGHT_DATE=Convert.ToDateTime("2023/01/01"), BOOK_KEEPER_NAME ="Mary"}
            //};
            //List<BookDataViewModel> queryData = new List<BookDataViewModel>();

            List<BookDataViewModel> queryData = await _bookRepository.GetQueryBookDataAsync("", SearchBookName, SearchBookClassId, SearchUserId, SearchBookStatusId);
            List<BookDataViewModel> books = new List<BookDataViewModel>();
            //if ((SearchBookName != "" && SearchBookName != null) || (SearchBookClassId != "" && SearchBookClassId != null) || (SearchUserId != "" && SearchUserId != null) || (SearchBookStatusId != "" && SearchBookStatusId != null))
            //{
            //    //queryData = await _bookRepository.GetQueryBookDataAsync("", SearchBookName, SearchBookClassId, SearchUserId, SearchBookStatusId);
            //    books = queryData.Where(
            //        p => p.BOOK_NAME.Contains(SearchBookName, StringComparison.OrdinalIgnoreCase)

            //    ).ToList();


            //}
            //else
                books = queryData.ToList();

            this.ViewBag.SearchBookName = SearchBookName;
            this.ViewBag.SearchBookClassId = SearchBookClassId;
            this.ViewBag.SearchUserId = SearchUserId;
            this.ViewBag.SearchBookStatusId = SearchBookStatusId;

            const int pageSize = 9;
            if (pg < 1) pg = 1;
            int recsCount = books.Count;
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = books.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;

            return View(data);
        }

        /// <summary>
        /// 新增書籍資料
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> CreateBook()
        {
            ViewBag.BookClassList = await _bookRepository.GetAllBookClassAsync();
            ViewBag.BookStatusList = await _bookRepository.GetAllBookStatusAsync();
            ViewBag.MemberList = await _bookRepository.GetAllMemberAsync();
            ViewBag.Action = "CreateBook";

            return View("ModifyBook");
        }

        /// <summary>
        /// 新增書籍資料
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateBook(BookData bookData)
        {
            //if (!ModelState.IsValid)
            if (!(ModelState["BOOK_NAME"].ValidationState == ModelValidationState.Valid
                && ModelState["BOOK_NOTE"].ValidationState == ModelValidationState.Valid
                && ModelState["BOOK_AUTHOR"].ValidationState == ModelValidationState.Valid
                && ModelState["BOOK_CLASS_ID"].ValidationState == ModelValidationState.Valid
                && ModelState["BOOK_PUBLISHER"].ValidationState == ModelValidationState.Valid
                && ModelState["BOOK_BOUGHT_DATE"].ValidationState == ModelValidationState.Valid
                ))
            {
                ViewBag.BookClassList = await _bookRepository.GetAllBookClassAsync();
                ViewBag.BookStatusList = await _bookRepository.GetAllBookStatusAsync();
                ViewBag.MemberList = await _bookRepository.GetAllMemberAsync();
                ViewBag.Action = "CreateBook";

                return View("ModifyBook", bookData);
            }

            // workshop 新增書籍要求
            bookData.BOOK_KEEPER = "";
            bookData.BOOK_STATUS = "A";

            var result = await _bookRepository.AddBookDataAsync(bookData);
            if (result)
                return RedirectToAction("Index");
            else
                return View("ModifyBook", bookData);
        }

        /// <summary>
        /// 修改書籍資料
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<IActionResult> EditBook(int Id)
        {
            var bookData = await _bookRepository.GetBookDataByIdAsync(Id);
            if (bookData == null)
                return NotFound();

            ViewBag.BookClassList = await _bookRepository.GetAllBookClassAsync();
            ViewBag.BookStatusList = await _bookRepository.GetAllBookStatusAsync();
            ViewBag.MemberList = await _bookRepository.GetAllMemberAsync();
            ViewBag.Action = "EditBook";

            return View("ModifyBook", bookData);
        }

        /// <summary>
        /// 修改書籍資料
        /// </summary>
        /// <returns></returns>
        [HttpPost()]
        //[RequireAntiforgeryToken]
        public async Task<IActionResult> EditBook(BookData bookData)
        {
            //if (!ModelState.IsValid)
            if (!(ModelState["BOOK_NAME"].ValidationState == ModelValidationState.Valid
                && ModelState["BOOK_NOTE"].ValidationState == ModelValidationState.Valid
                && ModelState["BOOK_AUTHOR"].ValidationState == ModelValidationState.Valid
                && ModelState["BOOK_CLASS_ID"].ValidationState == ModelValidationState.Valid
                && ModelState["BOOK_PUBLISHER"].ValidationState == ModelValidationState.Valid
                && ModelState["BOOK_BOUGHT_DATE"].ValidationState == ModelValidationState.Valid
                && ((bookData.BOOK_STATUS =="A") 
                     || (bookData.BOOK_STATUS == "B" && bookData.BOOK_KEEPER is not null)
                     || (bookData.BOOK_STATUS == "C" && bookData.BOOK_KEEPER is not null)
                     || (bookData.BOOK_STATUS == "U")
                    )
                ))
            {
                ViewBag.BookClassList = await _bookRepository.GetAllBookClassAsync();
                ViewBag.BookStatusList = await _bookRepository.GetAllBookStatusAsync();
                ViewBag.MemberList = await _bookRepository.GetAllMemberAsync();
                ViewBag.Action = "EditBook";

                return View("ModifyBook", bookData);
            }

            var result = await _bookRepository.UpdateBookDataAsync(bookData);
            if (result)
            {
                if (bookData.BOOK_STATUS == "B" || bookData.BOOK_STATUS == "C")
                {
                    if (!string.IsNullOrEmpty(bookData.BOOK_KEEPER))
                    {
                        var lend_record = new BookLend()
                        {
                            BOOK_ID = bookData.BOOK_ID,
                            KEEPER_ID = bookData.BOOK_KEEPER,
                            LEND_DATE = DateTime.Now.Date
                        };
                        result = await _bookRepository.AddBookLendRecordAsync(lend_record);
                    }                    
                }
                return RedirectToAction("Index");
            }                
            else
                return View("ModifyBook", bookData);
        }

        /// <summary>
        /// 刪除書籍資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int Id)
        {
            var deleteResult = await _bookRepository.DeleteBookDataAsync(Id);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// 借閱紀錄
        /// </summary>
        /// <param name="Id">書本ID</param>
        /// <returns></returns>
        public async Task<IActionResult> LendRecord(int bookId)
        {
            var bookData = await _bookRepository.GetBookDataByIdAsync(bookId);
            ViewBag.bookData = bookData;

            var lendRecordList = await _bookRepository.GetBookLendRecordAsync(bookId);
            return View(lendRecordList);
        }


    }
}
