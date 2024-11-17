namespace BookApi.Models
{
    /// <summary>
    /// 書籍資料
    /// </summary>
    public class BookData
    {
        /// <summary>
        /// PK 流水號
        /// </summary>
        public int BOOK_ID { get; set; }

        /// <summary>
        /// 書籍名稱
        /// </summary>
        public required string BOOK_NAME { get; set; }

        /// <summary>
        /// 類別代號BOOK_CLASS.BOOK_CLASS_ID
        /// </summary>
        public required string BOOK_CLASS_ID { get; set; }

        /// <summary>
        /// 書籍作者
        /// </summary>
        //[StringLength(30, ErrorMessage = "書籍作者 長度不可大於 30!")]
        public string? BOOK_AUTHOR { get; set; }

        /// <summary>
        /// 書籍購書日期
        /// </summary>
        public DateTime? BOOK_BOUGHT_DATE { get; set; }

        /// <summary>
        /// 出版商
        /// </summary>
        public string? BOOK_PUBLISHER { get; set; }

        /// <summary>
        /// 內容簡介
        /// </summary>
        public string? BOOK_NOTE { get; set; }

        /// <summary>
        /// 狀態(A可以借出 B以借出 U不可借出)
       /// </summary>
       public required string BOOK_STATUS { get; set; }

        /// <summary>
        /// 借書者
        /// </summary>
        public required string BOOK_KEEPER { get; set; }
    }
}
