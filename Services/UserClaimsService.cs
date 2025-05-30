﻿using System.Security.Claims;
using AdminHubApi.Constants;
using AdminHubApi.Entities;
using AdminHubApi.Extensions;
using AdminHubApi.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace AdminHubApi.Services;

public class UserClaimsService : IUserClaimsService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserClaimsService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
    
    public async Task<IList<Claim>> GetUserClaimsAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if(user == null)
            throw new ArgumentException("User not found", nameof(userId));
        
        return await _userManager.GetClaimsAsync(user);
    }

    public async Task AddPermissionClaimsAsync(string userId, IEnumerable<string> permissions)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if(user == null)
            throw new ArgumentException("User not found", nameof(userId));
        
        var claims = permissions.Select(p => new Claim(CustomClaimTypes.Permission, p));
        await _userManager.AddUserClaimsAync(user, claims);
    }

    public async Task RemovePermissionClaimsAsync(string userId, IEnumerable<string> permissions)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if(user == null)
            throw new ArgumentException("User not found", nameof(userId));
        
        var userClaims = await _userManager.GetClaimsAsync(user);
        var claimsToRemove = userClaims.Where(c => c.Type == CustomClaimTypes.Permission && permissions.Contains(c.Value)).ToList();

        foreach (var claim in claimsToRemove)
        {
            await _userManager.RemoveClaimAsync(user, claim);
        }
    }
    
    public async Task AddClaimAsync(string userId, string claimType, string claimValue)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if(user == null)
            throw new ArgumentException("User not found", nameof(userId));
        
        var claim = new Claim(claimType, claimValue);
        await _userManager.AddClaimAsync(user, claim);
    }
    
    public async Task RemoveClaimAsync(string userId, string claimType, string claimValue)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if(user == null)
            throw new ArgumentException("User not found", nameof(userId));
        
        var claim = new Claim(claimType, claimValue);
        await _userManager.RemoveClaimAsync(user, claim);
    }
}