using Elmah;
using System.Web.Mvc;
using System;
namespace com.JLT.Common.Utility
{
    public class ErrorLoggerFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            // Long only handled exceptions, because all other will be caught by ELMAH anyway.
            if (context.ExceptionHandled)
                ErrorSignal.FromCurrentContext().Raise(context.Exception);
        }
    }

    public static class ErrorHandler
    {
        public static void LogError(Exception ex, string message, com.JLT.Common.Utility.Enums.Severity severity)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }

    }
}