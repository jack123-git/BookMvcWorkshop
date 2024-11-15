using DapperMvcWorkshop.DataAccess;
using DapperMvcWorkshop.Models;
using DapperMvcWorkshop.ViewModels;
using System.Net;

namespace DapperMvcWorkshop.Repository
{
    public class BookRepository: IBookRepository
    {
        private readonly IDataAccess _dataAccess;

        public BookRepository(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        /// <summary>
        /// 取得書籍類別清單
        /// </summary>
        /// <returns>IEnumerable<BookClass></returns>
        public async Task<IEnumerable<BookClass>> GetAllBookClassAsync()
        {
            string sql = " Select BOOK_CLASS_ID, BOOK_CLASS_NAME From BOOK_CLASS";
            var result = await _dataAccess.GetDataAsync<BookClass, dynamic>(sql, new { }, System.Data.CommandType.Text);
            return result;
        }

        /// <summary>
        /// 取得書籍狀態清單
        /// </summary>
        /// <returns>IEnumerable<BookStatus></returns>
        public async Task<IEnumerable<BookStatus>> GetAllBookStatusAsync()
        {
            string sql = " Select CODE_ID as BOOK_STATUS_ID, CODE_NAME as BOOK_STATUS_NAME From BOOK_CODE Where CODE_TYPE = 'BOOK_STATUS'";
            var result = await _dataAccess.GetDataAsync<BookStatus, dynamic>(sql, new { }, System.Data.CommandType.Text);
            return result;
        }

        /// <summary>
        /// 取得借閱人清單
        /// </summary>
        /// <returns>IEnumerable<Member></returns>
        public async Task<IEnumerable<Member>> GetAllMemberAsync()
        {
            string sql = " Select [USER_ID], USER_CNAME , USER_ENAME From MEMBER_M ";
            var result = await _dataAccess.GetDataAsync<Member, dynamic>(sql, new { }, System.Data.CommandType.Text);
            return result;
        }

        public async Task<List<BookDataViewModel>> GetQueryBookDataAsync(string bookId="",string bookName="", string bookClassId = "", string BorrowId = "", string bookStatusCode = "")
        {
            string sql = @"
                SELECT A.BOOK_ID, A.BOOK_NAME, B.BOOK_CLASS_NAME, A.BOOK_BOUGHT_DATE, C.CODE_NAME AS BOOK_STATUS_NAME, E.USER_ENAME+'-'+E.USER_CNAME AS BORROWER_NAME
                FROM BOOK_DATA A 
                LEFT JOIN BOOK_CLASS B ON A.BOOK_CLASS_ID = B.BOOK_CLASS_ID
                LEFT JOIN BOOK_CODE C ON A.BOOK_STATUS = C.CODE_ID
                LEFT JOIN MEMBER_M E ON A.BOOK_KEEPER = E.[USER_ID]
                WHERE C.CODE_TYPE = 'BOOK_STATUS'
                AND (A.BOOK_NAME LIKE '%' + @BookName + '%' OR @BookName = '')
                AND (B.BOOK_CLASS_ID = @BookClassId OR @BookClassId = '')
                AND (E.[USER_ID]= @KeeperId OR @KeeperId = '')
                AND (A.BOOK_STATUS = @BookStatusCode OR @BookStatusCode = '')
                AND (A.BOOK_ID = @BookId OR @BookId = '')
                ORDER BY A.BOOK_BOUGHT_DATE DESC, A.BOOK_ID"
            ;

            var result = ((IEnumerable<BookDataViewModel>)await _dataAccess.GetDataAsync<BookDataViewModel, dynamic>(sql, new { BookId= bookId, BookName= bookName, BookClassId= bookClassId, KeeperId= BorrowId, BookStatusCode= bookStatusCode }, System.Data.CommandType.Text)).ToList();
            return result;
        }

        /// <summary>
        /// 取得書籍資料
        /// </summary>
        /// <param name="id">書本ID</param>
        /// <returns></returns>
        public async Task<BookData?> GetBookDataByIdAsync(int id)
        {
            string storedProcedure = "TP_SELECT_BOOK_DATA";
            var result = await _dataAccess.GetDataAsync<BookData, dynamic>(storedProcedure, new { BOOK_ID = id });
            return result.FirstOrDefault();
        }

        /// <summary>
        /// 新增書籍資料
        /// </summary>
        /// <param name="bookData"></param>
        /// <returns></returns>
        public async Task<bool> AddBookDataAsync(BookData bookData)
        {
            try
            {
                string storedProcedure = "TP_CREATE_BOOK_DATA";
                bool result = await _dataAccess.SaveDataAsync(storedProcedure, new
                {
                    BOOK_NAME = bookData.BOOK_NAME,
                    BOOK_CLASS_ID = bookData.BOOK_CLASS_ID,
                    BOOK_AUTHOR = bookData.BOOK_AUTHOR,
                    BOOK_BOUGHT_DATE = bookData.BOOK_BOUGHT_DATE,
                    BOOK_PUBLISHER = bookData.BOOK_PUBLISHER,
                    BOOK_NOTE = bookData.BOOK_NOTE,
                    BOOK_STATUS = bookData.BOOK_STATUS,
                    BOOK_KEEPER = bookData.BOOK_KEEPER
                    //,BOOK_AMOUNT = bookData.BOOK_AMOUNT,
                    //CREATE_DATE = bookData.CREATE_DATE,
                    //CREATE_USER = bookData.CREATE_USER,
                    //MODIFY_DATE = bookData.MODIFY_DATE,
                    //MODIFY_USER = bookData.MODIFY_USER
                });
                return result;
            }
            catch (Exception)
            {

                return false;
            }
        }

        /// <summary>
        /// 更新書籍資料
        /// </summary>
        /// <param name="bookData">書籍資料</param>
        /// <returns></returns>
        public async Task<bool> UpdateBookDataAsync(BookData bookData)
        {
            string storedProcedure = "TP_UPDATE_BOOK_DATA";
            bool result = await _dataAccess.SaveDataAsync(storedProcedure, new
            {
                BOOK_ID = bookData.BOOK_ID,
                BOOK_NAME = bookData.BOOK_NAME,
                BOOK_CLASS_ID = bookData.BOOK_CLASS_ID,
                BOOK_AUTHOR = bookData.BOOK_AUTHOR,
                BOOK_BOUGHT_DATE = bookData.BOOK_BOUGHT_DATE,
                BOOK_PUBLISHER = bookData.BOOK_PUBLISHER,
                BOOK_NOTE = bookData.BOOK_NOTE,
                BOOK_STATUS = bookData.BOOK_STATUS,
                BOOK_KEEPER = bookData.BOOK_KEEPER
                //,BOOK_AMOUNT = bookData.BOOK_AMOUNT,
                //CREATE_DATE = bookData.CREATE_DATE,
                //CREATE_USER = bookData.CREATE_USER,
                //MODIFY_DATE = bookData.MODIFY_DATE,
                //MODIFY_USER = bookData.MODIFY_USER
            });
            return result;
        }

        /// <summary>
        /// 刪除書籍資料
        /// </summary>
        /// <param name="id">書本ID</param>
        /// <returns></returns>
        public async Task<bool> DeleteBookDataAsync(int id)
        {
            try
            {
                await _dataAccess.SaveDataAsync("TP_DELETE_BOOK_DATA", new { BOOK_ID = id });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 新增書籍借閱紀錄
        /// </summary>
        /// <param name="bookLend"></param>
        /// <returns></returns>
        public async Task<bool> AddBookLendRecordAsync(BookLend bookLend)
        {
            try
            {
                string storedProcedure = "TP_CREATE_BOOK_LEND_RECORD";
                bool result = await _dataAccess.SaveDataAsync(storedProcedure, new
                {
                    BOOK_ID = bookLend.BOOK_ID,
                    KEEPER_ID = bookLend.KEEPER_ID,
                    LEND_DATE = bookLend
                });
                return result;
            }
            catch (Exception)
            {

                return false;
            }
        }

        /// <summary>
        /// 取得閱覽紀錄
        /// </summary>
        /// <param name="bookId">書本Id</param>
        /// <returns></returns>
        public async Task<List<BookLendViewModel>> GetBookLendRecordAsync(int bookId)
        {
            string storedProcedure = "TP_SELECT_BOOK_LEND_RECORD";
            var result = ((IEnumerable<BookLendViewModel>)await _dataAccess.GetDataAsync<BookLendViewModel, dynamic>(storedProcedure, new { BOOKID = bookId })).ToList();
            return result;
        }
    }
}
