using System;
using AutoMapper;
using LostAndFound.Helpers;
using LostAndFound.Interfaces;
using LostAndFound.Models;

namespace LostAndFound.Services;

public class BanService(IUnitOfWork unitOfWork, IMapper mapper) : IBanService
{
    public async Task<bool> BanUserAsync(BanDTO banDTO)
    {
        var result = await unitOfWork.BanRepository.BanUserAsync(banDTO);
        if (!result)
        {
            return false;
        }
        var save = await unitOfWork.Complete();
        if (!save)
        {
            return false;
        }
        return true;
        
    }

    public async Task<BanDTO?> GetBanByIdAsync(int id)
    {
        var ban = await unitOfWork.BanRepository.GetBanByIdAsync(id);
        if (ban == null) return null!;
        var banDTO = mapper.Map<BanDTO>(ban);
        return banDTO;
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

    public async Task<BanListDTO> GetBannedUsersAsync(BanFilterParams filterParams)
    {
        var bans = await unitOfWork.BanRepository.GetAllBansAsync(filterParams);
        if (bans == null) return null!;
        var bannedUsersDTO = mapper.Map<PagedList<BanDTO>>(bans);
        var bannedUsersListDTO = new BanListDTO
        {
            SearchTerm = filterParams.SearchTerm,
            BannedUsersList = bannedUsersDTO,
            TotalBannedUsers = await unitOfWork.BanRepository.GetBanCountAsync()
        };
        return bannedUsersListDTO;
    }

    public Task<string?> GetBanReasonAsync(string username)
    {
        throw new NotImplementedException();
    }

    public async Task<BanDTO?> GetUserBanAsync(int userId)
    {
        var ban = await unitOfWork.BanRepository.GetUserBanAsync(userId);
        if (ban == null) return null!;
        var banDTO = mapper.Map<BanDTO>(ban);
        return banDTO;
    }

    public Task<bool> IsUserBannedAsync(string username)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UnbanUserAsync(int userId)
    {
        var result = await unitOfWork.BanRepository.UnbanUserAsync(userId);
        if (!result)
        {
            return false;
        }
        var save = await unitOfWork.Complete();
        if (!save)
        {
            return false;
        }
        return true;
    }

    public async Task<bool> DeleteBanByIdAsync(int id)
    {
        var ban = await unitOfWork.BanRepository.GetBanByIdAsync(id);
        if (ban == null) return false;
        unitOfWork.BanRepository.DeleteBan(ban);
        var save = await unitOfWork.Complete();
        if (!save)
        {
            return false;
        }
        return true;
    }
}
