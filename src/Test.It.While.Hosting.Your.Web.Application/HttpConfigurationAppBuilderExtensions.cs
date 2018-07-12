#if NET47
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Filters;
using Owin;

namespace Test.It.While.Hosting.Your.Web.Application
{
    public static class HttpConfigurationExtensions
    {
        public static void InterceptExceptionFilter(this HttpConfiguration httpConfiguration, ExceptionHandler exceptionHandler)
        {
            var testExceptionFilter = new TestExceptionFilter();
            testExceptionFilter.OnExceptionCaught += exceptionHandler.Invoke;
            httpConfiguration.Filters.Add(testExceptionFilter);
        }

        private class TestExceptionFilter : ExceptionFilterAttribute
        {
            public event Action<Exception> OnExceptionCaught;

            public override void OnException(HttpActionExecutedContext actionExecutedContext)
            {
                OnExceptionCaught?.Invoke(actionExecutedContext.Exception);
                base.OnException(actionExecutedContext);
            }
        }

        public static void InterceptExceptionHandler(this HttpConfiguration configuration, ExceptionHandler eceptionHandler)
        {
            var currentExceptionHandler = configuration.Services.GetExceptionHandler();
            configuration.Services.Replace(typeof(IExceptionHandler), new InterceptingExceptionHandlerDecorator(currentExceptionHandler, eceptionHandler));
        }

        private class InterceptingExceptionHandlerDecorator : IExceptionHandler
        {
            private readonly IExceptionHandler _exceptionHandler;
            private readonly ExceptionHandler _testExceptionHandler;

            public InterceptingExceptionHandlerDecorator(IExceptionHandler exceptionHandler, ExceptionHandler testExceptionHandler)
            {
                _exceptionHandler = exceptionHandler;
                _testExceptionHandler = testExceptionHandler;
            }

            public async Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
            {
                _testExceptionHandler.Invoke(context.Exception);
                if (_exceptionHandler != null)
                {
                    await _exceptionHandler.HandleAsync(context, cancellationToken);
                }
            }
        }

        public static void InterceptExceptionLogger(this HttpConfiguration configuration, ExceptionHandler exceptionHandler)
        {
            configuration.Services.Insert(typeof(IExceptionLogger), 0, new TestExceptionLogger(exceptionHandler));
        }

        private class TestExceptionLogger : ExceptionLogger
        {
            private readonly ExceptionHandler _exceptionHandler;

            public TestExceptionLogger(ExceptionHandler exceptionHandler)
            {
                _exceptionHandler = exceptionHandler;
            }

            public override void Log(ExceptionLoggerContext context)
            {
                _exceptionHandler.Invoke(context.Exception);
            }
        }

        public static ExceptionHandler GetTestExceptionHandler(this IAppBuilder appBuilder)
        {
            return appBuilder.Properties[OwinProperties.ExceptionHandler] as ExceptionHandler; ;
        }
    }
}
#endif