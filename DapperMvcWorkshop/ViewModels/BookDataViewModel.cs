using System.ComponentModel.DataAnnotations;

namespace DapperMvcWorkshop.ViewModels
{
    /// <summary>
    /// 書籍資料查詢結果
    /// </summary>
    public class BookDataViewModel
    {
        /// <summary>
        /// PK 流水號
        /// </summary>
        [Display(Name = "書籍編號")]
        public int BOOK_ID { get; set; }

        /// <summary>
        /// 書籍名稱
        /// </summary>
        [Display(Name = "書名")]
        public required string BOOK_NAME { get; set; }

        /// <summary>
        /// 書籍作者
        /// </summary>
        [Display(Name = "書籍作者")]
        public string? BOOK_AUTHOR { get; set; }

        /// <summary>
        /// 出版商
        /// </summary>
        [Display(Name = "出版商")]
        public string? BOOK_PUBLISHER { get; set; }

        /// <summary>
        /// 內容簡介
        /// </summary>
        [Display(Name = "內容簡介")]
        public string? BOOK_NOTE { get; set; }

        /// <summary>
        /// 書籍購書日期
        /// </summary>
        [Display(Name = "購書日期")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? BOOK_BOUGHT_DATE { get; set; }

        /// <summary>
        /// 狀態BOOK_CODE.CODE_ID (A可以借出 B以借出 U不可借出)
       /// </summary>
       [Display(Name = "借閱狀態")]
       public required string BOOK_STATUS_NAME { get; set; }

        /// <summary>
        /// 借閱人
        /// </summary>
        [Display(Name = "借閱人")]
        public string? BORROWER_NAME { get; set; }

        /// <summary>
        /// 圖書類別
        /// </summary>
        [Display(Name = "圖書類別")]
        public required string BOOK_CLASS_NAME { get; set; }


    }
}

