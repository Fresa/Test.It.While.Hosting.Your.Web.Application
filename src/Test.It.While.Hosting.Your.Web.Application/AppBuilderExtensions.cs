#if NET47
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
#endif
#if NETCOREAPP2_1
using System;
using Microsoft.AspNetCore.Builder;

namespace Test.It.While.Hosting.Your.Web.Application
{
    public static class ApplicationBuilderExtensions
    {
        public static void CatchExceptions(this IApplicationBuilder appBuilder)
        {
            var exceptionHandler = appBuilder.Properties[OwinProperties.ExceptionHandler] as ExceptionHandler;
            appBuilder.Use((context, next) =>
            {
                try
                {
                    return next();
                }
                catch (Exception exception)
                {
                    exceptionHandler?.Invoke(exception);
                    throw;
                }
            });
        }
    }
}
#endif
