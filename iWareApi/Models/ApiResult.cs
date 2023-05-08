namespace iWareApi.Models
{
    /// <summary>
    /// 统一的Api格式
    /// </summary>
    public class ApiResult
    {
    
        /// <summary>
        /// 返回结果
        /// </summary>
        public object? Data { get; set; }
        
        /// <summary>
        /// 系统消息
        /// </summary>
        public string? Message { get; set; }
        
        
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }
    
    }
}
