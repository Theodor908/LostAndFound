using System;
using System.Security.Claims;

namespace LostAndFound.Extensions;

public static class ClaimsPrincipleExtensions
{
    public static string GetUsername(this ClaimsPrincipal user)
    {
        var username = user.FindFirstValue(ClaimTypes.Name) ?? throw new Exception("Invalid username");
        return username;
    }
    public static int GetUserId(this ClaimsPrincipal user)
    {
        var userId = user.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new Exception("Invalid user id");
        return int.Parse(userId);
    }
}
