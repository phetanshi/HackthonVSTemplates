using System.Security.Claims;

namespace DotnetApiTemplate.Api.Authorization
{
    public static class ClaimHelper
    {
        public const string EMPLOYEE_ID_KEY = "EmployeeId";

        public static int GetEmployeeId(this HttpContext context)
        {
            int employeeId = 0;
            if(context != null && context.User != null)
            {
                Claim employeeIdClaim = context.User.Claims.FirstOrDefault(x => x.Type == EMPLOYEE_ID_KEY);
                if(employeeIdClaim != null)
                {
                    employeeId = Convert.ToInt32(string.IsNullOrEmpty(employeeIdClaim.Value) ? 0 : employeeIdClaim.Value);
                }
            }
            return employeeId;
        }
    }
}
