using System.ComponentModel.DataAnnotations;

namespace DapperMvcWorkshop.Models
{
    /// <summary>
    /// 借閱紀錄
    /// </summary>
    public class BookLend
    {
        /// <summary>
        /// 書籍ID
        /// </summary>
        [Display(Name = "書籍ID")]
        public int BOOK_ID { get; set; }

        /// <summary>
        /// 借書者
        /// </summary>
        [Display(Name = "借書者")]
        public string KEEPER_ID { get; set; }

        /// <summary>
        /// 借書時間
        /// </summary>
        [Display(Name = "借書時間")]
        public DateTime LEND_DATE { get; set; }

    }
}
