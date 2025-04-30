using System;
using AutoMapper;
using LostAndFound.Interfaces;
using LostAndFound.Models;

namespace LostAndFound.Services;

public class ItemService(IMapper mapper, IUnitOfWork unitOfWork) : IItemService
{
    public async Task<bool> DeleteItemAsync(int id)
    {
        var item = await unitOfWork.ItemRepository.GetItemByIdAsync(id);
        if (item == null)
        {
            return false;
        }
        await unitOfWork.ItemRepository.DeleteItemAsync(item);
        var result = await unitOfWork.Complete();
        if (result == false)
        {
            return false;
        }
        return true;
    }

    public Task<List<ItemDTO>?> GetAllItemsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<ItemDTO?> GetItemDetailsByIdAsync(int id)
    {
        var item = await unitOfWork.ItemRepository.GetItemByIdAsync(id);
        if (item == null)
        {
            return null;
        }
        var itemDTO = mapper.Map<ItemDTO>(item);
        return itemDTO;
    }

    public Task<bool> UpdateItemAsync(int id, ItemDTO itemDto, bool isValid)
    {
        throw new NotImplementedException();
    }
}
