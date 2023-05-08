using iWareApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json.Serialization;

namespace iWareApi.Utility.Filters
{
    /// <summary>
    /// 将结构统一转换为ApiResult形式的过滤器
    /// </summary>
    public class CustomAsyncResultFilterAttribute : Attribute, IAsyncResultFilter
    {
       /// <summary>
       /// 处理执行结果
       /// </summary>
       /// <param name="context">结果上下文</param>
       /// <param name="next">结果执行代理</param>
       /// <returns></returns>
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (context.Result is JsonResult)
            {
                JsonResult data = (JsonResult)context.Result;

               


                
                context.Result = new JsonResult(new ApiResult
                {
                    Data = data.Value,
                    Success = true,
                    Message = "无异常信息"
                });
            }
            await next.Invoke();
        }
    }
}