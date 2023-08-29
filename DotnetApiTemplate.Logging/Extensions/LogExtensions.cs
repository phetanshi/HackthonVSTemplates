using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using DotnetApiTemplate.Logging.Database.Models;
using System;

namespace DotnetApiTemplate.Logging.Extensions
{
    public static class LogExtensions
    {
        public static void LogActivity(this ILogger logger, int employeeId, string activty, string? details, string uriOrMethod, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string callerName = "")
        {
            MethodBase caller = new StackTrace().GetFrame(1).GetMethod();
            string callerMethodName = caller.DeclaringType.FullName;
            string typeName = GetCallerTypeName(caller);

            ActivityLog activityLog = new ActivityLog();
            activityLog.EmployeeId = employeeId;
            activityLog.ActivityDesc = string.IsNullOrWhiteSpace(details) ? activty : details;

            activityLog.Url = string.IsNullOrWhiteSpace(uriOrMethod) ? typeName : uriOrMethod;

            activityLog.MethodName = $"Method : {callerMethodName} - Line No : {lineNumber}";
            activityLog.ActivityTimeStamp = DateTime.UtcNow;

            activityLog.ActivtyType = new ActivityType();
            activityLog.ActivtyType.Type = activty;
            activityLog.ActivtyType.Desc = activty;

            logger.Log<ActivityLog>(LogLevel.Information, eventId: 0, activityLog, null, LogCallBack);

        }

        public static void LogError(this ILogger logger, int employeeId, Exception exception, string uriOrMethod, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string callerName = "")
        {
            MethodBase caller = new StackTrace().GetFrame(1).GetMethod();
            string callerMethodName = caller.DeclaringType.FullName;
            string typeName = GetCallerTypeName(caller);

            ErrorLog errorLog = new ErrorLog();
            errorLog.EmployeeId = employeeId;
            errorLog.ErrorMessage = exception.Message;
            errorLog.Url = string.IsNullOrWhiteSpace(uriOrMethod) ? typeName : uriOrMethod;

            errorLog.ClassName = exception.TargetSite?.DeclaringType?.FullName;
            errorLog.MethodName = exception.TargetSite?.DeclaringType?.Name;
            errorLog.TimeStamp = DateTime.UtcNow;

            errorLog.ErrorType = new ErrorType();
            errorLog.ErrorType.Type = exception.GetType()?.FullName ?? "-";
            errorLog.ErrorType.Desc = exception.GetType()?.FullName ?? "-";

            logger.Log<ErrorLog>(LogLevel.Error, eventId: 0, state: errorLog, exception: exception, formatter: LogCallBack);

        }

        public static string LogCallBack(ActivityLog activityLog, Exception? ex)
        {
            return $"{activityLog.ActivityTimeStamp} : {activityLog.ActivityDesc}";
        }

        public static string LogCallBack(ErrorLog activityLog, Exception? ex)
        {
            return ex?.ToString() ?? $"{activityLog.ErrorMessage}";
        }

        private static string GetCallerTypeName(MethodBase caller)
        {
            string name = string.Empty;

            if (caller != null && caller.DeclaringType != null)
            {
                name = GetCallerTypeName(caller.DeclaringType);
            }

            return name;
        }

        private static string GetCallerTypeName(Type declaringType)
        {
            string name = string.Empty;
            if(declaringType.DeclaringType == null)
            {
                return declaringType.Name;
            }
            name = GetCallerTypeName(declaringType.DeclaringType);
            return name;
        }
    }
}
