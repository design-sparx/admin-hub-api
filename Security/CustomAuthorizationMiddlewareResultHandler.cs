﻿using System.Text.Json;
using AdminHubApi.Dtos.ApiResponse;
using AdminHubApi.Security.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;

namespace AdminHubApi.Security;

public class CustomAuthorizationMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler
{
    private readonly AuthorizationMiddlewareResultHandler _defaultHandler =
        new AuthorizationMiddlewareResultHandler();

    private readonly IPermissionMessageService _permissionMessageService;

    public CustomAuthorizationMiddlewareResultHandler(IPermissionMessageService permissionMessageService)
    {
        _permissionMessageService = permissionMessageService;
    }

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

            // Get the path and HTTP method
            string path = context.Request.Path.Value ?? "";
            string method = context.Request.Method;

            // Get a specific error message for the resource and action
            string errorMessage = _permissionMessageService.GetPermissionMessage(path, method);

            var response = new ApiResponse<object>
            {
                Succeeded = false,
                Message = "Permission denied",
                Errors = new List<string>() { errorMessage }
            };

            await WriteJsonResponseAsync(context, response);
        }
    }

    private static async Task WriteJsonResponseAsync<T>(HttpContext context, ApiResponse<T> response)
    {
        context.Response.ContentType = "application/json";

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        await JsonSerializer.SerializeAsync(context.Response.Body, response, options);
    }
}