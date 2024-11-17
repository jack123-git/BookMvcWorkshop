namespace BookApi.Models
{
    public class CommadResult
    {
        /// <summary>
        /// 命令執行狀態
        /// 0: 未執行完畢
        /// 1: 執行完畢
        /// </summary>
        public int CommandStatus { get; set; } = 0;

        /// <summary>
        /// 命令執行結果代碼
        /// 0: 失敗
        /// >0: 成功
        /// </summary>
        public int ResultCode { get; set; } = 0;

        /// <summary>
        /// 錯誤訊息
        /// </summary>
        public string ErrorMessage { get; set; } = "";
    }
}
