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
