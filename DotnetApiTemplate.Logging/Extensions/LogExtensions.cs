using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using DotnetApiTemplate.Logging.Database.Models;

namespace DotnetApiTemplate.Logging.Extensions
{
    public static class LogExtensions
    {
        public static Task LogActivity(this ILogger logger, int employeeId, string activty, string? details, string uriOrMethod, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string callerName = "")
        {
            MethodBase caller = new StackTrace().GetFrame(1).GetMethod();
            string callerMethodName = caller.DeclaringType.FullName;
            string typeName = GetCallerTypeName(caller);

            ActivityLog activityLog = new ActivityLog();
            activityLog.EmployeeId = employeeId;
            activityLog.ActivityDesc = string.IsNullOrWhiteSpace(details) ? activty : details;
            activityLog.Url = string.IsNullOrWhiteSpace(uriOrMethod) ? typeName : callerMethodName;
            activityLog.MethodName = $"Method : {callerMethodName} - Line No : {lineNumber}";
            activityLog.ActivityTimeStamp = DateTime.UtcNow;

            activityLog.ActivtyType = new ActivityType();
            activityLog.ActivtyType.Type = activty;
            activityLog.ActivtyType.Desc = activty;

            logger.Log<ActivityLog>(LogLevel.Information, eventId: 0, activityLog, null, LogCallBack);

            return Task.CompletedTask;
        }
        public static string LogCallBack(ActivityLog activityLog, Exception? ex)
        {
            if (ex != null)
                return ex.ToString();

            return $"{activityLog.ActivityTimeStamp} : {activityLog.ActivityDesc}";
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
