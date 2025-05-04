using System;
using LostAndFound.Models;

namespace LostAndFound.Interfaces;

public interface IItemService
{
    Task<ItemDTO?> GetItemDetailsByIdAsync(int id);
    Task<List<ItemDTO>?> GetAllItemsAsync();
    Task<bool> UpdateItemAsync(int id, ItemDTO itemDto, bool isValid);
    Task<bool> DeleteItemAsync(int id);
    Task<int> GetItemCountAsync();
}
