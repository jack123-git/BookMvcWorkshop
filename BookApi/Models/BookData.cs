using BookApi.Attibutes;
using System.ComponentModel.DataAnnotations;

namespace BookApi.Models
{
    /// <summary>
    /// 書籍資料
    /// </summary>
    [BookData]
    public class BookData
    {
        /// <summary>
        /// PK 流水號
        /// </summary>
        public int BOOK_ID { get; set; }

        /// <summary>
        /// 書籍名稱
        /// </summary>
        [Required(ErrorMessage = "書籍名稱 必須要有資料")]
        public required string BOOK_NAME { get; set; }

        /// <summary>
        /// 類別代號BOOK_CLASS.BOOK_CLASS_ID
        /// </summary>
        [Required(ErrorMessage = "書籍類別 必須要有資料")]
        public required string BOOK_CLASS_ID { get; set; }

        /// <summary>
        /// 書籍作者
        /// </summary>
        [Required(ErrorMessage = "書籍作者 必須要有資料")]
        [StringLength(30, ErrorMessage = "書籍作者 長度不可大於 30!")]
        public string? BOOK_AUTHOR { get; set; }

        /// <summary>
        /// 書籍購書日期
        /// </summary>
        [Required(ErrorMessage = "購書日期 必須要有資料")]
        public DateTime? BOOK_BOUGHT_DATE { get; set; }

        /// <summary>
        /// 出版商
        /// </summary>
        [Required(ErrorMessage = "出版商 必須要有資料")]
        public string? BOOK_PUBLISHER { get; set; }

        /// <summary>
        /// 內容簡介
        /// </summary>
        [Required(ErrorMessage = "內容簡介 必須要有資料")]
        public string? BOOK_NOTE { get; set; }

        /// <summary>
        /// 狀態(A可以借出 B以借出 U不可借出)
        /// </summary>
        [Required(ErrorMessage = "借閱狀態 必須要有資料")]
        public required string BOOK_STATUS { get; set; }

        /// <summary>
        /// 借書者
        /// </summary>
        /// 因為在新增資料時，此欄位為空白，故改為自訂屬性驗證[BookCard]
        //[Required(ErrorMessage = "借書者 必須要有資料")]
        public required string BOOK_KEEPER { get; set; }
    }
}
