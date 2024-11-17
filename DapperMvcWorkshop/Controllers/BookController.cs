using DapperMvcWorkshop.Extension;
using DapperMvcWorkshop.Repository;
using DapperMvcWorkshop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using static System.Reflection.Metadata.BlobBuilder;
using DapperMvcWorkshop.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using AspNetCoreHero.ToastNotification.Abstractions;

using System.Text;
using System.Text.Json;

namespace DapperMvcWorkshop.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;

        private readonly INotyfService _notyf;

        public BookController(IBookRepository bookRepository, INotyfService notyf)
        {
            _bookRepository = bookRepository;
            _notyf = notyf;
        }

        public async Task<IActionResult> Index(int pg = 1, string SearchBookName = "", string SearchBookClassId = "", string SearchUserId = "", string SearchBookStatusId = "")
        {
            // 測試 toast
            // _notyf.Success("Success Notification");
            var message = TempData["Message"];
            if (message != null)
            {
                if (message!.ToString().Contains("成功"))
                    _notyf.Success(message!.ToString(), 3);
                else
                    _notyf.Error(message!.ToString(), 3);
            }

            ViewBag.BookClassList = await _bookRepository.GetAllBookClassAsync();
            ViewBag.BookStatusList = await _bookRepository.GetAllBookStatusAsync();
            ViewBag.MemberList = await _bookRepository.GetAllMemberAsync();

            List<BookDataViewModel> queryData = await _bookRepository.GetQueryBookDataAsync(SearchBookName, SearchBookClassId, SearchUserId, SearchBookStatusId);
            List<BookDataViewModel> books = queryData.ToList();

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
            if (!(ModelState["BOOK_NAME"]!.ValidationState == ModelValidationState.Valid
                && ModelState["BOOK_NOTE"]!.ValidationState == ModelValidationState.Valid
                && ModelState["BOOK_AUTHOR"]!.ValidationState == ModelValidationState.Valid
                && ModelState["BOOK_CLASS_ID"]!.ValidationState == ModelValidationState.Valid
                && ModelState["BOOK_PUBLISHER"]!.ValidationState == ModelValidationState.Valid
                && ModelState["BOOK_BOUGHT_DATE"]!.ValidationState == ModelValidationState.Valid
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

            if (result.ResultCode != 0)
            {
                TempData["Message"] = $"新增書籍 {bookData.BOOK_NAME} 資料成功";
                return RedirectToAction("Index");
            }                
            else
            {
                _notyf.Error("新增書籍資料失敗", 3);
                return View("ModifyBook", bookData);
            }
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
            if (!(ModelState["BOOK_NAME"]!.ValidationState == ModelValidationState.Valid
                && ModelState["BOOK_NOTE"]!.ValidationState == ModelValidationState.Valid
                && ModelState["BOOK_AUTHOR"]!.ValidationState == ModelValidationState.Valid
                && ModelState["BOOK_CLASS_ID"]!.ValidationState == ModelValidationState.Valid
                && ModelState["BOOK_PUBLISHER"]!.ValidationState == ModelValidationState.Valid
                && ModelState["BOOK_BOUGHT_DATE"]!.ValidationState == ModelValidationState.Valid
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
            if (result.ResultCode != 0)
            {
                if ((bookData.BOOK_STATUS == "B" || bookData.BOOK_STATUS == "C") && (!string.IsNullOrEmpty(bookData.BOOK_KEEPER)))
                {
                    var lend_record = new BookLend()
                    {
                        BOOK_ID = bookData.BOOK_ID,
                        KEEPER_ID = bookData.BOOK_KEEPER,
                        LEND_DATE = DateTime.Now.Date
                    };
                    await _bookRepository.AddBookLendRecordAsync(lend_record);                    
                }
                TempData["Message"] = $"修改書籍 {bookData.BOOK_NAME} 資料成功";
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
            var bookData = await _bookRepository.GetBookDataByIdAsync(Id);
            if (bookData == null) 
            {
                _notyf.Error($"找不到書籍編號 {Id} 資料", 4);
                return NotFound();
            }

            var result = await _bookRepository.DeleteBookDataAsync(Id);
            if (result.ResultCode != 0)
            {
                TempData["Message"] = $"刪除書籍 {bookData.BOOK_NAME} 資料成功";
            }
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
