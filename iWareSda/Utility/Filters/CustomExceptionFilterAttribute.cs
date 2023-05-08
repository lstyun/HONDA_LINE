using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace iWareSda.Utility.Filters
{
    /// <summary>
    /// 统一处理异常的拦截器
    /// </summary>
    public class CustomExceptionFilterAttribute : Attribute, IExceptionFilter
    {
        private readonly ILogger<CustomExceptionFilterAttribute> _logger;
        private readonly IModelMetadataProvider _iModelMetadataProvider;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="modelMetadataProvider">元数据提供器</param>
        public CustomExceptionFilterAttribute(IModelMetadataProvider modelMetadataProvider, ILogger<CustomExceptionFilterAttribute> logger)
        {
            this._iModelMetadataProvider = modelMetadataProvider;
            this._logger = logger;
        }

        /// <summary>
        /// 异常处理
        /// </summary>
        /// <param name="context">异常上下文</param>
        public void OnException(ExceptionContext context)
        {
            _logger.LogError($"发生异常啦：{context.Exception.Message}");
          

        }

        

    }
}
