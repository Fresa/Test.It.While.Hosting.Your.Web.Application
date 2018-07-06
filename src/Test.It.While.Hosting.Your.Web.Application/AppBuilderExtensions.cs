using System;
using System.Web.Http;
using System.Web.Http.Filters;
using Owin;

namespace Test.It.While.Hosting.Your.Web.Application
{
    public static class AppBuilderExtensions
    {
        public static void CatchExceptions(this IAppBuilder appBuilder, HttpConfiguration httpConfiguration)
        {
            var exceptionHandler = appBuilder.Properties[OwinProperties.ExceptionHandler] as ExceptionHandler;
            var globalExceptionHandler = new GlobalExceptionHandler();
            globalExceptionHandler.OnExceptionCaught += exception => exceptionHandler?.Invoke(exception);
            httpConfiguration.Filters.Add(globalExceptionHandler);
        }

        private class GlobalExceptionHandler : ExceptionFilterAttribute
        {
            public event Action<Exception> OnExceptionCaught;

            public override void OnException(HttpActionExecutedContext actionExecutedContext)
            {
                OnExceptionCaught?.Invoke(actionExecutedContext.Exception);
                base.OnException(actionExecutedContext);
            }
        }
    }
}