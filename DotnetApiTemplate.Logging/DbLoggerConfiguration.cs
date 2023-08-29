using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;
using System.Security.Claims;


namespace DotnetApiTemplate.Logging
{
    public class DbLoggerConfiguration
    {
        public int EventId { get; set; }
        public LogLevel MinimumLogLevel { get; set; } = LogLevel.Information;
        public string ConnectionString { get; set; }
        public string LoginEmployeeId { get; set; }
        public IHttpContextAccessor AppContext { get; set; }

        public string GetLoginUserId()
        {
            if (this.AppContext != null)
            {
                return AppContext.HttpContext.User.Identity.Name ?? string.Empty;
            }
            else
                return string.Empty;
        }

        public int GetLoginEmployeeId()
        {
            int empId = 0;
            if(this.AppContext != null && this.AppContext.HttpContext != null 
                                    && this.AppContext.HttpContext.User != null 
                                    && this.AppContext.HttpContext.User.Identity.IsAuthenticated)
            {
                Claim employeeIdClaim = this.AppContext.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "EmployeeId");
                if(employeeIdClaim != null)
                {
                    empId = Convert.ToInt32(string.IsNullOrWhiteSpace(employeeIdClaim.Value) ? 0 : employeeIdClaim.Value);
                }
            }
            return empId;
        }

        public string GetDisplayUrl()
        {
            if (this.AppContext != null)
            {
                return AppContext.HttpContext.Request.GetDisplayUrl() ?? string.Empty;
            }
            else
                return string.Empty;
        }
    }
}