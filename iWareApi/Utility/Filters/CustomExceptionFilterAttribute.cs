using iWareApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace iWareApi.Utility.Filters
{
    /// <summary>
    /// 统一处理异常的拦截器
    /// </summary>
    public class CustomExceptionFilterAttribute : Attribute, IExceptionFilter
    {

        private readonly IModelMetadataProvider _iModelMetadataProvider;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="modelMetadataProvider">元数据提供器</param>
        public CustomExceptionFilterAttribute(IModelMetadataProvider modelMetadataProvider)
        {
            this._iModelMetadataProvider = modelMetadataProvider;
        }

        /// <summary>
        /// 异常处理
        /// </summary>
        /// <param name="context">异常上下文</param>
        public void OnException(ExceptionContext context)
        {

            if (!context.ExceptionHandled) 
            {
                context.ExceptionHandled = true;

                if (IsAjaxRequest(context.HttpContext.Request))
                {
                    context.Result = new JsonResult(new ApiResult
                    {
                        Data = null,
                        Success = false,
                        Message = context.Exception.Message

                    });
                } 
                else
                {
                    //ViewResult result = new ViewResult { ViewName = "~/Views/Shared/Error.cshtml" };
                    //result.ViewData = new ViewDataDictionary(_IModelMetadataProvider, context.ModelState);
                    //result.ViewData.Add("Exception", context.Exception);
                    //context.Result = result;

                    context.Result = new JsonResult(new ApiResult
                    {
                        Data = null,
                        Success = false,
                        Message = context.Exception.Message
                    });
                }
            }

        }

        private bool IsAjaxRequest(HttpRequest request) 
        {
            string? header = request.Headers["X-Requested-With"];
            return "XMLHttpRequest".Equals(header);
        }

    }
}
