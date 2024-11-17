using System.ComponentModel.DataAnnotations;

namespace BookApi.Models
{
    /// <summary>
    /// 書籍狀態
    /// </summary>
    public class BookStatus
    {
        /// <summary>
        /// 書籍狀態代碼
        /// </summary>
        [Display(Name = "書籍狀態代碼")]
        public string BOOK_STATUS_ID { get; set; }

        /// <summary>
        /// 書籍狀態
        /// </summary>
        [Display(Name = "書籍狀態")]
        public string? BOOK_STATUS_NAME { get; set; }
    }
}
