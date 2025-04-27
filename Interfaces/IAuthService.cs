using LostAndFound.Entities;
using LostAndFound.Models;

namespace LostAndFound.Interfaces;

public interface IAuthService
{
    Task<(bool Succeeded, string? ErrorMessage)> LoginAsync(string usernameOrEmail, string password, bool rememberMe);
    Task<(bool Succeeded, Dictionary<string, string>? Errors, AppUser? User)> RegisterAsync(RegisterDTO registerDTO);
    Task LogoutAsync();
}
