using DotnetApiTemplate.Logging.Database;
using DotnetApiTemplate.Logging.Database.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Diagnostics.Eventing.Reader;

namespace DotnetApiTemplate.Logging
{
    public class DbLogger : ILogger
    {
        private readonly string name;
        private readonly DbLoggerConfiguration logConfig;
        private readonly LogDbContext loggerDbContext;

        public DbLogger(string name, IConfiguration configuration, DbLoggerConfiguration logConfig)
        {
            this.name = name;
            this.logConfig = logConfig;
            loggerDbContext = new LogDbContext(configuration);
        }
        public DbLogger(IConfiguration configuration, DbLoggerConfiguration logConfig)
        {
            this.logConfig = logConfig;
            loggerDbContext = new LogDbContext(configuration);
        }
        public IDisposable BeginScope<TState>(TState state)
        {
            return default;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logConfig.MinimumLogLevel <= logLevel;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            try
            {
                string userId = logConfig.GetLoginUserId();
                string displayUrl = logConfig.GetDisplayUrl() ?? "-";

                if (!IsEnabled(logLevel))
                    return;

                string? message = formatter(state, exception);

                switch (logLevel)
                {
                    case LogLevel.Trace:
                    case LogLevel.Debug:
                    case LogLevel.Information:
                    case LogLevel.Warning:
                        ActivityLog logObj = GetActivityLogObject(message, state, logLevel);
                        LogActivity(logObj);
                        break;
                    case LogLevel.Critical:
                    case LogLevel.Error:
                        LogError(message, exception);
                        break;
                }
            }
            catch { }
        }


        #region ActivityLog

        private ActivityLog GetActivityLogObject<TState>(string message, TState state, LogLevel logLevel)
        {
            ActivityLog logObj = ReadObj<TState, ActivityLog>(state);

            if(logObj == null)
            {
                string url = logConfig.GetDisplayUrl() ?? "-";
                var employeeId = logConfig.GetLoginEmployeeId();

                logObj = new ActivityLog();
                logObj.EmployeeId = employeeId;
                logObj.ActivityDesc = message;
                logObj.Url = url;
                logObj.ActivityTimeStamp = DateTime.UtcNow;
                logObj.LogLevelId = (int)logLevel;
            }

            if(logObj.ActivtyType == null)
            {
                logObj.ActivtyType = new ActivityType();
                logObj.ActivtyType.Type = "Unknown";
                logObj.ActivtyType.Desc = "Unknown";
            }

            return logObj;
        }

        private ActivityType GetActivtyTypeFromDb(ActivityType activtyType)
        {
            try
            {
                if (activtyType == null)
                    return null;

                var activityType = loggerDbContext.ActivityTypes.FirstOrDefault(x => x.Type.ToLower() == activtyType.Type.ToLower());
                return activityType;
            }
            catch
            {
                return null;
            }
        }
        private void LogActivity(ActivityLog activityLog)
        {
            try
            {

                var activityType = GetActivtyTypeFromDb(activityLog.ActivtyType);

                if(activityType == null)
                {
                    loggerDbContext.ActivityTypes.Add(activityLog.ActivtyType);
                    loggerDbContext.SaveChanges();
                }

                activityLog.ActivityTypeId = activityLog.ActivityTypeId;

                loggerDbContext.ActivityLogs.Add(activityLog);
                loggerDbContext.SaveChanges();
            }
            catch { }
        }
        #endregion

        #region ErrorLog
        private ErrorLog GetErrorLogObject(string message, Exception exception, long parentErrorId, string url, int employeeId)
        {
            ErrorLog errorLog = new ErrorLog();
            errorLog.LogLevelId = (int)LogLevel.Error;
            errorLog.TimeStamp = DateTime.UtcNow;
            errorLog.Url = url;
            errorLog.EmployeeId = employeeId;
            errorLog.ErrorMessage = message;

            if (parentErrorId > 0)
                errorLog.ParentErrorId = parentErrorId;

            if (exception != null)
            {
                errorLog.ErrorMessage = exception.Message;
                errorLog.ClassName = exception.TargetSite?.DeclaringType?.FullName;
                errorLog.MethodName = exception.TargetSite?.DeclaringType?.Name;
                errorLog.StackTrace = exception.StackTrace ?? "-";

                errorLog.ErrorType = new ErrorType();
                errorLog.ErrorType.Type = exception.GetType()?.FullName ?? "-";
                errorLog.ErrorType.Desc = exception.GetType()?.FullName ?? "-";
            }
            else if (errorLog.ErrorType == null)
            {
                errorLog.ErrorType = new ErrorType();
                errorLog.ErrorType.Type = "Unknown Error Type";
                errorLog.ErrorType.Desc = "Unknown Error Type";
            }

            return errorLog;
        }

        private ErrorType GetErrorTypeFromDb(ErrorType type)
        {
            try
            {
                if (type == null)
                    return null;

                var errorType = loggerDbContext.ErrorTypes.FirstOrDefault(x => x.Type.ToLower() == type.Type.ToLower());
                return errorType;
            }
            catch
            {
                return null;
            }
        }

        private void LogError(string message, Exception? exception, long parentErrorId = 0)
        {
            try
            {
                string url = logConfig.GetDisplayUrl() ?? "-";
                var employeeId = logConfig.GetLoginEmployeeId();

                ErrorLog errorLog = GetErrorLogObject(message, exception, parentErrorId, url, employeeId);

                var errorType = GetErrorTypeFromDb(errorLog.ErrorType);

                if (errorType == null)
                {
                    loggerDbContext.ErrorTypes.Add(errorLog.ErrorType);
                    loggerDbContext.SaveChanges();
                }

                errorLog.ErrorTypeId = errorType.ErrorTypeId;

                loggerDbContext.ErrorLogs.Add(errorLog);
                loggerDbContext.SaveChanges();

                if (exception?.InnerException != null)
                    LogError(exception.InnerException.Message, exception.InnerException, parentErrorId);
            }
            catch { }
        }


        #endregion
        private TResult ReadObj<TState, TResult>(TState state)
        {
            try
            {
                var result = (TResult)Convert.ChangeType(state, typeof(TResult));
                return result;
            }
            catch { }
            return default(TResult);
        }
    }
}
