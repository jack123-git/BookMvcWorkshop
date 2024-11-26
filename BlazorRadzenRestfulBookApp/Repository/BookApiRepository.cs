using BookApi.Models;
using BookApi.ViewModels;

namespace BlazorRadzenRestfulBookApp.Repository
{
    public class BookApiRepository : IBookRepository
    {
        private readonly HttpClient httpClient;

        public BookApiRepository(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        /// <summary>
        /// 取得書籍類別清單
        /// </summary>
        /// <returns>IEnumerable<BookClass></returns>
        public async Task<IEnumerable<BookClass>> GetAllBookClassAsync()
        {
            var result = await httpClient.GetFromJsonAsync<List<BookClass>>("api/v1/BookClass");
            return result;
        }

        /// <summary>
        /// 取得書籍狀態清單
        /// </summary>
        /// <returns>IEnumerable<BookStatus></returns>
        public async Task<IEnumerable<BookStatus>> GetAllBookStatusAsync()
        {
            var result = await httpClient.GetFromJsonAsync<List<BookStatus>>("api/v1/BookStatus");
            return result;
        }

        /// <summary>
        /// 取得借閱人清單
        /// </summary>
        /// <returns>IEnumerable<Member></returns>
        public async Task<IEnumerable<Member>> GetAllMemberAsync()
        {
            var result = await httpClient.GetFromJsonAsync<List<Member>>("api/v1/Members");
            return result;
        }

        public async Task<List<BookDataViewModel>> GetQueryBookDataAsync(string bookName = "", string bookClassId = "", string borrowerId = "", string bookStatusCode = "")
        {
            bool firstWord = true;
            var query = $"api/v1/Books";
            if (bookName != "")
            {
                query += ((firstWord ? "?" : "&") + $"BookName={bookName}");
                firstWord = false;
            }
            if (bookClassId != "")
            {
                query += ((firstWord ? "?" : "&") + $"BookClassId={bookClassId}");
                firstWord = false;
            }
            if (borrowerId != "")
            {
                query += ((firstWord ? "?" : "&") + $"BorrowerId={borrowerId}");
                firstWord = false;
            }
            if (bookStatusCode != "")
            {
                query += ((firstWord ? "?" : "&") + $"BookStatusCode={bookStatusCode}");
                firstWord = false;
            }

            var result = await httpClient.GetFromJsonAsync<List<BookDataViewModel>>(query);
            return result;

        }

        /// <summary>
        /// 取得書籍資料
        /// </summary>
        /// <param name="id">書本ID</param>
        /// <returns></returns>
        public async Task<BookData?> GetBookDataByIdAsync(int id)
        {
            string query = $"api/v1/Books/{id}";
            var result = await httpClient.GetFromJsonAsync<BookData>(query);
            return result;
        }

        /// <summary>
        /// 新增書籍資料
        /// </summary>
        /// <param name="bookData"></param>
        /// <returns></returns>
        public async Task<CommadResult> AddBookDataAsync(BookData bookData)
        {
            var reponse = await httpClient.PostAsJsonAsync("api/v1/Books", bookData);
            reponse.EnsureSuccessStatusCode();

            var result = await reponse.Content.ReadFromJsonAsync<CommadResult>();
            return result;
        }

        /// <summary>
        /// 更新書籍資料
        /// </summary>
        /// <param name="bookData">書籍資料</param>
        /// <returns></returns>
        public async Task<CommadResult> UpdateBookDataAsync(BookData bookData)
        {
            var response = await httpClient.PutAsJsonAsync($"api/v1/Books/{bookData.BOOK_ID}", bookData);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<CommadResult>();
            return result;
        }

        /// <summary>
        /// 刪除書籍資料
        /// </summary>
        /// <param name="id">書本ID</param>
        /// <returns></returns>
        public async Task<CommadResult> DeleteBookDataAsync(int id)
        {
            var reponse = await httpClient.DeleteAsync($"api/v1/Books/{id}");
            reponse.EnsureSuccessStatusCode();

            var result = await reponse.Content.ReadFromJsonAsync<CommadResult>();

            return result;
        }

        /// <summary>
        /// 新增書籍借閱紀錄
        /// </summary>
        /// <param name="bookLend"></param>
        /// <returns></returns>
        public async Task<CommadResult> AddBookLendRecordAsync(BookLend bookLend)
        {
            var reponse = await httpClient.PostAsJsonAsync("api/v1/BookLendRecords", bookLend);
            reponse.EnsureSuccessStatusCode();

            var result = await reponse.Content.ReadFromJsonAsync<CommadResult>();
            return result;
        }

        /// <summary>
        /// 取得閱覽紀錄
        /// </summary>
        /// <param name="bookId">書本Id</param>
        /// <returns></returns>
        public async Task<List<BookLendViewModel>> GetBookLendRecordAsync(int bookId)
        {
            string query = $"api/v1/BookLendRecords?bookId={bookId}";
            var result = await httpClient.GetFromJsonAsync<List<BookLendViewModel>>(query);
            return result;
        }

    }
}
