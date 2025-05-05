using System.Text.Json;
using AdminHubApi.Dtos.ApiResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;

namespace AdminHubApi.Security
{
    public class CustomAuthorizationMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler
    {
        private readonly AuthorizationMiddlewareResultHandler _defaultHandler =
            new AuthorizationMiddlewareResultHandler();

        public async Task HandleAsync(
            RequestDelegate next,
            HttpContext context,
            AuthorizationPolicy policy,
            PolicyAuthorizationResult authorizeResult)
        {
            // If the authorization was successful, continue down the pipeline
            if (authorizeResult.Succeeded)
            {
                await _defaultHandler.HandleAsync(next, context, policy, authorizeResult);

                return;
            }

            // Check if unauthorized or forbidden
            if (context.User.Identity?.IsAuthenticated != true)
            {
                // Return a 401 Unauthorized with a descriptive message
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                var response = new ApiResponse<object>
                {
                    Succeeded = false,
                    Message = "Authentication required",
                    Errors = new List<string>()
                    {
                        "You need to authenticate to access this resource."
                    }
                };

                await WriteJsonResponseAsync(context, response);
            }
            else
            {
                // Return a 403 Forbidden with a descriptive message
                context.Response.StatusCode = StatusCodes.Status403Forbidden;

                // Check if it's related to project permissions
                if (context.Request.Path.StartsWithSegments("/api/projects"))
                {
                    // Customize message based on the HTTP method
                    string errorMessage = context.Request.Method switch
                    {
                        "POST" => "You do not have sufficient permissions to create a project.",
                        "PUT" => "You do not have sufficient permissions to edit a project.",
                        "DELETE" => "You do not have sufficient permissions to delete a project.",
                        _ => "You do not have sufficient permissions to perform this action on projects."
                    };

                    var response = new ApiResponse<object>
                    {
                        Succeeded = false,
                        Message = "Permission denied",
                        Errors = new List<string>()
                        {
                            errorMessage
                        }
                    };

                    await WriteJsonResponseAsync(context, response);
                }
                else
                {
                    // Generic permission error
                    var response = new ApiResponse<object>
                    {
                        Succeeded = false,
                        Message = "Permission denied",
                        Errors = new List<string>()
                        {
                            "You do not have sufficient permissions to perform this action."
                        }
                    };

                    await WriteJsonResponseAsync(context, response);
                }
            }
        }

        private static async Task WriteJsonResponseAsync<T>(HttpContext context, ApiResponse<T> response)
        {
            context.Response.ContentType = "application/json";
            await JsonSerializer.SerializeAsync(context.Response.Body, response);
        }
    }
}