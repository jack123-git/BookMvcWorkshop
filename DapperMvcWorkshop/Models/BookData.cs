using System.ComponentModel.DataAnnotations;

namespace DapperMvcWorkshop.Models
{
    /// <summary>
    /// 書籍資料
    /// </summary>
    public class BookData
    {
        /// <summary>
        /// PK 流水號
        /// </summary>
        [Display(Name = "PK 流水號")]
        public int BOOK_ID { get; set; }

        /// <summary>
        /// 書籍名稱
        /// </summary>
        [Display(Name = "書籍名稱")]
        [Required(ErrorMessage = "書籍名稱 必須要有資料")]
        [StringLength(200, ErrorMessage = "書籍名稱 長度不可大於 200!")]
        public required string BOOK_NAME { get; set; }

        /// <summary>
        /// 類別代號BOOK_CLASS.BOOK_CLASS_ID
        /// </summary>
        [Display(Name = "類別代號BOOK_CLASS.BOOK_CLASS_ID")]
        [Required(ErrorMessage = "類別代號BOOK_CLASS.BOOK_CLASS_ID 必須要有資料")]
        [StringLength(4, ErrorMessage = "類別代號BOOK_CLASS.BOOK_CLASS_ID 長度不可大於 4!")]
        public required string BOOK_CLASS_ID { get; set; }

        /// <summary>
        /// 書籍作者
        /// </summary>
        [Display(Name = "書籍作者")]
        //[StringLength(30, ErrorMessage = "書籍作者 長度不可大於 30!")]
        public string? BOOK_AUTHOR { get; set; }

        /// <summary>
        /// 書籍購書日期
        /// </summary>
        [Display(Name = "購書日期")]
        [DataType(DataType.DateTime)]
        public DateTime? BOOK_BOUGHT_DATE { get; set; }

        /// <summary>
        /// 出版商
        /// </summary>
        [Display(Name = "出版商")]
        //[StringLength(20, ErrorMessage = "出版商 長度不可大於 20!")]
        public string? BOOK_PUBLISHER { get; set; }

        /// <summary>
        /// 內容簡介
        /// </summary>
        [Display(Name = "內容簡介")]
        //[StringLength(1200, ErrorMessage = "內容簡介 長度不可大於 1200!")]
        public string? BOOK_NOTE { get; set; }

        /// <summary>
        /// 狀態(A可以借出 B以借出 U不可借出)
       /// </summary>
       [Display(Name = "借閱狀態")]
       //[Required(ErrorMessage = "狀態(A可以借出 B以借出 U不可借出) 必須要有資料")]
       [StringLength(1, ErrorMessage = "狀態(A可以借出 B以借出 U不可借出) 長度不可大於 1!")]
       public required string BOOK_STATUS { get; set; }

        /// <summary>
        /// 借書者
        /// </summary>
        [Display(Name = "借閱人")]
        [Required(ErrorMessage = "借閱人 必須要有資料")]
        [StringLength(12, ErrorMessage = "借閱人 長度不可大於 12!")]
        public required string BOOK_KEEPER { get; set; }
    }
}
