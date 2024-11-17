using System.ComponentModel.DataAnnotations;

namespace BookApi.ViewModels
{
    /// <summary>
    /// 書籍借閱紀錄
    /// </summary>
    public class BookLendViewModel
    {
        /// <summary>
        /// 書籍ID
        /// </summary>
        [Display(Name = "書籍ID")]
        public int BOOK_ID { get; set; }

        /// <summary>
        /// 借書時間
        /// </summary>
        [Display(Name = "借書時間")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime LEND_DATE { get; set; }

        /// <summary>
        /// 人員編號
        /// </summary>
        [Display(Name = "人員編號")]
        [Required(ErrorMessage = "人員編號 必須要有資料")]
        public required string USER_ID { get; set; }

        /// <summary>
        /// 中文名稱
        /// </summary>
        [Display(Name = "中文名稱")]
        //[StringLength(50, ErrorMessage = "中文名稱 長度不可大於 50!")]
        public string? USER_CNAME { get; set; }

        /// <summary>
        /// 英文名稱
        /// </summary>
        [Display(Name = "英文名稱")]
        //[StringLength(50, ErrorMessage = "英文名稱 長度不可大於 50!")]
        public string? USER_ENAME { get; set; }



    }
}
