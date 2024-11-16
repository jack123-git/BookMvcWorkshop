using DapperMvcWorkshop.Models;
using DapperMvcWorkshop.ViewModels;
using System.Collections.Generic;

namespace DapperMvcWorkshop.Repository
{
    public interface IBookRepository
    {
        /// <summary>
        /// 取得書籍類別清單
        /// </summary>
        /// <returns>IEnumerable<BookClass></returns>
        Task<IEnumerable<BookClass>> GetAllBookClassAsync();

        /// <summary>
        /// 取得書籍狀態清單
        /// </summary>
        /// <returns>IEnumerable<BookStatus></returns>
        Task<IEnumerable<BookStatus>> GetAllBookStatusAsync();

        /// <summary>
        /// 取得借閱人清單
        /// </summary>
        /// <returns>IEnumerable<Member></returns>
        Task<IEnumerable<Member>> GetAllMemberAsync();

        /// <summary>
        /// 查詢書籍清單
        /// </summary>
        /// <param name="bookId"></param>
        /// <param name="bookName"></param>
        /// <param name="bookClassId"></param>
        /// <param name="BorrowId"></param>
        /// <param name="bookStatusCode"></param>
        /// <returns></returns>
        Task<List<BookDataViewModel>> GetQueryBookDataAsync(string bookId = "", string bookName = "", string bookClassId = "", string BorrowId = "", string bookStatusCode = "");

        /// <summary>
        /// 取得書籍資料
        /// </summary>
        /// <param name="id">書本ID</param>
        /// <returns></returns>
        Task<BookData?> GetBookDataByIdAsync(int id);

        /// <summary>
        /// 新增書籍資料
        /// </summary>
        /// <param name="bookData"></param>
        /// <returns></returns>
        Task<CommadResult> AddBookDataAsync(BookData bookData);

        /// <summary>
        /// 更新書籍資料
        /// </summary>
        /// <param name="bookData">書籍資料</param>
        /// <returns></returns>
        Task<CommadResult> UpdateBookDataAsync(BookData bookData);

        /// <summary>
        /// 刪除書籍資料
        /// </summary>
        /// <param name="id">書本ID</param>
        /// <returns></returns>
        Task<CommadResult> DeleteBookDataAsync(int id);

        /// <summary>
        /// 新增書籍借閱紀錄
        /// </summary>
        /// <param name="bookLend"></param>
        /// <returns></returns>
        Task<CommadResult> AddBookLendRecordAsync(BookLend bookLend);

        /// <summary>
        /// 取得閱覽紀錄
        /// </summary>
        /// <param name="bookId">書本Id</param>
        /// <returns></returns>
        Task<List<BookLendViewModel>> GetBookLendRecordAsync(int bookId);
    }
}
