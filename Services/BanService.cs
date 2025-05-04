using System;
using LostAndFound.Interfaces;
using LostAndFound.Models;

namespace LostAndFound.Services;

public class BanService(IUnitOfWork unitOfWork) : IBanService
{
    public Task<bool> BanUserAsync(string username, string reason)
    {
        throw new NotImplementedException();
    }

    public async Task<int> GetBanCountAsync()
    {
        return await unitOfWork.BanRepository.GetBanCountAsync();
    }

    public Task<DateTime?> GetBanEndDateAsync(string username)
    {
        throw new NotImplementedException();
    }

    public Task<List<MemberDTO>?> GetBannedUsersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<string?> GetBanReasonAsync(string username)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsUserBannedAsync(string username)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UnbanUserAsync(string username)
    {
        throw new NotImplementedException();
    }
}
