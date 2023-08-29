using DotnetApiTemplate.Api.Services;
using DotnetApiTemplate.Api.Services.Definitions;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace DotnetApiTemplate.Api.Authorization
{
    public static class AuthorizationHandlerExtensions
    {
        internal static async Task WindowsAuthentication(this AuthorizationHandlerContext context, IAuthorizationRequirement requirement, IEmployeeService employeeService, Func<int, Task<bool>> isAuthorized)
        {
            if(context == null 
                || employeeService == null 
                || isAuthorized == null)
            {
                context.Fail();
                return;
            }

            var employeeId = await GetEmployeeIdFromLogin(employeeService, context?.User?.Identity?.Name ?? "");
            bool isAuthZ = await isAuthorized(employeeId);

            if (!isAuthZ)
            {
                context.Fail();
                return;
            }

            if(!IsClaimAlreadyExists(context, ClaimHelper.EMPLOYEE_ID_KEY))
            {
                context.AddAppClaim(ClaimHelper.EMPLOYEE_ID_KEY, employeeId.ToString());
            }
            context.Succeed(requirement);
        }
        internal static async Task AzureAdAuthentication(this AuthorizationHandlerContext context, IAuthorizationRequirement requirement, IEmployeeService employeeService, Func<int, Task<bool>> isAuthorized)
        {
            if (context == null
                || context.User == null
                || context.User.Claims == null
                || employeeService == null
                || isAuthorized == null)
            {
                context.Fail();
                return;
            }

            Claim emailClaim = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email);
            Claim emailClaimPreferred = context.User.Claims.FirstOrDefault(x => x.Type == ClaimHelper.PREFERRED_USERNAME);

            if (emailClaim == null && emailClaimPreferred == null)
            {
                context.Fail();
                return;
            }

            string emailId = emailClaim?.Value ?? emailClaimPreferred.Value;

            var employeeId = await GetEmployeeIdFromLogin(employeeService, emailId);
            bool isAuthZ = await isAuthorized(employeeId);

            if (!isAuthZ)
            {
                context.Fail();
                return;
            }

            if (!IsClaimAlreadyExists(context, ClaimHelper.EMPLOYEE_ID_KEY))
            {
                context.AddAppClaim(ClaimHelper.EMPLOYEE_ID_KEY, employeeId.ToString());
            }
            context.Succeed(requirement);

        }

        private static async Task<int> GetEmployeeIdFromLogin(IEmployeeService employeeService, string loginUserId)
        {
            if (string.IsNullOrEmpty(loginUserId) || employeeService == null)
                return 0;

            var emp = await employeeService.Get(loginUserId);
            return emp?.EmployeeId ?? 0;
        }

        private static bool IsClaimAlreadyExists(AuthorizationHandlerContext context, string claimKey)
        {
            if (string.IsNullOrEmpty(claimKey) || context == null || context.User == null)
                return false;

            Claim claim = context.User.Claims.FirstOrDefault(x => x.Type == claimKey);
            return claim != null && !string.IsNullOrEmpty(claim.Value);
        }

        private static string GetClaimValue(this AuthorizationHandlerContext context, string claimKey)
        {
            throw new NotImplementedException();
        }

        private static void AddAppClaims(this AuthorizationHandlerContext context, List<string> configClaims)
        {
            if(configClaims != null && configClaims.Count > 0)
            {
                foreach (var claimKey in configClaims)
                {
                    if(!IsClaimAlreadyExists(context, claimKey))
                    {
                        string claimVal = GetClaimValue(context, claimKey);
                        AddAppClaim(context, claimKey, claimVal);
                    }
                }
            }
        }
        private static void AddAppClaim(this AuthorizationHandlerContext context, string claimKey, string claimVal)
        {
            var claims = new List<Claim>
            {
                new Claim(claimKey, claimVal)
            };

            var appIdentity = new ClaimsIdentity(claims);
            context.User.AddIdentity(appIdentity);
        }

        

        
    }
}
