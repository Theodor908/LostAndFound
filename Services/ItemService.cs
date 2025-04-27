using System;
using AutoMapper;
using LostAndFound.Interfaces;

namespace LostAndFound.Services;

public class ItemService(IMapper mapper, IUnitOfWork unitOfWork): IItemService
{
    
}
