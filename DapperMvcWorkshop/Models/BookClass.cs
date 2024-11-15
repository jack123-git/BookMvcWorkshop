using System.ComponentModel.DataAnnotations;

namespace DapperMvcWorkshop.Models
{
    /// <summary>
    /// 書本類別
    /// </summary>
    public class BookClass
    {
        /// <summary>
        /// 類別代號
        /// </summary>
        [Display(Name = "類別代號")]
        public string BOOK_CLASS_ID { get; set; }

        /// <summary>
        /// 類別名稱
        /// </summary>
        [Display(Name = "類別名稱")]
        public string BOOK_CLASS_NAME { get; set; }
    }
}
