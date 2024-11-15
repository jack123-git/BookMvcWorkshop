using System.ComponentModel.DataAnnotations;

namespace DapperMvcWorkshop.Models
{
    /// <summary>
    /// 借閱人資料
    /// </summary>
    public class Member
    {
        /// <summary>
        /// 人員編號
        /// </summary>
        [Display(Name = "人員編號")]
        public string USER_ID { get; set; }

        /// <summary>
        /// 中文名稱
        /// </summary>
        [Display(Name = "中文名稱")]
        public string? USER_CNAME { get; set; }

        /// <summary>
        /// 英文名稱
        /// </summary>
        [Display(Name = "英文名稱")]
        public string? USER_ENAME { get; set; }

        /// <summary>
        /// 名稱(英文+中文)
        /// </summary>
        [Display(Name = "姓名")]
        public string USER_Name { get { return $"{USER_ENAME}-{USER_CNAME}"; } }
    }
}
