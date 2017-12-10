using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.Filters;
using WebAppDms.Common;
using WebAppDms.Common.ExceptionHandling;

namespace WebAppDms.App_Start
{
    public class WebApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (!(context.Exception is DomainException))
                LoggingService.Error("系统异常\r\n" + context.Exception.ToString());

            var message = context.Exception.Message;
            if (context.Exception.InnerException != null)
                message = context.Exception.InnerException.Message;
            var obj = new { result = false, message = message };
            var str = JsonConvert.SerializeObject(obj);

            context.Response = new HttpResponseMessage { StatusCode = HttpStatusCode.InternalServerError, Content = new StringContent(str, Encoding.GetEncoding("UTF-8"), "application/json") };

            base.OnException(context);



            ////1.异常日志记录（正式项目里面一般是用log4net记录异常日志）
            //Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "——" +
            //    actionExecutedContext.Exception.GetType().ToString() + "：" + actionExecutedContext.Exception.Message + "——堆栈信息：" +
            //    actionExecutedContext.Exception.StackTrace);

            ////2.返回调用方具体的异常信息
            //if (actionExecutedContext.Exception is NotImplementedException)
            //{
            //    var oResponse = new HttpResponseMessage(HttpStatusCode.NotImplemented);
            //    oResponse.Content = new StringContent("方法不被支持");
            //    oResponse.ReasonPhrase = "This Func is Not Supported";
            //    actionExecutedContext.Response = oResponse;
            //}
            //else if (actionExecutedContext.Exception is TimeoutException)
            //{
            //    actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.RequestTimeout);
            //}
            ////.....这里可以根据项目需要返回到客户端特定的状态码。如果找不到相应的异常，统一返回服务端错误500
            //else
            //{
            //    actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            //}

            //base.OnException(actionExecutedContext);
        }
    }
}