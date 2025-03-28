using System;
using AutoMapper;
using LostAndFound.Entities;
using LostAndFound.Models;

namespace LostAndFound.Helpers;

public class AutoMapperProfiles: Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<ItemDTO, Item>();
        CreateMap<Item, ItemDTO>();
    }
}
